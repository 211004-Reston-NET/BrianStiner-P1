using System;
using Serilog;

namespace UserInterface{
    class Program{
        public static void Main(string[] args){
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Console()
                .CreateLogger();
            try{
                Log.Information("Starting up");


                bool repeat = true;
                IFactory factory = new Factory();
                IMenu page = factory.GetMenu(MenuType.Main);

                while (repeat){
                    page.Display();
                    MenuType nextmenu = page.Choice(); if(nextmenu == MenuType.RealExit) {repeat = false; break;}
                    page = factory.GetMenu(nextmenu);                           
                }


            }catch (Exception ex){
                Log.Fatal(ex, "Application terminated unexpectedly");
            }finally{
                Log.CloseAndFlush();
            }
        }
    }
}