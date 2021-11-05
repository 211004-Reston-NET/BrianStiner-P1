using System;
using System.Collections.Generic;
using Toolbox;
using Models;
using BusinessLogic;

namespace UserInterface{
    public class AddProductMenu : IMenu{
        IBusiness BL; MenuBuilder Builder;
        public AddProductMenu(IBusiness BL){
            this.BL = BL;
            Builder = new MenuBuilder(BL);
        }
        public void Display(){    
 
            Builder.Add("Fill in Product's info,");
            Builder.Add("What is their name?",'b');
            string name = Builder.GetString();

            Builder.Add("What is their Category?",'b');
            string category = Builder.GetString();

            Builder.Add("What is their Description?",'b');
            string description = Builder.GetString();

            Builder.Add("What is their price?",'b');
            decimal price = Builder.GetDecimal();

            Product newProduct = new Product(name, category, description, price);
            BL.Add(newProduct);

            Builder.Pause($"{newProduct.Name} Added!");   
        }

        public MenuType Choice(){return MenuType.Product;}
        
            
    }
}

