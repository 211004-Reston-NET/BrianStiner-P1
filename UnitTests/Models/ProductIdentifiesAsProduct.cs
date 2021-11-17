using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Models;
using Xunit;

namespace UnitTests{
    [TestClass]
    public class ProductIdentifiesAsProduct{
        [Fact]
        public void Product_Equals_Product(){
            Product Product = new Product();
            Xunit.Assert.Equal(Product.Identify(), "Product");
        }
    }
}