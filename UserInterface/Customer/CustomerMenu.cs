using System;
using System.Collections.Generic;
using Toolbox;
using BusinessLogic;

namespace UserInterface{
    public class CustomerMenu : IMenu{
        IBusiness BL;
        MenuBuilder Builder;
        public CustomerMenu(IBusiness BL){
            this.BL = BL;
            Builder = new MenuBuilder(BL);
        }

        public void Display(){
            Builder.Reset(new List<string>()
                {"Customer Menu,",
                "What do you want to do?", 
                "[0] - Go back", 
                "------ Database ------",
                "[1] - Add a Customer", 
                "[2] - Delete a Customer",
                "[3] - Show all Customers",
                "------ Current  -------",
                "[4] - Select Customer",});

            if(Current.customer != null){
                Builder.Add(new List<string>(){
                "[5] - Modify Customer",
                "[6] - Show Customer",
                "[7] - Create Order for Customer",
                "[8] - Show Past Orders"}, 'f');
            }
        }
    

        public MenuType Choice(){

            switch (Builder.GetInt()){
                case 0:
                    return MenuType.Main;
                case 1:
                    return MenuType.AddCustomer;
                case 2:
                    return MenuType.DeleteCustomer; 
                case 3:
                    return MenuType.ShowAllCustomers; 
                case 4:
                    return MenuType.SelectCustomer;
                case 5:
                    return MenuType.ModifyCustomer;
                case 6:
                    return MenuType.ShowCurrentCustomer;
                case 7:
                    return MenuType.CustomerOrder;
                case 8:
                    return MenuType.CustomerPastOrder;
                default:
                    Builder.Pause("Not a choice. Try again.");
                    return MenuType.Customer;
            }
            
        }
    }
}
