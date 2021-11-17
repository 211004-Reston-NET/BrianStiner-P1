using System;
using System.Collections;
using System.Collections.Generic;
using Models;


/* The logic to perform CRUD operations on the models.
Contains repository specific logic on storing and accessing data
*/

namespace BusinessLogic
{
    public interface IBusiness
    {

        public void TransactOrders(Customer customer);
        // public void TransactOrders(Store store);
    
        // The IsValid methods use regex to check if the input is valid
        public bool IsValidName(string name);
        public bool IsValidUsername(string username);
        public bool IsValidEmail(string email);
        public bool IsValidPhone(string phone);
        public bool IsValidAddress(string address);
        public bool IsValidPassword(string password);
        public bool IsValidPrice(decimal price);
        public bool IsValidQuantity(int quantity);


        // User methods for dealing with password security and user authentication.
        public bool IsEqual(string password, string password2);
        public string SaltedHashPassword( string p_salt, string p_unhashedpassword); //The unhashed password traveling from form, to controller, to the business logic is an ignorable security risk.
        public bool CheckPassword(string p_passwordtocheck, User p_User);
        public bool IsUniqueUsername(string p_username);
        public bool CreateUser(string p_username, string p_unhashedpassword1, string p_unhashedpassword2, string p_email = "", string p_phone = "");
        public bool Login(string p_username, string p_passwordtocheck);
        
        public bool IsValidCustomer(Customer customer);
        public bool IsValidProduct(Product product);
        public bool IsValidStore(Store store);
        public bool IsValidOrder(Order order);
        public bool IsValidLineItem(LineItem lineItem);
        
        /// <summary> These will pass a Class to our _repo database </summary>
        /// <param name="p_IC">This is the IClass we will be adding to the database</param>
        void Add(Customer p_IC);
        void Add(Store p_IC);
        void Add(Order p_IC);
        void Add(LineItem p_IC);
        void Add(InventoryItem p_IC);
        void Add(Product p_IC);


        /// <summary> These will pass a Class to the database. </summary>
        /// <returns>It will return a list of Classes</returns>
        List<Customer> GetAll(Customer p_IC);
        List<Store> GetAll(Store p_IC);
        List<Order> GetAll(Order p_IC);
        List<LineItem> GetAll(LineItem p_IC);
        List<InventoryItem> GetAll(InventoryItem p_IC);
        List<Product> GetAll(Product p_IC);


        /// <summary> These will pass a Class to the database for deletion. </summary>
        void Delete(Customer p_IC);
        void Delete(Store p_IC);
        void Delete(Order p_IC);
        void Delete(LineItem p_IC);
        void Delete(InventoryItem p_IC);
        void Delete(Product p_IC);

        /// <summary> These return a Class from the database that matches the Id </summary>
        Customer Get(Customer p_IC);
        Store Get(Store p_IC);
        Order Get(Order p_IC);
        LineItem Get(LineItem p_IC);
        InventoryItem Get(InventoryItem p_IC);
        Product Get(Product p_IC);

        /// <summary> These will pass a Class to the database for updating. </summary>
        void Update(Customer p_IC);
        void Update(Store p_IC);
        void Update(Order p_IC);
        void Update(LineItem p_IC);
        void Update(InventoryItem p_IC);
        void Update(Product p_IC);



        /// <summary> These will pass a Class to the database. </summary>
        /// <returns>It will return a list of Classes</returns>
        List<Customer> Search(Customer p_IC, string p_search);
        List<Store> Search(Store p_IC, string p_search);
        List<Order> Search(Order p_IC, string p_search);
        List<LineItem> Search(LineItem p_IC, string p_search);
        List<InventoryItem> Search(InventoryItem p_IC, string p_search);
        List<Product> Search(Product p_IC, string p_search);


        /// <summary> These will Search all and return arraylist from database. </summary>
        /// <returns>It will return an arraylist</returns>
        ArrayList SearchAll(string p_search);
    }
}