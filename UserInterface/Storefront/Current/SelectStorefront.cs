using System;
using System.Collections.Generic;
using Toolbox;
using Models;
using BusinessLogic;

namespace UserInterface{
    public class SelectStorefrontMenu : IMenu{
        IBusiness BL;
        MenuBuilder Builder;
        public SelectStorefrontMenu(IBusiness BL){
            this.BL = BL;
            Builder = new MenuBuilder(BL);
        }
        public void Display(){
            Current.storefront = Builder.SearchAndSelect(new Store());  
            Builder.Pause($"Store set to {Current.storefront.Name}!");
        }
        public MenuType Choice(){return MenuType.Storefront;}
    }
}