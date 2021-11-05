using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace UnitTests
{
    [TestClass]
    public class HelloEqualsHello
    {
        [TestMethod]
        public void HelloEqualsHelloTest()
        {
            Assert.AreEqual("Hello", "Hello");
        }
    }
}
