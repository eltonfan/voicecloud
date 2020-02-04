// Coded by chuangen http://chuangen.name.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace Elton.VoiceCloud.ISR
{
    public abstract class IsrEngine : IIsrEngine
    {
        readonly BackgroundWorker bw = null;
        protected readonly IsrClient client = null;
        protected IsrEngine(IsrConfig config, string recog_grammar, string recog_params)
        {
            this.client = new IsrClient(config, recog_grammar, recog_params);

            bw = new BackgroundWorker();
            bw.WorkerReportsProgress = true;
            bw.WorkerSupportsCancellation = true;
            bw.DoWork += new DoWorkEventHandler(bw_DoWork);
            bw.ProgressChanged += new ProgressChangedEventHandler(bw_ProgressChanged);
            bw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bw_RunWorkerCompleted);
        }

        void bw_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker bw = sender as BackgroundWorker;
            string uriString = e.Argument as string;
            e.Result = "";
            e.Result = this.RecognizeInternal(bw, uriString);
        }

        void bw_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            this.OnResultArrived(new ResultArrivedEventArgs(e.UserState as string));
        }

        void bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.Recognizing = false;
            this.OnRecognizeCompleted(new RecognizeCompletedEventArgs(e.Result as string, e.Error, e.Cancelled));
        }

        /// <summary>
        /// 请求取消挂起的后台操作。
        /// </summary>
        public virtual void CancelAsync()
        {
            if (this.bw.IsBusy)
                this.bw.CancelAsync();
        }

        /// <summary>
        /// 语音识别。
        /// </summary>
        public virtual string Recognize(string uriString)
        {
            return RecognizeInternal(null, uriString);
        }
        /// <summary>
        /// 开始执行后台操作。
        /// </summary>
        /// <param name="uriString"></param>
        public virtual void RecognizeAsync(string uriString)
        {
            if (this.Recognizing)
                return;
            this.bw.RunWorkerAsync(uriString);
            this.Recognizing = true;
        }

        protected abstract string RecognizeInternal(BackgroundWorker bw, string uriString);

        protected virtual void OnRecognizeCompleted(RecognizeCompletedEventArgs e)
        {
            if (this.RecognizeCompleted != null)
                this.RecognizeCompleted(this, e);
        }

        protected virtual void OnResultArrived(ResultArrivedEventArgs e)
        {
            if (this.ResultArrived != null)
                this.ResultArrived(this, e);
        }

        /// <summary>
        /// 是否正在识别中。
        /// </summary>
        public bool Recognizing { get; protected set; }
        /// <summary>
        /// 调用 ReportProgress 时发生。
        /// </summary>
        public event EventHandler<ResultArrivedEventArgs> ResultArrived;
        /// <summary>
        /// 当后台操作已完成、被取消或引发异常时发生。
        /// </summary>
        public event EventHandler<RecognizeCompletedEventArgs> RecognizeCompleted;
    }
}
