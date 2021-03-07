using System;
using System.Collections.Generic;
using System.ComponentModel;
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
		private double plot_max = 1024.0;
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
			tcpReceiver = null;
			guiContext.Post(delegate(object d)
			{
				btn_receive.Text = "Принять";
				ClearAll();
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
				tcpSender = new TcpSender(IPAddress.Parse(comb_this_ip.Text), int.Parse(txt_this_port.Text), IPAddress.Parse(txt_dist_ip.Text), int.Parse(txt_dist_port.Text), sendFilePath, lab_fileName.Text, (int)num_bufferSize.Value, rb_dist.Checked);
				tcpSender.OnUploadFinished += TcpSender_OnUploadFinished;
				tcpSender.OnUploadError += TcpSender_OnError;
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
			tcpSender = null;
			guiContext.Post(delegate(object d)
			{
				btn_send.Text = "Отправить";
				ClearAll();
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
            if (tcpReceiver != null)
            {
                fileSize = tcpReceiver.GetFileSize;
                procBytes = tcpReceiver.GetGotBytes;
            }
            else
            {
                if (!(tcpSender != null))
                {
                    lab_speed.Text = "-";
                    lab_time.Text = "-";
                    return;
                }
                fileSize = tcpSender.GetFileSize;
                procBytes = tcpSender.GetUploadedBytes;
            }
            pic_progress.Invalidate();
			pic_plot.Invalidate();
			if (stopwatch.ElapsedMilliseconds >= 1000L)
			{
				double item = (double)(procBytes - lastBytes) / ((double)stopwatch.ElapsedMilliseconds / 1000.0);
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
				double plotMax = 1024.0;
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
				lab_maxSpeed.Text = Tools.ConvertSpeed(plot_max);
				lab_speed.Text = Tools.ConvertSpeed(avgSpeedData);
				lab_time.Text = ((avgSpeedTimeData == 0.0) ? "∞" : Tools.ConvertTime((int)((double)(fileSize - procBytes) / avgSpeedTimeData)));
				lastBytes = procBytes;
				stopwatch.Restart();
			}
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
				proc = (double)tcpReceiver.GetGotBytes / (double)tcpReceiver.GetFileSize;
			}
			else
			{
				if (!(tcpSender != null))
				{
					graphics.DrawString(0.ToString("0.00") + "%", SystemFonts.DefaultFont, Brushes.Black, (float)(pictureBox.Width / 2) - SystemFonts.DefaultFont.Size * 4f / 2f, (float)(pictureBox.Height / 2) - (float)SystemFonts.DefaultFont.Height / 2f - 2f);
					return;
				}
				proc = (double)tcpSender.GetUploadedBytes / (double)tcpSender.GetFileSize;
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
