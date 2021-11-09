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
                Store teststore = context.Stores.Find();

                //Assert
                Xunit.Assert.Equal(teststore.Id, 10000);
                Xunit.Assert.Equal(teststore.Name, "Test Store");
                Xunit.Assert.Equal(teststore.Address, "112 test test 01101");

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
                                    }}}}}});
                        context.SaveChanges();
            }         
}}}       