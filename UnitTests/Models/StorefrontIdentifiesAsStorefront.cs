using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Models;
using Xunit;

namespace UnitTests{
    [TestClass]
    public class StorefrontIdentifiesAsStorefront{
        [Fact]
        public void Storefront_Equals_Storefront(){
            Store store = new Store();
            Xunit.Assert.Equal(store.Identify(), "Store");
        }
    }
}