using System;
using System.Collections.Generic;
using BusinessLogic;
using Toolbox;

namespace UserInterface
{
    public class StorefrontMenu : IMenu{
        IBusiness BL; MenuBuilder Builder;
        public StorefrontMenu(IBusiness BL) {
            this.BL = BL;
            Builder = new MenuBuilder(BL);
        }
        public void Display(){
            Builder.Reset(new List<string>()
                {"Storefront Menu,",
                "What do you want to do?", 
                "[0] - Go back",
                "----------Database------------", 
                "[1] - Add Storefront", 
                "[2] - Delete a Storefront",
                "[3] - View all Storefronts",
                "----------Current-------------",
                "[4] - Select Storefront"});
            if(Current.storefront != null){
                Builder.Add(new List<string>()
                {"[5] - Modify Storefront",
                "[6] - View Storefront",
                "[7] - Create Order",
                "[8] - Show Past Orders"}, 'f');
            }
        }

        public MenuType Choice(){

            switch (Builder.GetInt())
            {
                case 0:
                    return MenuType.Main;
                case 1:
                    return MenuType.AddStorefront;
                case 2:
                    return MenuType.DeleteStorefront; 
                case 3:
                    return MenuType.ShowAllStorefronts;
                case 4:
                    return MenuType.SelectStorefront;
                case 5:
                    return MenuType.ModifyStorefront;
                case 6:
                    return MenuType.ShowStorefront;
                case 7:
                    return MenuType.StorefrontOrder;
                default:
                    Builder.Pause("Not a choice. Try again.");
                    return MenuType.Storefront;
            }
            
        }
    }
}
