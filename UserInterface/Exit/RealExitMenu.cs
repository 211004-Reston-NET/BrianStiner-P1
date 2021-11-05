using System;
using Toolbox;
using System.Collections.Generic;
using BusinessLogic;

namespace UserInterface{
    public class RealExitMenu : IMenu{
        
        IBusiness BL;
        MenuBuilder Builder;
        public RealExitMenu(IBusiness BL){
            this.BL = BL;
            Builder = new MenuBuilder(BL);
        }
        public void Display(){
            Builder.Pause("GoodBye!");
        }
        public MenuType Choice(){  
            return MenuType.RealExit;
        }
    }
}
