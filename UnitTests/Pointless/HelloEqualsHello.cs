using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Xunit;

namespace UnitTests
{
    [TestClass]
    public class HelloEqualsHello
    {
        [Fact]
        public void HelloEqualsHelloTest()
        {
            Xunit.Assert.Equal("Hello", "Hello");
        }
    }
}
