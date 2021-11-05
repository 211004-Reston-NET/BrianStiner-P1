using System;
using System.Collections.Generic;
using Toolbox;
using Models;
using BusinessLogic;

namespace UserInterface{
    public class ModifyProductMenu : IMenu{
        IBusiness BL; MenuBuilder Builder;
        public ModifyProductMenu(IBusiness BL){
            this.BL = BL;
            Builder = new MenuBuilder(BL);
        }

        //ask for a string and search against all of the Product database to return a select List<Product>, show it, user selects one, modify that Product.
        public void Display(){

            Product OurProduct = Builder.SearchAndSelect(new Product());//Search database for Product list//Select Product from list

            
            BL.Delete(OurProduct);

            do{
                Builder.Add(new List<string>(){
                    "What do you want to change?",
                    "[1] - Name",
                    "[2] - Category",
                    "[3] - Description",
                    "[4] - Price"});

                int choice = Builder.GetInt();
                Builder.Add("What is the new value?",'b');

                switch(choice)                                          
                {
                    case 1:
                        OurProduct.Name = Builder.GetString();
                        break;
                    case 2:
                        OurProduct.Category = Builder.GetString();
                        break;  
                    case 3:
                        OurProduct.Description = Builder.GetString();
                        break;
                    case 4:
                        OurProduct.Price = Builder.GetDecimal();
                        break;
                    default:
                        Builder.Reset(new List<string>(){
                            "Invalid choice, only 1-4 are valid",
                            "You have not made a change to the product",
                            "",});
                        break;
                }
                Builder.Add(new List<string>(){
                    "---------------------------",
                    "Do you want to make another",
                   $"change to {OurProduct.Name}?"});

            } while(Builder.Choice());

            BL.Add(OurProduct);

            Builder.Pause($"{OurProduct.Name} updated!");
        }

        public MenuType Choice(){return MenuType.Product;}

    }
}
