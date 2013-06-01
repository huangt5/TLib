using System;
using NUnit.Framework;
using TLib.Core.Task.BasicTask;

namespace TLib.Sample.Core.Task
{
    [TestFixture]
    public class BasicTaskSample
    {
        [Test]
        public void GenericTask()
        {
            var task = new GenericTask(() => Console.WriteLine("Generic task"));
            task.Start();
        }

        [Test]
        public void MultiTasks()
        {
            var tasks = new MultiTasks();
            tasks.AddAction(new GenericTask(()=>Console.WriteLine("Task 1")));
            tasks.AddAction(new GenericTask(()=>Console.WriteLine("Task 2")));
            tasks.StartAsync();
        }
    }
}
