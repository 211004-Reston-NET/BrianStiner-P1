using System.Collections.Generic;
using Models;
using DataAccessLogic;
using System.Text.RegularExpressions;

namespace BusinessLogic
{
    /// <summary>
    /// Handles all the business logic for the our economic application.
    /// They are in charge of further processing/sanitizing/furthur validation of data
    /// *Mostly passing data to and from the repository.*
    /// </summary>
    public class Business :IBusiness
    {
        private IRepository _repo;
        public Business(IRepository repo)
        {
            _repo = repo;
        }

        // Store buying All orders from distributor and adding to inventory
        public void TransactOrders(Store s){
            foreach(Order o in  s.Orders){
                if(o.Active){
                foreach(LineItem li in o.LineItems){
                    s.Expenses += li.Total*.7M;                                                                     //Stores get a discount from the distributor
                    if(s.Inventory.Find(inv => inv.Id == li.Id) != null){                                           //Does it exist?
                    s.Inventory.Find(inv => inv.Product.Id == li.Product.Id).Quantity += li.Quantity;               //Add if it does by merging
                    }else{s.Inventory.Add(li);}                                                                     //Add if it doesn't by adding
                }
                }   
                o.Active = false;
            }
            Update(s);
        }
        
        // Customer buying from store, increasing store revanue, decreasing store inventory, and increasing customer's totalspent
        public void TransactOrders(Customer c, Store s){
            bool orderFailure = false;
            foreach(Order o in  c.Orders){
                if(o.Active){
                foreach(LineItem li in o.LineItems){
                    if(s.Inventory.Find(inv => inv.Product.Id == li.Product.Id) != null){                               // if it exists,
                    if(s.Inventory.Find(inv => inv.Product.Id == li.Product.Id).Quantity >= li.Quantity){               // if it has enough,
                        s.Inventory.Find(inv => inv.Product.Id == li.Product.Id).Quantity -= li.Quantity;               // remove from store
                        s.Revenue += li.Total;                                                                          // add to store revenue 
                        c.TotalSpent += li.Total;                                                                       // add to customer spent                                               
                    }else{orderFailure = true; break;} 
                }   }     
                o.Active = !orderFailure;
                }}
            Update(c);
            Update(s);
        }      

        // Returns bool if string is a valid name using regex
        public bool IsValidName(string name){
            return Regex.IsMatch(name, @"^[a-zA-Z,'.\s]+$");
        }
        // Returns bool if string is a valid username using regex
        public bool IsValidUsername(string username){
            return Regex.IsMatch(username, @"^[a-zA-Z0-9_~-]+$");
        }
        // Returns bool if string is a valid email using regex
        public bool IsValidEmail(string email){
            return Regex.IsMatch(email, @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$");
        }
        // Returns bool if string is a valid phone number using regex
        public bool IsValidPhone(string phone){
            return Regex.IsMatch(phone, @"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$"); // 9 numbers with optional spaces, dashes, or periods.
        }
        // Returns bool if string is a valid address with zipcode using regex
        public bool IsValidAddress(string address){
            return Regex.IsMatch(address, @"^[0-9]{1,6}[a-zA-Z\s,.'-]+[0-9]{5}$"); // house number, street, zipcode
        }
        // Returns bool if string is a valid password using regex
        public bool IsValidPassword(string password){
            return Regex.IsMatch(password, @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,20}$"); //lowercase, uppercase, number, special character, 8-20 characters
        }
        // Returns bool if decimal is a valid price using regex
        public bool IsValidPrice(decimal price){
            return Regex.IsMatch(price.ToString(), @"^[0-9]+(\.[0-9]{1,2})?$");
        }
        // Returns bool if int is a valid quantity using regex
        public bool IsValidQuantity(int quantity){
            return 0<quantity && quantity<=999;
        }



        // Returns bool to verify that two passwords are equal
        public bool IsEqual(string password, string password2){
            return password == password2;
        }



        // Salts and Hashes a password of a User
        public string SaltedHashPassword( string p_salt, string p_unhashedpassword){
            return BCrypt.Net.BCrypt.HashPassword(p_salt + p_unhashedpassword);
        }



        // Checks if entered password matches the Customer's salted hashed password
        public bool CheckPassword(string p_passwordtocheck, User p_User){
            return BCrypt.Net.BCrypt.Verify(p_User.Username + p_passwordtocheck, p_User.Password);
        }


        
        // Returns bool if the user's username is unique
        public bool IsUniqueUsername(string p_username){
            return _repo.GetAll(new User()).Find(u => u.Username == p_username) == null ? true : false;
        }



        // combines functions to check if a username is unique and password is valid, if so, creates a new user and adds to repository
        public bool CreateUser(string p_username, string p_unhashedpassword1, string p_unhashedpassword2, string p_email = "", string p_phone = ""){
            if(p_email == null){p_email = "";} if(p_phone == null){p_phone = "";}
            if(IsUniqueUsername(p_username) && IsValidUsername(p_username)){
            if(IsValidPassword(p_unhashedpassword1) && IsEqual(p_unhashedpassword1, p_unhashedpassword2)){
                User u = new User(p_username, SaltedHashPassword(p_username, p_unhashedpassword1), p_email, p_phone);
                _repo.Add(u);
                return true;
            }}
            return false;
        }




        // Returns bool if user can login
        public bool Login(string p_username, string p_unhashedpassword){
            User u = _repo.GetAll(new User()).Find(user => user.Username == p_username);
            if(u != null){
                return CheckPassword(p_unhashedpassword, u);
            }
            return false;
        }

        public bool IsValidCustomer(Customer c){
            if(IsValidName(c.Name)){
            if(IsValidAddress(c.Address)){
            if(IsValidEmail(c.Email)){
            if(IsValidPhone(c.Phone)){
                return true;}}}}
            return false;
        }
        public bool IsValidStore(Store s){
            if(IsValidName(s.Name)){
            if(IsValidAddress(s.Address)){
                return true;}}
            return false;
        }
        public bool IsValidProduct(Product p){
            if(IsValidName(p.Name)){
            if(IsValidPrice(p.Price)){
                return true;}}
            return false;
        }



        //repo pipeline
        //Customer
        public void Add(Customer p_IC){
            _repo.Add(p_IC);       
        }
        public List<Customer> GetAll(Customer p_IC){
            return _repo.GetAll(p_IC);
        }
        public void Delete(Customer p_IC){
            _repo.Delete(p_IC);
        }
        public List<Customer> Search(Customer p_IC, string p_search){
            return _repo.Search(p_IC, p_search);
        }


        //Storefront
        public void Add(Store p_IC){
            _repo.Add(p_IC);       
        }
    public List<Store> GetAll(Store p_IC){
            return _repo.GetAll(p_IC);
        }
        public void Delete(Store p_IC){
            _repo.Delete(p_IC);       
        }
        public List<Store> Search(Store p_IC, string p_search){
            return _repo.Search(p_IC, p_search);
        }


        //Order
        public void Add(Order p_IC){
            _repo.Add(p_IC);       
        }
        public List<Order> GetAll(Order p_IC){
            return _repo.GetAll(p_IC);
        }
        public void Delete(Order p_IC){
            _repo.Delete(p_IC);       
        }
        public List<Order> Search(Order p_IC, string p_search){
            return _repo.Search(p_IC, p_search);
        }


        //LineItem
        public void Add(LineItem p_IC){
            _repo.Add(p_IC);       
        }
        public List<LineItem> GetAll(LineItem p_IC){
            return _repo.GetAll(p_IC);
        }
        public void Delete(LineItem p_IC){
            _repo.Delete(p_IC);       
        }
        public List<LineItem> Search(LineItem p_IC, string p_search){
            return _repo.Search(p_IC, p_search);
        }


        //Product
        public void Add(Product p_IC){
            _repo.Add(p_IC);       
        }
        public List<Product> GetAll(Product p_IC){
            return _repo.GetAll(p_IC);
        }
        public void Delete(Product p_IC){
            _repo.Delete(p_IC);       
        }
        public List<Product> Search(Product p_IC, string p_search){
            return _repo.Search(p_IC, p_search);
        }

        //User
        public void Add(User p_IC){
            _repo.Add(p_IC);       
        }
        public List<User> GetAll(User p_IC){
            return _repo.GetAll(p_IC);
        }
        public void Delete(User p_IC){
            _repo.Delete(p_IC);       
        }
        public List<User> Search(User p_IC, string p_search){
            return _repo.Search(p_IC, p_search);
        }



        // Methods Get: parameter class. Overloaded 5 times. Passes and returns a class with matching ID
        public Customer Get(Customer p_IC){
            return _repo.Get(p_IC);
        }
        public Store Get(Store p_IC){
            return _repo.Get(p_IC);
        }
        public Order Get(Order p_IC){
            return _repo.Get(p_IC);
        }
        public LineItem Get(LineItem p_IC){
            return _repo.Get(p_IC);
        }
        public Product Get(Product p_IC){
            return _repo.Get(p_IC);
        }
        public User Get(User p_IC){
            return _repo.Get(p_IC);
        }


        // Methods Update: parameter class. Overloaded 5 times. Passes a Class to update the matching ID.
        public void Update(Customer p_IC){
            _repo.Update(p_IC);
        }
        public void Update(Store p_IC){
            _repo.Update(p_IC);
        }
        public void Update(Order p_IC){
            _repo.Update(p_IC);
        }
        public void Update(LineItem p_IC){
            _repo.Update(p_IC);
        }
        public void Update(Product p_IC){
            _repo.Update(p_IC);
        }
        public void Update(User p_IC){
            _repo.Update(p_IC);
        }

        
    }
}