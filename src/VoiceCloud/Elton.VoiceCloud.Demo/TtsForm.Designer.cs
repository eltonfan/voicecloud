namespace Mavplus.VoiceCloud.Demo
{
    partial class TtsForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.comboBox3 = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.progressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.btnSpeakText = new System.Windows.Forms.Button();
            this.rtbContent = new System.Windows.Forms.RichTextBox();
            this.btnSpeakFormatText = new System.Windows.Forms.Button();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // comboBox3
            // 
            this.comboBox3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox3.FormattingEnabled = true;
            this.comboBox3.Location = new System.Drawing.Point(527, 6);
            this.comboBox3.Name = "comboBox3";
            this.comboBox3.Size = new System.Drawing.Size(106, 20);
            this.comboBox3.TabIndex = 39;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(480, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 38;
            this.label3.Text = "音量：";
            // 
            // comboBox2
            // 
            this.comboBox2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Location = new System.Drawing.Point(383, 6);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(91, 20);
            this.comboBox2.TabIndex = 37;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(336, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 36;
            this.label2.Text = "语速：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 35;
            this.label1.Text = "演讲者：";
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(71, 6);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(259, 20);
            this.comboBox1.TabIndex = 32;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.progressBar});
            this.statusStrip1.Location = new System.Drawing.Point(0, 324);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(651, 22);
            this.statusStrip1.TabIndex = 43;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(434, 17);
            this.toolStripStatusLabel1.Spring = true;
            this.toolStripStatusLabel1.Text = "状态栏。";
            this.toolStripStatusLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // progressBar
            // 
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(200, 16);
            // 
            // btnSpeakText
            // 
            this.btnSpeakText.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSpeakText.Location = new System.Drawing.Point(14, 294);
            this.btnSpeakText.Name = "btnSpeakText";
            this.btnSpeakText.Size = new System.Drawing.Size(75, 23);
            this.btnSpeakText.TabIndex = 44;
            this.btnSpeakText.Text = "合成并播放";
            this.btnSpeakText.UseVisualStyleBackColor = true;
            // 
            // rtbContent
            // 
            this.rtbContent.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.rtbContent.Location = new System.Drawing.Point(12, 32);
            this.rtbContent.Name = "rtbContent";
            this.rtbContent.Size = new System.Drawing.Size(625, 251);
            this.rtbContent.TabIndex = 45;
            this.rtbContent.Text = "昨天下午，当杭州市气象局，在灿烂阳光中，宣布杭州入秋，小伙伴们的反应是，“不是已经进入秋天很久了吗？”今年，是秋天抢足了夏天的风头，以至于秋天真正到来时，秋意早就" +
    "透支得差不多了。\n请默写下面听到的句子。\nstop 2\nting\ncatherine#How are you?";
            // 
            // btnSpeakFormatText
            // 
            this.btnSpeakFormatText.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSpeakFormatText.Location = new System.Drawing.Point(111, 294);
            this.btnSpeakFormatText.Name = "btnSpeakFormatText";
            this.btnSpeakFormatText.Size = new System.Drawing.Size(75, 23);
            this.btnSpeakFormatText.TabIndex = 46;
            this.btnSpeakFormatText.Text = "带格式";
            this.btnSpeakFormatText.UseVisualStyleBackColor = true;
            // 
            // TtsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(651, 346);
            this.Controls.Add(this.btnSpeakFormatText);
            this.Controls.Add(this.rtbContent);
            this.Controls.Add(this.btnSpeakText);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.comboBox3);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.comboBox2);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboBox1);
            this.Name = "TtsForm";
            this.Text = "TTS测试";
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBox3;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripProgressBar progressBar;
        private System.Windows.Forms.Button btnSpeakText;
        private System.Windows.Forms.RichTextBox rtbContent;
        private System.Windows.Forms.Button btnSpeakFormatText;
    }
}