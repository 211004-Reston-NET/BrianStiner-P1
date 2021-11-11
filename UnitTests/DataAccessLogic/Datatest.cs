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


        [Fact]
        public void TestStoreMatchesBeforeandAfter()
        {
            //Arrange
            using (var context = new revaturedatabaseContext(_options))
            {
                //Act
                var _repo = new DataAccessLogic.RepositorySQL(context);

                Store teststore = _repo.Get(new Store { Id = 10000 });

                //Assert
                Xunit.Assert.Equal(10000, teststore.Id);
                Xunit.Assert.Equal("Test Store", teststore.Name);
                Xunit.Assert.Equal("112 test test 01101", teststore.Address);

            }
        }

        [Fact]
        public void TestCustomerMatchesBeforeandAfter()
        {
            //Arrange
            using (var context = new revaturedatabaseContext(_options))
            {
                //Act
                var _repo = new DataAccessLogic.RepositorySQL(context);

                Customer testcustomer = _repo.Get(new Customer { Id = 10000 });

                //Assert
                Xunit.Assert.Equal(10000, testcustomer.Id);
                Xunit.Assert.Equal("Test", testcustomer.Name);
                Xunit.Assert.Equal("112 test test 01101", testcustomer.Address);
                Xunit.Assert.Equal("testtest@email.com", testcustomer.Email);
                Xunit.Assert.Equal("123-456-7890", testcustomer.Phone);

            }
        }

        [Fact]
        public void TestOrderMatchesBeforeandAfter()
        {
            //Arrange
            using (var context = new revaturedatabaseContext(_options))
            {
                //Act
                var _repo = new DataAccessLogic.RepositorySQL(context);

                Order testorder = _repo.Get(new Order { Id = 11101 });

                //Assert
                Xunit.Assert.Equal(11101, testorder.Id);
                Xunit.Assert.Equal("112 test test 01101", testorder.Address);
                Xunit.Assert.True(testorder.Active);


            }
        }

        [Fact]
        public void TestLineItemMatchesBeforeandAfter()
        {
            //Arrange
            using (var context = new revaturedatabaseContext(_options))
            {
                //Act
                var _repo = new DataAccessLogic.RepositorySQL(context);

                LineItem testlineitem = _repo.Get(new LineItem { Id = 11111 });

                // Id = 11110,
                // Quantity = 10,
                // Total = 100.00m,
                // Product = new Product{
                //     Id = 10000,
                //     Name = "Test",
                //     Price = 10.00m,
                //     Description = "Test",
                //     Category = "Test",}

                //Assert
                Xunit.Assert.Equal(11111, testlineitem.Id );
                Xunit.Assert.Equal(10, testlineitem.Quantity);
                Xunit.Assert.Equal(10000, testlineitem.Product.Id);
                Xunit.Assert.Equal(10.00m, testlineitem.Product.Price);
                Xunit.Assert.Equal("Test", testlineitem.Product.Name);
                Xunit.Assert.Equal("Test", testlineitem.Product.Description);
                Xunit.Assert.Equal("Test", testlineitem.Product.Category);

            }
        }

        [Fact]
        public void TestProductMatchesBeforeandAfter()
        {
            //Arrange
            using (var context = new revaturedatabaseContext(_options))
            {
                //Act
                var _repo = new DataAccessLogic.RepositorySQL(context);

                Product testproduct = _repo.Get(new Product { Id = 10001 });
                //Assert
                Xunit.Assert.Equal(10001, testproduct.Id);
                Xunit.Assert.Equal("Test2", testproduct.Name);
                Xunit.Assert.Equal("Test2", testproduct.Description);
                Xunit.Assert.Equal(10.00m, testproduct.Price);
                Xunit.Assert.Equal("Test2", testproduct.Category);

            }
        }

        [Fact]
        public void TestStoreAdd()
        {
            //Arrange
            using (var context = new revaturedatabaseContext(_options))
            {
                //Act
                var _repo = new DataAccessLogic.RepositorySQL(context);

                Store teststore = new Store { Id = 10001, Name = "TestStore2", Address = "112 test test 01101" };
                _repo.Add(teststore);
                context.SaveChanges();

                Store teststore2 = _repo.Get(new Store { Id = 10001 });

                //Assert
                Xunit.Assert.Equal(teststore.Id, teststore2.Id);
                Xunit.Assert.Equal(teststore.Name, teststore2.Name);
                Xunit.Assert.Equal(teststore.Address, teststore2.Address);

            }
        }

        [Fact]
        public void TestCustomerAdd()
        {
            //Arrange
            using (var context = new revaturedatabaseContext(_options))
            {
                //Act
                var _repo = new DataAccessLogic.RepositorySQL(context);

                Customer testcustomer = new Customer { Id = 10001, Name = "Testadd", Address = "1 test 01101", Email = "customer@add.com", Phone = "123-456-7890" };
                _repo.Add(testcustomer);
                context.SaveChanges();

                Customer testcustomer2 = _repo.Get(new Customer { Id = 10001 });

                //Assert
                Xunit.Assert.Equal(testcustomer.Id, testcustomer2.Id);
                Xunit.Assert.Equal(testcustomer.Name, testcustomer2.Name);
                Xunit.Assert.Equal(testcustomer.Address, testcustomer2.Address);
                Xunit.Assert.Equal(testcustomer.Email, testcustomer2.Email);
                Xunit.Assert.Equal(testcustomer.Phone, testcustomer2.Phone);

            }
        }

        [Fact]
        public void TestOrderAdd()
        {
            //Arrange
            using (var context = new revaturedatabaseContext(_options))
            {
                //Act
                var _repo = new DataAccessLogic.RepositorySQL(context);

                Order testorder = new Order { Id = 10001, Address = "1 test 01101", Active = true };
                _repo.Add(testorder);
                context.SaveChanges();

                Order testorder2 = _repo.Get(new Order { Id = 10001 });

                //Assert
                Xunit.Assert.Equal(testorder.Id, testorder2.Id);
                Xunit.Assert.Equal(testorder.Address, testorder2.Address);
                Xunit.Assert.Equal(testorder.Active, testorder2.Active);

            }
        }

        [Fact]
        public void TestLineItemAdd()
        {
            //Arrange
            using (var context = new revaturedatabaseContext(_options))
            {
                //Act
                var _repo = new DataAccessLogic.RepositorySQL(context);

                LineItem testlineitem = new LineItem { Id = 12201, Quantity = 10, Product = new Product { Id = 12202, Name = "Test", Price = 10.00m, Description = "Test", Category = "Test" } };
                _repo.Add(testlineitem);
                context.SaveChanges();

                LineItem testlineitem2 = _repo.Get(new LineItem { Id = 12201 });

                //Assert
                Xunit.Assert.Equal(testlineitem.Id, testlineitem2.Id);
                Xunit.Assert.Equal(testlineitem.Quantity, testlineitem2.Quantity);
                Xunit.Assert.Equal(testlineitem.Product.Id, testlineitem2.Product.Id);
                Xunit.Assert.Equal(testlineitem.Product.Name, testlineitem2.Product.Name);
                Xunit.Assert.Equal(testlineitem.Product.Price, testlineitem2.Product.Price);
                Xunit.Assert.Equal(testlineitem.Product.Description, testlineitem2.Product.Description);
                Xunit.Assert.Equal(testlineitem.Product.Category, testlineitem2.Product.Category);

            }
        }

        [Fact]
        public void TestProductAdd()
        {
            //Arrange
            using (var context = new revaturedatabaseContext(_options))
            {
                //Act
                var _repo = new DataAccessLogic.RepositorySQL(context);

                Product testproduct = new Product { Id = 10110, Name = "Test", Price = 10.00m, Description = "Test", Category = "Test" };
                _repo.Add(testproduct);
                context.SaveChanges();

                Product testproduct2 = _repo.Get(new Product { Id = 10110 });

                //Assert
                Xunit.Assert.Equal(testproduct.Id, testproduct2.Id);
                Xunit.Assert.Equal(testproduct.Name, testproduct2.Name);
                Xunit.Assert.Equal(testproduct.Price, testproduct2.Price);
                Xunit.Assert.Equal(testproduct.Description, testproduct2.Description);
                Xunit.Assert.Equal(testproduct.Category, testproduct2.Category);

            }
        }

        [Fact]
        public void TestStoreDelete()
        {
            //Arrange
            using (var context = new revaturedatabaseContext(_options))
            {
                
                var _repo = new DataAccessLogic.RepositorySQL(context);
                _repo.Add(new Store { Id = 10001, Name = "TestStore2", Address = "112 test test 01101" });
                context.SaveChanges();

                //Act
                Store teststore = _repo.Get(new Store { Id = 10001 });

                Xunit.Assert.Equal(10001, teststore.Id);
                Xunit.Assert.Equal("TestStore2", teststore.Name);
                Xunit.Assert.Equal("112 test test 01101", teststore.Address);


                _repo.Delete(teststore);
                context.SaveChanges();

                List<Store> allteststores = _repo.GetAll(new Store());
                
                
                //Assert
                Xunit.Assert.False(allteststores.Contains(teststore));

            }
        }

        [Fact]
        public void TestCustomerDelete()
        {
            //Arrange
            using (var context = new revaturedatabaseContext(_options))
            {

                var _repo = new DataAccessLogic.RepositorySQL(context);
                _repo.Add(new Customer { Id = 10001, Name = "Testdelete", Address = "1 test 01101", Email = "Customer@delete.com", Phone = "123-456-7890" });
                context.SaveChanges();

                //Act
                Customer testcustomer = _repo.Get(new Customer { Id = 10001 });

                Xunit.Assert.Equal(10001, testcustomer.Id);
                Xunit.Assert.Equal("Testdelete", testcustomer.Name);
                Xunit.Assert.Equal("1 test 01101", testcustomer.Address);
                Xunit.Assert.Equal("Customer@delete.com", testcustomer.Email);
                Xunit.Assert.Equal("123-456-7890", testcustomer.Phone);

                _repo.Delete(testcustomer);
                context.SaveChanges();

                List<Customer> alltestcustomers = _repo.GetAll(new Customer());

                //Assert
                Xunit.Assert.False(alltestcustomers.Contains(testcustomer));
            }
        }

        [Fact]
        public void TestOrderDelete()
        {
            //Arrange
            using (var context = new revaturedatabaseContext(_options))
            {

                var _repo = new DataAccessLogic.RepositorySQL(context);
                _repo.Add(new Order { Id = 10001, Address = "1 test 01101", Active = true });
                context.SaveChanges();

                //Act
                Order testorder = _repo.Get(new Order { Id = 10001 });

                Xunit.Assert.Equal(10001, testorder.Id);
                Xunit.Assert.Equal("1 test 01101", testorder.Address);
                Xunit.Assert.True(testorder.Active);

                _repo.Delete(testorder);
                context.SaveChanges();

                List<Order> alltestorders = _repo.GetAll(new Order());

                //Assert
                Xunit.Assert.False(alltestorders.Contains(testorder));
            }
        }

        [Fact]
        public void TestLineItemDelete()
        {
            //Arrange
            using (var context = new revaturedatabaseContext(_options))
            {

                var _repo = new DataAccessLogic.RepositorySQL(context);
                _repo.Add(new LineItem { Id = 2222, Quantity = 10, Product = new Product { Id = 22221, Name = "Test", Price = 10.00m, Description = "Test", Category = "Test" } });
                context.SaveChanges();

                //Act
                LineItem testlineitem = _repo.Get(new LineItem { Id = 2222 });

                Xunit.Assert.Equal(2222, testlineitem.Id);
                Xunit.Assert.Equal(10, testlineitem.Quantity);
                Xunit.Assert.Equal(22221, testlineitem.Product.Id);
                Xunit.Assert.Equal("Test", testlineitem.Product.Name);
                Xunit.Assert.Equal(10.00m, testlineitem.Product.Price);
                Xunit.Assert.Equal("Test", testlineitem.Product.Description);
                Xunit.Assert.Equal("Test", testlineitem.Product.Category);

                _repo.Delete(testlineitem);
                context.SaveChanges();

                List<LineItem> alltestlineitems = _repo.GetAll(new LineItem());

                //Assert
                Xunit.Assert.False(alltestlineitems.Contains(testlineitem));
            }
        }

        [Fact]
        public void TestProductDelete()
        {
            //Arrange
            using (var context = new revaturedatabaseContext(_options))
            {

                var _repo = new DataAccessLogic.RepositorySQL(context);
                _repo.Add(new Product { Id = 10110, Name = "Test", Price = 10.00m, Description = "Test", Category = "Test" });
                context.SaveChanges();

                //Act
                Product testproduct = _repo.Get(new Product { Id = 10110 });

                Xunit.Assert.Equal(10110, testproduct.Id);
                Xunit.Assert.Equal("Test", testproduct.Name);
                Xunit.Assert.Equal(10.00m, testproduct.Price);
                Xunit.Assert.Equal("Test", testproduct.Description);
                Xunit.Assert.Equal("Test", testproduct.Category);

                _repo.Delete(testproduct);
                context.SaveChanges();

                List<Product> alltestproducts = _repo.GetAll(new Product());

                //Assert
                Xunit.Assert.False(alltestproducts.Contains(testproduct));
            }
        }

        [Fact]
        public void TestStoreUpdate()
        {
            //Arrange
            using (var context = new revaturedatabaseContext(_options))
            {

                var _repo = new DataAccessLogic.RepositorySQL(context);
                _repo.Add(new Store { Id = 10001, Name = "TestStore2", Address = "112 test test 01101" });
                context.SaveChanges();

                //Act
                Store teststore = _repo.Get(new Store { Id = 10001 });

                Xunit.Assert.Equal(10001, teststore.Id);
                Xunit.Assert.Equal("TestStore2", teststore.Name);
                Xunit.Assert.Equal("112 test test 01101", teststore.Address);

                teststore.Name = "TestStore3";
                teststore.Address = "112 test test 01102";

                _repo.Update(teststore);
                context.SaveChanges();

                Store teststore2 = _repo.Get(new Store { Id = 10001 });

                //Assert
                Xunit.Assert.Equal(10001, teststore2.Id);
                Xunit.Assert.Equal("TestStore3", teststore2.Name);
                Xunit.Assert.Equal("112 test test 01102", teststore2.Address);

            }
        }

        [Fact]
        public void TestCustomerUpdate()
        {
            //Arrange
            using (var context = new revaturedatabaseContext(_options))
            {

                var _repo = new DataAccessLogic.RepositorySQL(context);
                _repo.Add(new Customer { Id = 10001, Name = "TestCustomer2", Address = "112 test test 01101", Email = "Customer@update.com", Phone = "123-456-7890" });
                context.SaveChanges();

                //Act
                Customer testcustomer = _repo.Get(new Customer { Id = 10001 });

                Xunit.Assert.Equal(10001, testcustomer.Id);
                Xunit.Assert.Equal("TestCustomer2", testcustomer.Name);
                Xunit.Assert.Equal("112 test test 01101", testcustomer.Address);
                Xunit.Assert.Equal("Customer@update.com", testcustomer.Email);
                Xunit.Assert.Equal("123-456-7890", testcustomer.Phone);

                testcustomer.Name = "Customerupdate3";
                testcustomer.Address = "4545 Sick burn st 78451";
                testcustomer.Email = "SSmcflurry@icecream.com";
                testcustomer.Phone = "123-456-7770";

                _repo.Update(testcustomer);
                context.SaveChanges();

                Customer testcustomer2 = _repo.Get(new Customer { Id = 10001 });

                //Assert
                Xunit.Assert.Equal(10001, testcustomer2.Id);
                Xunit.Assert.Equal("Customerupdate3", testcustomer2.Name);
                Xunit.Assert.Equal("4545 Sick burn st 78451", testcustomer2.Address);
                Xunit.Assert.Equal("SSmcflurry@icecream.com", testcustomer2.Email);
                Xunit.Assert.Equal("123-456-7770", testcustomer2.Phone);

            }
        }

        [Fact]
        public void TestOrderUpdate()
        {
            //Arrange
            using (var context = new revaturedatabaseContext(_options))
            {

                var _repo = new DataAccessLogic.RepositorySQL(context);
                _repo.Add(new Order { Id = 10001, Address = "1 test 01101", Active = true });
                context.SaveChanges();

                //Act
                Order testorder = _repo.Get(new Order { Id = 10001 });

                Xunit.Assert.Equal(10001, testorder.Id);
                Xunit.Assert.Equal("1 test 01101", testorder.Address);
                Xunit.Assert.True(testorder.Active);

                testorder.Address = "1 test 01102";
                testorder.Active = false;

                _repo.Update(testorder);
                context.SaveChanges();

                Order testorder2 = _repo.Get(new Order { Id = 10001 });

                //Assert
                Xunit.Assert.Equal(10001, testorder2.Id);
                Xunit.Assert.Equal("1 test 01102", testorder2.Address);
                Xunit.Assert.False(testorder2.Active);

            }
        }

        [Fact]
        public void TestLineItemUpdate()
        {
            //Arrange
            using (var context = new revaturedatabaseContext(_options))
            {

                var _repo = new DataAccessLogic.RepositorySQL(context);
                _repo.Add(new LineItem { Id = 22111, Quantity = 10, Product = new Product { Id = 221111, Name = "Test", Price = 10.00m, Description = "Test", Category = "Test" } });
                context.SaveChanges();

                //Act
                LineItem testlineitem = _repo.Get(new LineItem { Id = 22111 });

                Xunit.Assert.Equal(22111, testlineitem.Id);
                Xunit.Assert.Equal(10, testlineitem.Quantity);
                Xunit.Assert.Equal(221111, testlineitem.Product.Id);
                Xunit.Assert.Equal("Test", testlineitem.Product.Name);
                Xunit.Assert.Equal(10.00m, testlineitem.Product.Price);
                Xunit.Assert.Equal("Test", testlineitem.Product.Description);
                Xunit.Assert.Equal("Test", testlineitem.Product.Category);

                testlineitem.Quantity = 20;
                testlineitem.Product.Name = "Test2";
                testlineitem.Product.Price = 20.00m;
                testlineitem.Product.Description = "Test2";
                testlineitem.Product.Category = "Test2";

                _repo.Update(testlineitem);
                context.SaveChanges();

                LineItem testlineitem2 = _repo.Get(new LineItem { Id = 22111 });

                //Assert
                Xunit.Assert.Equal(22111, testlineitem2.Id);
                Xunit.Assert.Equal(20, testlineitem2.Quantity);
                Xunit.Assert.Equal(221111, testlineitem2.Product.Id);
                Xunit.Assert.Equal("Test2", testlineitem2.Product.Name);
                Xunit.Assert.Equal(20.00m, testlineitem2.Product.Price);
                Xunit.Assert.Equal("Test2", testlineitem2.Product.Description);
                Xunit.Assert.Equal("Test2", testlineitem2.Product.Category);

            }
        }

        [Fact]
        public void TestProductUpdate()
        {
            //Arrange
            using (var context = new revaturedatabaseContext(_options))
            {

                var _repo = new DataAccessLogic.RepositorySQL(context);
                _repo.Add(new Product { Id = 10110, Name = "Test", Price = 10.00m, Description = "Test", Category = "Test" });
                context.SaveChanges();

                //Act
                Product testproduct = _repo.Get(new Product { Id = 10110 });

                Xunit.Assert.Equal(10110, testproduct.Id);
                Xunit.Assert.Equal("Test", testproduct.Name);
                Xunit.Assert.Equal(10.00m, testproduct.Price);
                Xunit.Assert.Equal("Test", testproduct.Description);
                Xunit.Assert.Equal("Test", testproduct.Category);

                testproduct.Name = "Test2";
                testproduct.Price = 20.00m;
                testproduct.Description = "Test2";
                testproduct.Category = "Test2";

                _repo.Update(testproduct);
                context.SaveChanges();

                Product testproduct2 = _repo.Get(new Product { Id = 10110 });

                //Assert
                Xunit.Assert.Equal(10110, testproduct2.Id);
                Xunit.Assert.Equal("Test2", testproduct2.Name);
                Xunit.Assert.Equal(20.00m, testproduct2.Price);
                Xunit.Assert.Equal("Test2", testproduct2.Description);
                Xunit.Assert.Equal("Test2", testproduct2.Category);

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
                        Phone = "123-456-7890",
                        Orders = new List<Order>{
                            new Order{
                                Id = 11101,
                                Address = "112 test test 01101",
                                Active = true,
                                LineItems = new List<LineItem>{
                                    new LineItem{
                                        Id = 10100,
                                        Quantity = 10,
                                        Total = 100.00m,
                                        Product = new Product{
                                            Id = 11111,
                                            Name = "Test",
                                            Price = 10.00m,
                                            Description = "Test",
                                            Category = "Test"
                                        }},
                                    new LineItem{
                                        Id = 10001,
                                        Quantity = 10,
                                        Total = 100.00m,
                                        Product = new Product{
                                            Id = 11110,
                                            Name = "Test2",
                                            Price = 10.00m,
                                            Description = "Test2",
                                            Category = "Test2",
                                        }}}},
                            new Order{
                                Id = 11102,
                                Address = "112 test test 01101",
                                Active = true,
                                LineItems = new List<LineItem>{
                                    new LineItem{
                                        Id = 11010,
                                        Quantity = 10,
                                        Total = 100.00m,
                                        Product = new Product{
                                            Id = 11100,
                                            Name = "Test",
                                            Price = 10.00m,
                                            Description = "Test",
                                            Category = "Test"
                                        }},
                                    new LineItem{
                                        Id = 001111,
                                        Quantity = 10,
                                        Total = 100.00m,
                                        Product = new Product{
                                            Id = 11000,
                                            Name = "Test2",
                                            Price = 10.00m,
                                            Description = "Test2",
                                            Category = "Test2"
                                    }}}}}});
                context.Stores.AddRange(
                    new Store{
                        Id = 10000,
                        Name = "Test Store",
                        Address = "112 test test 01101",
                        Orders = new List<Order>{
                            new Order{
                                Id = 11001,
                                Address = "112 test test 01101",
                                Active = true,
                                LineItems = new List<LineItem>{
                                    new LineItem{
                                        Id = 11110,
                                        Quantity = 10,
                                        Total = 100.00m,
                                        Product = new Product{
                                            Id = 10000,
                                            Name = "Test",
                                            Price = 10.00m,
                                            Description = "Test",
                                            Category = "Test",
                                        }},
                                    new LineItem{
                                        Id = 11111,
                                        Quantity = 10,
                                        Total = 100.00m,
                                        Product = new Product{
                                            Id = 10001,
                                            Name = "Test2",
                                            Price = 10.00m,
                                            Description = "Test2",
                                            Category = "Test2",
                                        }}}},
                            new Order{
                                Id = 11112,
                                Address = "112 test test 01101",
                                Active = true,
                                LineItems = new List<LineItem>{
                                    new LineItem{
                                        Id = 10010,
                                        Quantity = 10,
                                        Total = 100.00m,
                                        Product = new Product{
                                            Id = 10011,
                                            Name = "Test",
                                            Price = 10.00m,
                                            Description = "Test",
                                            Category = "Test2",
                                        }},
                                    new LineItem{
                                        Id = 00111,
                                        Quantity = 10,
                                        Total = 100.00m,
                                        Product = new Product{
                                            Id = 10111,
                                            Name = "Test2",
                                            Price = 10.00m,
                                            Description = "Test2",
                                            Category = "Test2"
                                    }}}}}});
                        context.SaveChanges();
            }         
}}}       