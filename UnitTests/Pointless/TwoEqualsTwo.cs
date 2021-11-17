using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Xunit;

namespace UnitTests
{
    [TestClass]
    public class TwoEqualsTwo
    {
        [Fact]
        public void TwoEqualsTwoTest()
        {
            Xunit.Assert.Equal(2, 2);
        }
    }
}
