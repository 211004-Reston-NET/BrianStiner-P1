using System;
using System.Collections.Generic;
using Toolbox;
using Models;
using BusinessLogic;

namespace UserInterface{
    public class ShowStorefrontMenu : IMenu{
        IBusiness BL;
        MenuBuilder Builder;
        public  ShowStorefrontMenu(IBusiness BL){
            this.BL = BL;
            Builder = new MenuBuilder(BL);
        }
        public void Display(){
            Builder.Add("---Showing Storefront---");
            Builder.Add();
            Builder.Add(Current.storefront.ToStringList());
            Builder.Add();
            Builder.Pause($"{Current.storefront.Name} Shown!");
        }
        public MenuType Choice(){return MenuType.Storefront;}
    }
}
