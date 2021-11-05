using System;
using System.Collections.Generic;
using BusinessLogic;
using Toolbox;

namespace UserInterface{
    public class ProductMenu : IMenu{
        MenuBuilder Builder; IBusiness BL;
        public ProductMenu(IBusiness BL){
            this.BL = BL;
            Builder = new MenuBuilder(BL);
        }
        public void Display(){
            Builder.Reset(new List<string>()
                {"Product Menu,",
                "What do you want to do?", 
                "[0] - Go back", 
                "[1] - Add Product", 
                "[2] - Delete a Product",
                "[3] - Modify a Product",
                "[4] - Show all Products"});
        }

        public MenuType Choice(){
            
            switch (Builder.GetInt()){
                case 0:
                    return MenuType.Main;
                case 1:
                    return MenuType.AddProduct;
                case 2:
                    return MenuType.DeleteProduct; 
                case 3:
                    return MenuType.ModifyProduct; 
                case 4:
                    return MenuType.ShowAllProducts;
                default:
                    Builder.Pause("Not a choice. Try again.");
                    return MenuType.Main;
            }
            
        }
    }
}
