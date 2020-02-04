using Elton.VoiceCloud.TextToSpeech;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Elton.VoiceCloud.Demo
{
    public partial class TtsForm : Form
    {
        readonly TextToSpeechEngine tts = null;
        readonly WavePlayer player = null;
        public TtsForm()
        {
            InitializeComponent();

            player = new WavePlayer();
            player.ProgressChanged += player_ProgressChanged;

            var config = new TtsConfig();
            config.ServerUrl = "dev.voicecloud.cn";
            config.ApplicationId = "518fcbd0";
            config.Timeout = 10000;
            config.MaxTextSize = 4096;
            tts = new TextToSpeechEngine(config);
            tts.AudioReceived += tts_AudioReceived;
            tts.ProgressChanged += tts_ProgressChanged;

            btnSpeakText.Click += btnSpeakText_Click;
            btnSpeakFormatText.Click += btnSpeakFormatText_Click;
        }

        void player_ProgressChanged(object sender, TextToSpeech.ProgressChangedEventArgs e)
        {
            SpeechPart speechPart = e.Data;
            rtbContent.Select(0, speechPart.TextIndex);
            rtbContent.SelectionColor = Color.Gray;
            rtbContent.Select(speechPart.TextIndex, speechPart.TextLength);
            rtbContent.SelectionColor = Color.Red;
            rtbContent.Select(speechPart.TextIndex + speechPart.TextLength, 0);
        }

        void tts_ProgressChanged(object sender, TextToSpeech.ProgressChangedEventArgs e)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new EventHandler<TextToSpeech.ProgressChangedEventArgs>(tts_ProgressChanged), sender, e);
                return;
            }

            SpeechPart speechPart = e.Data;

            progressBar.Maximum = speechPart.TotalCount;
            progressBar.Value = speechPart.TextIndex + speechPart.TextLength;


            player.PlayRawAudioSync(speechPart);
        }

        void tts_AudioReceived(object sender, AudioReceivedEventArgs e)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new EventHandler<AudioReceivedEventArgs>(tts_AudioReceived), sender, e);
                return;
            }

            progressBar.Maximum = e.TotalCount;
            progressBar.Value = e.Index;
        }

        void btnSpeakText_Click(object sender, EventArgs e)
        {
            btnSpeakText.Enabled = false;
            try
            {
                tts.Volume = (comboBox3.SelectedItem as EnumData<SpeechVolumeEnums>).Value;
                tts.Speed = (comboBox2.SelectedItem as EnumData<SpeechSpeedEnums>).Value;
                tts.Speaker = (comboBox1.SelectedItem as SpeakerInfo).Id;
                tts.SpeakText(rtbContent.Text);
            }
            catch(Exception ex)
            {

            }
            btnSpeakText.Enabled = true;
        }
        void btnSpeakFormatText_Click(object sender, EventArgs e)
        {
            btnSpeakFormatText.Enabled = false;
            try
            {
                tts.Volume = (comboBox3.SelectedItem as EnumData<SpeechVolumeEnums>).Value;
                tts.Speed = (comboBox2.SelectedItem as EnumData<SpeechSpeedEnums>).Value;
                tts.Speaker = (comboBox1.SelectedItem as SpeakerInfo).Id;

                tts.SpeakFormatText(rtbContent.Text);
            }
            catch (Exception ex)
            {

            }
            btnSpeakFormatText.Enabled = true;
        }

        public class EnumData<T>
        {
            public T Value { get; private set; }
            public EnumData(T value)
            {
                this.Value = value;
            }

            public override string ToString()
            {
                return EnumHelper.GetName(typeof(T), this.Value);
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            foreach (SpeakerInfo item in SpeakerInfo.GetList())
                comboBox1.Items.Add(item);
            foreach (SpeechSpeedEnums item in Enum.GetValues(typeof(SpeechSpeedEnums)))
                comboBox2.Items.Add(new EnumData<SpeechSpeedEnums>(item));
            foreach (SpeechVolumeEnums item in Enum.GetValues(typeof(SpeechVolumeEnums)))
                comboBox3.Items.Add(new EnumData<SpeechVolumeEnums>(item));
            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 2;
            comboBox3.SelectedIndex = 4;
        }
    }
}
