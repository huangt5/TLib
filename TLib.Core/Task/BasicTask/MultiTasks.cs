using System.Collections.Generic;

namespace TLib.Core.Task.BasicTask
{
    class MultiTasks : BaseTask
    {
        List<BaseTask>  _subActions = new List<BaseTask>();

        public void AddAction(BaseTask action)
        {
            _subActions.Add(action);
        }

        protected override void Run()
        {
            foreach (var action in _subActions)
            {
                action.Start();
            }
        }
    }
}
