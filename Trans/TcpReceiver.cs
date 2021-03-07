using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace Trans
{
	public class TcpReceiver : TcpFuncs
	{
		private IPAddress localIP;
		private int localPort;
		private Thread thread;
		private TcpClient tcpClient;
		private TcpListener tcpListener;
		private bool connect;
		private IPAddress remoteIP;
		private int remotePort;
		private string savePath;
		private string fileName;
		private FileStream fileStream;
		private long fileSize;
		private long gotBytes;
		private bool isContinue;

		public long GetGotBytes
		{
			get
			{
				return gotBytes;
			}
		}

		public long GetFileSize
		{
			get
			{
				return fileSize;
			}
		}

		public bool IsContinue
		{
			get
			{
				return isContinue;
			}
		}

		public TcpReceiver(IPAddress localIP, int localPort, IPAddress remoteIP, int remotePort, string savePath, bool connect)
		{
			this.localIP = localIP;
			this.localPort = localPort;
			this.savePath = savePath;
			this.connect = connect;
			this.remoteIP = remoteIP;
			this.remotePort = remotePort;
			thread = new Thread(new ThreadStart(Run));
		}

		public void Start()
		{
			thread.Start();
		}

		public void Abort()
		{
			if (tcpClient != null)
			{
				tcpClient.Close();
			}
			if (tcpListener != null)
			{
				tcpListener.Stop();
			}
			if (fileStream != null)
			{
				fileStream.Close();
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
					ReceiveFileData(stream);
					long startPos = CheckIsContinueNeeded();
					SendStartPos(startPos, stream);
					if (startPos > 0L)
					{
						isContinue = true;
					}else if (startPos != -1)
                    {
						ReceiveFile(startPos, stream);
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
				OnReceiveError(ex.Message);
				Abort();
			}
			OnReceiveFinished();
		}

		private long CheckIsContinueNeeded()
		{
			if (File.Exists(savePath + fileName))
			{
				FileStream fileStream = File.OpenRead(savePath + fileName);
				long length = fileStream.Length;
				fileStream.Close();
				if (length < fileSize)
				{
					return length;
				}
				if (length == fileSize)
				{
					return -1L;
				}
			}
			return 0L;
		}

		private void SendStartPos(long startPos, NetworkStream netStream)
        {
			byte[] bytes = BitConverter.GetBytes(startPos);
			Send(netStream, bytes);
		}

		private void ReceiveFileData(NetworkStream netStream)
		{
			string[] array = Encoding.UTF8.GetString(Receive(netStream)).Split(new char[]
			{
				'*'
			});
			fileName = array[0];
			fileSize = long.Parse(array[1]);
		}

		private void ReceiveFile(long startPos, NetworkStream netStream)
		{
			if (startPos == 0L)
			{
				fileStream = File.Create(savePath + fileName);
			}
			else
			{
				fileStream = File.Open(savePath + fileName, FileMode.Append);
			}
			gotBytes = startPos;
			while (gotBytes < fileSize)
			{
				byte[] array = Receive(netStream);
				gotBytes += (long)array.Length;
				fileStream.Write(array, 0, array.Length);
			}
			fileStream.Close();
		}

		public event TcpReceiver.TcpEventHandler OnReceiveFinished;

		public event TcpReceiver.TcpErrorHandler OnReceiveError;

		public delegate void TcpEventHandler();

		public delegate void TcpErrorHandler(string message);
	}
}
