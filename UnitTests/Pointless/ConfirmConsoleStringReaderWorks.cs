using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using Xunit;

namespace UnitTests
 {
    [TestClass]
    public class ConfirmConsoleStringReaderWorks
        {
            [Fact]
            public void TestConsoleReader()
            {
                var matchInput = new StringReader("555-555-5555\r\n(555)-555-5555\r\n(555) 555-5555\r\n555.555.5555");
                Console.SetIn(matchInput);
                Xunit.Assert.Equal("555-555-5555", Console.ReadLine());
                Xunit.Assert.Equal("(555)-555-5555", Console.ReadLine());
                Xunit.Assert.Equal("(555) 555-5555", Console.ReadLine());
                Xunit.Assert.Equal("555.555.5555", Console.ReadLine());
            }
        }
 }