using System;
using System.ComponentModel;

namespace TLib.Core.Task.BasicTask
{
    /// <summary>
    /// Basic task support synchronized and asynchronized start.
    /// </summary>
    abstract class BaseTask
    {
        public void Start()
        {
            Run();
            OnActionCompleted();
        }

        public void StartAsync()
        {
            BackgroundWorker worker = new BackgroundWorker();
            worker.DoWork += StartAsyncImpl;
            worker.RunWorkerCompleted += StartAsyncComplete;
            worker.RunWorkerAsync();
        }

        private void StartAsyncComplete(object sender, RunWorkerCompletedEventArgs e)
        {
            OnActionCompleted();
        }

        private void StartAsyncImpl(object sender, DoWorkEventArgs e)
        {
            Run();
        }

        protected abstract void Run();
        public event Action ActionCompleted;

        private void OnActionCompleted()
        {
            if (ActionCompleted != null)
            {
                ActionCompleted();
            }
        }
    }
}
