using System;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;

namespace Trans
{
	public abstract class TcpFuncs
	{
		protected BinaryFormatter formatter = new BinaryFormatter();
		protected long sentCompressedBytes;
		protected long sentDecompressedBytes;
		protected long receivedCompressedBytes;
		protected long receivedDecompressedBytes;
		protected CompressionType compressionType;

		public long SentCompressedBytes
		{
			get
			{
				return sentCompressedBytes;
			}
		}

		public long SentDecompressedBytes
		{
			get
			{
				return sentDecompressedBytes;
			}
		}

		public long ReceivedCompressedBytes
		{
			get
			{
				return receivedCompressedBytes;
			}
		}

		public long ReceivedDecompressedBytes
		{
			get
			{
				return receivedDecompressedBytes;
			}
		}

		public TcpFuncs(CompressionType compressionType = CompressionType.None)
        {
			this.compressionType = compressionType;
		}

		protected void ListenAndCheckBeginBytes(IPAddress localIP, int localPort, ref TcpListener tcpListener, ref TcpClient tcpClient)
		{
			tcpListener = new TcpListener(localIP, localPort);
			tcpListener.Start();
			try
			{
				tcpClient = tcpListener.AcceptTcpClient();
			}
			catch (SocketException)
			{
				tcpListener.Stop();
				Abort();
				return;
			}
			NetworkStream stream = tcpClient.GetStream();
			bool notValid = !IsValidBeginBytes(stream, Config.beginBytes);
			if (notValid)
			{
				stream.Close();
				tcpClient.Close();
				tcpListener.Stop();
				Abort();
			}
		}

		protected TcpClient ConnectAndSendBeginBytes(IPAddress remoteIP, int remotePort)
		{
			TcpClient tcpClient = new TcpClient();
			try
			{
				tcpClient.Connect(remoteIP, remotePort);
			}
			catch (SocketException)
			{
				Abort();
			}
			NetworkStream stream = tcpClient.GetStream();
			Send(stream, Config.beginBytes);
			return tcpClient;
		}

		protected void Send(NetworkStream stream, byte[] data)
		{
			byte[] compressedData = Tools.Compress(data, compressionType);
			formatter.Serialize(stream, compressedData);
			sentCompressedBytes += compressedData.Length;
			sentDecompressedBytes += data.Length;
		}

		protected byte[] Receive(NetworkStream stream)
		{
			byte[] compressedData = (byte[])formatter.Deserialize(stream);
			byte[] decompressedData = Tools.Decompress(compressedData, compressionType);
			receivedCompressedBytes += compressedData.Length;
			receivedDecompressedBytes += decompressedData.Length;
			return decompressedData;
		}

		protected bool IsValidBeginBytes(NetworkStream stream, byte[] beginBytes)
		{
			byte[] array = Receive(stream);
			return Enumerable.SequenceEqual(array, beginBytes);
		}

		protected void ClearDataStatistics()
        {
			sentCompressedBytes = 0;
			sentDecompressedBytes = 0;
			receivedCompressedBytes = 0;
			receivedDecompressedBytes = 0;
		}

		protected void SetCompresstionType(CompressionType type)
        {
			compressionType = type;
        }

		public abstract void Abort();
	}
}
