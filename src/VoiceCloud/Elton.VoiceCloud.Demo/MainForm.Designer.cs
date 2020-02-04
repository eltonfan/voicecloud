namespace Elton.VoiceCloud.Demo
{
    partial class MainForm
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
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.btnLocalSR = new System.Windows.Forms.Button();
            this.txtInput = new System.Windows.Forms.TextBox();
            this.btnNetworkSR = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtAudioUrl = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnAudioFile = new System.Windows.Forms.Button();
            this.txtAudioFile = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnLocalSR
            // 
            this.btnLocalSR.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLocalSR.Location = new System.Drawing.Point(22, 20);
            this.btnLocalSR.Name = "btnLocalSR";
            this.btnLocalSR.Size = new System.Drawing.Size(75, 36);
            this.btnLocalSR.TabIndex = 20;
            this.btnLocalSR.Text = "开始录音";
            this.btnLocalSR.UseVisualStyleBackColor = true;
            // 
            // txtInput
            // 
            this.txtInput.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtInput.Location = new System.Drawing.Point(12, 12);
            this.txtInput.Multiline = true;
            this.txtInput.Name = "txtInput";
            this.txtInput.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtInput.Size = new System.Drawing.Size(357, 280);
            this.txtInput.TabIndex = 22;
            // 
            // btnNetworkSR
            // 
            this.btnNetworkSR.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnNetworkSR.Location = new System.Drawing.Point(22, 47);
            this.btnNetworkSR.Name = "btnNetworkSR";
            this.btnNetworkSR.Size = new System.Drawing.Size(75, 35);
            this.btnNetworkSR.TabIndex = 31;
            this.btnNetworkSR.Text = "开始录音";
            this.btnNetworkSR.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtAudioUrl);
            this.groupBox1.Controls.Add(this.btnNetworkSR);
            this.groupBox1.Location = new System.Drawing.Point(375, 192);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(239, 100);
            this.groupBox1.TabIndex = 32;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "网络串流";
            // 
            // txtAudioUrl
            // 
            this.txtAudioUrl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtAudioUrl.Location = new System.Drawing.Point(22, 20);
            this.txtAudioUrl.Name = "txtAudioUrl";
            this.txtAudioUrl.Size = new System.Drawing.Size(211, 21);
            this.txtAudioUrl.TabIndex = 32;
            this.txtAudioUrl.Text = "http://127.0.0.1:8092/audio";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnLocalSR);
            this.groupBox2.Location = new System.Drawing.Point(375, 116);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(239, 70);
            this.groupBox2.TabIndex = 33;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "麦克风";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.txtAudioFile);
            this.groupBox3.Controls.Add(this.btnAudioFile);
            this.groupBox3.Location = new System.Drawing.Point(375, 12);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(239, 98);
            this.groupBox3.TabIndex = 34;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "音频文件";
            // 
            // btnAudioFile
            // 
            this.btnAudioFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAudioFile.Location = new System.Drawing.Point(6, 47);
            this.btnAudioFile.Name = "btnAudioFile";
            this.btnAudioFile.Size = new System.Drawing.Size(75, 36);
            this.btnAudioFile.TabIndex = 20;
            this.btnAudioFile.Text = "开始识别";
            this.btnAudioFile.UseVisualStyleBackColor = true;
            // 
            // txtAudioFile
            // 
            this.txtAudioFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtAudioFile.Location = new System.Drawing.Point(6, 20);
            this.txtAudioFile.Name = "txtAudioFile";
            this.txtAudioFile.Size = new System.Drawing.Size(211, 21);
            this.txtAudioFile.TabIndex = 33;
            this.txtAudioFile.Text = "audio.wav";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(626, 320);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.txtInput);
            this.Name = "MainForm";
            this.Text = "讯飞语音API测试";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnLocalSR;
        private System.Windows.Forms.TextBox txtInput;
        private System.Windows.Forms.Button btnNetworkSR;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtAudioUrl;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox txtAudioFile;
        private System.Windows.Forms.Button btnAudioFile;
    }
}

