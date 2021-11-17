using System.Collections.Generic;
using System.Linq;
using Models;
using DataAccessLogic;
using System.Collections;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLogic
{
    public class RepositorySQL : IRepository
    {
        private revaturedatabaseContext _context;
        public RepositorySQL(revaturedatabaseContext p_context)
        {
            _context = p_context;
        }

        // Useful Methods in Repository: Add, Update, Delete, Get, GetAll
        // Method Add:
        // Class parameter. Overloaded 5 times. Used to add a new Class to the database.
        public void Add(Customer p_IC){ 
            _context.Add(p_IC);
            _context.SaveChanges();
        }
        public void Add(Store p_IC){
            _context.Add(p_IC);
            _context.SaveChanges();
        }
        public void Add(Order p_IC){
            _context.Entry(p_IC).State = EntityState.Added;
            _context.Entry(p_IC.Customer).State = EntityState.Modified;
            _context.SaveChanges();
        }
        public void Add(LineItem p_IC){ 
            _context.Entry(p_IC).State = EntityState.Added;
            _context.Entry(p_IC.Order).State = EntityState.Modified;
            _context.Entry(p_IC.Product).State = EntityState.Unchanged;
            _context.SaveChanges();
        }
        public void Add(InventoryItem p_IC){
            _context.Entry(p_IC).State = EntityState.Added;
            _context.Entry(p_IC.Store).State = EntityState.Modified;
            _context.Entry(p_IC.Product).State = EntityState.Unchanged;
            _context.SaveChanges();
        }
        public void Add(Product p_IC){
            _context.Add(p_IC);
            _context.SaveChanges();
        }
        public void Add(User p_IC){
            _context.Add(p_IC);
            _context.SaveChanges();
        }

        // Method Delete:
        // Class parameter. Overloaded 5 times. Used to delete a Class from the database.
        public void Delete(Customer p_IC){
            _context.Remove(p_IC);
            _context.SaveChanges();
        }
        public void Delete(Store p_IC){
            _context.Remove(p_IC);
            _context.SaveChanges();
        }
        public void Delete(Order p_IC){
            foreach(LineItem lineItem in p_IC.LineItems){
                _context.Entry(lineItem).State = EntityState.Deleted;
                _context.Entry(lineItem.Product).State = EntityState.Detached;
                _context.Entry(lineItem.Order).State = EntityState.Modified;
            }
            _context.SaveChanges();
            _context.Entry(p_IC).State = EntityState.Deleted;
            _context.SaveChanges();
        }
        public void Delete(LineItem p_IC){
            _context.Entry(p_IC).State = EntityState.Deleted;
            _context.Entry(p_IC.Product).State = EntityState.Detached;
            _context.SaveChanges();
        }
        public void Delete(InventoryItem p_IC){
            _context.Entry(p_IC.Store).State = EntityState.Detached;
            _context.Entry(p_IC).State = EntityState.Deleted;
            _context.Entry(p_IC.Product).State = EntityState.Detached;
            
            _context.SaveChanges();
        }
        public void Delete(Product p_IC){
            _context.Remove(p_IC);
            _context.SaveChanges();
        }
        public void Delete(User p_IC){
            _context.Remove(p_IC);
            _context.SaveChanges();
        }

        //Method GetAll:
        // Class parameter. Overloaded 5 times. Uses Linq to grab all classes from the database.
        public List<Customer> GetAll(Customer p_IC){
            var i = (from c in _context.Customers
                    select new Customer
                    {
                        Id = c.Id,
                        Name = c.Name,
                        Phone = c.Phone,
                        Email = c.Email,
                        Address = c.Address,
                        TotalSpent = c.TotalSpent,
                        Orders = (from o in _context.Orders
                                where o.CustomerId == c.Id
                                select new Order
                                {
                                    Id = o.Id,
                                    CustomerId = o.CustomerId,
                                    Address = o.Address,
                                    Active = o.Active,
                                    LineItems = (from li in _context.LineItems
                                                where li.OrderId == o.Id
                                                select new LineItem
                                                {
                                                    Id = li.Id,
                                                    OrderId = li.OrderId,
                                                    ProductId = li.ProductId,
                                                    Quantity = li.Quantity,
                                                    Product = (from p in _context.Products
                                                            where p.Id == li.ProductId
                                                            select new Product
                                                            {
                                                                Id = p.Id,
                                                                Name = p.Name,
                                                                Description = p.Description,
                                                                Category = p.Category,
                                                                Price = p.Price,
                                                            }).FirstOrDefault()
                                                }).ToList()
                                }).ToList()
                    }).ToList();
            return i;

        }
        public List<Store> GetAll(Store p_IC){
            var stores = (from s in _context.Stores
                          select new Store
                          {
                              Id = s.Id,
                              Name = s.Name,
                              Address = s.Address,
                              Revenue = s.Revenue,
                              Expenses = s.Expenses,
                              Inventory = ( from i in _context.Inventory
                                            where i.StoreId == s.Id
                                            select new InventoryItem
                                            {
                                                Id = i.Id,
                                                StoreId = s.Id,
                                                ProductId = i.ProductId,
                                                Quantity = i.Quantity,
                                                Product = ( from p in _context.Products
                                                            where p.Id == i.ProductId
                                                            select new Product
                                                            {
                                                                Id = p.Id,
                                                                Name = p.Name,
                                                                Description = p.Description,
                                                                Category = p.Category,
                                                                Price = p.Price,
                                                            }).FirstOrDefault()
                                             }).ToList()
                            }).ToList();
            return stores;
        }
        public List<Order> GetAll(Order p_IC){
            var orders =   (from o in _context.Orders
                            select new Order
                            {
                                Id = o.Id,
                                CustomerId = o.CustomerId,
                                Address = o.Address,
                                Active = o.Active,
                                LineItems = (   from li in _context.LineItems
                                                where li.OrderId == o.Id
                                                select new LineItem
                                                {
                                                    Id = li.Id,
                                                    OrderId = o.Id,
                                                    ProductId = li.ProductId,
                                                    Quantity = li.Quantity,
                                                    Product = ( from p in _context.Products
                                                                where p.Id == li.ProductId
                                                                select new Product
                                                                {
                                                                    Id = p.Id,
                                                                    Name = p.Name,
                                                                    Description = p.Description,
                                                                    Category = p.Category,
                                                                    Price = p.Price,
                                                                }).FirstOrDefault()
                                              }).ToList()
                            }).ToList();
            return orders;
        }                                     
        public List<LineItem> GetAll(LineItem p_IC){
            return _context.LineItems.Include(li => li.Product).Include(li => li.Order).ToList();
        }
        public List<InventoryItem> GetAll(InventoryItem p_IC){
            var inv = _context.Inventory.Include(i => i.Product).Include(i => i.Store).ToList();
            return inv;
        }                                                     
        public List<Product> GetAll(Product p_IC){
            var pro = _context.Products.ToList();
            return pro;
        }
        public List<User> GetAll(User p_IC){
            return _context.Users.ToList();
        }

        // Method Get. 
        // Matches ID to an entry in the database. Grabs all info from the database and returns it as a class.
        public Customer Get(Customer p_IC){
            return GetAll(p_IC).Where(x => x.Id == p_IC.Id).FirstOrDefault();  
        }
        public Store Get(Store p_IC){
            return GetAll(p_IC).Where(x => x.Id == p_IC.Id).FirstOrDefault();
        }
        public Order Get(Order p_IC){
            return GetAll(p_IC).Where(x => x.Id == p_IC.Id).FirstOrDefault();
        }
        public LineItem Get(LineItem p_IC){
            return GetAll(p_IC).Where(x => x.Id == p_IC.Id).FirstOrDefault();
        }
        public InventoryItem Get(InventoryItem p_IC){
            return GetAll(p_IC).Where(x => x.Id == p_IC.Id).FirstOrDefault();
        }
        public Product Get(Product p_IC){
            return GetAll(p_IC).Where(x => x.Id == p_IC.Id).FirstOrDefault();
        }
        public User Get(User p_IC){
            return GetAll(p_IC).Where(x => x.Id == p_IC.Id).FirstOrDefault();
        }

        // Method Update:
        // Class parameter. Overloaded 5 times. Used to update a class in the database.
        public void Update(Customer p_IC){
            _context.Entry(p_IC).State = EntityState.Modified;
            foreach (var order in p_IC.Orders)
            {
                _context.Entry(order).State = EntityState.Modified;
                foreach (var lineItem in order.LineItems)
                {
                    _context.Entry(lineItem).State = EntityState.Modified;
                    _context.Entry(lineItem.Product).State = EntityState.Detached;
                }
            }
            _context.SaveChanges();
        }
        public void Update(Store p_IC){
            _context.Entry(p_IC).State = EntityState.Modified;
            foreach (var inv in p_IC.Inventory)
            {
                _context.Entry(inv).State = EntityState.Modified;
            }
            _context.SaveChanges();
        }
        public void Update(Order p_IC){
            _context.Update(p_IC);
            _context.SaveChanges();
        }
        public void Update(LineItem p_IC){
            _context.Update(p_IC);
            _context.SaveChanges();
        }
        public void Update(InventoryItem p_IC){
            _context.Update(p_IC);
            _context.SaveChanges();
        }
        public void Update(Product p_IC){
            _context.Update(p_IC);
            _context.SaveChanges();
        }
        public void Update(User p_IC){
            _context.Update(p_IC);
            _context.SaveChanges();
        }

        // Method Search: 
        //Class parameter. Overloaded 5 times. Used to search the database and return all classes that match the search criteria.
        public List<Customer> Search(Customer p_IC, string p_Search){
            return _context.Customers.Where(IC => IC.Name.Contains(p_Search) || IC.Email.Contains(p_Search) || IC.Phone.Contains(p_Search) || IC.Address.Contains(p_Search)).ToList();
        }
        public List<Store> Search(Store p_IC, string p_Search){
            return _context.Stores.Where(IC => IC.Name.Contains(p_Search) || IC.Address.Contains(p_Search)).ToList(); 
        }
        public List<Order> Search(Order p_IC, string p_search){
            return _context.Orders.Where(IC => IC.Address.Contains(p_search)).ToList();
        }
        public List<LineItem> Search(LineItem p_IC, string p_Search){
            return _context.LineItems.Where(IC => IC.Quantity.ToString().Contains(p_Search) || IC.Product.Name.Contains(p_Search) || IC.Product.Description.Contains(p_Search)|| IC.Product.Category.Contains(p_Search)).ToList();
        }
        public List<InventoryItem> Search(InventoryItem p_IC, string p_Search){
            return _context.Inventory.Where(IC => IC.Product.Name.Contains(p_Search) || IC.Product.Description.Contains(p_Search) || IC.Product.Category.Contains(p_Search) || IC.Store.Name.Contains(p_Search) || IC.Store.Address.Contains(p_Search)).ToList();
        }
        public List<Product> Search(Product p_IC, string p_Search){
            return _context.Products.Where(IC => IC.Name.Contains(p_Search) || IC.Description.Contains(p_Search) || IC.Category.Contains(p_Search) || IC.Price.ToString().Contains(p_Search)).ToList();
        }
        public List<User> Search(User p_IC, string p_Search){
            return _context.Users.Where(IC => IC.Username.Contains(p_Search) || IC.Email.Contains(p_Search) || IC.Phone.Contains(p_Search)).ToList();
        }


        //Search all
        public ArrayList SearchAll(string p_Search){
            ArrayList list = new ArrayList();
            
            foreach(var item in Search(new Customer(), p_Search)){list.Add(item.ToStringList());}
            foreach(var item in Search(new Store(), p_Search)){list.Add(item.ToStringList());}
            foreach(var item in Search(new Order(), p_Search)){list.Add(item.ToStringList());}
            foreach(var item in Search(new Product(), p_Search)){list.Add(item.ToStringList());}
            foreach(var item in Search(new User(), p_Search)){list.Add(item.ToStringList());}

            return list;
        }

    }
}