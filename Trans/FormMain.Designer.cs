namespace Trans
{
	// Token: 0x02000003 RID: 3
	public partial class FormMain : global::System.Windows.Forms.Form
	{
		// Token: 0x06000018 RID: 24 RVA: 0x00002FD4 File Offset: 0x000011D4
		protected override void Dispose(bool disposing)
		{
			bool flag = disposing && this.components != null;
			if (flag)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x06000019 RID: 25 RVA: 0x0000300C File Offset: 0x0000120C
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.group_send = new System.Windows.Forms.GroupBox();
            this.label12 = new System.Windows.Forms.Label();
            this.comb_compression = new System.Windows.Forms.ComboBox();
            this.num_bufferSize = new System.Windows.Forms.NumericUpDown();
            this.label11 = new System.Windows.Forms.Label();
            this.btn_selectFile = new System.Windows.Forms.Button();
            this.lab_fileName = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.btn_send = new System.Windows.Forms.Button();
            this.group_receive = new System.Windows.Forms.GroupBox();
            this.btn_selectDir = new System.Windows.Forms.Button();
            this.txt_saveIn = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.btn_receive = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txt_this_port = new System.Windows.Forms.TextBox();
            this.txt_dist_port = new System.Windows.Forms.TextBox();
            this.txt_dist_ip = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.lab_time = new System.Windows.Forms.Label();
            this.lab_speed = new System.Windows.Forms.Label();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.comb_this_ip = new System.Windows.Forms.ComboBox();
            this.timer_status = new System.Windows.Forms.Timer(this.components);
            this.rb_this = new System.Windows.Forms.RadioButton();
            this.rb_dist = new System.Windows.Forms.RadioButton();
            this.lab_maxSpeed = new System.Windows.Forms.Label();
            this.lab_minSpeed = new System.Windows.Forms.Label();
            this.panel_trans = new System.Windows.Forms.Panel();
            this.lab_compress = new System.Windows.Forms.Label();
            this.lab_received = new System.Windows.Forms.Label();
            this.lab_sent = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.pic_compress = new System.Windows.Forms.PictureBox();
            this.pic_download = new System.Windows.Forms.PictureBox();
            this.pic_upload = new System.Windows.Forms.PictureBox();
            this.pic_plot = new System.Windows.Forms.PictureBox();
            this.pic_progress = new System.Windows.Forms.PictureBox();
            this.group_send.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.num_bufferSize)).BeginInit();
            this.group_receive.SuspendLayout();
            this.panel_trans.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pic_compress)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_download)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_upload)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_plot)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_progress)).BeginInit();
            this.SuspendLayout();
            // 
            // group_send
            // 
            this.group_send.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.group_send.Controls.Add(this.label12);
            this.group_send.Controls.Add(this.comb_compression);
            this.group_send.Controls.Add(this.num_bufferSize);
            this.group_send.Controls.Add(this.label11);
            this.group_send.Controls.Add(this.btn_selectFile);
            this.group_send.Controls.Add(this.lab_fileName);
            this.group_send.Controls.Add(this.label7);
            this.group_send.Location = new System.Drawing.Point(8, 120);
            this.group_send.Name = "group_send";
            this.group_send.Size = new System.Drawing.Size(416, 81);
            this.group_send.TabIndex = 0;
            this.group_send.TabStop = false;
            this.group_send.Text = "Отправка";
            // 
            // label12
            // 
            this.label12.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(216, 57);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(48, 13);
            this.label12.TabIndex = 10;
            this.label12.Text = "Сжатие:";
            // 
            // comb_compression
            // 
            this.comb_compression.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.comb_compression.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comb_compression.FormattingEnabled = true;
            this.comb_compression.Items.AddRange(new object[] {
            "Нет",
            "DeflateStream",
            "lzma"});
            this.comb_compression.Location = new System.Drawing.Point(269, 54);
            this.comb_compression.Name = "comb_compression";
            this.comb_compression.Size = new System.Drawing.Size(134, 21);
            this.comb_compression.TabIndex = 9;
            // 
            // num_bufferSize
            // 
            this.num_bufferSize.Location = new System.Drawing.Point(131, 55);
            this.num_bufferSize.Maximum = new decimal(new int[] {
            16384,
            0,
            0,
            0});
            this.num_bufferSize.Name = "num_bufferSize";
            this.num_bufferSize.Size = new System.Drawing.Size(61, 20);
            this.num_bufferSize.TabIndex = 8;
            this.num_bufferSize.Value = new decimal(new int[] {
            32,
            0,
            0,
            0});
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(15, 57);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(110, 13);
            this.label11.TabIndex = 7;
            this.label11.Text = "Размер буфера (кб):";
            // 
            // btn_selectFile
            // 
            this.btn_selectFile.Location = new System.Drawing.Point(60, 20);
            this.btn_selectFile.Name = "btn_selectFile";
            this.btn_selectFile.Size = new System.Drawing.Size(26, 23);
            this.btn_selectFile.TabIndex = 6;
            this.btn_selectFile.Text = "...";
            this.btn_selectFile.UseVisualStyleBackColor = true;
            this.btn_selectFile.Click += new System.EventHandler(this.btn_selectFile_Click);
            // 
            // lab_fileName
            // 
            this.lab_fileName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lab_fileName.Location = new System.Drawing.Point(90, 24);
            this.lab_fileName.Name = "lab_fileName";
            this.lab_fileName.Size = new System.Drawing.Size(313, 19);
            this.lab_fileName.TabIndex = 4;
            this.lab_fileName.Text = "Не выбран";
            this.lab_fileName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(15, 25);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(39, 13);
            this.label7.TabIndex = 3;
            this.label7.Text = "Файл:";
            // 
            // btn_send
            // 
            this.btn_send.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_send.Location = new System.Drawing.Point(8, 207);
            this.btn_send.Name = "btn_send";
            this.btn_send.Size = new System.Drawing.Size(416, 23);
            this.btn_send.TabIndex = 5;
            this.btn_send.Text = "Отправить";
            this.btn_send.UseVisualStyleBackColor = true;
            this.btn_send.Click += new System.EventHandler(this.btn_send_Click);
            // 
            // group_receive
            // 
            this.group_receive.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.group_receive.Controls.Add(this.btn_selectDir);
            this.group_receive.Controls.Add(this.txt_saveIn);
            this.group_receive.Controls.Add(this.label9);
            this.group_receive.Location = new System.Drawing.Point(8, 240);
            this.group_receive.Name = "group_receive";
            this.group_receive.Size = new System.Drawing.Size(416, 58);
            this.group_receive.TabIndex = 1;
            this.group_receive.TabStop = false;
            this.group_receive.Text = "Прием";
            // 
            // btn_selectDir
            // 
            this.btn_selectDir.Location = new System.Drawing.Point(93, 24);
            this.btn_selectDir.Name = "btn_selectDir";
            this.btn_selectDir.Size = new System.Drawing.Size(26, 23);
            this.btn_selectDir.TabIndex = 10;
            this.btn_selectDir.Text = "...";
            this.btn_selectDir.UseVisualStyleBackColor = true;
            this.btn_selectDir.Click += new System.EventHandler(this.btn_selectDir_Click);
            // 
            // txt_saveIn
            // 
            this.txt_saveIn.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txt_saveIn.Location = new System.Drawing.Point(125, 26);
            this.txt_saveIn.Name = "txt_saveIn";
            this.txt_saveIn.ReadOnly = true;
            this.txt_saveIn.Size = new System.Drawing.Size(273, 20);
            this.txt_saveIn.TabIndex = 8;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(15, 29);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(72, 13);
            this.label9.TabIndex = 4;
            this.label9.Text = "Сохранить в:";
            // 
            // btn_receive
            // 
            this.btn_receive.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_receive.Location = new System.Drawing.Point(8, 304);
            this.btn_receive.Name = "btn_receive";
            this.btn_receive.Size = new System.Drawing.Size(416, 23);
            this.btn_receive.TabIndex = 9;
            this.btn_receive.Text = "Принять";
            this.btn_receive.UseVisualStyleBackColor = true;
            this.btn_receive.Click += new System.EventHandler(this.btn_receive_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(5, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(93, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Этот компьютер:";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(259, 4);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(107, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Другой компьютер:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 31);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(17, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "IP";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(8, 66);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(32, 13);
            this.label4.TabIndex = 5;
            this.label4.Text = "Порт";
            // 
            // txt_this_port
            // 
            this.txt_this_port.Location = new System.Drawing.Point(46, 63);
            this.txt_this_port.Name = "txt_this_port";
            this.txt_this_port.Size = new System.Drawing.Size(127, 20);
            this.txt_this_port.TabIndex = 7;
            // 
            // txt_dist_port
            // 
            this.txt_dist_port.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txt_dist_port.Location = new System.Drawing.Point(297, 63);
            this.txt_dist_port.Name = "txt_dist_port";
            this.txt_dist_port.Size = new System.Drawing.Size(127, 20);
            this.txt_dist_port.TabIndex = 11;
            // 
            // txt_dist_ip
            // 
            this.txt_dist_ip.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txt_dist_ip.Location = new System.Drawing.Point(297, 31);
            this.txt_dist_ip.Name = "txt_dist_ip";
            this.txt_dist_ip.Size = new System.Drawing.Size(127, 20);
            this.txt_dist_ip.TabIndex = 10;
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(259, 66);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(32, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "Порт";
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(259, 31);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(17, 13);
            this.label6.TabIndex = 8;
            this.label6.Text = "IP";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(5, 343);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(59, 13);
            this.label8.TabIndex = 13;
            this.label8.Text = "Осталось:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(5, 367);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(58, 13);
            this.label10.TabIndex = 14;
            this.label10.Text = "Скорость:";
            // 
            // lab_time
            // 
            this.lab_time.AutoSize = true;
            this.lab_time.Location = new System.Drawing.Point(70, 343);
            this.lab_time.Name = "lab_time";
            this.lab_time.Size = new System.Drawing.Size(34, 13);
            this.lab_time.TabIndex = 20;
            this.lab_time.Text = "0 сек";
            // 
            // lab_speed
            // 
            this.lab_speed.AutoSize = true;
            this.lab_speed.Location = new System.Drawing.Point(70, 367);
            this.lab_speed.Name = "lab_speed";
            this.lab_speed.Size = new System.Drawing.Size(56, 13);
            this.lab_speed.TabIndex = 21;
            this.lab_speed.Text = "0 бит/сек";
            // 
            // comb_this_ip
            // 
            this.comb_this_ip.FormattingEnabled = true;
            this.comb_this_ip.Location = new System.Drawing.Point(46, 28);
            this.comb_this_ip.Name = "comb_this_ip";
            this.comb_this_ip.Size = new System.Drawing.Size(127, 21);
            this.comb_this_ip.TabIndex = 22;
            // 
            // timer_status
            // 
            this.timer_status.Enabled = true;
            this.timer_status.Tick += new System.EventHandler(this.timer_status_Tick);
            // 
            // rb_this
            // 
            this.rb_this.AutoSize = true;
            this.rb_this.Checked = true;
            this.rb_this.Location = new System.Drawing.Point(11, 89);
            this.rb_this.Name = "rb_this";
            this.rb_this.Size = new System.Drawing.Size(62, 17);
            this.rb_this.TabIndex = 9;
            this.rb_this.TabStop = true;
            this.rb_this.Text = "Сервер";
            this.rb_this.UseVisualStyleBackColor = true;
            // 
            // rb_dist
            // 
            this.rb_dist.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.rb_dist.AutoSize = true;
            this.rb_dist.Location = new System.Drawing.Point(262, 89);
            this.rb_dist.Name = "rb_dist";
            this.rb_dist.Size = new System.Drawing.Size(62, 17);
            this.rb_dist.TabIndex = 23;
            this.rb_dist.Text = "Сервер";
            this.rb_dist.UseVisualStyleBackColor = true;
            // 
            // lab_maxSpeed
            // 
            this.lab_maxSpeed.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lab_maxSpeed.AutoSize = true;
            this.lab_maxSpeed.Location = new System.Drawing.Point(344, 482);
            this.lab_maxSpeed.Name = "lab_maxSpeed";
            this.lab_maxSpeed.Size = new System.Drawing.Size(56, 13);
            this.lab_maxSpeed.TabIndex = 25;
            this.lab_maxSpeed.Text = "0 бит/сек";
            // 
            // lab_minSpeed
            // 
            this.lab_minSpeed.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lab_minSpeed.AutoSize = true;
            this.lab_minSpeed.Location = new System.Drawing.Point(344, 554);
            this.lab_minSpeed.Name = "lab_minSpeed";
            this.lab_minSpeed.Size = new System.Drawing.Size(56, 13);
            this.lab_minSpeed.TabIndex = 26;
            this.lab_minSpeed.Text = "0 бит/сек";
            // 
            // panel_trans
            // 
            this.panel_trans.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel_trans.Controls.Add(this.label13);
            this.panel_trans.Controls.Add(this.lab_compress);
            this.panel_trans.Controls.Add(this.pic_compress);
            this.panel_trans.Controls.Add(this.lab_received);
            this.panel_trans.Controls.Add(this.lab_sent);
            this.panel_trans.Controls.Add(this.pic_download);
            this.panel_trans.Controls.Add(this.pic_upload);
            this.panel_trans.Controls.Add(this.label1);
            this.panel_trans.Controls.Add(this.lab_minSpeed);
            this.panel_trans.Controls.Add(this.group_send);
            this.panel_trans.Controls.Add(this.lab_maxSpeed);
            this.panel_trans.Controls.Add(this.group_receive);
            this.panel_trans.Controls.Add(this.pic_plot);
            this.panel_trans.Controls.Add(this.label2);
            this.panel_trans.Controls.Add(this.btn_receive);
            this.panel_trans.Controls.Add(this.label3);
            this.panel_trans.Controls.Add(this.rb_dist);
            this.panel_trans.Controls.Add(this.label4);
            this.panel_trans.Controls.Add(this.btn_send);
            this.panel_trans.Controls.Add(this.txt_this_port);
            this.panel_trans.Controls.Add(this.rb_this);
            this.panel_trans.Controls.Add(this.label6);
            this.panel_trans.Controls.Add(this.comb_this_ip);
            this.panel_trans.Controls.Add(this.label5);
            this.panel_trans.Controls.Add(this.lab_speed);
            this.panel_trans.Controls.Add(this.txt_dist_ip);
            this.panel_trans.Controls.Add(this.lab_time);
            this.panel_trans.Controls.Add(this.txt_dist_port);
            this.panel_trans.Controls.Add(this.pic_progress);
            this.panel_trans.Controls.Add(this.label8);
            this.panel_trans.Controls.Add(this.label10);
            this.panel_trans.Location = new System.Drawing.Point(12, 12);
            this.panel_trans.Name = "panel_trans";
            this.panel_trans.Size = new System.Drawing.Size(432, 578);
            this.panel_trans.TabIndex = 27;
            // 
            // lab_compress
            // 
            this.lab_compress.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lab_compress.ForeColor = System.Drawing.Color.Olive;
            this.lab_compress.Location = new System.Drawing.Point(181, 404);
            this.lab_compress.Name = "lab_compress";
            this.lab_compress.Size = new System.Drawing.Size(217, 24);
            this.lab_compress.TabIndex = 33;
            this.lab_compress.Text = "Cэкономлено 0 Байт";
            this.lab_compress.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lab_received
            // 
            this.lab_received.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lab_received.ForeColor = System.Drawing.Color.Maroon;
            this.lab_received.Location = new System.Drawing.Point(277, 374);
            this.lab_received.Name = "lab_received";
            this.lab_received.Size = new System.Drawing.Size(121, 24);
            this.lab_received.TabIndex = 30;
            this.lab_received.Text = "0 Байт";
            this.lab_received.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lab_sent
            // 
            this.lab_sent.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lab_sent.ForeColor = System.Drawing.Color.Green;
            this.lab_sent.Location = new System.Drawing.Point(277, 344);
            this.lab_sent.Name = "lab_sent";
            this.lab_sent.Size = new System.Drawing.Size(121, 24);
            this.lab_sent.TabIndex = 29;
            this.lab_sent.Text = "0 Байт";
            this.lab_sent.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label13
            // 
            this.label13.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label13.Location = new System.Drawing.Point(300, 558);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(47, 13);
            this.label13.TabIndex = 34;
            this.label13.Text = "1 мин^";
            this.label13.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // pic_compress
            // 
            this.pic_compress.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pic_compress.Image = global::Trans.Properties.Resources.Zip32;
            this.pic_compress.Location = new System.Drawing.Point(400, 404);
            this.pic_compress.Name = "pic_compress";
            this.pic_compress.Size = new System.Drawing.Size(24, 24);
            this.pic_compress.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pic_compress.TabIndex = 31;
            this.pic_compress.TabStop = false;
            // 
            // pic_download
            // 
            this.pic_download.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pic_download.Image = global::Trans.Properties.Resources.DownArrow32;
            this.pic_download.Location = new System.Drawing.Point(400, 374);
            this.pic_download.Name = "pic_download";
            this.pic_download.Size = new System.Drawing.Size(24, 24);
            this.pic_download.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pic_download.TabIndex = 28;
            this.pic_download.TabStop = false;
            // 
            // pic_upload
            // 
            this.pic_upload.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pic_upload.Image = global::Trans.Properties.Resources.UpArrow32;
            this.pic_upload.Location = new System.Drawing.Point(400, 344);
            this.pic_upload.Name = "pic_upload";
            this.pic_upload.Size = new System.Drawing.Size(24, 24);
            this.pic_upload.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pic_upload.TabIndex = 27;
            this.pic_upload.TabStop = false;
            // 
            // pic_plot
            // 
            this.pic_plot.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pic_plot.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pic_plot.Location = new System.Drawing.Point(8, 488);
            this.pic_plot.Name = "pic_plot";
            this.pic_plot.Size = new System.Drawing.Size(330, 67);
            this.pic_plot.TabIndex = 24;
            this.pic_plot.TabStop = false;
            this.pic_plot.SizeChanged += new System.EventHandler(this.pic_plot_SizeChanged);
            this.pic_plot.Paint += new System.Windows.Forms.PaintEventHandler(this.pic_plot_Paint);
            // 
            // pic_progress
            // 
            this.pic_progress.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pic_progress.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pic_progress.Location = new System.Drawing.Point(8, 453);
            this.pic_progress.Name = "pic_progress";
            this.pic_progress.Size = new System.Drawing.Size(416, 26);
            this.pic_progress.TabIndex = 19;
            this.pic_progress.TabStop = false;
            this.pic_progress.SizeChanged += new System.EventHandler(this.pic_progress_SizeChanged);
            this.pic_progress.Paint += new System.Windows.Forms.PaintEventHandler(this.pic_progress_Paint);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(455, 602);
            this.Controls.Add(this.panel_trans);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(468, 610);
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Trans";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormMain_FormClosed);
            this.group_send.ResumeLayout(false);
            this.group_send.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.num_bufferSize)).EndInit();
            this.group_receive.ResumeLayout(false);
            this.group_receive.PerformLayout();
            this.panel_trans.ResumeLayout(false);
            this.panel_trans.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pic_compress)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_download)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_upload)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_plot)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_progress)).EndInit();
            this.ResumeLayout(false);

		}

		// Token: 0x0400001A RID: 26
		private global::System.ComponentModel.IContainer components = null;

		// Token: 0x0400001B RID: 27
		private global::System.Windows.Forms.GroupBox group_send;

		// Token: 0x0400001C RID: 28
		private global::System.Windows.Forms.GroupBox group_receive;

		// Token: 0x0400001D RID: 29
		private global::System.Windows.Forms.Label label1;

		// Token: 0x0400001E RID: 30
		private global::System.Windows.Forms.Label label2;

		// Token: 0x0400001F RID: 31
		private global::System.Windows.Forms.Label label3;

		// Token: 0x04000020 RID: 32
		private global::System.Windows.Forms.Label label4;

		// Token: 0x04000021 RID: 33
		private global::System.Windows.Forms.TextBox txt_this_port;

		// Token: 0x04000022 RID: 34
		private global::System.Windows.Forms.TextBox txt_dist_port;

		// Token: 0x04000023 RID: 35
		private global::System.Windows.Forms.TextBox txt_dist_ip;

		// Token: 0x04000024 RID: 36
		private global::System.Windows.Forms.Label label5;

		// Token: 0x04000025 RID: 37
		private global::System.Windows.Forms.Label label6;

		// Token: 0x04000026 RID: 38
		private global::System.Windows.Forms.Button btn_send;

		// Token: 0x04000027 RID: 39
		private global::System.Windows.Forms.Label lab_fileName;

		// Token: 0x04000028 RID: 40
		private global::System.Windows.Forms.Label label7;

		// Token: 0x04000029 RID: 41
		private global::System.Windows.Forms.Button btn_receive;

		// Token: 0x0400002A RID: 42
		private global::System.Windows.Forms.TextBox txt_saveIn;

		// Token: 0x0400002B RID: 43
		private global::System.Windows.Forms.Label label9;

		// Token: 0x0400002C RID: 44
		private global::System.Windows.Forms.Button btn_selectFile;

		// Token: 0x0400002D RID: 45
		private global::System.Windows.Forms.Label label8;

		// Token: 0x0400002E RID: 46
		private global::System.Windows.Forms.Label label10;

		// Token: 0x0400002F RID: 47
		private global::System.Windows.Forms.PictureBox pic_progress;

		// Token: 0x04000030 RID: 48
		private global::System.Windows.Forms.Label lab_time;

		// Token: 0x04000031 RID: 49
		private global::System.Windows.Forms.Label lab_speed;

		// Token: 0x04000032 RID: 50
		private global::System.Windows.Forms.OpenFileDialog openFileDialog;

		// Token: 0x04000033 RID: 51
		private global::System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;

		// Token: 0x04000034 RID: 52
		private global::System.Windows.Forms.Button btn_selectDir;

		// Token: 0x04000035 RID: 53
		private global::System.Windows.Forms.ComboBox comb_this_ip;

		// Token: 0x04000036 RID: 54
		private global::System.Windows.Forms.Timer timer_status;

		// Token: 0x04000037 RID: 55
		private global::System.Windows.Forms.Label label11;

		// Token: 0x04000038 RID: 56
		private global::System.Windows.Forms.NumericUpDown num_bufferSize;

		// Token: 0x04000039 RID: 57
		private global::System.Windows.Forms.RadioButton rb_this;

		// Token: 0x0400003A RID: 58
		private global::System.Windows.Forms.RadioButton rb_dist;

		// Token: 0x0400003B RID: 59
		private global::System.Windows.Forms.PictureBox pic_plot;

		// Token: 0x0400003C RID: 60
		private global::System.Windows.Forms.Label lab_maxSpeed;

		// Token: 0x0400003D RID: 61
		private global::System.Windows.Forms.Label lab_minSpeed;
        private System.Windows.Forms.Panel panel_trans;
        private System.Windows.Forms.PictureBox pic_download;
        private System.Windows.Forms.PictureBox pic_upload;
        private System.Windows.Forms.Label lab_received;
        private System.Windows.Forms.Label lab_sent;
        private System.Windows.Forms.PictureBox pic_compress;
        private System.Windows.Forms.Label lab_compress;
        private System.Windows.Forms.ComboBox comb_compression;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
    }
}
