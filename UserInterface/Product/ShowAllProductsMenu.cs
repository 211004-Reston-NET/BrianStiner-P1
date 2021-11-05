using System;
using System.Collections.Generic;
using Toolbox;
using BusinessLogic;
using Models;

namespace UserInterface{
    public class ShowAllProductsMenu : IMenu{
        IBusiness BL; MenuBuilder Builder;
        public ShowAllProductsMenu(IBusiness BL){
            this.BL = BL;
            Builder = new MenuBuilder(BL);
        }
        public void Display(){

            Builder.ShowAll(BL.GetAll(new Product()));

            Builder.Pause("Products in database shown!");
        }

        public MenuType Choice(){return MenuType.Product;} 
    }
}
