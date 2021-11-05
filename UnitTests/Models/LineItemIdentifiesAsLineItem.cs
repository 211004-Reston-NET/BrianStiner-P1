using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Models;

namespace UnitTests{
    [TestClass]
    public class LineItemIdentifiesAsLineItem{
        [TestMethod]
        public void LineItem_Equals_LineItem(){
            LineItem LineItem = new LineItem();
            Assert.AreEqual(LineItem.Identify(), "LineItem");
        }
    }
}