using System;
using System.Collections.Generic;
using BusinessLogic;
using Models;
using Toolbox;

namespace UserInterface{
    class StorefrontOrderMenu : IMenu{
        IBusiness BL; MenuBuilder Builder;
        public StorefrontOrderMenu(IBusiness BL){
            this.BL = BL;
            Builder = new MenuBuilder(BL);
        }
        
        public void Display(){

            Order newOrder;
            do{    
                newOrder = Builder.CreateOrder(Current.storefront.Address);
                Current.storefront.Orders.Add(newOrder);
                Builder.Reset("Would you like to add another order?");
            }while(Builder.Choice());

            Builder.ResetPause($"{Current.storefront.Name}'s Order created!");
            BL.Update(Current.storefront);
        }
        public MenuType Choice(){return MenuType.Storefront;}
    }
}