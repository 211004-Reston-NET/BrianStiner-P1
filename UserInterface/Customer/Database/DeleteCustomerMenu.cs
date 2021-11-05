using System;
using System.Collections.Generic;
using Toolbox;
using Models;
using BusinessLogic;

namespace UserInterface{
    public class DeleteCustomerMenu : IMenu{
        IBusiness BL;
        MenuBuilder Builder;
        public  DeleteCustomerMenu(IBusiness BL){
            this.BL = BL;
            Builder = new MenuBuilder(BL);
        }

        //ask for a string and search against all of the Customer database to return a select List<Customer>, show it, user selects one, modify that customer.
        public void Display(){

            Customer OurCustomer = Builder.SearchAndSelect(new Customer());     //Search and select a customer from the list

            BL.Delete(OurCustomer);                                             //Delete Customer
        
            Builder.ResetPause("Customer deleted from database");                    //Reset menu for new menu selection
        }

        public MenuType Choice(){return MenuType.Customer;}
    }
}
