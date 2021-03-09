using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Trans
{
	[Serializable]
	public class Config
	{
		public static int plotPointsCount = 60;
		public static float plotMaxCof = 0.8f;
		public string this_ip;
		public string this_port;
		public string dist_ip;
		public string dist_port;
		public decimal bufferSizeKb;
		public string saveIn;
		public bool rb_this;
		public bool rb_dist;
		public CompressionType compressionType;

		public Config()
		{
			this_ip = "";
			this_port = "";
			dist_ip = "";
			dist_port = "";
			bufferSizeKb = 32m;
			saveIn = "";
			rb_this = true;
			rb_dist = false;
			compressionType = CompressionType.None;
		}

		public static Config Load()
		{
			BinaryFormatter binaryFormatter = new BinaryFormatter();
			Config result = null;
			using (FileStream fileStream = File.OpenRead(configPath))
			{
				result = (Config)binaryFormatter.Deserialize(fileStream);
			}
			return result;
		}

		public void Save()
		{
			BinaryFormatter binaryFormatter = new BinaryFormatter();
			using (FileStream fileStream = File.Create(configPath))
			{
				binaryFormatter.Serialize(fileStream, this);
			}
		}

		public static string configPath = "config.bin";

		public static byte[] beginBytes = new byte[]
		{
			123,
			50,
			40,
			44,
			37,
			28,
			99
		};
	}
}
