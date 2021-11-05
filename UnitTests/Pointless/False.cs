using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace UnitTests
{
    [TestClass]
    public class False
    {
        [TestMethod]
        public void FalseTest()
        {
            Assert.IsFalse(false);
        }
    }
}
