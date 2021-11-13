using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    public partial class Customer : IClass
    {
        //Variables -----------------------------------------------------------------------------
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Phone { get; set; }
        [Required]
        public decimal TotalSpent { get; set; }
        [Required]
        public int Picture { get; set; }

        public virtual List<Order> Orders { get; set; }

        //Constructors ---------------------------------------------------------------------------
        public Customer(){TotalSpent = 0;}
        public Customer(int p_Id):this(){ Id = p_Id; }
        public Customer( string p_name):this(){this.Name = p_name;}
        public Customer( string p_name, string p_address):this( p_name){this.Address = p_address;}
        public Customer( string p_name, string p_address, string p_email):this( p_name, p_address){this.Email = p_email;}
        public Customer( string p_name, string p_address, string p_email, string p_phone):this( p_name, p_address, p_email){this.Phone = p_phone;}
        public Customer( string p_name, string p_address, string p_email, string p_phone, decimal p_totalSpent):this( p_name, p_address, p_email, p_phone){this.TotalSpent = p_totalSpent;}
        public Customer( string p_name, string p_address, string p_email, string p_phone, decimal p_totalSpent, List<Order> p_Orders):this( p_name, p_address, p_email, p_phone, p_totalSpent){this.Orders = p_Orders;}
        public Customer( string p_name, string p_address, string p_email, string p_phone, decimal p_totalSpent, List<Order> p_Orders, int p_id):this( p_name, p_address, p_email, p_phone, p_totalSpent, p_Orders){this.Id = p_id;}

        //Interface --------------------------------------------------------------------------------
        public string Identify() { return "Customer"; }
        public List<string> ToStringList(){return ToStringList(false);}
        public List<string> ToStringList(bool p_showpastorders){
            List<string> stringlist = new List<string>(){
            $"{this.Id}",
            $"{this.Name}",
            $"{this.Phone}",
            $"{this.Email}",
            $"{this.Address}",
            $"{this.TotalSpent}",};
            return stringlist;
        }
        #nullable enable
        public ArrayList ToArrayList(ArrayList? p_al){
            if(p_al == null){p_al = new ArrayList();}
            p_al.Add(Id);
            p_al.Add(Name);
            p_al.Add(Phone);
            p_al.Add(Email);
            p_al.Add(Address);
            p_al.Add(TotalSpent);
            foreach(var item in Orders){p_al.AddRange(item.ToArrayList(p_al));}
            return p_al;
        }
        #nullable disable
    }

    public partial class Store : IClass
    {
        //Variables -----------------------------------------------------------------------------
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public decimal Expenses { get; set; }
        [Required]
        public decimal Revenue { get; set; }
        [Required]
        public virtual List<LineItem> Inventory { get; set; }
        public virtual List<Order> Orders { get; set; }
        [NotMapped]
        public decimal Profit {get => Revenue-Expenses; set => Profit = value;}
        
        //Constructors ---------------------------------------------------------------------------
        public Store(){}
        public Store(int p_Id){this.Id = p_Id;}
        public Store(string p_name, string p_address){this.Name = p_name;this.Address = p_address; }
        public Store(string p_name, string p_address, decimal p_expenses):this(p_name, p_address){this.Expenses = p_expenses;}
        public Store(string p_name, string p_address, decimal p_expenses, decimal p_revenue):this(p_name, p_address, p_expenses){this.Revenue = p_revenue;}
        public Store(string p_name, string p_address, decimal p_expenses, decimal p_revenue, List<LineItem> p_Inventory):this(p_name, p_address, p_expenses, p_revenue){this.Inventory = p_Inventory;}
        public Store(string p_name, string p_address, decimal p_expenses, decimal p_revenue, List<LineItem> p_Inventory, List<Order> p_Orders):this(p_name, p_address, p_expenses, p_revenue, p_Inventory){this.Orders = p_Orders;}
        public Store(string p_name, string p_address, decimal p_expenses, decimal p_revenue, List<LineItem> p_Inventory, List<Order> p_Orders, int p_id):this(p_name, p_address, p_expenses, p_revenue, p_Inventory, p_Orders){this.Id = p_id;}
       
        //Interface --------------------------------------------------------------------------------
        public string Identify() { return "Storefront"; }
        public List<string> ToStringList(){return ToStringList(false);}
        public List<string> ToStringList(bool p_showpastorders){
            List<string> stringlist = new List<string>() {
            $"{Id}",
            $"{Name}",
            $"{Address}",
            $"{Expenses}",
            $"{Revenue}",
            $"{Profit}",};
            return stringlist;
        }
        #nullable enable
        public ArrayList ToArrayList(ArrayList? p_al){
            if(p_al == null){p_al = new ArrayList();}
            p_al.Add(Id);
            p_al.Add(Name);
            p_al.Add(Address);
            p_al.Add(Expenses);
            p_al.Add(Revenue);
            p_al.Add(Profit);
            foreach(var item in Orders){p_al.AddRange(item.ToArrayList(p_al));}
            return p_al;
        }
        #nullable disable
            
    }

    public partial class Order : IClass
    {
        //Variables -----------------------------------------------------------------------------
        [Key]
        public int Id { get; set; }
        [Required] 
        public string Address { get; set; }
        [Required]
        public bool Active { get; set; }
        [NotMapped]
        public decimal Total { get => CalculateTotalPrice(); set => Total = value; }


        public virtual List<LineItem> LineItems { get; set; }

        //Constructors ---------------------------------------------------------------------------
        public Order(){}
        public Order(int p_Id):this(){this.Id = p_Id;}
        public Order(string p_location):this(){this.Address = p_location;}
        public Order(string p_location, int p_Id):this(p_Id){this.Address = p_location;}
        public Order(string p_location, int p_Id, List<LineItem> p_LineItems):this(p_location, p_Id){this.LineItems = p_LineItems;}
        public Order(string p_location, int p_Id, bool p_Active, List<LineItem> p_LineItems):this(p_location, p_Id, p_LineItems){this.Active = p_Active;}

        //Interface --------------------------------------------------------------------------------
        public string Identify() { return "Order"; }
        public List<string> ToStringList(){
            List<string> stringlist = new List<string>(){
            $"{Id}",
            $"{this.Active}",
            $"{Address}",};
            // $"LineItems: "};
            // foreach(LineItem oli in LineItems){ 
            //     foreach(string s in oli.ToStringList()){
            //         stringlist.Add($"| {s}");
            //     }
            // }
            // stringlist.Add($"-------------------------------");
            stringlist.Add($"{Total}");
            return stringlist;
        }
        #nullable enable
        public ArrayList ToArrayList(ArrayList? p_al){
            if(p_al == null){p_al = new ArrayList();}
            p_al.Add(Id);
            p_al.Add(Address);
            p_al.Add(Active);
            foreach(var item in LineItems){p_al.AddRange(item.ToArrayList(p_al));}
            p_al.Add(Total);
            return p_al;
        }
        #nullable disable
        //Methods ---------------------------------------------------------------------------------
        public decimal CalculateTotalPrice(){
            decimal orderTotal = 0;
            foreach(LineItem oli in LineItems){orderTotal += oli.Total;}
            return orderTotal;
        }
    }

    public partial class LineItem : IClass
    {
        //Variables -----------------------------------------------------------------------------
        [Key]
        public int Id { get; set; }
        [Required]
        public int ProductId { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public decimal Total { get => CalculateTotalPrice(); set => Total = value; }
        [Required]
        public virtual Product Product { get; set; }


        //Constructors ---------------------------------------------------------------------------
        public LineItem(){}
        public LineItem(int p_Id):this(){this.ProductId = p_Id;}
        public LineItem(int p_Id, int p_quantity):this(p_Id){this.Quantity = p_quantity;}
        public LineItem(int p_Id, int p_quantity, decimal p_total):this(p_Id, p_quantity){this.Total = p_total;}
        public LineItem(int p_Id, int p_quantity, decimal p_total, Product p_product):this(p_Id, p_quantity, p_total){this.Product = p_product;}
        public LineItem(int p_Id, int p_quantity, Product p_product):this(p_Id, p_quantity){this.Product = p_product;}



        //Interface --------------------------------------------------------------------------------
        public string Identify() { return "LineItem"; }
        public List<string> ToStringList(){
            List<string> stringlist = new List<string>();
            // stringlist.Add($"Product:");
            // foreach(string s in Product.ToStringList()){
            //     stringlist.Add($"| {s}");
            // }
            stringlist.Add($"{Quantity}");
            //stringlist.Add($"------------------------------");
            stringlist.Add($"{Total}");
            return stringlist;
        
        //turns itself into a list of variables
            
        }
        #nullable enable
        public ArrayList ToArrayList(ArrayList? p_al){
            if(p_al == null){p_al = new ArrayList();}
            p_al.Add(Id);
            p_al.Add(Quantity);
            p_al.AddRange(Product.ToArrayList(p_al));
            p_al.Add(Total);
            return p_al;
        }
        #nullable disable

        //Methods ---------------------------------------------------------------------------------
        public decimal CalculateTotalPrice(){
            return Product.Price * Quantity;
        }
    }

    public partial class Product : IClass
    {
        //Variables -----------------------------------------------------------------------------
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Category { get; set; }
        [Required]
        public decimal Price { get; set; }

        [Required]
        public virtual List<LineItem> LineItems { get; set; }

        //Constructors ---------------------------------------------------------------------------
        public Product(){}
        public Product(int p_Id){this.Id = p_Id;}
        public Product(int p_Id, string p_name):this(p_Id){this.Name = p_name;}
        public Product(int p_Id, string p_name, string p_description):this(p_Id, p_name){this.Description = p_description;}
        public Product(int p_Id, string p_name, string p_description, string p_category):this(p_Id, p_name, p_description){this.Category = p_category;}
        public Product(int p_Id, string p_name, string p_description, string p_category, decimal p_price):this(p_Id, p_name, p_description, p_category){this.Price = p_price;}
        public Product(string p_name, string p_description, string p_category):this(){this.Name = p_name; this.Description = p_description; this.Category = p_category;}
        public Product(string p_name, string p_description, string p_category, decimal p_price):this(p_name, p_description, p_category){this.Price = p_price;}

        //Interface --------------------------------------------------------------------------------
        public string Identify() { return "Product"; }
        public List<string> ToStringList(){
            List<string> stringlist = new List<string>(){
            $"{Id}",
            $"{Name}",
            $"{Description}",
            $"{Category}",
            $"{Price}"};
            return stringlist;
        }
        #nullable enable
        public ArrayList ToArrayList(ArrayList? p_al){
            if(p_al == null){p_al = new ArrayList();}
            p_al.Add(Id);
            p_al.Add(Name);
            p_al.Add(Description);
            p_al.Add(Category);
            p_al.Add(Price);
            return p_al;
        }
        #nullable disable
    }

    //User has a username. Maybe an email and phone number. A password is only stored in the database through the business layer.
    public partial class User : IClass
    {
        //Variables -----------------------------------------------------------------------------
        [Key]
        public int Id { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Phone { get; set; }

        //Constructors ---------------------------------------------------------------------------
        public User(){}
        public User(int p_Id):this(){this.Id = p_Id;}
        public User(string p_username):this(){this.Username = p_username;}
        public User(string p_username, string password):this(p_username){this.Password = password;}
        public User(string p_username, string password, string p_email):this(p_username, password){this.Email = p_email;}
        public User(string p_username, string password, string p_email, string p_phone):this(p_username, password, p_email){this.Phone = p_phone;}
        public User(string p_username, string password, string p_email, string p_phone, int p_Id):this(p_username, password, p_email, p_phone){this.Id = p_Id;}

        //Interface --------------------------------------------------------------------------------
        public string Identify() { return "User"; }
        public List<string> ToStringList(){
            List<string> stringlist = new List<string>(){
            $"Username: {Username}",
            $"Email: {Email}",
            $"Phone: {Phone}"};
            return stringlist;
        }
    }
}