using System;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;

namespace Trans
{
	public abstract class TcpFuncs
	{
		protected BinaryFormatter formatter = new BinaryFormatter();

        public TcpFuncs()
        {
			
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
				Thread.ResetAbort();
				return;
			}
			NetworkStream stream = tcpClient.GetStream();
			bool notValid = !IsValidBeginBytes(stream, Config.beginBytes);
			if (notValid)
			{
				stream.Close();
				tcpClient.Close();
				tcpListener.Stop();
				Thread.ResetAbort();
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
				Thread.ResetAbort();
			}
			NetworkStream stream = tcpClient.GetStream();
			Send(stream, Config.beginBytes);
			return tcpClient;
		}

		protected void Send(NetworkStream stream, byte[] data)
		{
			formatter.Serialize(stream, Tools.Compress7Zip(data));
		}

		protected byte[] Receive(NetworkStream stream)
		{
			return Tools.Decompress7Zip((byte[])formatter.Deserialize(stream));
		}

		protected bool IsValidBeginBytes(NetworkStream stream, byte[] beginBytes)
		{
			byte[] array = Receive(stream);
			for (int i = 0; i < array.Length; i++)
			{
				bool byteNotValid = array[i] != beginBytes[i];
				if (byteNotValid)
				{
					return false;
				}
			}
			return true;
		}
	}
}
