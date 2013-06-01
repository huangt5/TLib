using System;

namespace TLib.Core.Task.BasicTask
{
    class GenericTask : BaseTask
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
