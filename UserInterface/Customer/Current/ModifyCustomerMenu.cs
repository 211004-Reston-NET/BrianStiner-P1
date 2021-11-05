using System;
using System.Collections.Generic;
using Toolbox;
using Models;
using BusinessLogic;

namespace UserInterface{
    public class ModifyCustomerMenu : IMenu{
        IBusiness BL;
        MenuBuilder Builder;
        public ModifyCustomerMenu(IBusiness BL){
            this.BL = BL;
            Builder = new MenuBuilder(BL);
        }

        //ask for a string and search against all of the Customer database to return a select List<Customer>, show it, user selects one, modify that customer.
        public void Display(){

            

            //Modify Customer
            do{
                Builder.Reset(Current.customer.ToStringList());
                Builder.Add(new List<string>(){
                    "What do you want to change?",
                    "[1] - Name",
                    "[2] - Address",
                    "[3] - Email",
                    "[4] - Phone Number"},'f');

                int choice = Builder.GetInt();
                Builder.Add("Enter the new value:",'f');
                switch(choice){
                    case 1:
                        Current.customer.Name = Builder.GetString(); break;
                    case 2:
                        Current.customer.Address = Builder.GetAddress(); break;
                    case 3:
                        Current.customer.Email = Builder.GetEmail(); break;
                    case 4:
                        Current.customer.Phone = Builder.GetPhoneNumber(); break;
                    default:
                        Builder.Reset(new List<string>(){
                           $"Invalid Choice, only 1-4 are valid options",
                           $"You have not updated {Current.customer.Name}",
                           $"Please try again."});
                        break;
                }

                Builder.Add(new List<string>(){
                   $"---------------------------",
                   $"Do you want to make another",
                   $"change to {Current.customer.Name}?"},'f');
            } while(Builder.Choice());

            BL.Update(Current.customer);

            Builder.ResetPause($"{Current.customer.Name} Modified!");
        }

        public MenuType Choice(){return MenuType.Customer;} //Best choice is no choice
    }
}
