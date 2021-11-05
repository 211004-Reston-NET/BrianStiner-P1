using System;
using System.Collections.Generic;
using Toolbox;
using Models;
using BusinessLogic;

namespace UserInterface{
    public class StorefrontPastOrdersMenu : IMenu{
        IBusiness BL;
        MenuBuilder Builder;
        public  StorefrontPastOrdersMenu(IBusiness BL){
            this.BL = BL;
            Builder = new MenuBuilder(BL);
        }
        public void Display(){

            BL.Update(Current.storefront);
            Builder.Reset($"---All Orders for {Current.storefront.Name}---");
            Builder.Add();
            Builder.Add(Current.storefront.ToStringList(true));
            Builder.Add();
            Builder.Pause($"{Current.storefront.Name} shown!");
        }

        public MenuType Choice(){return MenuType.Storefront;}
    }
}