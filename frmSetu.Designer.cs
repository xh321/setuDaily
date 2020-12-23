namespace setuDaily
{
    partial class frmSetu
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.btnGKD = new System.Windows.Forms.Button();
            this.container = new System.Windows.Forms.Panel();
            this.picSetu = new System.Windows.Forms.PictureBox();
            this.cms = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.复制涩图ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.quickSave = new System.Windows.Forms.ToolStripMenuItem();
            this.保存涩图ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.设为壁纸ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.resetZoom = new System.Windows.Forms.ToolStripMenuItem();
            this.重新加载当前涩图ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.progress = new System.Windows.Forms.ProgressBar();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtProxy = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.comboSource = new System.Windows.Forms.ComboBox();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.comSetuType = new System.Windows.Forms.ComboBox();
            this.lblStatus = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnSetWall = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.comStyle = new System.Windows.Forms.ComboBox();
            this.sfd = new System.Windows.Forms.SaveFileDialog();
            this.container.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picSetu)).BeginInit();
            this.cms.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnGKD
            // 
            this.btnGKD.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnGKD.Font = new System.Drawing.Font("微软雅黑", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnGKD.Location = new System.Drawing.Point(0, 650);
            this.btnGKD.Name = "btnGKD";
            this.btnGKD.Size = new System.Drawing.Size(1112, 49);
            this.btnGKD.TabIndex = 1;
            this.btnGKD.Text = "再来1️🐙给👴👀👀";
            this.btnGKD.UseVisualStyleBackColor = true;
            this.btnGKD.Click += new System.EventHandler(this.btnGKD_Click);
            // 
            // container
            // 
            this.container.AutoScroll = true;
            this.container.Controls.Add(this.picSetu);
            this.container.Location = new System.Drawing.Point(6, 7);
            this.container.Name = "container";
            this.container.Size = new System.Drawing.Size(750, 444);
            this.container.TabIndex = 2;
            this.container.SizeChanged += new System.EventHandler(this.container_SizeChanged);
            // 
            // picSetu
            // 
            this.picSetu.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picSetu.ContextMenuStrip = this.cms;
            this.picSetu.Location = new System.Drawing.Point(6, 5);
            this.picSetu.Name = "picSetu";
            this.picSetu.Size = new System.Drawing.Size(729, 312);
            this.picSetu.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.picSetu.TabIndex = 1;
            this.picSetu.TabStop = false;
            this.picSetu.Tag = "IDLE";
            this.picSetu.LoadCompleted += new System.ComponentModel.AsyncCompletedEventHandler(this.picSetu_LoadCompleted);
            this.picSetu.LoadProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.picSetu_LoadProgressChanged);
            this.picSetu.SizeChanged += new System.EventHandler(this.picSetu_SizeChanged);
            this.picSetu.Click += new System.EventHandler(this.picSetu_Click);
            this.picSetu.MouseDown += new System.Windows.Forms.MouseEventHandler(this.picSetu_MouseDown);
            this.picSetu.MouseMove += new System.Windows.Forms.MouseEventHandler(this.picSetu_MouseMove);
            this.picSetu.MouseUp += new System.Windows.Forms.MouseEventHandler(this.picSetu_MouseUp);
            // 
            // cms
            // 
            this.cms.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.cms.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.复制涩图ToolStripMenuItem,
            this.toolStripMenuItem1,
            this.quickSave,
            this.保存涩图ToolStripMenuItem,
            this.设为壁纸ToolStripMenuItem,
            this.resetZoom,
            this.重新加载当前涩图ToolStripMenuItem});
            this.cms.Name = "cms";
            this.cms.Size = new System.Drawing.Size(252, 186);
            // 
            // 复制涩图ToolStripMenuItem
            // 
            this.复制涩图ToolStripMenuItem.Enabled = false;
            this.复制涩图ToolStripMenuItem.Name = "复制涩图ToolStripMenuItem";
            this.复制涩图ToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
            this.复制涩图ToolStripMenuItem.Size = new System.Drawing.Size(251, 26);
            this.复制涩图ToolStripMenuItem.Text = "复制涩图";
            this.复制涩图ToolStripMenuItem.Click += new System.EventHandler(this.复制涩图ToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Enabled = false;
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Alt) 
            | System.Windows.Forms.Keys.C)));
            this.toolStripMenuItem1.Size = new System.Drawing.Size(251, 26);
            this.toolStripMenuItem1.Text = "复制涩图链接";
            this.toolStripMenuItem1.Click += new System.EventHandler(this.toolStripMenuItem1_Click);
            // 
            // quickSave
            // 
            this.quickSave.Enabled = false;
            this.quickSave.Name = "quickSave";
            this.quickSave.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.quickSave.Size = new System.Drawing.Size(251, 26);
            this.quickSave.Text = "快速保存";
            this.quickSave.Click += new System.EventHandler(this.quickSave_Click);
            // 
            // 保存涩图ToolStripMenuItem
            // 
            this.保存涩图ToolStripMenuItem.Enabled = false;
            this.保存涩图ToolStripMenuItem.Name = "保存涩图ToolStripMenuItem";
            this.保存涩图ToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Alt) 
            | System.Windows.Forms.Keys.S)));
            this.保存涩图ToolStripMenuItem.Size = new System.Drawing.Size(251, 26);
            this.保存涩图ToolStripMenuItem.Text = "保存涩图";
            this.保存涩图ToolStripMenuItem.Click += new System.EventHandler(this.保存涩图ToolStripMenuItem_Click);
            // 
            // 设为壁纸ToolStripMenuItem
            // 
            this.设为壁纸ToolStripMenuItem.Enabled = false;
            this.设为壁纸ToolStripMenuItem.Name = "设为壁纸ToolStripMenuItem";
            this.设为壁纸ToolStripMenuItem.Size = new System.Drawing.Size(251, 26);
            this.设为壁纸ToolStripMenuItem.Text = "设为壁纸";
            this.设为壁纸ToolStripMenuItem.Click += new System.EventHandler(this.设为壁纸ToolStripMenuItem_Click);
            // 
            // resetZoom
            // 
            this.resetZoom.Enabled = false;
            this.resetZoom.Name = "resetZoom";
            this.resetZoom.Size = new System.Drawing.Size(251, 26);
            this.resetZoom.Text = "重置缩放比例";
            this.resetZoom.Click += new System.EventHandler(this.resetZoom_Click);
            // 
            // 重新加载当前涩图ToolStripMenuItem
            // 
            this.重新加载当前涩图ToolStripMenuItem.Enabled = false;
            this.重新加载当前涩图ToolStripMenuItem.Name = "重新加载当前涩图ToolStripMenuItem";
            this.重新加载当前涩图ToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F5;
            this.重新加载当前涩图ToolStripMenuItem.Size = new System.Drawing.Size(251, 26);
            this.重新加载当前涩图ToolStripMenuItem.Text = "重新加载当前涩图";
            this.重新加载当前涩图ToolStripMenuItem.Click += new System.EventHandler(this.重新加载当前涩图ToolStripMenuItem_Click);
            // 
            // progress
            // 
            this.progress.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.progress.Location = new System.Drawing.Point(0, 625);
            this.progress.Name = "progress";
            this.progress.Size = new System.Drawing.Size(1112, 25);
            this.progress.TabIndex = 3;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtProxy);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.comboSource);
            this.groupBox1.Controls.Add(this.button3);
            this.groupBox1.Controls.Add(this.button2);
            this.groupBox1.Controls.Add(this.comSetuType);
            this.groupBox1.Controls.Add(this.lblStatus);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.btnSave);
            this.groupBox1.Controls.Add(this.btnSetWall);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.comStyle);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox1.Location = new System.Drawing.Point(0, 487);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1112, 138);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "设置";
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(320, 102);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(216, 15);
            this.label3.TabIndex = 12;
            this.label3.Text = "给👴挂代理（写地址在后面）：";
            // 
            // txtProxy
            // 
            this.txtProxy.Location = new System.Drawing.Point(553, 99);
            this.txtProxy.Name = "txtProxy";
            this.txtProxy.Size = new System.Drawing.Size(547, 25);
            this.txtProxy.TabIndex = 11;
            this.txtProxy.TextChanged += new System.EventHandler(this.txtProxy_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 81);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 15);
            this.label2.TabIndex = 10;
            this.label2.Text = "色图源：";
            // 
            // comboSource
            // 
            this.comboSource.FormattingEnabled = true;
            this.comboSource.Items.AddRange(new object[] {
            "https://api.lolicon.app",
            "https://api.yukari.one"});
            this.comboSource.Location = new System.Drawing.Point(12, 99);
            this.comboSource.Name = "comboSource";
            this.comboSource.Size = new System.Drawing.Size(174, 23);
            this.comboSource.TabIndex = 9;
            this.comboSource.SelectedIndexChanged += new System.EventHandler(this.comboSource_SelectedIndexChanged);
            // 
            // button3
            // 
            this.button3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button3.Enabled = false;
            this.button3.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button3.Location = new System.Drawing.Point(553, 59);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(199, 37);
            this.button3.TabIndex = 8;
            this.button3.Text = "打开作者Pixiv主页";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button2.Enabled = false;
            this.button2.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button2.Location = new System.Drawing.Point(758, 59);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(159, 37);
            this.button2.TabIndex = 7;
            this.button2.Text = "复制涩图";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // comSetuType
            // 
            this.comSetuType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comSetuType.FormattingEnabled = true;
            this.comSetuType.Items.AddRange(new object[] {
            "健康涩图",
            "纯R18涩图"});
            this.comSetuType.Location = new System.Drawing.Point(12, 53);
            this.comSetuType.Name = "comSetuType";
            this.comSetuType.Size = new System.Drawing.Size(174, 23);
            this.comSetuType.TabIndex = 6;
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(192, 56);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(0, 15);
            this.lblStatus.TabIndex = 5;
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Enabled = false;
            this.button1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button1.Location = new System.Drawing.Point(923, 57);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(177, 37);
            this.button1.TabIndex = 4;
            this.button1.Text = "复制涩图链接";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Enabled = false;
            this.btnSave.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnSave.Location = new System.Drawing.Point(758, 16);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(159, 37);
            this.btnSave.TabIndex = 3;
            this.btnSave.Text = "给👴📦存";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnSetWall
            // 
            this.btnSetWall.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSetWall.Enabled = false;
            this.btnSetWall.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnSetWall.Location = new System.Drawing.Point(923, 16);
            this.btnSetWall.Name = "btnSetWall";
            this.btnSetWall.Size = new System.Drawing.Size(177, 37);
            this.btnSetWall.TabIndex = 2;
            this.btnSetWall.Text = "给👴🐍为🖊📄";
            this.btnSetWall.UseVisualStyleBackColor = true;
            this.btnSetWall.Click += new System.EventHandler(this.btnSetWall_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(192, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(442, 15);
            this.label1.TabIndex = 1;
            this.label1.Text = "如果出现图片跑偏，放到最大再缩小就可以了。建议窗口最大化。";
            this.label1.Visible = false;
            // 
            // comStyle
            // 
            this.comStyle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comStyle.FormattingEnabled = true;
            this.comStyle.Items.AddRange(new object[] {
            "滚轮缩放模式",
            "图片原始大小",
            "按比例缩放到屏幕大小"});
            this.comStyle.Location = new System.Drawing.Point(12, 24);
            this.comStyle.Name = "comStyle";
            this.comStyle.Size = new System.Drawing.Size(174, 23);
            this.comStyle.TabIndex = 0;
            this.comStyle.SelectedIndexChanged += new System.EventHandler(this.comStyle_SelectedIndexChanged);
            // 
            // sfd
            // 
            this.sfd.DefaultExt = "jpg";
            this.sfd.Filter = "jpg 图片文件|*jpg";
            // 
            // frmSetu
            // 
            this.AcceptButton = this.btnGKD;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1112, 699);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.progress);
            this.Controls.Add(this.container);
            this.Controls.Add(this.btnGKD);
            this.KeyPreview = true;
            this.Name = "frmSetu";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.frmSetu_Load);
            this.Resize += new System.EventHandler(this.frmSetu_Resize);
            this.container.ResumeLayout(false);
            this.container.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picSetu)).EndInit();
            this.cms.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnGKD;
        private System.Windows.Forms.Panel container;
        private System.Windows.Forms.PictureBox picSetu;
        private System.Windows.Forms.ProgressBar progress;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox comStyle;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnSetWall;
        private System.Windows.Forms.SaveFileDialog sfd;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ContextMenuStrip cms;
        private System.Windows.Forms.ToolStripMenuItem 复制涩图ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 保存涩图ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 设为壁纸ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem 重新加载当前涩图ToolStripMenuItem;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.ComboBox comSetuType;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.ToolStripMenuItem quickSave;
        private System.Windows.Forms.ToolStripMenuItem resetZoom;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboSource;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtProxy;
    }
}

