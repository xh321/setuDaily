using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Runtime.InteropServices;
using System.Threading;
//调用WINDOWS API函数时要用到
using Microsoft.Win32; //写入注册表时要用到


namespace setuDaily
{
    public partial class frmSetu : Form
    {
        const int SPI_SETDESKWALLPAPER  = 20;
        const int SPIF_UPDATEINIFILE    = 0x01;
        const int SPIF_SENDWININICHANGE = 0x02;

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        static extern int SystemParametersInfo(int uAction, int uParam, string lpvParam, int fuWinIni);

        public enum Style : int
        {
            Fill,
            Fit,
            Span,
            Stretch,
            Tile,
            Center
        }

        public setuInfo currSetuInfo;
        public string currTitle="";
        public frmSetu()
        {
            InitializeComponent();
        }

        public static Image addEXIF(Image image, string title, string author, string tags, string tips)
        {
            //              标题  作者   备注    标记    主题
            int[]    id   = {270, 315, 0x9C9C, 0x9C9E, 0x9C9F};
            short[]  type = {2, 2, 1, 1, 1};
            string[] str  = {title, author, tips, tags, ""};
            for (int iid = 0; iid < (Math.Min(image.PropertyItems.Length, 4)); iid++)
            {
                string comments = str[iid];
                int    index    = 0;

                PropertyItem pi = image.PropertyItems[iid];
                byte[]       s;


                int length = 0;
                if (type[iid] == 2)
                {
                    length = Encoding.UTF8.GetBytes(comments).Length;
                    s      = new byte[length + 1];
                    for (int i = 0; i < length; i++)
                    {
                        s[i] = Encoding.UTF8.GetBytes(comments)[i];
                    }

                    s[length] = 0;
                }
                else
                {
                    length = Encoding.Unicode.GetBytes(comments).Length;
                    s      = new byte[length];
                    for (int i = 0; i < length; i++)
                    {
                        s[i] = Encoding.Unicode.GetBytes(comments)[i];
                    }
                }

                pi.Id    = id[iid]; //想要修改的tag的id,如果该id没有定义则添加一个新的tag
                pi.Type  = type[iid];
                pi.Value = s;
                pi.Len   = pi.Value.Length;
                image.SetPropertyItem(pi);
            }

            return image;
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            // 按快捷键Ctrl+S执行按钮的点击事件方法
            if (keyData == (Keys) Shortcut.CtrlC)
            {
                复制涩图ToolStripMenuItem.PerformClick();
                return true;
            }
            else if (keyData == (Keys) Shortcut.CtrlS)
            {
                quickSave.PerformClick();
                return true;
            }
            else if (keyData == (Keys) Shortcut.F5)
            {
                重新加载当前涩图ToolStripMenuItem.PerformClick();
                return true;
            }
            else if (keyData == (Keys) Keys.Enter)
            {
                btnGKD.PerformClick();
                return true;
            }

            return base.ProcessCmdKey(ref msg, keyData); // 其他键按默认处理　
        }

        private void frmSetu_Resize(object sender, EventArgs e)
        {
            reSetSize((frmSetu) sender);
            container.Width = this.Width - 25;
            container.Height =
                ((frmSetu) sender).Height - btnGKD.Height - progress.Height - groupBox1.Height - 50;
            picSetu.Width  = this.Width       - 30;
            picSetu.Height = container.Height - 50;
        }

        private void reSetSize(frmSetu frm)
        {
            //picSetu.Width    = frm.Width                  - 20;
            //picSetu.Height   = frm.Height - btnGKD.Height - 50;
            container.Width  = frm.Width                                                       - 25;
            container.Height = frm.Height - btnGKD.Height - progress.Height - groupBox1.Height - 50;
        }

        private void reSetSizeByImage(PictureBox pic)
        {
            if ((this.Width  > pic.Width                                                       + 50) &&
                (this.Height > pic.Height + btnGKD.Height + progress.Height + groupBox1.Height + 55))
            {
                return;
            }

            if (!(comStyle.Text == "滚轮缩放模式") && (this.Width < pic.Width + 50))
            {
                this.Width = pic.Width + 50;
            }

            if ((this.Height < pic.Height + btnGKD.Height + progress.Height + groupBox1.Height + 55))
            {
                this.Height = pic.Height + btnGKD.Height + progress.Height + groupBox1.Height + 55;
            }

            Rectangle ScreenArea = System.Windows.Forms.Screen.GetWorkingArea(this);
            Point     to         = new Point();
            if (this.Width >= ScreenArea.Width || this.Height >= ScreenArea.Height)
            {
                WindowState = FormWindowState.Maximized;
            }
            else
            {
                if (ScreenArea.Width - this.Left < this.Width)
                {
                    to.X     = this.Left - (this.Width - ScreenArea.Width + this.Left);
                    to.Y     = this.Top;
                    Location = to;
                }

                if (ScreenArea.Height - this.Top < this.Height)
                {
                    to.X     = this.Left;
                    to.Y     = this.Top - (this.Height - ScreenArea.Height + this.Top);
                    Location = to;
                }
            }
        }

        private void frmSetu_Load(object sender, EventArgs e)
        {
            Control.CheckForIllegalCrossThreadCalls = false;
            //reSetSize((frmSetu) sender);
            container.Width = this.Width;
            container.Height =
                ((frmSetu) sender).Height - btnGKD.Height - progress.Height - groupBox1.Height - 50;
            picSetu.Width             =  this.Width       - 30;
            picSetu.Height            =  container.Height - 50;
            comStyle.SelectedIndex    =  0;
            comSetuType.SelectedIndex =  1;
            picSetu.MouseWheel        += new System.Windows.Forms.MouseEventHandler(this.picSetu_MouseWheel);
//            SyncCallBack(loadData());
            //异步调取涩图
            fh = new FuncHandle(this.loadData);
            AsyncCallback callback = new AsyncCallback(this.AsyncCallbackImpl);
            fh.BeginInvoke(callback, null);

            frmSetu_Resize(this, null);
        }

        //picSetu鼠标滚轮事件
        private void picSetu_MouseWheel(object sender, MouseEventArgs e)
        {
            if (picSetu.Image == null) return;
            if (comStyle.Text == "图片原始大小") return;
            comStyle.Enabled = false;
            //计算缩放后的锚点和宽高
            int i     = e.Delta           * SystemInformation.MouseWheelScrollLines / 4;
            int left  = picSetu.Left  - i / 2, top    = picSetu.Top    - i / 2;
            int width = picSetu.Width + i,     heigth = picSetu.Height + i;

            if (i < 0) //缩小时需要考虑与显示范围间关系，放大时无需考虑
            {
                //计算缩放后图片有效范围
                double WidthScale  = Convert.ToDouble(picSetu.Image.Width)  / width;
                double HeigthScale = Convert.ToDouble(picSetu.Image.Height) / heigth;
                if (WidthScale > HeigthScale)
                {
                    top    = top + Convert.ToInt32(Math.Ceiling(heigth - (picSetu.Image.Height / WidthScale))) / 2;
                    heigth = Convert.ToInt32(Math.Ceiling(picSetu.Image.Height / WidthScale));
                }
                else
                {
                    left  = left + Convert.ToInt32(Math.Ceiling(width - (picSetu.Image.Width / HeigthScale))) / 2;
                    width = Convert.ToInt32(Math.Ceiling(picSetu.Image.Width / HeigthScale));
                }

                if (left > 0) //左侧在显示范围内部，调整到左边界
                {
                    if (width - left < container.Width) width = container.Width;
                    else width                                = width - left;
                    left = 0;
                }

                if (left + width < container.Width) //右侧在显示范围内部，调整到右边界
                {
                    if (container.Width - width > 0) left = 0;
                    else left                             = container.Width - width;
                    width = container.Width - left;
                }

                if (top > 0) //上侧在显示范围内部，调整到上边界
                {
                    if (heigth - top < container.Height) heigth = container.Height;
                    else heigth                                 = heigth - top;
                    top = 0;
                }

                if (top + heigth < container.Height) //下侧在显示范围内部，调整到下边界
                {
                    if (container.Height - heigth > 0) top = 0;
                    else top                               = container.Height - heigth;
                    heigth = container.Height - top;
                }
            }

            picSetu.Width  = width;
            picSetu.Height = heigth;
            picSetu.Left   = left;
            picSetu.Top    = top;
        }

        public void SyncCallBack(setuInfo re)
        {
            picSetu.LoadAsync(re.data[0].url);

            reSetSizeByImage(picSetu);
            container.Width  = this.Width                                                       - 25;
            container.Height = this.Height - btnGKD.Height - progress.Height - groupBox1.Height - 50;
        }

        public void AsyncCallbackImpl(IAsyncResult ar)
        {
            setuInfo re = fh.EndInvoke(ar);
            if (re != null)
            {
                this.Text = re.data[0].title + " by " + re.data[0].author + "(" + re.data[0].uid + ") | tags: ";
                foreach (string s in re.data[0].tags)
                {
                    this.Text = this.Text + s + "  ";
                }

                currTitle = this.Text;
                currSetuInfo = re;
                lblStatus.Text = "色图原始分辨率：" + re.data[0].width + "X" + re.data[0].height + "；是否R-18：" +
                                 (re.data[0].r18 == "true" ? "是" : "否");
                btnGKD.Enabled = true;
                btnGKD.Text    = "太慢🌶，给👴停下，下一张";
                picSetu.LoadAsync(re.data[0].url);
                comStyle.Enabled = true;
            }
            else
            {
                comStyle.Enabled = true;
                btnGKD.Enabled   = true;
            }
        }

        public delegate setuInfo FuncHandle();

        FuncHandle fh;

        private setuInfo loadData()
        {
            string r18type = "";
            this.Text = "每日涩图 - 加载信息中...";
            switch (comSetuType.Text)
            {
                case "健康涩图":
                    r18type = "0";
                    break;
                case "R18-健康混合":
                    r18type = "2";
                    break;
                case "纯R18涩图":
                    r18type = "1";
                    break;
            }

            string ret = HttpGet("http://api.lolicon.app/setu?r18=" + r18type);
            if (ret != "")
            {
                setuInfo rt = JsonConvert.DeserializeObject<setuInfo>(ret);
                return rt;
            }
            else
            {
                return null;
            }
        }

        private void picSetu_Resize(object sender, EventArgs e)
        {
        }

        private void btnGKD_Click(object sender, EventArgs e)
        {
            if (btnGKD.Text == "太慢🌶，给👴停下，下一张")
            {
                btnGKD.Text = "再来1️🐙给👴👀👀";
                picSetu.CancelAsync();
            }

            btnGKD.Enabled = false;
            fh             = new FuncHandle(this.loadData);
            AsyncCallback callback = new AsyncCallback(this.AsyncCallbackImpl);
            fh.BeginInvoke(callback, null);
        }

        public static bool CheckValidationResult(object                   sender,
            System.Security.Cryptography.X509Certificates.X509Certificate certificate, X509Chain chain,
            SslPolicyErrors                                               errors)
        {
            return true;
        }

        public string HttpGet(string url, string post_parament = "")
        {
            string         html;
            HttpWebRequest Web_Request = (HttpWebRequest) WebRequest.Create(url);
            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            Web_Request.Timeout                             = 30000;
            Web_Request.Method                              = "GET";
            Web_Request.UserAgent                           = "Mozilla/4.0";
            Web_Request.Headers.Add("Accept-Encoding", "gzip, deflate");
            Web_Request.ServerCertificateValidationCallback =
                new RemoteCertificateValidationCallback(CheckValidationResult);
            //Web_Request.Credentials = CredentialCache.DefaultCredentials;

            //设置代理属性WebProxy-------------------------------------------------
            //WebProxy proxy = new WebProxy("111.13.7.120", 80);
            //在发起HTTP请求前将proxy赋值给HttpWebRequest的Proxy属性
            //Web_Request.Proxy = proxy;
            this.Text = "每日涩图 - 获取返回中...";
            HttpWebResponse Web_Response = null;
            try
            {
                Web_Response = (HttpWebResponse) Web_Request.GetResponse();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "报错了", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return "";
            }

            if (Web_Response.ContentEncoding.ToLower() == "gzip") // 如果使用了GZip则先解压
            {
                using (Stream Stream_Receive = Web_Response.GetResponseStream())
                {
                    using (var Zip_Stream = new GZipStream(Stream_Receive, CompressionMode.Decompress))
                    {
                        using (StreamReader Stream_Reader = new StreamReader(Zip_Stream, Encoding.UTF8))
                        {
                            html      = Stream_Reader.ReadToEnd();
                            this.Text = "每日涩图 - 加载信息中...";
                        }
                    }
                }
            }
            else
            {
                using (Stream Stream_Receive = Web_Response.GetResponseStream())
                {
                    using (StreamReader Stream_Reader = new StreamReader(Stream_Receive, Encoding.UTF8))
                    {
                        html = Stream_Reader.ReadToEnd();
                    }
                }
            }

            return html;
        }

        private void picSetu_LoadCompleted(object sender, AsyncCompletedEventArgs e)
        {
            this.Text   = currTitle;
            picSetu.Tag = "IDLE";
            if (e.Cancelled)
            {
                return;
            }

            btnGKD.Text     = "再来1️🐙给👴👀👀";
            container.Width = this.Width - 25;
            container.Height =
                this.Height - btnGKD.Height - progress.Height - groupBox1.Height - 50;
            picSetu.Width  = this.Width       - 30;
            picSetu.Height = container.Height - 50;
            container.Left = 0;
            container.Top  = 0;
            picSetu.Top    = 5;
            picSetu.Left   = 5;
            reSetSizeByImage(picSetu);
            container.Width                   = this.Width                                                       - 25;
            container.Height                  = this.Height - btnGKD.Height - progress.Height - groupBox1.Height - 50;
            btnSave.Enabled                   = true;
            btnSetWall.Enabled                = true;
            button1.Enabled                   = true;
            button2.Enabled                   = true;
            button3.Enabled                   = true;
            btnGKD.Enabled                    = true;
            quickSave.Enabled                 = true;
            重新加载当前涩图ToolStripMenuItem.Enabled = true;
            保存涩图ToolStripMenuItem.Enabled     = true;
            复制涩图ToolStripMenuItem.Enabled     = true;
            设为壁纸ToolStripMenuItem.Enabled     = true;
            if (comStyle.Text == "滚轮缩放模式")
                resetZoom.Enabled = true;
            else
                resetZoom.Enabled = false;
            toolStripMenuItem1.Enabled = true;
        }

        private void picSetu_LoadProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progress.Value = e.ProgressPercentage;
            picSetu.Tag    = "LOADING";
            this.Text      = "[" + e.ProgressPercentage + "%]" + currTitle;
        }

        private void comStyle_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comStyle.Text == "图片原始大小")
            {
                picSetu.SizeMode = PictureBoxSizeMode.AutoSize;
                label1.Visible   = false;
            }
            else if (comStyle.Text == "按比例缩放到屏幕大小")
            {
                WindowState      = FormWindowState.Maximized;
                picSetu.SizeMode = PictureBoxSizeMode.Zoom;

                container.Width  = this.Width                                                       - 25;
                container.Height = this.Height - btnGKD.Height - progress.Height - groupBox1.Height - 50;

                picSetu.Height = container.Height - 15;
                picSetu.Width  = container.Width  - 15;
            }
            else
            {
                picSetu.SizeMode = PictureBoxSizeMode.Zoom;
                container.Width  = this.Width;
                container.Height = this.Height - btnGKD.Height - progress.Height - groupBox1.Height - 50;
                label1.Visible   = true;
                picSetu.Height   = container.Height - 15;
                picSetu.Width    = container.Width  - 15;
            }
        }

        public Point   MouseDownPoint;
        public Boolean IsSelected;

        private void picSetu_MouseDown(object sender, MouseEventArgs e)
        {
            if (picSetu.Image == null) return;
            if (comStyle.Text == "图片原始大小") return;
            if (e.Button == MouseButtons.Left)
            {
                //记录摁下点坐标，作为平移原点
                MouseDownPoint.X = PointToClient(System.Windows.Forms.Cursor.Position).X;
                MouseDownPoint.Y = PointToClient(System.Windows.Forms.Cursor.Position).Y;
                IsSelected       = true;
                picSetu.Cursor   = Cursors.Hand;
            }
        }

        private void picSetu_MouseMove(object sender, MouseEventArgs e)
        {
            if (picSetu.Image == null) return;
            if (comStyle.Text == "图片原始大小") return;
            //计算图片有效范围
            double WidthScale  = Convert.ToDouble(picSetu.Image.Width)  / picSetu.Width;
            double HeigthScale = Convert.ToDouble(picSetu.Image.Height) / picSetu.Height;
            int InvalidTop    = picSetu.Top,
                InvalidHeigth = picSetu.Height,
                InvalidLeft   = picSetu.Left,
                InvalidWidth  = picSetu.Width;
            if (WidthScale > HeigthScale)
            {
                InvalidTop = InvalidTop +
                             ((int) Math.Ceiling(InvalidHeigth - (picSetu.Image.Height / WidthScale))) / 2;
                InvalidHeigth = (int) Math.Ceiling(picSetu.Image.Height / WidthScale);
            }
            else
            {
                InvalidLeft = InvalidLeft +
                              ((int) Math.Ceiling(InvalidWidth - (picSetu.Image.Width / HeigthScale))) / 2;
                InvalidWidth = (int) Math.Ceiling(picSetu.Image.Width / HeigthScale);
            }

            //鼠标是否摁在图片上
            bool IsMouseInPanel = InvalidLeft <
                                  PointToClient(System.Windows.Forms.Cursor.Position).X &&
                                  PointToClient(System.Windows.Forms.Cursor.Position).X <
                                  InvalidLeft + InvalidWidth &&
                                  InvalidTop <
                                  PointToClient(System.Windows.Forms.Cursor.Position).Y &&
                                  PointToClient(System.Windows.Forms.Cursor.Position).Y < InvalidTop + InvalidHeigth;
            if (IsSelected && IsMouseInPanel)
            {
                //计算平移后图片有效范围的锚点和宽高
                int left  = InvalidLeft + (PointToClient(System.Windows.Forms.Cursor.Position).X - MouseDownPoint.X);
                int top   = InvalidTop  + (PointToClient(System.Windows.Forms.Cursor.Position).Y - MouseDownPoint.Y);
                int right = left        + InvalidWidth;
                int down  = top         + InvalidHeigth;

                if (left >= InvalidLeft && left >= 0) left = 0; //向右平移且平移后在显示范围内部，调整到左边界
                if (left < InvalidLeft && right <= container.Width)
                    left = left + container.Width - right;  //向左平移且平移后在显示范围内部，调整到右边界
                if (top >= InvalidTop && top >= 0) top = 0; //向下平移且平移后在显示范围内部，调整到上边界
                if (top < InvalidTop && down <= container.Height)
                    top = top + container.Height - down; //向上平移且平移后在显示范围内部，调整到下  边界

                //有效范围锚点换算到整体的锚点
                left = left + picSetu.Left - InvalidLeft;
                top  = top  + picSetu.Top  - InvalidTop;

                if (InvalidLeft <= 0) picSetu.Left = left;
                if (InvalidTop  <= 0) picSetu.Top  = top;

                //记录当前平移点坐标，作为平移下一次代码执行时的平移原点
                MouseDownPoint.X = PointToClient(System.Windows.Forms.Cursor.Position).X;
                MouseDownPoint.Y = PointToClient(System.Windows.Forms.Cursor.Position).Y;
            }
        }

        private void picSetu_MouseUp(object sender, MouseEventArgs e)
        {
            if (picSetu.Image == null) return;
            if (comStyle.Text == "图片原始大小") return;
            IsSelected     = false;
            picSetu.Cursor = Cursors.SizeAll;
        }

        private void picSetu_SizeChanged(object sender, EventArgs e)
        {
        }

        private void container_SizeChanged(object sender, EventArgs e)
        {
            container.Top = 0;
        }

        ImageCodecInfo GetEncoderInfo(String mimeType)

        {
            int              j;
            ImageCodecInfo[] encoders;
            encoders = ImageCodecInfo.GetImageEncoders();
            for (j = 0; j < encoders.Length; ++j)
            {
                if (encoders[j].MimeType == mimeType)
                    return encoders[j];
            }

            return null;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (picSetu.Image == null) return;


            if (MessageBox.Show("是否保存原图？\r\n如果不保存原图可以同时内嵌图片的标题，tags,作者等信息，但可能会有一定的压缩。", "每日色图", MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question) == DialogResult.Yes)
            {
                sfd.FileName = currSetuInfo.data[0].title + ".png";
                sfd.ShowDialog();
                if (sfd.FileName != "")
                {
                    picSetu.Image.Save(sfd.FileName);
                }
            }
            else
            {
                sfd.FileName = currSetuInfo.data[0].title + ".jpg";
                sfd.ShowDialog();
                if (sfd.FileName != "")
                {
                    String artworkUrl = "https://www.pixiv.net/artworks/" + currSetuInfo.data[0].pid;
                    String IllUrl = "https://www.pixiv.net/users/" + +currSetuInfo.data[0].uid;
                    String catUrl = currSetuInfo.data[0].url;
                    String tips = "画师主页：" + IllUrl + "\r\n作品页面：" + artworkUrl + "\r\n墙内链接：" + catUrl;
                    ImageCodecInfo                 jpgEncoder = GetEncoderInfo("image/jpeg");
                    System.Drawing.Imaging.Encoder myEncoder;
                    EncoderParameter               myEncoderParameter;
                    EncoderParameters              myEncoderParameters;
                    // for the Quality parameter category.
                    myEncoder = System.Drawing.Imaging.Encoder.Quality;
                    // EncoderParameter object in the array.
                    myEncoderParameters = new EncoderParameters(1);
                    //设置质量 数字越大质量越好，但是到了一定程度质量就不会增加了，MSDN上没有给范围，只说是32为非负整数
                    myEncoderParameter           = new EncoderParameter(myEncoder, 100L);
                    myEncoderParameters.Param[0] = myEncoderParameter;
                    MemoryStream before = new MemoryStream();
                    picSetu.Image.Save(before, jpgEncoder, myEncoderParameters);
                    Image tmp = Image.FromStream(before);
                    addEXIF(tmp, currSetuInfo.data[0].title, currSetuInfo.data[0].author,
                        String.Join(";", currSetuInfo.data[0].tags.ToArray()), tips).Save(sfd.FileName);
                }
            }


            MessageBox.Show("保存成功", "每日色图", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnSetWall_Click(object sender, EventArgs e)
        {
            if (picSetu.Image == null) return;
            sfd.Title = "请选择一个固定位置保存壁纸文件";
            sfd.ShowDialog();
            if (sfd.FileName != "")
            {
                picSetu.Image.Save(sfd.FileName);
                String       strSavePath = sfd.FileName;
                Style        style       = Style.Fit;
                DialogResult rt1         = MessageBox.Show("是否要以填充方式显示？", "壁纸显示方式", MessageBoxButtons.YesNoCancel);
                if (rt1 == DialogResult.Yes)
                {
                    style = Style.Fill;
                }
                else if (rt1 == DialogResult.No)
                {
                    DialogResult rt2 = MessageBox.Show("是否要以适应方式显示？", "壁纸显示方式", MessageBoxButtons.YesNoCancel);
                    if (rt2 == DialogResult.Yes)
                    {
                        style = Style.Fit;
                    }
                    else if (rt2 == DialogResult.No)
                    {
                        DialogResult rt3 = MessageBox.Show("是否要以拉伸方式显示？", "壁纸显示方式", MessageBoxButtons.YesNoCancel);
                        if (rt1 == DialogResult.Yes)
                        {
                            style = Style.Stretch;
                        }
                        else if (rt1 == DialogResult.No)
                        {
                            return;
                        }
                        else
                        {
                            return;
                        }
                    }
                    else
                    {
                        return;
                    }
                }
                else
                {
                    return;
                }

                using (RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Control Panel\Desktop", true))
                {
                    if (style == Style.Fill)
                    {
                        key.SetValue(@"WallpaperStyle", 10.ToString());
                        key.SetValue(@"TileWallpaper", 0.ToString());
                    }

                    if (style == Style.Fit)
                    {
                        key.SetValue(@"WallpaperStyle", 6.ToString());
                        key.SetValue(@"TileWallpaper", 0.ToString());
                    }

                    if (style == Style.Span) // Windows 8 or newer only!
                    {
                        key.SetValue(@"WallpaperStyle", 22.ToString());
                        key.SetValue(@"TileWallpaper", 0.ToString());
                    }

                    if (style == Style.Stretch)
                    {
                        key.SetValue(@"WallpaperStyle", 2.ToString());
                        key.SetValue(@"TileWallpaper", 0.ToString());
                    }

                    if (style == Style.Tile)
                    {
                        key.SetValue(@"WallpaperStyle", 0.ToString());
                        key.SetValue(@"TileWallpaper", 1.ToString());
                    }

                    if (style == Style.Center)
                    {
                        key.SetValue(@"WallpaperStyle", 0.ToString());
                        key.SetValue(@"TileWallpaper", 0.ToString());
                    }
                }

                SystemParametersInfo(SPI_SETDESKWALLPAPER,
                    0,
                    strSavePath,
                    SPIF_UPDATEINIFILE | SPIF_SENDWININICHANGE);
                MessageBox.Show("设置完毕", "每日色图", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                Clipboard.SetDataObject(currSetuInfo.data[0].url);
            }
            catch (Exception eX)
            {
                MessageBox.Show("有错误发生！\n" + eX.Message, "每日色图", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void 复制涩图ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (picSetu.Image == null) return;
                Clipboard.SetDataObject(picSetu.Image);
            }
            catch (Exception eX)
            {
                MessageBox.Show("有错误发生！\n" + eX.Message, "每日色图", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void 保存涩图ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (picSetu.Image == null) return;
                btnSave_Click(null, null);
            }
            catch (Exception eX)
            {
                MessageBox.Show("有错误发生！\n" + eX.Message, "每日色图", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void 设为壁纸ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (picSetu.Image == null) return;
                btnSetWall_Click(null, null);
            }
            catch (Exception eX)
            {
                MessageBox.Show("有错误发生！\n" + eX.Message, "每日色图", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            try
            {
                if (picSetu.Image == null) return;
                Clipboard.SetDataObject(currSetuInfo.data[0].url);
            }
            catch (Exception eX)
            {
                MessageBox.Show("有错误发生！\n" + eX.Message, "每日色图", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void 重新加载当前涩图ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (currSetuInfo.data[0].url != "")
                {
                    picSetu.CancelAsync();
                    while (picSetu.Tag != "IDLE")
                    {
                    }

                    picSetu.LoadAsync(currSetuInfo.data[0].url);
                }
            }
            catch (Exception eX)
            {
                MessageBox.Show("有错误发生！\n" + eX.Message, "每日色图", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (picSetu.Image == null) return;
                Clipboard.SetDataObject(picSetu.Image);
            }
            catch (Exception eX)
            {
                MessageBox.Show("有错误发生！\n" + eX.Message, "每日色图", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string getAppDir()
        {
            return Environment.CurrentDirectory.EndsWith("\\")
                ? Environment.CurrentDirectory
                : Environment.CurrentDirectory + "\\";
        }

        public static long GetTimeStamp()
        {
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt64(ts.TotalSeconds * 1000);
        }

        private void quickSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (picSetu.Image == null) return;
                if (!Directory.Exists(getAppDir()         + "saves"))
                    Directory.CreateDirectory(getAppDir() + "saves");

                String artworkUrl =
                    "https://www.pixiv.net/artworks/" + currSetuInfo.data[0].pid;
                String IllUrl =
                    "https://www.pixiv.net/users/" + +currSetuInfo.data[0].uid;
                String catUrl = currSetuInfo.data[0].url;
                String tips =
                    "画师主页：" + IllUrl + "\r\n作品页面：" + artworkUrl + "\r\n墙内链接：" + catUrl;
                ImageCodecInfo                 jpgEncoder = GetEncoderInfo("image/jpeg");
                System.Drawing.Imaging.Encoder myEncoder;
                EncoderParameter               myEncoderParameter;
                EncoderParameters              myEncoderParameters;
                // for the Quality parameter category.
                myEncoder = System.Drawing.Imaging.Encoder.Quality;
                // EncoderParameter object in the array.
                myEncoderParameters = new EncoderParameters(1);
                //设置质量 数字越大质量越好，但是到了一定程度质量就不会增加了，MSDN上没有给范围，只说是32为非负整数
                myEncoderParameter           = new EncoderParameter(myEncoder, 100L);
                myEncoderParameters.Param[0] = myEncoderParameter;
                MemoryStream before = new MemoryStream();
                picSetu.Image.Save(before, jpgEncoder, myEncoderParameters);
                Image tmp = Image.FromStream(before);
                addEXIF(tmp, currSetuInfo.data[0].title, currSetuInfo.data[0].author,
                    String.Join(";", currSetuInfo.data[0].tags.ToArray()), tips).Save(
                    getAppDir() + "saves\\" + currSetuInfo.data[0].title + "@" + currSetuInfo.data[0].author +
                    ".jpg");
            }
            catch (Exception eX)
            {
                MessageBox.Show("有错误发生！\n" + eX.Message, "每日色图", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void picSetu_Click(object sender, EventArgs e)
        {
        }

        private void resetZoom_Click(object sender, EventArgs e)
        {
            try
            {
                if (picSetu.Image == null) return;
                container.Width = this.Width - 25;
                container.Height =
                    this.Height - btnGKD.Height - progress.Height - groupBox1.Height - 50;
                picSetu.Width  = this.Width       - 30;
                picSetu.Height = container.Height - 50;
                container.Left = 0;
                container.Top  = 0;
                picSetu.Top    = 5;
                picSetu.Left   = 5;
                reSetSizeByImage(picSetu);
                container.Width  = this.Width                                                       - 25;
                container.Height = this.Height - btnGKD.Height - progress.Height - groupBox1.Height - 50;
            }
            catch (Exception eX)
            {
                MessageBox.Show("有错误发生！\n" + eX.Message, "每日色图", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (picSetu.Image == null) return;
            //http://touch.pixiv.net/member_illust.php?id=1015668
            System.Diagnostics.Process.Start(
                "http://touch.pixiv.net/member_illust.php?id=" + currSetuInfo.data[0].uid);
        }
    }

    public class setuItem
    {
        /// <summary>
        /// 
        /// </summary>
        public int pid { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int p { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int uid { get; set; }

        /// <summary>
        /// 伊19
        /// </summary>
        public string title { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string author { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string url { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string r18 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int width { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int height { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public List<string> tags { get; set; }
    }

    public class setuInfo
    {
        /// <summary>
        /// 
        /// </summary>
        public int code { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string msg { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int count { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public List<setuItem> data { get; set; }
    }
}