using Xunit;
using System.Collections.Generic;
using Models;
using DataAccessLogic;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests
{
    [TestClass]
    public class Datatest
    {
        private readonly DbContextOptions<revaturedatabaseContext> _options;

        public Datatest()
        {
            _options = new DbContextOptionsBuilder<revaturedatabaseContext>() //In-memory database
                .UseSqlite("Filename = TestingDatabase.Db")
                .Options;
            Seed();
        }


        [TestMethod]
        public void TestStoreMatchesBeforeandAfter()
        {
            //Arrange
            using (var context = new revaturedatabaseContext(_options))
            {
                //Act
                Store teststore = context.Stores.FirstOrDefaultAsync().Result;

                //Assert
                Xunit.Assert.Equal(10000, teststore.Id);
                Xunit.Assert.Equal("TestStore", teststore.Name);
                Xunit.Assert.Equal("112 test test 01101", teststore.Address);

            }
        }


        private void Seed(){
            using (var context = new revaturedatabaseContext(_options)) //makes a customer and store
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                context.Customers.AddRange(
                    new Customer{
                        Id = 10000,
                        Name = "Test",
                        Address = "112 test test 01101",
                        Email = "testtest@email.com",
                        Orders = new List<Order>{
                            new Order{
                                Id = 11101,
                                Address = "112 test test 01101",
                                Active = true,
                                LineItems = new List<LineItem>{
                                    new LineItem{
                                        Id = 11110,
                                        Product = new Product{
                                            Id = 10110,
                                            Name = "Test",
                                            Price = 10.00m,
                                            Description = "Test",
                                        }},
                                    new LineItem{
                                        Id = 11111,
                                        Product = new Product{
                                            Id = 10111,
                                            Name = "Test2",
                                            Price = 10.00m,
                                            Description = "Test2",
                                        }}}},
                            new Order{
                                Id = 11112,
                                Address = "112 test test 01101",
                                Active = true,
                                LineItems = new List<LineItem>{
                                    new LineItem{
                                        Id = 11010,
                                        Product = new Product{
                                            Id = 10110,
                                            Name = "Test",
                                            Price = 10.00m,
                                            Description = "Test",
                                        }},
                                    new LineItem{
                                        Id = 00111,
                                        Product = new Product{
                                            Id = 10011,
                                            Name = "Test2",
                                            Price = 10.00m,
                                            Description = "Test2",
                                    }}}}}});
                context.Stores.AddRange(
                    new Store{
                        Id = 10000,
                        Name = "Test Store",
                        Address = "112 test test 01101",
                        Orders = new List<Order>{
                            new Order{
                                Id = 11111,
                                Address = "112 test test 01101",
                                Active = true,
                                LineItems = new List<LineItem>{
                                    new LineItem{
                                        Id = 11110,
                                        Product = new Product{
                                            Id = 10110,
                                            Name = "Test",
                                            Price = 10.00m,
                                            Description = "Test",
                                        }},
                                    new LineItem{
                                        Id = 11111,
                                        Product = new Product{
                                            Id = 10111,
                                            Name = "Test2",
                                            Price = 10.00m,
                                            Description = "Test2",
                                        }}}},
                            new Order{
                                Id = 11112,
                                Address = "112 test test 01101",
                                Active = true,
                                LineItems = new List<LineItem>{
                                    new LineItem{
                                        Id = 10010,
                                        Product = new Product{
                                            Id = 10110,
                                            Name = "Test",
                                            Price = 10.00m,
                                            Description = "Test",
                                        }},
                                    new LineItem{
                                        Id = 11111,
                                        Product = new Product{
                                            Id = 10111,
                                            Name = "Test2",
                                            Price = 10.00m,
                                            Description = "Test2",
                                    }}}}}});
                        context.SaveChanges();
            }         
}}}       