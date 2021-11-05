using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Models;
using BusinessLogic;

/* For now this makes pretty menus out of List<strings>
*/

namespace Toolbox
{
    //This set of methods builds the visuals out of List<string> menulines, which is where the menu data is stored
    //Some useful methods are: Buildmenu, Add, Reset, Pause, TestInt, TestString, Search, ShowAll, and SelectClassFromList
    public class MenuBuilder
    {
        
        List<string> menulines = new List<string>();    //This is the list of strings that will be displayed
        IBusiness BL;                                   //This is the business logic object that will be used to interact with the database in Search().
        public MenuBuilder(IBusiness BL)
        {
            this.BL = BL;
        }
        public void ConsoleBuildMenu()
        {
            int max = 38;
            //menulines is foreach'd and each string is checked for length, rest of length is filled with spaces, and then written to the screen.
            //if else for a string thats too long and needs to get split by spaces. 
            Console.Clear();
            Console.WriteLine(@"   ___________________________________________     ");
            Console.WriteLine(@" / \                                          \.   ");
            Console.WriteLine(@" \_ |                                         |.   ");
            foreach( string line in menulines)
            {
                if(line.Length<max){
                    Console.WriteLine($"    |   {line}{new string(' ', max-line.Length)}|.   ");
                }else{
                    foreach(string subline in line.Insert(line.Length/2, "-").Split("-")){
                        Console.WriteLine($"    |   {subline}{new string(' ', max-subline.Length)}|.   ");}
                }
            } 
            Console.WriteLine(@"    |                                         |.   "); 
            Console.WriteLine(@"    |                                         |.   "); 
            Console.WriteLine(@"    |   ______________________________________|___ ");
            Console.WriteLine(@"    |  /                                         /.");
            Console.WriteLine(@"    \_/_________________________________________/. ");

        }
        //Builds a asp.net menu from menulines and returns is as a view
        public string AspBuildMenu()
        {
            string menu = "";
            foreach(string line in menulines) { menu += $"<li>{line}</li>";}
            return menu;
        }


        public void Add(string s = ""){         //no BuildMenu for single lines. //If nothing is sent in, just adds a blank line
            menulines.Add(s);
        }
        public void Add(string s, char f){      //char f is just an overloading flag that puts a BuildMenu after line. The value doesn't matter.
            Add(s);
            ConsoleBuildMenu();
        }
        public void Add(List<string> ls){
            foreach(string s in ls){Add(s);}
        }
        public void Add(List<string> ls,char f){ //char f is just an overloading flag that puts a BuildMenu after line. The value doesn't matter.
            foreach(string s in ls){Add(s);}
            ConsoleBuildMenu();
        }
        public void Reset(string s = ""){
            menulines = new List<string>();
            Add(s, 'b');
        }
        public void Reset(List<string> ls){
            menulines = ls;
            ConsoleBuildMenu();
        }
        public void Pause(){
            Add();Add();
            Add("Press Enter to Continue...",'f');
            Console.ReadLine();
        }
        public void Pause(string s){
            Add(s);
            Pause();
        }
        public void Pause(List<string> s){
            Add(s);
            Pause();
        }
        public void ResetPause(string s){
            Reset(s);
            Pause();
        }
        public void ResetPause(List<string> ls){
            Reset(ls);
            Pause();
        }




        //Method GetAddress: Returns a string that is the address of the user's input.
        public string GetAddress(){
            Add("Please enter your address: ", 'f');
            string address = Console.ReadLine(); Add(address);                           //1 to 6 numbers, a space, 1 to 5 words, 5 numbers, then an enter.
            while(!Regex.IsMatch(address, @"\d{1,6} (\b\w*\b\s){1,5}\d{5}\.?")){            //address must be in this format: "12325 Main Road St 12345"
                Add("Enter number, street name, and zipcode.");
                Add("Invalid address format. Please try again: ", 'f');
                address = Console.ReadLine(); Add(address); 
            }
            return address;
        }

        //Method GetEmail: Returns a string that is the email address of the user.
        public string GetEmail(){
            Add($"Please enter your email address: ", 'f');
            string email = Console.ReadLine(); Add(email);
            while(!Regex.IsMatch(email, @"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$")){ //regex to check for valid email address
                Add("Invalid email address. Please try again: ", 'f');
                email = Console.ReadLine(); Add(email);
            }
            return email;
        }

        //Method GetPhoneNumber: Returns a string of the phone number entered by the user.
        public string GetPhoneNumber(){
            Add($"Please enter your phone number: ", 'f');
            string phone = Console.ReadLine();      //regex to remove parentheses and replace spaces and dots with dashes
            phone = Regex.Replace(phone, @"[()]", "");
            phone = Regex.Replace(phone, @"[ .]", "-");
            Add(phone);

            while(!Regex.IsMatch(phone, @"^\d{3}-\d{3}-\d{4}$")){                               //regex for phone number
                Add("Invalid phone number. Please enter a valid phone number.", 'f');
                phone = Console.ReadLine(); Add(phone);
            }
            return phone;
        }

        //Method GetInt: Returns an int if the user enters a valid positive int using regex.
        public int GetInt(){
            string s = Console.ReadLine(); Add(s);
            int i = 0;
            bool valid = false;
            while(!valid){
                if(Regex.IsMatch(s, @"^\d+$")){                                                 //regex for positive ints
                    i = int.Parse(s);
                    valid = true;
                }else{
                    Add("Please enter a valid positive integer.", 'f');
                    s = Console.ReadLine(); Add(s);
                }
            }
            return i;
        }
        //Method GetDecimal: Returns a decimal if the user enters a valid decimal using regex.
        public decimal GetDecimal(){
            string s = Console.ReadLine(); Add(s);
            decimal d = 0;
            bool valid = false;
            while(!valid){
                if(Regex.IsMatch(s, @"^\d+\.\d+$")){                                             //regex for positive decimals
                    d = decimal.Parse(s);
                    valid = true;
                }else{
                    Add("Please enter a valid decimal.", 'f');
                    s = Console.ReadLine(); Add(s);
                }
            }
            return d;
        }

        //Method GetString: Returns a string if the user enters a valid string using regex.
        public string GetString(){
            string s = Console.ReadLine(); Add(s);
            string i = "";
            bool valid = false;
            while(!valid){
                if(Regex.IsMatch(s, @"^[a-zA-Z ]+$")){                                           //regex for letters and spaces only.
                    i = s; valid = true;
                }else{
                    Add("Please enter a valid string.", 'f');
                    s = Console.ReadLine(); Add(s);
                } 
            }
            return i;
        }
        //Method Choice: no parameters. char YorN reads first char of input and checks for a valid choice. y,a,1 are true. n,b,2 are false.
        public bool Choice(){
            char YorN;
            try{YorN = Char.ToLower(Convert.ToChar(Console.ReadLine().Substring(0, 1)));} //gets first char of input and converts to lowercase.
            catch(System.ArgumentOutOfRangeException){                                    //if no input, starts over.
                Add("Type something. Then hit enter.", 'f');
                return Choice();
            }
            if(YorN=='y'||YorN=='a'||YorN=='1'){return true;}                           
            else if(YorN=='n'||YorN=='b'||YorN=='0'){return false;}  
            else{
                Add(YorN.ToString());
                Add("Enter a valid choice. y/n a/b 1/0", 'f');                            //if input is not valid, starts over.
                return Choice();                                            
            }
        }

        //Methods named Search: Customer p_IC parameter: searchs Customer database List<Customer> that match the search string.

        public List<Customer> Search(Customer p_IC){
            List<Customer> SelectC = BL.GetAll(new Customer());
            if(SelectC.Count == 0){Pause("No Customers to search.");}
            if(SelectC.Count < 5){return SelectC;}
            do{   
                Reset($"Search {p_IC.Identify()}s:");
                string entered = Console.ReadLine(); Add(entered);
                
                SelectC = BL.Search(p_IC, entered);

                if(SelectC.Count == 0){Add("No results.");}
                else{
                    Add();Add($" Showing {p_IC.Identify()}s");Add();

                    foreach(Customer c in SelectC){
                        Add();Add($" --------------------");
                        Add(c.ToStringList());
                    }
                    Add("=============================="); 
                    Add("Do you want to search again?", 'b');
                }
            } while (Choice());
            return SelectC;
        }
        public List<Store> Search(Store p_IC){
            List<Store> SelectC = BL.GetAll(new Store());
            if(SelectC.Count == 0){Add("No Storefronts to search.");}
            if(SelectC.Count < 5){return SelectC;}
            do{   
                Reset($"Search {p_IC.Identify()}s:");
                string entered = Console.ReadLine(); Add(entered);

                SelectC = BL.Search(p_IC, entered);

                if(SelectC.Count == 0){Add("No results.");}
                else{
                    Add();Add($" Showing {p_IC.Identify()}s");Add();

                    foreach(Store c in SelectC){
                        Add();Add($" --------------------");
                        Add(c.ToStringList());
                    }
                    Add("=============================="); 
                    Add("Do you want to search again?", 'b');
                }
            } while (Choice());
            return SelectC;
        }
        public List<Order> Search(Order p_IC){
            List<Order> SelectC = BL.GetAll(new Order()); 
            if(SelectC.Count == 0){Add("No Orders to search.");}
            if(SelectC.Count < 5){return SelectC;}
            do{   
                Reset($"Search {p_IC.Identify()}s:");
                string entered = Console.ReadLine(); Add(entered);

                SelectC = BL.Search(p_IC, entered);
                if(SelectC.Count == 0){Add("No results.");}
                else{
                    Add();Add($" Showing {p_IC.Identify()}s");Add();

                    foreach(Order c in SelectC){
                        Add();Add($" --------------------");
                        Add(c.ToStringList());
                    }
                }
                Add("=============================="); 
                Add("Do you want to search again?", 'b');
            } while (Choice());
            return SelectC;
        }
        public List<LineItem> Search(LineItem p_IC){
            List<LineItem> SelectC = BL.GetAll(new LineItem()); 
            if(SelectC.Count == 0){Add("No LineItems to search.");}
            if(SelectC.Count < 5){return SelectC;}
            do{   
                Reset($"Search {p_IC.Identify()}s:");
                string entered = Console.ReadLine(); Add(entered);

                SelectC = BL.Search(p_IC, entered);

                Add();Add($" Showing {p_IC.Identify()}s");Add();
                
                foreach(LineItem c in SelectC){
                    Add();Add($" --------------------");
                    Add(c.ToStringList());
                }
                Add("=============================="); 
                Add("Do you want to search again?", 'b');
            } while (Choice());
            return SelectC;
        }
        public List<Product> Search(Product p_IC){
            List<Product> SelectC = BL.GetAll(new Product());  
            if(SelectC.Count == 0){Add("No Products to search.");}
            if(SelectC.Count < 5){return SelectC;}
            do{   
                Reset($"Search {p_IC.Identify()}s:");
                string entered = Console.ReadLine(); Add(entered);

                SelectC = BL.Search(p_IC, entered);
                Add();Add($" Showing {p_IC.Identify()}s");Add();

                foreach(Product c in SelectC){
                    Add();Add($" --------------------");
                    Add(c.ToStringList());
                }
                Add("=============================="); 
                Add("Do you want to search again?", 'b');
            } while (Choice());
            return SelectC;
        }


        //Five overwritten "ShowAll Classes in this Class list" methods.
        //Builds a menu out around a foreach loop through the list.

        public void ShowAll(List<Customer> AllIC){
            Reset($"Show all {AllIC[0].Identify()}s,");
            Add(); int cnt = 0;
            foreach(Customer ic in AllIC){
                Add();Add($"[{cnt++}] --------------------");
                Add(ic.ToStringList());
            }
            Add("===================================",'f');
        }public void ShowAll(List<Store> AllIC){
            Reset($"Show all {AllIC[0].Identify()}s,");
            Add(); int cnt = 0;
            foreach(Store ic in AllIC){
                Add();Add($"[{cnt++}] --------------------");
                Add(ic.ToStringList());
            }
            Add("===================================",'f');
        }public void ShowAll(List<Order> AllIC){
            Reset($"Show all {AllIC[0].Identify()}s,");
            Add(); int cnt = 0;
            foreach(Order ic in AllIC){
                Add();Add($"[{cnt++}] --------------------");
                Add(ic.ToStringList());
            }
            Add("===================================",'f');
        }public void ShowAll(List<LineItem> AllIC){
            Reset($"Show all {AllIC[0].Identify()}s,");
            Add(); int cnt = 0;
            foreach(LineItem ic in AllIC){
                Add();Add($"[{cnt++}] --------------------");
                Add(ic.ToStringList());
            }
            Add("===================================",'f');
        }public void ShowAll(List<Product> AllIC){
            Reset($"Show all {AllIC[0].Identify()}s,");
            Add(); int cnt = 0;
            foreach(Product ic in AllIC){
                Add();Add($"[{cnt++}] --------------------");
                Add(ic.ToStringList());
            }
            Add("===================================",'f');
        }


        //Select method: Class p_IC parameter: Selects a Class from the list.
        //returns the selected Class.

        public Product Select(List<Product> p_ICList){
            ShowAll(p_ICList);
            Add("Enter number for your choice.",'b');
            int choice = GetInt();
            while (choice >= p_ICList.Count || choice < 0){
                Pause("Out of range selection.");
                ShowAll(p_ICList);
                Add("Enter number for your choice.",'b');
                choice = GetInt();
            }
            return p_ICList[choice];
        }
        public LineItem Select(List<LineItem> p_ICList){
            ShowAll(p_ICList);
            Add("Enter number for your choice.",'b');
            int choice = GetInt();
            while (choice >= p_ICList.Count || choice < 0){
                Pause("Thats not a valid selection. Try again.");
                ShowAll(p_ICList);
                Add("Enter number for your choice.",'b');
                choice = GetInt();
            }
            return p_ICList[choice];
        }
        public Order Select(List<Order> p_ICList){
            ShowAll(p_ICList);
            Add("Enter number for your choice.",'b');
            int choice = GetInt();
            while (choice >= p_ICList.Count || choice < 0){
                Pause("Thats not a valid selection. Try again.");
                ShowAll(p_ICList);
                Add("Enter number for your choice.",'b');
                choice = GetInt();
            }
            return p_ICList[choice];
        }
        public Customer Select(List<Customer> p_ICList){
            ShowAll(p_ICList);
            Add("Enter number for your choice.",'b');
            int choice = GetInt();
            while (choice >= p_ICList.Count || choice < 0){
                Pause("Thats not a valid selection. Try again.");
                ShowAll(p_ICList);
                Add("Enter number for your choice.",'b');
                choice = GetInt();
            }
            return p_ICList[choice];
        }
        public Store Select(List<Store> p_ICList){
            ShowAll(p_ICList);
            Add("Enter number for your choice.",'b');
            int choice = GetInt();
            while (choice >= p_ICList.Count || choice < 0){
                Pause("Thats not a valid selection. Try again.");
                ShowAll(p_ICList);
                Add("Enter number for your choice.",'b');
                choice = GetInt();
            }
            return p_ICList[choice];
        }

        //Five overwritten methods to combine the above methods.
        public Order SearchAndSelect(Order p_IC){return Select(Search(p_IC));}
        public LineItem SearchAndSelect(LineItem p_IC){return Select(Search(p_IC));}
        public Product SearchAndSelect(Product p_IC){return Select(Search(p_IC));}
        public Customer SearchAndSelect(Customer p_IC){return Select(Search(p_IC));}
        public Store SearchAndSelect(Store p_IC){return Select(Search(p_IC));}


        //Method CreateOrder: Creates an Order. Calls CreateLineItem() in a loop
        //Returns the created Order. Adds Orders to database.
        //Used in CreateOrder() and CreateStorefrontOrder().
        //To skip picking a location each time, send in location
        public Order CreateOrder(string p_Location){
            Order o = new Order();
            LineItem li = new LineItem();
            o.Address = p_Location;    
                        
            do{
                li = CreateLineItem();

                if(o.LineItems.Find(x => x.Product.Id == li.Product.Id) == null){                     //if the product is not already in the order
                    o.LineItems.Add(li);                                                //add it to the order
                }else{
                    Add("Merged items in your order.");
                    o.LineItems.Find(x => x.Product.Id == li.Product.Id).Quantity += li.Quantity;    //add the quantity to the existing item
                }
                
                Add("Another Lineitem?", 'b');
            }while(Choice());

            BL.Add(o);
            return o;
        }

        //Method CreateLineItem: Creates a LineItem. Picks a Product from the database.
        //Adds LineItems to database. Returns the created LineItem.
        //Used in CreateOrder() method.

        public LineItem CreateLineItem(){
            LineItem li = new LineItem();
            Product p = new Product();

            p = SearchAndSelect(p);
            Add();Add(p.Name,'b');
            li.Product = p;
            Add("How many of this product do you want?", 'b');
            li.Quantity = GetInt();
            li.Product.Id = p.Id;

            BL.Add(li);
            return li;
        }

        // Store buying All orders from distributor and adding to inventory
        public void TransactStoreOrders(Store s){
            ResetPause(s.ToStringList());
            foreach(Order o in  s.Orders){
                if(o.Active){
                foreach(LineItem li in o.LineItems){
                    s.Expenses += li.Total*.7M;                                                                     //Stores get a discount from the distributor
                    if(s.Inventory.Find(inv => inv.Id == li.Id) != null){                                           //Does it exist?
                    s.Inventory.Find(inv => inv.Product.Id == li.Product.Id).Quantity += li.Quantity;                 //Add if it does by merging
                    }else{s.Inventory.Add(li);}                                                                     //Add if it doesn't by adding
                }
                }   
                o.Active = false;
                Pause($"Order {o.Id} with {o.LineItems.Count} items complete.");     
            }
            BL.Update(s);
        }
        
        
        
        
        // Customer buying from store, increasing store revanue, decreasing store inventory, and increasing customer's totalspent
        public void TransactCustomerOrders(Customer c, Store s){
            bool orderFailure = false;
            ResetPause(c.ToStringList());
            foreach(Order o in  c.Orders){
                if(o.Active){
                foreach(LineItem li in o.LineItems){
                    if(s.Inventory.Find(inv => inv.Product.Id == li.Product.Id) != null){                                            // if it exists,
                    if(s.Inventory.Find(inv => inv.Product.Id == li.Product.Id).Quantity >= li.Quantity){        // if it has enough,
                        s.Inventory.Find(inv => inv.Product.Id == li.Product.Id).Quantity -= li.Quantity;        // remove from store
                        s.Revenue += li.Total;                                                                            // add to store revenue 
                        c.TotalSpent += li.Total;                                                                         // add to customer spent                                               
                    }else{
                        ResetPause(new List<string>()
                        {"Sorry, we don't have enough of",
                        $"{li.Product.Name} to complete your order"});  
                        orderFailure = true; break;
                    }}}     
                Pause($"Order {o.Id} with {o.LineItems.Count} items complete.");     
                o.Active = !orderFailure;
                }
                }
            BL.Update(c);
            BL.Update(s);
            }      
    }
    //----------------------------------------------------------------------------------------------------------------------
}