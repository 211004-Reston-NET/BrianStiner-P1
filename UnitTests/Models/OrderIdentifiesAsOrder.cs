using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Models;
using Xunit;

namespace UnitTests{
    [TestClass]
    public class OrderIdentifiesAsOrder{
        [Fact]
        public void Order_Equals_Order(){
            Order order = new Order();
            Xunit.Assert.Equal(order.Identify(), "Order");
        }
    }
}