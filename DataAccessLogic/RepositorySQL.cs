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
            _context.Add(p_IC);
            _context.SaveChanges();
        }
        public void Add(LineItem p_IC){
            _context.Add(p_IC);
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
            _context.Remove(p_IC);
            _context.SaveChanges();
        }
        public void Delete(LineItem p_IC){
            _context.Remove(p_IC);
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
            return _context.Customers.ToList();
        }
        public List<Store> GetAll(Store p_IC){
            return _context.Stores.ToList();   
        }
        public List<Order> GetAll(Order p_IC){
            return _context.Orders.ToList();  
        }                                     
        public List<LineItem> GetAll(LineItem p_IC){
            return _context.LineItems.ToList();
        }                                                     
        public List<Product> GetAll(Product p_IC){
            return _context.Products.ToList();
        }
        public List<User> GetAll(User p_IC){
            return _context.Users.ToList();
        }

        // Method Get. 
        // Matches ID to an entry in the database. Grabs all info from the database and returns it as a class.
        public Customer Get(Customer p_IC){
            return _context.Customers.Where(x => x.Id == p_IC.Id).FirstOrDefault();  
        }
        public Store Get(Store p_IC){
            return _context.Stores.Where(x => x.Id == p_IC.Id).FirstOrDefault();
        }
        public Order Get(Order p_IC){
            return _context.Orders.Where(x => x.Id == p_IC.Id).FirstOrDefault();
        }
        public LineItem Get(LineItem p_IC){
            return _context.LineItems.Where(x => x.Id == p_IC.Id).FirstOrDefault();
        }
        public Product Get(Product p_IC){
            return _context.Products.Where(x => x.Id == p_IC.Id).FirstOrDefault();
        }
        public User Get(User p_IC){
            return _context.Users.Where(x => x.Id == p_IC.Id).FirstOrDefault();
        }

        // Method Update:
        // Class parameter. Overloaded 5 times. Used to update a class in the database.
        public void Update(Customer p_IC){
            _context.Update(p_IC);
            _context.SaveChanges();
        }
        public void Update(Store p_IC){
            _context.Update(p_IC);
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
        public List<Product> Search(Product p_IC, string p_Search){
            return _context.Products.Where(IC => IC.Name.Contains(p_Search) || IC.Description.Contains(p_Search) || IC.Category.Contains(p_Search) || IC.Price.ToString().Contains(p_Search)).ToList();
        }
        public List<User> Search(User p_IC, string p_Search){
            return _context.Users.Where(IC => IC.Username.Contains(p_Search) || IC.Email.Contains(p_Search) || IC.Phone.Contains(p_Search)).ToList();
        }


        //Search all
        public ArrayList SearchAll(string p_Search){
            ArrayList list = new ArrayList();
            list.Add(Search(new Customer(), p_Search));
            list.Add(Search(new Store(), p_Search));
            list.Add(Search(new Order(), p_Search));
            list.Add(Search(new LineItem(), p_Search));
            list.Add(Search(new Product(), p_Search));
            list.Add(Search(new User(), p_Search));
            return list;
        }
    }
}