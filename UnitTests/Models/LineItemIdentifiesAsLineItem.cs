using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Models;
using Xunit;

namespace UnitTests{
    [TestClass]
    public class LineItemIdentifiesAsLineItem{
        [Fact]
        public void LineItem_Equals_LineItem(){
            LineItem li = new LineItem();
            Xunit.Assert.Equal(li.Identify(), "LineItem");
        }
    }
}