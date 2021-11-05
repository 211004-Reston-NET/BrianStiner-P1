using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Models;

namespace UnitTests
{
    [TestClass]
    public class CustomerIdentifiesAsCustomer{
        [TestMethod]
        public void Customer_Equals_Customer(){
            Customer cust = new Customer();
            Assert.AreEqual(cust.Identify(), "Customer");
        }
    }
}