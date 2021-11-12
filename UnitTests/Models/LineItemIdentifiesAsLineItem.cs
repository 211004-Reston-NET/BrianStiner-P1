using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Models;

namespace UnitTests{
    [TestClass]
    public class LineItemIdentifiesAsLineItem{
        [TestMethod]
        public void LineItem_Equals_LineItem(){
            LineItem li = new LineItem();
            Assert.AreEqual(li.Identify(), "LineItem");
        }
    }
}