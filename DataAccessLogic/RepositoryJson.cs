using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Models;
using M = Models;

/* The logic to perform CRUD operations on the models.
Contains repository specific logic on storing and accessing data
*/

namespace DataAccessLogic
{
    //Access the json files where data is stored.
    public class RepositoryJson : IRepository
    {
        private const string c_filepath = "./../DataAccessLogic/Database/";
        private string _jsonString;

        //Overridden AddClass. Receives Customer adds to Class.json file for storage.
        //We add one IClass to the file by first adding to a list of IClasses, translating that list into json serial, then overwriting to the correct file.

        public void Add(M.Customer p_IC){
            List<M.Customer> listOfCustomer = GetAll(p_IC);
            listOfCustomer.Add(p_IC);
            _jsonString = JsonSerializer.Serialize(listOfCustomer, new JsonSerializerOptions{WriteIndented=true});
            File.WriteAllText($"{c_filepath}{p_IC.Identify()}s.json", _jsonString);
        } public void Add(M.Store p_IC){
            List<M.Store> listOfStorefront = GetAll(p_IC);
            listOfStorefront.Add(p_IC);
            _jsonString = JsonSerializer.Serialize(listOfStorefront, new JsonSerializerOptions{WriteIndented=true});
            File.WriteAllText($"{c_filepath}{p_IC.Identify()}s.json", _jsonString);
        }public void Add(M.Order p_IC){
            List<M.Order> listOfOrders = GetAll(p_IC);
            listOfOrders.Add(p_IC);
            _jsonString = JsonSerializer.Serialize(listOfOrders, new JsonSerializerOptions{WriteIndented=true});
            File.WriteAllText($"{c_filepath}{p_IC.Identify()}s.json", _jsonString);
        }public void Add(M.LineItem p_IC){
            List<M.LineItem> listOflineItems = GetAll(p_IC);
            listOflineItems.Add(p_IC);
            _jsonString = JsonSerializer.Serialize(listOflineItems, new JsonSerializerOptions{WriteIndented=true});
            File.WriteAllText($"{c_filepath}{p_IC.Identify()}s.json", _jsonString);
        }public void Add(M.Product p_IC){
            List<M.Product> listOfProduct = GetAll(p_IC);
            listOfProduct.Add(p_IC);
            _jsonString = JsonSerializer.Serialize(listOfProduct, new JsonSerializerOptions{WriteIndented=true});
            File.WriteAllText($"{c_filepath}{p_IC.Identify()}s.json", _jsonString);
        }





        //File IClass will announce their place and be put in a .json they choose
        //Try to read all text from the file and turn into List. If nothings there alert and send blank List.
        public List<M.Customer> GetAll(M.Customer p_IC){ 
            try{
                _jsonString = File.ReadAllText($"{c_filepath}{p_IC.Identify()}s.json"); 
                return JsonSerializer.Deserialize<List<M.Customer>>(_jsonString);    
            }catch (System.IO.FileNotFoundException){
                Console.WriteLine($"There is no {c_filepath}{p_IC.Identify()}s.json file");Console.ReadLine();
                return new List<M.Customer>(); 
            }
            
        }public List<M.Store> GetAll(M.Store p_IC){  
            try{
                _jsonString = File.ReadAllText($"{c_filepath}{p_IC.Identify()}s.json"); 
                return JsonSerializer.Deserialize<List<M.Store>>(_jsonString);
            }catch (System.IO.FileNotFoundException){
                Console.WriteLine($"There is no {c_filepath}{p_IC.Identify()}s.json file");Console.ReadLine();
                return new List<M.Store>(); 
            }
        }public List<M.Order> GetAll(M.Order p_IC){ 
            try{
                _jsonString = File.ReadAllText($"{c_filepath}{p_IC.Identify()}s.json"); 
                return JsonSerializer.Deserialize<List<M.Order>>(_jsonString);    
            }catch (System.IO.FileNotFoundException){
                Console.WriteLine($"There is no {c_filepath}{p_IC.Identify()}s.json file");Console.ReadLine();
                return new List<M.Order>(); 
            } 
        }public List<M.LineItem> GetAll(M.LineItem p_IC){
            try{
                _jsonString = File.ReadAllText($"{c_filepath}{p_IC.Identify()}s.json"); 
                return JsonSerializer.Deserialize<List<M.LineItem>>(_jsonString);    
            }catch (System.IO.FileNotFoundException){
                Console.WriteLine($"There is no {c_filepath}{p_IC.Identify()}s.json file");Console.ReadLine();
                return new List<M.LineItem>(); 
            } 
        }public List<M.Product> GetAll(M.Product p_IC){  
            try{
                _jsonString = File.ReadAllText($"{c_filepath}{p_IC.Identify()}s.json"); 
                return JsonSerializer.Deserialize<List<M.Product>>(_jsonString);    
            }catch (System.IO.FileNotFoundException){
                Console.WriteLine($"There is no {c_filepath}{p_IC.Identify()}s.json file");Console.ReadLine();
                return new List<M.Product>(); 
            } 
        }


        //Overridden Delete a Class. Receives Class and a List<M.Class> from Class.json.
        //Uses List.Remove(p_IC) on the list then Overwriting the file with the shorter list.
        public void Delete(M.Customer p_IC){
            List<M.Customer> listOfCustomer = GetAll(p_IC);
            listOfCustomer.Remove(p_IC);
            _jsonString = JsonSerializer.Serialize(listOfCustomer, new JsonSerializerOptions{WriteIndented=true});
            File.WriteAllText($"{c_filepath}{p_IC.Identify()}s.json", _jsonString);
        }public void Delete(M.Store p_IC){
            List<M.Store> listOfStorefront = GetAll(p_IC);
            listOfStorefront.Remove(p_IC);
            _jsonString = JsonSerializer.Serialize(listOfStorefront, new JsonSerializerOptions{WriteIndented=true});
            File.WriteAllText($"{c_filepath}{p_IC.Identify()}s.json", _jsonString);
        }public void Delete(M.Order p_IC){
            List<M.Order> listOfOrders = GetAll(p_IC);
            listOfOrders.Remove(p_IC);
            _jsonString = JsonSerializer.Serialize(listOfOrders, new JsonSerializerOptions{WriteIndented=true});
            File.WriteAllText($"{c_filepath}{p_IC.Identify()}s.json", _jsonString);
        }public void Delete(M.LineItem p_IC){
            List<M.LineItem> listOflineItems = GetAll(p_IC);
            listOflineItems.Remove(p_IC);
            _jsonString = JsonSerializer.Serialize(listOflineItems, new JsonSerializerOptions{WriteIndented=true});
            File.WriteAllText($"{c_filepath}{p_IC.Identify()}s.json", _jsonString);
        }public void Delete(M.Product p_IC){
            List<M.Product> listOfProduct = GetAll(p_IC);
            listOfProduct.Remove(p_IC);
            _jsonString = JsonSerializer.Serialize(listOfProduct, new JsonSerializerOptions{WriteIndented=true});
            File.WriteAllText($"{c_filepath}{p_IC.Identify()}s.json", _jsonString);
        }

        public Customer Get(Customer p_IC)
        {
            throw new NotImplementedException();
        }

        public Store Get(Store p_IC)
        {
            throw new NotImplementedException();
        }

        public Order Get(Order p_IC)
        {
            throw new NotImplementedException();
        }

        public LineItem Get(LineItem p_IC)
        {
            throw new NotImplementedException();
        }

        public Product Get(Product p_IC)
        {
            throw new NotImplementedException();
        }

        public void Update(Customer p_IC)
        {
            throw new NotImplementedException();
        }

        public void Update(Store p_IC)
        {
            throw new NotImplementedException();
        }

        public void Update(Order p_IC)
        {
            throw new NotImplementedException();
        }

        public void Update(LineItem p_IC)
        {
            throw new NotImplementedException();
        }

        public void Update(Product p_IC)
        {
            throw new NotImplementedException();
        }

        public List<Customer> Search(Customer p_IC, string p_Search)
        {
            throw new NotImplementedException();
        }

        public List<Store> Search(Store p_IC, string p_Search)
        {
            throw new NotImplementedException();
        }

        public List<Order> Search(Order p_IC, string p_Search)
        {
            throw new NotImplementedException();
        }

        public List<LineItem> Search(LineItem p_IC, string p_Search)
        {
            throw new NotImplementedException();
        }

        public List<Product> Search(Product p_IC, string p_Search)
        {
            throw new NotImplementedException();
        }
    }
}