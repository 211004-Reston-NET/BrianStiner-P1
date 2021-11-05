using System;
using System.Collections.Generic;
using Toolbox;
using BusinessLogic;
using Models;

namespace UserInterface{
    public class ShowAllStorefrontsMenu : IMenu{
        IBusiness BL;
        MenuBuilder Builder;
        public ShowAllStorefrontsMenu(IBusiness BL){
            this.BL = BL;
            Builder = new MenuBuilder(BL);
        }
        public void Display(){

            Builder.ShowAll(BL.GetAll(new Store()));

            Builder.Pause();
        }

        public MenuType Choice(){return MenuType.Storefront;}
        
    }
}
