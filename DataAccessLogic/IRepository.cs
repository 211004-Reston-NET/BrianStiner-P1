using System.Collections;
using System.Collections.Generic;
using M = Models;

namespace DataAccessLogic
{
    public interface IRepository
    {
        // Useful Methods in Repository: Add, Delete, GetAll, Get, Update



        /// <summary>
        /// These will add a Class in our database
        /// </summary>
        /// <param name="p_IC">This is the Class we will be adding to the database</M.param>
        void Add(M.Customer p_IC);
        void Add(M.Store p_IC);
        void Add(M.Order p_IC);
        void Add(M.LineItem p_IC);
        void Add(M.InventoryItem p_IC);
        void Add(M.Product p_IC);
        void Add(M.User p_IC);


        /// <summary>
        /// These will delete a Class in our database
        /// </summary>
        /// <param name="p_IC">This is the Class we will be deleting from the database</M.param>
        void Delete(M.Customer p_IC);
        void Delete(M.Store p_IC);
        void Delete(M.Order p_IC);
        void Delete(M.LineItem p_IC);
        void Delete(M.InventoryItem p_IC);
        void Delete(M.Product p_IC);
        void Delete(M.User p_IC);


        /// <summary>
        /// These will return a list of Classes stored in the database.
        /// </summary>
        /// <returns>It will return a list of Classes</returns>
        List<M.Customer> GetAll(M.Customer p_IC);
        List<M.Store> GetAll(M.Store p_IC);
        List<M.Order> GetAll(M.Order p_IC);
        List<M.LineItem> GetAll(M.LineItem p_IC);
        List<M.InventoryItem> GetAll(M.InventoryItem p_IC);
        List<M.Product> GetAll(M.Product p_IC);
        List<M.User> GetAll(M.User p_IC);

        /// <summary>
        /// These will return a Class stored in the database.
        /// </summary>
        /// <returns>It will return a Class</returns>
        M.Customer Get(M.Customer p_IC);
        M.Store Get(M.Store p_IC);
        M.Order Get(M.Order p_IC);
        M.LineItem Get(M.LineItem p_IC);
        M.InventoryItem Get(M.InventoryItem p_IC);
        M.Product Get(M.Product p_IC);
        M.User Get(M.User p_IC);

        /// <summary>
        /// These will update a Class in our database
        /// </summary>
        /// <param name="p_IC">This is the Class we will be updating in the database</M.param>
        void Update(M.Customer p_IC);
        void Update(M.Store p_IC);
        void Update(M.Order p_IC);
        void Update(M.LineItem p_IC);
        void Update(M.InventoryItem p_IC);
        void Update(M.Product p_IC);
        void Update(M.User p_IC);

        /// <summary>
        /// These will return all Classes that match the search criteria
        /// </summary>
        /// <param name="p_IC">This is the Class we will be searching for in the database</M.param>
        /// <param name="p_Search">This is the search criteria</param>
        /// <returns>It will return a list of Classes</returns>
        List<M.Customer> Search(M.Customer p_IC, string p_Search);
        List<M.Store> Search(M.Store p_IC, string p_Search);
        List<M.Order> Search(M.Order p_IC, string p_Search);
        List<M.LineItem> Search(M.LineItem p_IC, string p_Search);
        List<M.InventoryItem> Search(M.InventoryItem p_IC, string p_Search);
        List<M.Product> Search(M.Product p_IC, string p_Search);
        List<M.User> Search(M.User p_IC, string p_Search);

        ArrayList SearchAll(string p_Search);
        
    }
}