using System;

namespace TLib.Core.Task.BasicTask
{
    public class GenericTask : BaseTask
    {
        private Action _action;

        public GenericTask(Action action)
        {
            _action = action;
        }

        protected override void Run()
        {
            _action();
        }
    }
}
