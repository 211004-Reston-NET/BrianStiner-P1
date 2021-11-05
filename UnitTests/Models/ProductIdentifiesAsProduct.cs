using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Models;

namespace UnitTests{
    [TestClass]
    public class ProductIdentifiesAsProduct{
        [TestMethod]
        public void Product_Equals_Product(){
            Product Product = new Product();
            Assert.AreEqual(Product.Identify(), "Product");
        }
    }
}