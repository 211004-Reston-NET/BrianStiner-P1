using System;
using System.Collections.Generic;
using Models;


/* The logic to perform CRUD operations on the models.
Contains repository specific logic on storing and accessing data
*/

namespace BusinessLogic
{
    public interface IBusiness
    {
    
        /// <summary> These will pass a Class to our _repo database </summary>
        /// <param name="p_IC">This is the IClass we will be adding to the database</param>
        void Add(Customer p_IC);
        void Add(Store p_IC);
        void Add(Order p_IC);
        void Add(LineItem p_IC);
        void Add(Product p_IC);


        /// <summary> These will pass a Class to the database. </summary>
        /// <returns>It will return a list of Classes</returns>
        List<Customer> GetAll(Customer p_IC);
        List<Store> GetAll(Store p_IC);
        List<Order> GetAll(Order p_IC);
        List<LineItem> GetAll(LineItem p_IC);
        List<Product> GetAll(Product p_IC);


        /// <summary> These will pass a Class to the database for deletion. </summary>
        void Delete(Customer p_IC);
        void Delete(Store p_IC);
        void Delete(Order p_IC);
        void Delete(LineItem p_IC);
        void Delete(Product p_IC);

        /// <summary> These return a Class from the database that matches the Id </summary>
        Customer Get(Customer p_IC);
        Store Get(Store p_IC);
        Order Get(Order p_IC);
        LineItem Get(LineItem p_IC);
        Product Get(Product p_IC);

        /// <summary> These will pass a Class to the database for updating. </summary>
        void Update(Customer p_IC);
        void Update(Store p_IC);
        void Update(Order p_IC);
        void Update(LineItem p_IC);
        void Update(Product p_IC);



        /// <summary> These will pass a Class to the database. </summary>
        /// <returns>It will return a list of Classes</returns>
        List<Customer> Search(Customer p_IC, string p_search);
        List<Store> Search(Store p_IC, string p_search);
        List<Order> Search(Order p_IC, string p_search);
        List<LineItem> Search(LineItem p_IC, string p_search);
        List<Product> Search(Product p_IC, string p_search);

    }
}