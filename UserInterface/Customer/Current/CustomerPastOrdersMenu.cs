using System;
using System.Collections.Generic;
using Toolbox;
using Models;
using BusinessLogic;

namespace UserInterface{
    public class CustomerPastOrdersMenu : IMenu{
        IBusiness BL;
        MenuBuilder Builder;
        public  CustomerPastOrdersMenu(IBusiness BL){
            this.BL = BL;
            Builder = new MenuBuilder(BL);
        }
        public void Display(){

            BL.Update(Current.customer);
            Builder.Reset($"---All Orders for {Current.customer.Name}---");
            Builder.Add();
            Builder.Add(Current.customer.ToStringList(true));
            Builder.Add();
            Builder.Pause($"{Current.customer.Name} shown!");
        }

        public MenuType Choice(){return MenuType.Customer;}
    }
}