using System.Configuration;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using DataAccessLogic.Entity;
using DataAccessLogic;
using BusinessLogic;

namespace UserInterface{ 
    class Factory : IFactory{
        public IMenu GetMenu(MenuType currentMenu){

            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsetting.json")    
                .Build();
            
            DbContextOptionsBuilder<revaturedatabaseContext> options = new DbContextOptionsBuilder<revaturedatabaseContext>()
                .UseSqlServer(configuration.GetConnectionString("DefaultConnection"));

            //All menus require dependency injection becuase all the menus require menubuilder which requires BusinessLogic.
            //This could have been designed so that menubuilder doesn't require BusinessLogic, but I didn't want to change the design so close to the deadline.
            //And functionality is not affected.
            
            switch (currentMenu){

                //Customer
                case MenuType.Customer:
                    return new CustomerMenu(new Business(new RepositorySQL(new revaturedatabaseContext(options.Options)))); 

                case MenuType.ShowAllCustomers:
                    return new ShowAllCustomersMenu(new Business(new RepositorySQL( new revaturedatabaseContext(options.Options))));
                case MenuType.AddCustomer:
                    return new AddCustomerMenu(new Business(new RepositorySQL( new revaturedatabaseContext(options.Options))));
                case MenuType.DeleteCustomer:
                    return new DeleteCustomerMenu(new Business(new RepositorySQL( new revaturedatabaseContext(options.Options))));

                case MenuType.ModifyCustomer:
                    return new ModifyCustomerMenu(new Business(new RepositorySQL(new revaturedatabaseContext(options.Options))));
                case MenuType.SelectCustomer:
                    return new SelectCustomerMenu(new Business(new RepositorySQL(new revaturedatabaseContext(options.Options)))); 
                case MenuType.ShowCurrentCustomer:
                    return new ShowCurrentCustomerMenu(new Business(new RepositorySQL(new revaturedatabaseContext(options.Options))));
                case MenuType.CustomerOrder:
                    return new CustomerOrderMenu(new Business(new RepositorySQL(new revaturedatabaseContext(options.Options))));  
                case MenuType.CustomerPastOrder:
                    return new CustomerPastOrdersMenu(new Business(new RepositorySQL(new revaturedatabaseContext(options.Options))));   

                //Storefront
                case MenuType.Storefront:
                    return new StorefrontMenu(new Business(new RepositorySQL(new revaturedatabaseContext(options.Options))));

                case MenuType.ShowAllStorefronts:
                    return new ShowAllStorefrontsMenu(new Business(new RepositorySQL( new revaturedatabaseContext(options.Options)))); 
                case MenuType.AddStorefront:
                    return new AddStorefrontMenu(new Business(new RepositorySQL( new revaturedatabaseContext(options.Options)))); 
                case MenuType.DeleteStorefront:
                    return new DeleteStorefrontMenu(new Business(new RepositorySQL( new revaturedatabaseContext(options.Options))));

                case MenuType.ModifyStorefront:
                    return new ModifyStorefrontMenu(new Business(new RepositorySQL(new revaturedatabaseContext(options.Options))));
                case MenuType.SelectStorefront:
                    return new SelectStorefrontMenu(new Business(new RepositorySQL(new revaturedatabaseContext(options.Options)))); 
                case MenuType.ShowStorefront:
                    return new ShowStorefrontMenu(new Business(new RepositorySQL(new revaturedatabaseContext(options.Options)))); 
                case MenuType.StorefrontOrder:
                    return new StorefrontOrderMenu(new Business(new RepositorySQL(new revaturedatabaseContext(options.Options))));
                case MenuType.StorefrontPastOrder:
                    return new StorefrontPastOrdersMenu(new Business(new RepositorySQL(new revaturedatabaseContext(options.Options))));
                
                //Product
                case MenuType.Product:
                    return new ProductMenu(new Business(new RepositorySQL(new revaturedatabaseContext(options.Options))));

                case MenuType.ShowAllProducts:
                    return new ShowAllProductsMenu(new Business(new RepositorySQL( new revaturedatabaseContext(options.Options)))); 
                case MenuType.AddProduct:
                    return new AddProductMenu(new Business(new RepositorySQL( new revaturedatabaseContext(options.Options))));  
                case MenuType.ModifyProduct:
                    return new ModifyProductMenu(new Business(new RepositorySQL( new revaturedatabaseContext(options.Options)))); 
                case MenuType.DeleteProduct:
                    return new DeleteProductMenu(new Business(new RepositorySQL( new revaturedatabaseContext(options.Options)))); 

                //Others
                case MenuType.Checkout:
                    return new CheckoutMenu(new Business(new RepositorySQL(new revaturedatabaseContext(options.Options))));    
                case MenuType.Exit:
                    return new ExitMenu(new Business(new RepositorySQL( new revaturedatabaseContext(options.Options)))); 
                case MenuType.RealExit:
                    return new RealExitMenu(new Business(new RepositorySQL( new revaturedatabaseContext(options.Options)))); 
                    
                default:
                    return new MainMenu(new Business(new RepositorySQL( new revaturedatabaseContext(options.Options))));  
            }
        }
    }
}