using System;
using NUnit.Framework;
using TLib.Core.Text;

namespace TLib.Sample.Core.Text
{
    [TestFixture]
    public class StringExtensionsSample
    {
        [Test]
        public void Match1()
        {
            string input = "Hellow world!";
            string output = input.Match1(@" (\w+)!");
            Console.WriteLine(output);
        }
    }
}
