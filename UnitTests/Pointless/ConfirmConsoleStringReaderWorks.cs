using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
 
namespace UnitTests
 {
    [TestClass]
    public class ConfirmConsoleStringReaderWorks
        {
            [TestMethod]
            public void TestConsoleReader()
            {
                var matchInput = new StringReader("555-555-5555\r\n(555)-555-5555\r\n(555) 555-5555\r\n555.555.5555");
                Console.SetIn(matchInput);
                Assert.AreEqual("555-555-5555", Console.ReadLine());
                Assert.AreEqual("(555)-555-5555", Console.ReadLine());
                Assert.AreEqual("(555) 555-5555", Console.ReadLine());
                Assert.AreEqual("555.555.5555", Console.ReadLine());
            }
        }
 }