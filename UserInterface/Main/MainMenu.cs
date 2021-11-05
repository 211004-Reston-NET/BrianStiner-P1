using System;
using System.Collections.Generic;
using BusinessLogic;
using Toolbox;

namespace UserInterface{
    public class MainMenu : IMenu{
        MenuBuilder Builder; IBusiness BL;
        public MainMenu(IBusiness BusinessLogic){
            BL = BusinessLogic;
            Builder = new MenuBuilder(BL);
        }
        
        public void Display(){
            Builder.Reset(new List<string>()
                {"Welcome to the Main Menu,",
                "Which menu do you want?",
                "[0] - Exit", 
                "[1] - Customers", 
                "[2] - Storefronts", 
                "[3] - Products"});
            if(Current.storefront != null){
            Builder.Add("[4] - Checkout",'f');}
            
            
        }

        public MenuType Choice(){
            switch (Builder.GetInt())
            {
                case 1:
                    return MenuType.Customer;
                case 2:
                    return MenuType.Storefront;
                case 3:
                    return MenuType.Product;
                case 0:
                    return MenuType.Exit;
                case 4:
                    return MenuType.Checkout;
                default:
                    Builder.Pause("Not a choice. Try again.");
                    return MenuType.Main;
            }
        }
    }
}
