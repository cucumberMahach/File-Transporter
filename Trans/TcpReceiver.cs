using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace Trans
{
	public class TcpReceiver : TcpFuncs
	{
		protected IPAddress localIP;
		protected int localPort;
		protected Thread thread;
		protected TcpClient tcpClient;
		protected TcpListener tcpListener;
		protected bool connect;
		protected IPAddress remoteIP;
		protected int remotePort;
		protected string savePath;
		protected string fileName;
		protected FileStream fileStream;
		protected long fileSize;
		protected byte[] fileHash;
		protected long gotBytes;
		protected bool isContinue;

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
			/*try
			{*/
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
					if (startPos > 0)
					{
						isContinue = true;
					}
					if (startPos == -1)
					{
						OnReceiveMessage(ReceiverMessageType.SIZES_ARE_EQUAL, "Файл уже загружен");
					}
					else
					{
						ReceiveFile(startPos, stream);
					}
				}
				tcpClient.Close();
				if (tcpListener != null)
				{
					tcpListener.Stop();
				}
			/*}
			catch (Exception ex)
			{
				OnReceiveError(ex.Message);
				Abort();
			}*/
			OnReceiveFinished();
		}

		/// <summary>
		/// Check file exist, file size and hashes
		/// </summary>
		/// <returns>
		/// Returns which byte to start loading from or
		/// -1 then sizes are equal
		/// </returns>
		protected long CheckIsContinueNeeded()
		{
			if (File.Exists(savePath + fileName))
			{
				using (FileStream fileStream = File.OpenRead(savePath + fileName)) {
					long length = fileStream.Length;
					if (length < fileSize)
					{
						return length;
					}
					if (length == fileSize)
					{
						return -1;
					}
				}
			}
			return 0;
		}

		protected void SendStartPos(long startPos, NetworkStream netStream)
        {
			byte[] bytes = BitConverter.GetBytes(startPos);
			Send(netStream, bytes);
		}

		protected void ReceiveFileData(NetworkStream netStream)
		{
			//Receive file name and file size
			string[] array = Encoding.UTF8.GetString(Receive(netStream)).Split(new char[]{'*'});
			fileName = array[0];
			fileSize = long.Parse(array[1]);
		}

		protected void ReceiveFile(long startPos, NetworkStream netStream)
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
		public event TcpReceiver.TcpMessageHandler OnReceiveMessage;

		public delegate void TcpEventHandler();
		public delegate void TcpErrorHandler(string message);
		public delegate void TcpMessageHandler(ReceiverMessageType type, string text);
	}

	public enum ReceiverMessageType
	{
		NO_TYPE,
		SIZES_ARE_EQUAL
	}
}
