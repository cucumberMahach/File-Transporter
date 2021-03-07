using System;
using System.Globalization;

namespace Trans
{
	public class Tools
	{
		private static readonly NumberFormatInfo format = (NumberFormatInfo)CultureInfo.InvariantCulture.NumberFormat.Clone();
		public static string ConvertSpeed(double bps)
		{
			format.NumberGroupSeparator = " ";
			format.NumberDecimalDigits = 2;
			string text = "#,#.00";
			string result;
			if (bps == 0.0)
			{
				result = "0 байт/сек";
			}
			else
			{
				if (bps < 1024.0)
				{
					result = bps.ToString(text, format) + " байт/сек";
				}
				else
				{
					double kbps = bps / 1024.0;
					if (kbps < 1024.0)
					{
						result = kbps.ToString(text, format) + " кб/сек";
					}
					else
					{
						double mbps = kbps / 1024.0;
						if (mbps < 1024.0)
						{
							result = mbps.ToString(text, format) + " мб/сек";
						}
						else
						{
							double gbps = mbps / 1024.0;
							if (gbps < 1024.0)
							{
								result = gbps.ToString(text, format) + " гб/сек";
							}
							else
							{
								result = (gbps / 1024.0).ToString(text, format) + " тб/сек";
							}
						}
					}
				}
			}
			return result;
		}

		public static string ConvertTime(int sec)
		{
			int h = sec / 60 / 60;
			int m = sec / 60 % 60;
			int s = sec % 60;
			return string.Concat(new string[]
			{
				h.ToString(),
				" ч ",
				m.ToString(),
				" мин ",
				s.ToString(),
				" сек"
			});
		}

		public static double Map(double value, double low, double high, double low_2, double high_2)
		{
			double num = (value - low) / (high - low);
			return low_2 + (high_2 - low_2) * num;
		}

		public static double GetNearestUnitOver(double val)
		{
			int num = (int)Math.Floor(val);
			while (num % 5 != 0)
			{
				num++;
			}
			return num;
		}

		public static double RoundMaxSpeed(double bps)
		{
			double result;
			if (bps == 0.0)
			{
				result = 0.0;
			}
			else
			{
				if (bps < 1024.0)
				{
					result = GetNearestUnitOver(bps);
				}
				else
				{
					double kbps = bps / 1024.0;
					if (kbps < 1024.0)
					{
						result = GetNearestUnitOver(kbps) * 1024.0;
					}
					else
					{
						double mbps = kbps / 1024.0;
						if (mbps < 1024.0)
						{
							result = GetNearestUnitOver(mbps) * 1024.0 * 1024.0;
						}
						else
						{
							double gbps = mbps / 1024.0;
							if (gbps < 1024.0)
							{
								result = GetNearestUnitOver(gbps) * 1024.0 * 1024.0 * 1024.0;
							}
							else
							{
								result = GetNearestUnitOver(gbps / 1024.0) * 1024.0 * 1024.0 * 1024.0 * 1024.0;
							}
						}
					}
				}
			}
			return result;
		}
	}
}
