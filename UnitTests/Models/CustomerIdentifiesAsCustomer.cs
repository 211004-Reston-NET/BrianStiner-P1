// using Microsoft.VisualStudio.TestTools.UnitTesting;
// using System;
// using Models;
// using Xunit;
// using System.Collections;

// namespace UnitTests
// {
//     [TestClass]
//     public class CustomerIdentifiesAsCustomer{
//         [Fact]
//         public void Customer_Equals_Customer(){
//             Customer cust = new Customer();
//             Xunit.Assert.Equal(cust.Identify(), "Customer");
//         }
//         [Fact]
//         public void Customer_Equals_Customer_After_ArrayList(){
//             Customer cust = new Customer("test","test st 10111", "test@test.test", "111-111-1111");
//             Customer cust2 = new Customer();
//             ArrayList al = new ArrayList();

//             al = cust.ToArrayList();
//             cust2.FromArrayList(al);

//             Xunit.Assert.Equal(cust2.Identify(), cust.Identify());
//             Xunit.Assert.Equal(cust2.ToString(), cust.ToString());  
//         }
//         [Fact]
//         public void Store_Equals_Store_After_ArrayList(){
//             Store store = new Store("test", "test st", 10, 10);
//             Store store2 = new Store();
//             ArrayList al = new ArrayList();

//             al = store.ToArrayList();
//             store2.FromArrayList(al);

//             Xunit.Assert.Equal(store2.Identify(), store.Identify());
//             Xunit.Assert.Equal(store2.ToString(), store.ToString());
//         }
//         [Fact]
//         public void Product_Equals_Product_After_ArrayList(){
//             Product prod = new Product(10, "test", "test", "10", 10);
//             Product prod2 = new Product();
//             ArrayList al = new ArrayList();

//             al = prod.ToArrayList();
//             prod2.FromArrayList(al);

//             Xunit.Assert.Equal(prod2.Identify(), prod.Identify());
//             Xunit.Assert.Equal(prod2.ToString(), prod.ToString());
//         }
//         [Fact]
//         public void LineItem_Equals_LineItem_After_ArrayList(){
//             LineItem li = new LineItem(10, 10, 10);
//             LineItem li2 = new LineItem();
//             ArrayList al = new ArrayList();

//             al = li.ToArrayList();
//             li2.FromArrayList(al);

//             Xunit.Assert.Equal(li2.Identify(), li.Identify());
//             Xunit.Assert.Equal(li2.ToString(), li.ToString());
//         }
//         [Fact]
//         public void InventoryItem_Equals_InvetoryItem_After_ArrayList(){
//             InventoryItem ii = new InventoryItem(10, 10, 10);
//             InventoryItem ii2 = new InventoryItem();
//             ArrayList al = new ArrayList();

//             al = ii.ToArrayList();
//             ii2.FromArrayList(al);

//             Xunit.Assert.Equal(ii2.Identify(), ii.Identify());
//             Xunit.Assert.Equal(ii2.ToString(), ii.ToString());
//         }

//         public void Order_Equals_Order_After_ArrayList(){
//             Order order = new Order("10", 10);
//             Order order2 = new Order();
//             ArrayList al = new ArrayList();

//             al = order.ToArrayList();
//             order2.FromArrayList(al);

//             Xunit.Assert.Equal(order2.Identify(), order.Identify());
//             Xunit.Assert.Equal(order2.ToString(), order.ToString());
//         }
//     }
// }