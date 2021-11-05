
using Models;
using System;

namespace UserInterface{
    public sealed class Current{    
        public static Customer customer;           // The current customer
        public static Store storefront;       // The current storefront


        private static readonly Current instance = new Current();    // Singleton instance for Current. This version is thread safe.
        static Current(){} 
        private Current(){}    
        public static Current Instance{get{return instance;}} 
    }  
}