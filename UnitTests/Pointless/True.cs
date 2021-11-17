using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Xunit;

namespace UnitTests
{
    [TestClass]
    public class True
    {
        [Fact]
        public void TrueTest()
        {
            Xunit.Assert.True(true);
        }
    }
}
