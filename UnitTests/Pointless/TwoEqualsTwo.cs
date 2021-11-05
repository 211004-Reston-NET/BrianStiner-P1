using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace UnitTests
{
    [TestClass]
    public class TwoEqualsTwo
    {
        [TestMethod]
        public void TwoEqualsTwoTest()
        {
            Assert.AreEqual(2, 2);
        }
    }
}
