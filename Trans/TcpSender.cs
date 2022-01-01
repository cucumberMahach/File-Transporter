using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace Trans
{
    public class TcpSender : TcpFuncs
    {
        protected IPAddress remoteIP;
        protected int remotePort;
        protected Thread thread;
        protected TcpClient tcpClient;
        protected TcpListener tcpListener;
        protected string filePath;
        protected string fileName;
        protected bool connect;
        protected int bufferSize;
        protected long uploadedBytes;
        protected long fileSize;
        protected bool isContinue;
        protected IPAddress localIP;
        protected int localPort;
        protected CompressionType prefferedCompressionType;

        protected bool isAborted = false;

        public long FileSize
        {
            get
            {
                return fileSize;
            }
        }

        public long UploadedBytes
        {
            get
            {
                return uploadedBytes;
            }
        }

        public bool IsContinue
        {
            get
            {
                return isContinue;
            }
        }

        public TcpSender(IPAddress localIP, int localPort, IPAddress remoteIP, int remotePort, string filePath, string fileName, int bufferSizeKb, CompressionType compressionType, bool connect)
        {
            this.localIP = localIP;
            this.localPort = localPort;
            this.remoteIP = remoteIP;
            this.remotePort = remotePort;
            this.filePath = filePath;
            this.fileName = fileName;
            this.connect = connect;
            bufferSize = bufferSizeKb * 1024;
            prefferedCompressionType = compressionType;
            thread = new Thread(new ThreadStart(Run));
        }

        public void Start()
        {
            thread.Start();
        }

        public override void Abort()
        {
            if (isAborted)
                return;
            isAborted = true;
            if (tcpClient != null)
            {
                tcpClient.Close();
            }
            if (tcpListener != null)
            {
                tcpListener.Stop();
            }
            thread.Abort();
        }

        public void Run()
        {
            try
            {
                if (connect)
                {
                    tcpClient = ConnectAndSendBeginBytes(remoteIP, remotePort);
                }
                else
                {
                    ListenAndCheckBeginBytes(localIP, localPort, ref tcpListener, ref tcpClient);
                }
                using (NetworkStream stream = tcpClient.GetStream())
                {
                    using (FileStream fileStream = File.OpenRead(filePath))
                    {
                        SendConnectionConfig(stream);
                        SetCompresstionType(prefferedCompressionType);
                        SendFileData(fileStream, stream);
                        uploadedBytes = ReceiveUploadedBytes(stream);
                        if (uploadedBytes == -1)
                        {
                            OnUploadMessage(SenderMessageType.SizesAreEqual, "Файл уже загружен");
                        }
                        else
                        {
                            SendFile(fileStream, stream);
                        }
                    }
                }
                tcpClient.Close();
                if (tcpListener != null)
                {
                    tcpListener.Stop();
                }
            }
            catch (Exception ex)
            {
                if (ex is ThreadAbortException || ex is ThreadStateException)
                    return;
                OnUploadError(ex.Message);
                Abort();
            }
            OnUploadFinished();
        }

        protected void SendConnectionConfig(NetworkStream netStream)
        {
            byte[] compressionBytes = BitConverter.GetBytes((int)prefferedCompressionType);
            Send(netStream, compressionBytes);
        }

        protected long ReceiveUploadedBytes(NetworkStream netStream)
        {
            byte[] value = Receive(netStream);
            return BitConverter.ToInt64(value, 0);
        }

        protected void SendFileData(FileStream fileStream, NetworkStream netStream)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(
                fileName + "*" + fileStream.Length.ToString()
                );
            Send(netStream, bytes); //File name and file size
        }

        protected void SendFile(FileStream fileStream, NetworkStream netStream)
        {
            fileSize = fileStream.Length;
            if (uploadedBytes != 0L)
            {
                isContinue = true;
                fileStream.Seek(uploadedBytes, SeekOrigin.Begin);
            }
            else
            {
                fileStream.Position = 0;
            }
            byte[] array;
            if (fileSize - uploadedBytes < bufferSize)
            {
                array = new byte[fileSize - uploadedBytes];
            }
            else
            {
                array = new byte[bufferSize];
            }
            while (fileStream.Read(array, 0, array.Length) > 0)
            {
                Send(netStream, array);
                uploadedBytes += array.Length;
                if (fileSize - uploadedBytes < array.Length && fileSize - uploadedBytes != 0L)
                {
                    array = new byte[fileSize - uploadedBytes];
                }
            }
            fileStream.Close();
        }

        public event TcpSender.TcpEventHandler OnUploadFinished;
        public event TcpSender.TcpErrorHandler OnUploadError;
        public event TcpSender.TcpMessageHandler OnUploadMessage;

        public delegate void TcpEventHandler();
        public delegate void TcpErrorHandler(string message);
        public delegate void TcpMessageHandler(SenderMessageType type, string text);
    }

    public enum SenderMessageType
    {
        None,
        SizesAreEqual
    }
}
