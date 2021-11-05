using System;
using System.Collections.Generic;
using Toolbox;
using Models;
using BusinessLogic;

namespace UserInterface{
    public class ModifyStorefrontMenu : IMenu{
        IBusiness BL;
        MenuBuilder Builder;
        public ModifyStorefrontMenu(IBusiness BL){
            this.BL = BL;
            Builder = new MenuBuilder(BL);
        }
        public void Display(){
            do{
                Builder.Reset(Current.storefront.ToStringList());
                Builder.Add(new List<string>(){
                    "What do you want to change?",
                    "[1] - Name",
                    "[2] - Address"},'f');

                switch(Builder.GetInt()){
                    case 1:
                        Builder.Add("Enter new Name:");
                        Current.storefront.Name = Builder.GetString();      break;
                    case 2:
                        Builder.Add("Enter new Address:");
                        Current.storefront.Address = Builder.GetAddress();  break;
                    default:                                                
                        Builder.Reset(new List<string>(){
                            $"Invalid choice. Only 1 and 2 work.",
                            $"{Current.storefront.Name} has not been updated.",
                            $"Please try again."});                         break;
                }
                Builder.Add(new List<string>(){
                    "---------------------------",
                    "Do you want to make another",
                   $"change to {Current.storefront.Name}?"});

            } while(Builder.Choice());

            Builder.Pause($"{Current.storefront.Name} has been updated!");
        }

        public MenuType Choice(){return MenuType.Storefront;}

    }
}
