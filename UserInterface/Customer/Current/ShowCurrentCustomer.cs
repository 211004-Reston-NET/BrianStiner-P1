using System;
using System.Collections.Generic;
using Toolbox;
using Models;
using BusinessLogic;

namespace UserInterface{
    public class ShowCurrentCustomerMenu : IMenu{
        IBusiness BL;
        MenuBuilder Builder;
        public  ShowCurrentCustomerMenu(IBusiness BL){
            this.BL = BL;
            Builder = new MenuBuilder(BL);
        }
        public void Display(){

            BL.Update(Current.customer);
            Builder.Reset("---Showing Current Customer---");
            Builder.Add();
            Builder.Add(Current.customer.ToStringList());
            Builder.Add();
            Builder.Pause($"{Current.customer.Name} shown!");
        }

        public MenuType Choice(){return MenuType.Customer;}
    }
}
