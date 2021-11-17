using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Models;
using Xunit;

namespace UnitTests{
    [TestClass]
    public class OneEqualsOne{
        [Fact]
        public void one_equals_one_true(){
            Xunit.Assert.Equal(1, 1);
        }
    }
}
    /*/
    Your app should store meaningful logging information using serilog or Nlog
    Your app should have at least 10 meaningful unit tests. You can also unit test database calls.
    Your app must be able to add and search for customers and storefronts.
    Your app must be able to replenish inventory and place orders.
    Your app should be able to view Order history.
    /*/
