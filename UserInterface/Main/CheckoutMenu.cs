using System;
using System.Collections.Generic;
using BusinessLogic;
using Toolbox;
using Models;

namespace UserInterface{
    public class CheckoutMenu : IMenu{
        MenuBuilder Builder; IBusiness BL;
        public CheckoutMenu(IBusiness BusinessLogic){
            BL = BusinessLogic;
            Builder = new MenuBuilder(BL);
        }
        public void Display(){
            Builder.ResetPause(new List<string>()
            {"Checkout time!",
            "We'll auto-purchase your orders",
            "and ring you up!"});   
                
            var c = Current.customer;
            var s = Current.storefront;

            // Store buying All orders from distributor and adding to Inventory
            if(s.Orders.Count > 0){
                Builder.ResetPause(s.ToStringList());
                foreach(Order o in  s.Orders){
                    if(o.Active){
                    foreach(LineItem li in o.LineItems){
                        s.Expenses += li.Total*.7M;                                                                                       //Stores get a discount from the distributor
                        if(s.Inventory.Find(inv => inv.ProductId == li.ProductId) != null){                                      //does it exist?
                        s.Inventory.Find(inv => inv.ProductId == li.ProductId).Quantity += li.Quantity;       //add if it does by merging
                        }else{s.Inventory.Add(li);}                                                                      //add if it doesn't by adding
                    }
                    }   
                    o.Active = false;
                    Builder.Add($"Order {o.Id} with {o.LineItems.Count} items complete.");     
                } 
            }
            // Customer buying from store, increasing store revanue, decreasing store Inventory, and increasing customer's totalspent
            bool orderSuccess = true;
            if(c.Orders.Count > 0){
                Builder.ResetPause(c.ToStringList());
                foreach(Order o in  c.Orders){
                if(o.Active){
                foreach(LineItem li in o.LineItems){
                    if(s.Inventory.Find(inv => inv.ProductId == li.ProductId) != null){                                           // if it exists,
                    if(s.Inventory.Find(inv => inv.ProductId == li.ProductId).Quantity >= li.Quantity){        // if it has enough,
                        s.Inventory.Find(inv => inv.ProductId == li.ProductId).Quantity -= li.Quantity;        // remove from store
                        s.Revenue += li.Total;                                                                                             // add to store revenue 
                        c.TotalSpent += li.Total;                                                                                          // add to customer spent                                               
                    }else{
                        Builder.ResetPause(new List<string>()
                        {"Sorry, we don't have enough of",
                        $"{li.Product.Name} to complete your order"});  
                        orderSuccess = false; break;
                    }}}                                                                                     
                Builder.Add($"Order {o.Id} with {o.LineItems.Count} items complete.");                                                        // if it doesn't exist,
                o.Active = !orderSuccess;
                }}   
            }
            

            Current.customer = c;
            Current.storefront = s;
            BL.Update(Current.customer);
            BL.Update(Current.storefront);
            Builder.ResetPause("Thank you for shopping with us!");
            
        }
        public MenuType Choice(){return MenuType.Main;}
    }
}
