using System;
using System.Collections.Generic;
using Toolbox;
using Models;
using BusinessLogic;

namespace UserInterface{
    public class SelectCustomerMenu : IMenu{
        IBusiness BL;
        MenuBuilder Builder;
        public  SelectCustomerMenu(IBusiness BL){
            this.BL = BL;
            Builder = new MenuBuilder(BL);
        }

        //Calls Search and Select from builder, then sets CurrentCustomer to the selected customer
        public void Display(){

            Customer OurCustomer = Builder.SearchAndSelect(new Customer());

            Current.customer = OurCustomer;

            Builder.ResetPause($"Customer is now {Current.customer.Name}!");
        }
        public MenuType Choice(){return MenuType.Customer;}
    }
}