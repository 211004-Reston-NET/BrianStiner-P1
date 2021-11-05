using System;
using Toolbox;
using System.Collections.Generic;
using BusinessLogic;

namespace UserInterface
{
    public class ExitMenu : IMenu{
        IBusiness BL;
        MenuBuilder Builder;
        public ExitMenu(IBusiness BL){
            this.BL = BL;
            Builder = new MenuBuilder(BL);
        }

        public void Display(){
            Builder.Reset(new List<string>()
                {"Exit Menu",
                "Are you sure you want to quit?",
                "[0] - Actually quit", 
                "[1] - Go back"});
        }

        public MenuType Choice(){
            switch (Builder.GetInt()){
                case 1:
                    return MenuType.Main;
                case 0:
                    return MenuType.RealExit;
                default:
                    Console.WriteLine("Not a choice. Try again.",1);
                    return MenuType.Exit;
            } 
        }
        
    }
}
