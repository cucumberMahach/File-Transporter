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
        private IPAddress remoteIP;
        private int remotePort;
        private Thread thread;
        private TcpClient tcpClient;
        private TcpListener tcpListener;
        private string filePath;
        private string fileName;
        private bool connect;
        private int bufferSize;
        private long uploadedBytes;
        private long fileSize;
        private bool isContinue;
        private IPAddress localIP;
        private int localPort;

        private bool isAborted = false;

        public long GetFileSize
        {
            get
            {
                return fileSize;
            }
        }

        public long GetUploadedBytes
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

        public TcpSender(IPAddress localIP, int localPort, IPAddress remoteIP, int remotePort, string filePath, string fileName, int bufferSizeKb, bool connect)
        {
            this.localIP = localIP;
            this.localPort = localPort;
            this.remoteIP = remoteIP;
            this.remotePort = remotePort;
            this.filePath = filePath;
            this.fileName = fileName;
            this.connect = connect;
            bufferSize = bufferSizeKb * 1024;
            thread = new Thread(new ThreadStart(Run));
        }

        public void Start()
        {
            thread.Start();
        }

        public void Abort()
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
                        SendFileData(fileStream, stream);
                        uploadedBytes = ReceiveUploadedBytes(stream);
                        if (uploadedBytes != -1)
                            SendFile(fileStream, stream);
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

        private long ReceiveUploadedBytes(NetworkStream netStream)
        {
            byte[] value = Receive(netStream);
            return BitConverter.ToInt64(value, 0);
        }

        private void SendFileData(FileStream fileStream, NetworkStream netStream)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(fileName + "*" + fileStream.Length.ToString());
            Send(netStream, bytes);
        }

        private void SendFile(FileStream fileStream, NetworkStream netStream)
        {
            fileSize = fileStream.Length;
            if (uploadedBytes != 0L)
            {
                isContinue = true;
                fileStream.Seek(uploadedBytes, SeekOrigin.Begin);
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

        public delegate void TcpEventHandler();

        public delegate void TcpErrorHandler(string message);
    }
}
