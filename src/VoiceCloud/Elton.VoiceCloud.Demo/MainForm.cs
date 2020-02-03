using Mavplus.VoiceCloud.ISR;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mavplus.VoiceCloud.Demo
{
    public partial class MainForm : Form
    {
        readonly AudioFileIsrEngine isr = null;
        readonly IsrConfig config = null;
        public MainForm()
        {
            InitializeComponent();

            this.config = new IsrConfig();
            config.ServerUrl = "dev.voicecloud.cn";
            config.ApplicationId = "518fcbd0";
            config.Timeout = 10000;
            isr = new AudioFileIsrEngine(config,
                "builtin:grammar/../search/location.abnf?language=zh-cn");

            btnAudioFile.Click += btnAudioFile_Click;
            InitLocalSR();
        }

        void btnAudioFile_Click(object sender, EventArgs e)
        {
            btnAudioFile.Enabled = false;
            try
            {
                txtInput.Text += isr.Recognize(txtAudioFile.Text);
            }
            catch(Exception ex)
            {
                MessageBox.Show(this, "识别出错：" + ex.Message, "提示");
            }
            finally
            {
                btnAudioFile.Enabled = true;
            }
        }

        LocalAudioIsrEngine localSR = null;
        void InitLocalSR()
        {
            this.localSR = new LocalAudioIsrEngine(this, this.config,
                "builtin:grammar/../search/location.abnf?language=zh-cn");
            this.localSR.ResultArrived += new EventHandler<ResultArrivedEventArgs>(localSR_DataArrived);
            this.localSR.RecognizeCompleted += new EventHandler<RecognizeCompletedEventArgs>(localSR_RecognizeCompleted);

            this.btnLocalSR.Click += new EventHandler(btnLocalSR_Click);
        }


        void localSR_DataArrived(object sender, ResultArrivedEventArgs e)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new EventHandler<ResultArrivedEventArgs>(localSR_DataArrived), sender, e);
                return;
            }
            txtInput.AppendText(e.Text);
        }

        void localSR_RecognizeCompleted(object sender, RecognizeCompletedEventArgs e)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new EventHandler<RecognizeCompletedEventArgs>(localSR_RecognizeCompleted), sender, e);
                return;
            }

            btnLocalSR.Text = localSR.Recognizing ? "停止录音" : "开始录音";
        }


        void btnLocalSR_Click(object sender, EventArgs e)
        {
            if (localSR.Recognizing)
                localSR.CancelAsync();
            else
                localSR.RecognizeAsync(null);

            btnLocalSR.Text = localSR.Recognizing ? "停止录音" : "开始录音";
        }


        NetworkAudioIsrEngine networkSR = null;
        void InitNetworkSR()
        {
            this.networkSR = new NetworkAudioIsrEngine(this.config,
                "builtin:grammar/../search/location.abnf?language=zh-cn");
            this.networkSR.ResultArrived += new EventHandler<ResultArrivedEventArgs>(networkSR_ResultArrived);
            this.networkSR.RecognizeCompleted += new EventHandler<RecognizeCompletedEventArgs>(networkSR_RecognizeCompleted);

            this.btnNetworkSR.Click += new EventHandler(btnNetworkSR_Click);
        }

        void networkSR_ResultArrived(object sender, ResultArrivedEventArgs e)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new EventHandler<ResultArrivedEventArgs>(networkSR_ResultArrived), sender, e);
                return;
            }
            txtInput.AppendText(e.Text);
        }

        void networkSR_RecognizeCompleted(object sender, RecognizeCompletedEventArgs e)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new EventHandler<RecognizeCompletedEventArgs>(networkSR_RecognizeCompleted), sender, e);
                return;
            }

            btnNetworkSR.Text = networkSR.Recognizing ? "停止录音" : "开始录音";
        }


        void btnNetworkSR_Click(object sender, EventArgs e)
        {
            if (networkSR.Recognizing)
                networkSR.CancelAsync();
            else
            {
                string httpAudioUrl = txtAudioUrl.Text.Trim();
                networkSR.RecognizeAsync(httpAudioUrl);
            }

            btnNetworkSR.Text = networkSR.Recognizing ? "停止录音" : "开始录音";
        }
    }
}
