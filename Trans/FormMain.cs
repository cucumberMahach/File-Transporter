using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Net;
using System.Threading;
using System.Windows.Forms;

namespace Trans
{
    public partial class FormMain : Form
	{
		private SynchronizationContext guiContext;
		private string sendFilePath;
		private Config config;
		private TcpReceiver tcpReceiver;
		private TcpSender tcpSender;
		private List<double> speedData = new List<double>();
		private List<double> speedTimeData = new List<double>();
		private Stopwatch stopwatch = new Stopwatch();
		private long lastBytes = 0L;
		private List<double> plotValues = new List<double>();
        private double plot_min = 0.0;
		private double plot_max = 1000.0;
		private bool continueSkipped;

		public FormMain()
		{
			guiContext = SynchronizationContext.Current;
			InitializeComponent();
			IPAddress[] hostAddresses = Dns.GetHostAddresses(Dns.GetHostName());
			foreach (IPAddress ipaddress in hostAddresses)
			{
				if (!ipaddress.IsIPv6LinkLocal && !ipaddress.IsIPv6Multicast && !ipaddress.IsIPv6SiteLocal && !ipaddress.IsIPv6Teredo)
				{
					comb_this_ip.Items.Add(ipaddress.ToString());
				}
			}
			comb_compression.SelectedIndex = 0;
			LoadConfig();
		}

		private void LoadConfig()
		{
			try
			{
				config = Config.Load();
				comb_this_ip.Text = config.this_ip;
				txt_this_port.Text = config.this_port;
				txt_dist_ip.Text = config.dist_ip;
				txt_dist_port.Text = config.dist_port;
				num_bufferSize.Value = config.bufferSizeKb;
				txt_saveIn.Text = config.saveIn;
				rb_this.Checked = config.rb_this;
				rb_dist.Checked = config.rb_dist;
				comb_compression.SelectedIndex = (int)config.compressionType;
			}
			catch (Exception)
			{
				config = new Config();
				config.Save();
			}
		}

		private void SaveConfig()
		{
			config.this_ip = comb_this_ip.Text;
			config.this_port = txt_this_port.Text;
			config.dist_ip = txt_dist_ip.Text;
			config.dist_port = txt_dist_port.Text;
			config.bufferSizeKb = num_bufferSize.Value;
			config.saveIn = txt_saveIn.Text;
			config.rb_this = rb_this.Checked;
			config.rb_dist = rb_dist.Checked;
			config.compressionType = (CompressionType) Enum.GetValues(typeof(CompressionType)).GetValue(comb_compression.SelectedIndex);
			config.Save();
		}

		private void btn_selectFile_Click(object sender, EventArgs e)
		{
			openFileDialog.ShowDialog();
			if (openFileDialog.FileName != "")
			{
				lab_fileName.Text = openFileDialog.SafeFileName;
				sendFilePath = openFileDialog.FileName;
			}
		}

		private void btn_selectDir_Click(object sender, EventArgs e)
		{
			folderBrowserDialog.ShowDialog();
			if (folderBrowserDialog.SelectedPath != "")
			{
				txt_saveIn.Text = folderBrowserDialog.SelectedPath + "\\";
			}
		}

		private void btn_receive_Click(object sender, EventArgs e)
		{
			SaveConfig();
			if (btn_receive.Text != "Отмена")
			{
				SetEnableControls(false);
				btn_receive.Enabled = true;
				btn_receive.Text = "Отмена";
				tcpReceiver = new TcpReceiver(IPAddress.Parse(comb_this_ip.Text), int.Parse(txt_this_port.Text), IPAddress.Parse(txt_dist_ip.Text), int.Parse(txt_dist_port.Text), txt_saveIn.Text, rb_dist.Checked);
				tcpReceiver.OnReceiveFinished += TcpReceiver_OnReceiveFinished;
				tcpReceiver.OnReceiveError += TcpReceiver_OnError;
				tcpReceiver.OnReceiveMessage += TcpReceiver_OnMessage;
				stopwatch.Start();
				tcpReceiver.Start();
			}
			else
			{
				tcpReceiver.Abort();
				TcpReceiver_OnReceiveFinished();
				SetEnableControls(true);
				btn_receive.Text = "Принять";
			}
		}

		private void TcpReceiver_OnReceiveFinished()
		{
			guiContext.Post(delegate(object d)
			{
				DrawStatistics(tcpReceiver);
				btn_receive.Text = "Принять";
				ClearAll();
				tcpReceiver = null;
			}, null);
		}

		private void btn_send_Click(object sender, EventArgs e)
		{
			SaveConfig();
			if (btn_send.Text != "Отмена")
			{
				SetEnableControls(false);
				btn_send.Enabled = true;
				btn_send.Text = "Отмена";
				CompressionType compressionType = (CompressionType)Enum.GetValues(typeof(CompressionType)).GetValue(comb_compression.SelectedIndex);
				tcpSender = new TcpSender(IPAddress.Parse(comb_this_ip.Text), int.Parse(txt_this_port.Text), IPAddress.Parse(txt_dist_ip.Text), int.Parse(txt_dist_port.Text), sendFilePath, lab_fileName.Text, (int)num_bufferSize.Value, compressionType, rb_dist.Checked);
				tcpSender.OnUploadFinished += TcpSender_OnUploadFinished;
				tcpSender.OnUploadError += TcpSender_OnError;
				tcpSender.OnUploadMessage += TcpSender_OnMessage;
				stopwatch.Start();
				tcpSender.Start();
			}
			else
			{
				tcpSender.Abort();
				TcpSender_OnUploadFinished();
				SetEnableControls(true);
				btn_send.Text = "Отправить";
			}
		}

		private void TcpSender_OnMessage(SenderMessageType type, string text)
		{
			guiContext.Post(delegate (object d)
			{
				MessageBox.Show(text, "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
			}, null);
		}

		private void TcpReceiver_OnMessage(ReceiverMessageType type, string text)
		{
			guiContext.Post(delegate (object d)
			{
				MessageBox.Show(text, "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
			}, null);
		}

		private void TcpSender_OnError(string message)
		{
			guiContext.Post(delegate(object d)
			{
				MessageBox.Show(message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			}, null);
			TcpSender_OnUploadFinished();
		}

		private void TcpReceiver_OnError(string message)
		{
			guiContext.Post(delegate(object d)
			{
				MessageBox.Show(message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			}, null);
			TcpReceiver_OnReceiveFinished();
		}

		private void TcpSender_OnUploadFinished()
		{
			guiContext.Post(delegate(object d)
			{
				DrawStatistics(tcpSender);
				btn_send.Text = "Отправить";
				ClearAll();
				tcpSender = null;
			}, null);
		}

		private void SetEnableControls(bool enable)
		{
			rb_dist.Enabled = enable;
			rb_this.Enabled = enable;
			group_send.Enabled = enable;
			group_receive.Enabled = enable;
			btn_receive.Enabled = enable;
			btn_send.Enabled = enable;
			comb_this_ip.Enabled = enable;
			txt_this_port.Enabled = enable;
			txt_dist_ip.Enabled = enable;
			txt_dist_port.Enabled = enable;
		}

		private void ClearAll()
		{
			SetEnableControls(true);
			speedData.Clear();
			speedTimeData.Clear();
			stopwatch.Stop();
			lastBytes = 0L;
			plotValues.Add(0.0);
			pic_progress.Invalidate();
			pic_plot.Invalidate();
			continueSkipped = false;
		}

		private void timer_status_Tick(object sender, EventArgs e)
		{
            long fileSize;
            long procBytes;
			TcpFuncs bundle;
            if (tcpReceiver != null)
            {
                fileSize = tcpReceiver.FileSize;
                procBytes = tcpReceiver.GotBytes;
				bundle = tcpReceiver;

			}
            else
            {
                if (!(tcpSender != null))
                {
                    lab_speed.Text = "-";
                    lab_time.Text = "-";
                    return;
                }
                fileSize = tcpSender.FileSize;
                procBytes = tcpSender.UploadedBytes;
				bundle = tcpSender;

			}
            pic_progress.Invalidate();
			pic_plot.Invalidate();
			if (stopwatch.ElapsedMilliseconds >= 1000L)
			{
				double item = (double)(procBytes - lastBytes) / ((double)stopwatch.ElapsedMilliseconds / 1000.0) * 8;
				if (tcpReceiver != null)
				{
					if (!continueSkipped && tcpReceiver.IsContinue)
					{
						item = 0.0;
						continueSkipped = true;
					}
				}
				else
				{
					if (tcpSender != null)
					{
						if (!continueSkipped && tcpSender.IsContinue)
						{
							item = 0.0;
							continueSkipped = true;
						}
					}
				}
				speedData.Add(item);
				if (speedData.Count > 5)
				{
					speedData.RemoveAt(0);
				}
				speedTimeData.Add(item);
				if (speedTimeData.Count > 10)
				{
					speedTimeData.RemoveAt(0);
				}
				double avgSpeedData = 0.0;
				double avgSpeedTimeData = 0.0;
				foreach (double speed in speedData)
				{
					avgSpeedData += speed / (double)speedData.Count;
				}
				foreach (double speed in speedTimeData)
				{
					avgSpeedTimeData += speed / (double)speedTimeData.Count;
				}
				plotValues.Add(avgSpeedData);
				if (plotValues.Count > 60)
				{
					plotValues.RemoveAt(0);
				}
				double plotMin = plotValues[0];
				double plotMax = 1000.0;
				foreach (double plotValue in plotValues)
				{
					if (plotValue < plotMin)
					{
						plotMin = plotValue;
					}
					if (plotValue > plotMax)
					{
						plotMax = plotValue;
					}
				}
				plot_min = plotMin;
				plot_max = Tools.RoundMaxSpeed(plotMax * (double)(1f - Config.plotMaxCof + 1f));
				lab_maxSpeed.Text = Tools.ConvertBytes(plot_max/8, Tools.Units.Speed);
				lab_speed.Text = Tools.ConvertBytes(avgSpeedData/8, Tools.Units.Speed);
				lab_time.Text = ((avgSpeedTimeData == 0.0) ? "∞" : Tools.ConvertTime((int)((double)(fileSize - procBytes) * 8 / avgSpeedTimeData)));

				lastBytes = procBytes;
				stopwatch.Restart();
			}
			DrawStatistics(bundle);
		}

		private void DrawStatistics(TcpFuncs bundle)
        {
			lab_sent.Text = Tools.ConvertBytes(bundle.SentDecompressedBytes, Tools.Units.Data);
			lab_received.Text = Tools.ConvertBytes(bundle.ReceivedDecompressedBytes, Tools.Units.Data);
			long savedBytes = bundle.SentDecompressedBytes - bundle.SentCompressedBytes + (bundle.ReceivedDecompressedBytes - bundle.ReceivedCompressedBytes);
			lab_compress.Text = "Сэкономлено " + Tools.ConvertBytes(savedBytes, Tools.Units.Data);
		}

		private void FormMain_FormClosed(object sender, FormClosedEventArgs e)
		{
			SaveConfig();
			Environment.Exit(0);
		}

		private void pic_progress_Paint(object sender, PaintEventArgs e)
		{
			Graphics graphics = e.Graphics;
			PictureBox pictureBox = pic_progress;
			double proc;
			if (tcpReceiver != null)
			{
				proc = (double)tcpReceiver.GotBytes / (double)tcpReceiver.FileSize;
			}
			else
			{
				if (!(tcpSender != null))
				{
					graphics.DrawString(0.ToString("0.00") + "%", SystemFonts.DefaultFont, Brushes.Black, (float)(pictureBox.Width / 2) - SystemFonts.DefaultFont.Size * 4f / 2f, (float)(pictureBox.Height / 2) - (float)SystemFonts.DefaultFont.Height / 2f - 2f);
					return;
				}
				proc = (double)tcpSender.UploadedBytes / (double)tcpSender.FileSize;
			}
			graphics.FillRectangle(Brushes.LightGreen, 0f, 0f, (float)(proc * (double)pictureBox.Width), (float)pictureBox.Height);
			if (!double.IsNaN(proc))
			{
				graphics.DrawString((proc * 100.0).ToString("0.00") + "%", SystemFonts.DefaultFont, Brushes.Black, (float)(pictureBox.Width / 2) - SystemFonts.DefaultFont.Size * 5f / 2f, (float)(pictureBox.Height / 2) - (float)SystemFonts.DefaultFont.Height / 2f - 2f);
			}
		}

		private void pic_plot_SizeChanged(object sender, EventArgs e)
		{
			pic_plot.Invalidate();
		}

		private void pic_progress_SizeChanged(object sender, EventArgs e)
		{
			pic_progress.Invalidate();
		}

		private void pic_plot_Paint(object sender, PaintEventArgs e)
		{
			Graphics graphics = e.Graphics;
			PictureBox pictureBox = pic_plot;
			int x = 0;
			int y = 0;
			for (int i = 0; i < plotValues.Count; i++)
			{
				double plotCof = Tools.Map(plotValues[i], 0.0, plot_max, 0.0, (double)Config.plotMaxCof);
				if (double.IsNaN(plotCof))
				{
					plotCof = 0.0;
				}
				int x2 = (int)((float)pictureBox.Width / (float)Config.plotPointsCount * (float)i);
				int y2 = (int)((double)pictureBox.Height - ((double)pictureBox.Height * plotCof + 5.0));
				if (i != 0)
				{
					graphics.DrawLine(Pens.DarkViolet, x, y, x2, y2);
				}
				x = x2;
				y = y2;
			}
		}
	}
}
