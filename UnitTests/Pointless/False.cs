using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Xunit;

namespace UnitTests
{
    [TestClass]
    public class False
    {
        [Fact]
        public void FalseTest()
        {
            Xunit.Assert.False(false);
        }
    }
}
