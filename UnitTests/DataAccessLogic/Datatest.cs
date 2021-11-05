// using Xunit;
// using System.Collections.Generic;
// using Models;

// namespace UnitTests
// {
//     public class Datatest
//     {
//         private readonly DbContextOptions<revaturedatabaseContext> _options;

//         public Datatest()
//         {
//             _options = new DbContextOptionsBuilder<revaturedatabaseContext>() //In-memory database
//                 .UseSqlite("Filename = TestingDatabase.Db"); 
//             Seed();
//         }


//         [Fact]
//         public void GetAllReturnsAll()
//         {
//             Assert.True(true);
//         }
//         public void AddCustomerAndGetCustomerAreEqual()
//         {
//             using (var context = new revaturedatabaseContext(_options))
//             {
//                 var tester = new Customer()
//                 {
//                     Name = "Test",
//                     Address = "10 Test 10110",
//                     Phone = "222-212-2212",
//                     Email = "Test@email.com",
//                 };
//                 context.Customer.Add(tester);
//                 context.SaveChanges();
//                 var result = context.Customer.Get(tester);
//                 Assert.Equal(tester, result);
//             }
//         }






//         private void Seed(){
//             using (var context = new revaturedatabaseContext(_options))
//             {
//                 context.Database.EnsureDeleted();
//                 context.Database.EnsureCreated();

//                 context.Customer.AddRange(
//                     new Customer{
//                         Id = 10000,
//                         Name = "Test",
//                         Address = "112 test test 01101",
//                         Email = "testtest@email.com",
//                         Orders = new List<Order>{
//                             new Order{
//                                 Id = 11111,
//                                 Address = "112 test test 01101",
//                                 Active = true,
//                                 LineItems = new List<LineItem>{
//                                     new LineItem{
//                                         Id = 11110,
//                                         Product = new Product{
//                                             Id = 10110,
//                                             Name = "Test",
//                                             Price = 10.00m,
//                                             Description = "Test",
//                                         }},
//                                     new LineItem{
//                                         Id = 11111,
//                                         Product = new Product{
//                                             Id = 10111,
//                                             Name = "Test2",
//                                             Price = 10.00m,
//                                             Description = "Test2",
//                                         }}}},
//                             new Order{
//                                 Id = 11112,
//                                 Address = "112 test test 01101",
//                                 Active = true,
//                                 LineItems = new List<LineItem>{
//                                     new LineItem{
//                                         Id = 11110,
//                                         Product = new Product{
//                                             Id = 10110,
//                                             Name = "Test",
//                                             Price = 10.00m,
//                                             Description = "Test",
//                                         }},
//                                     new LineItem{
//                                         Id = 11111,
//                                         Product = new Product{
//                                             Id = 10111,
//                                             Name = "Test2",
//                                             Price = 10.00m,
//                                             Description = "Test2",
//                                     }}}}}});
//                         context.SaveChanges();
//                     }
                    
        
           
// }}}       