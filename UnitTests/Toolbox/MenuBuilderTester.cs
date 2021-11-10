// using Microsoft.VisualStudio.TestTools.UnitTesting;
// using System;
// using System.IO;

// using Microsoft.EntityFrameworkCore;
// using Microsoft.Extensions.Configuration;
// using DataAccessLogic;
// using BusinessLogic;
 
// namespace UnitTests
//  {
//     [TestClass]
//     public class MenuBuilderTests
//         {
//             public static IConfigurationRoot configuration = new ConfigurationBuilder()
//                 .AddJsonFile("C:/Users/maste/Documents/211004-Reston-NET/BrianStiner-PO/UserInterface/appsetting.json")    
//                 .Build();
            
//             public static DbContextOptionsBuilder<revaturedatabaseContext> options = new DbContextOptionsBuilder<revaturedatabaseContext>()
//                 .UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
//             revaturedatabaseContext context = new revaturedatabaseContext(options.Options);

//             private MenuBuilder builder = new MenuBuilder(new Business(new RepositorySQL(new revaturedatabaseContext(options.Options)))); 

//             [TestMethod]
//             public void TestEmail()
//             {
//                 //Arrange
//                 var matchInput = new StringReader("ksajdkfjsd@jksdf.jdfs\r\n232232@23222.210");
//                 Console.SetIn(matchInput);

//                 Assert.AreEqual("ksajdkfjsd@jksdf.jdfs", builder.GetEmail());
//                 Assert.AreEqual("232232@23222.210", builder.GetEmail());
//             }
//             [TestMethod]
//             public void TestPhone()
//             {
//                 var matchInput = new StringReader("555-555-5555\r\n(555)-555-5555\r\n(555) 555 5555\r\n555.555.5555");
//                 Console.SetIn(matchInput);
//                 Assert.AreEqual("555-555-5555", builder.GetPhoneNumber());
//                 Assert.AreEqual("555-555-5555", builder.GetPhoneNumber());
//                 Assert.AreEqual("555-555-5555", builder.GetPhoneNumber());
//                 Assert.AreEqual("555-555-5555", builder.GetPhoneNumber());

//             }
//             [TestMethod]
//             public void TestInt()
//             {
//                 var matchInput = new StringReader("1\r\n2\r\n3\r\n4\r\n5\r\n6\r\n7\r\n8\r\n9\r\n10\r\n");
//                 Console.SetIn(matchInput);
                
//                 for(int i = 1; i < 11; i++)
//                 {
//                     Assert.AreEqual(i, builder.GetInt());
//                 }
//             }
//             [TestMethod]
//             public void TestString()
//             {
//                 var matchInput = new StringReader("test\r\nTEST\r\n t e s t\r\n\r\n");
//                 Console.SetIn(matchInput);

//                 Assert.AreEqual($"test", builder.GetString());
//                 Assert.AreEqual($"TEST", builder.GetString());
//                 Assert.AreEqual($" t e s t", builder.GetString());
//             }
//             [TestMethod]
//             public void TestDecimal()
//             {
//                 var matchInput = new StringReader("1.1\r\n2.2");
//                 Console.SetIn(matchInput);
                
//                 Assert.AreEqual(1.1m, builder.GetDecimal());
//                 Assert.AreEqual(2.2m, builder.GetDecimal());
//             }
//             [TestMethod]
//             public void TestChoice()
//             {
//                 var matchInput = new StringReader("1\r\nyes\r\nya\r\nabsolutely\r\nno\r\nnever\r\n0\r\n0yes");
//                 Console.SetIn(matchInput);
                
//                 for(int i = 0; i < 4; i++){
//                     Assert.AreEqual(true, builder.Choice());
//                 }for (int i = 0; i < 4; i++){
//                     Assert.AreEqual(false, builder.Choice());
//                 }
//             }
//         }
//  }