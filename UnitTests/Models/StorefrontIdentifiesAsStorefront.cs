using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Models;

namespace UnitTests{
    [TestClass]
    public class StorefrontIdentifiesAsStorefront{
        [TestMethod]
        public void Storefront_Equals_Storefront(){
            Store store = new Store();
            Assert.AreEqual(store.Identify(), "Storefront");
        }
    }
}