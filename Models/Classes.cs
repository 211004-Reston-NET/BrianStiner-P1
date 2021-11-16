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
        [RegularExpression(@"^[a-zA-Z,'.\s]+$", ErrorMessage = "Name must be letters, commas, apostrophes, periods, or spaces.")]
        [StringLength(50, ErrorMessage = "Name must be less than 50 characters.")] 
        [Required]
        public string Name { get; set; }
        [RegularExpression(@"^[0-9]{1,6}[a-zA-Z\s,.'-]+[0-9]{5}$", ErrorMessage = "Address must be letters, numbers, commas, apostrophes, periods, or spaces.")]
        [StringLength(50, ErrorMessage = "Address must be less than 50 characters.")]
        [Required]
        public string Address { get; set; }
        [Required]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$", ErrorMessage = "Email must be a valid email address.")]
        [StringLength(50, ErrorMessage = "Email must be less than 50 characters.")]
        public string Email { get; set; }
        [Required]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Phone must be in the format of (xxx)-xxx-xxxx.")]
        [StringLength(12, ErrorMessage = "Phone must be less than 12 characters.")]
        public string Phone { get; set; }
        [Required]
        [DataType(DataType.Currency), Column(TypeName = "decimal(18, 2)")]
        public decimal TotalSpent { get; set; }

        public int Picture { get; set; }

        public virtual List<Order> Orders { get; set; }

        //Constructors ---------------------------------------------------------------------------
        public Customer(){TotalSpent = 0; Orders = new List<Order>();}
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
            p_al.Add(Orders.Count);
            foreach(Order o in Orders){
                p_al.Add(o.Id);
                p_al.Add(o.Active);
                p_al.Add(o.Address);
                p_al.Add(o.LineItems.Count);
                foreach(LineItem li in o.LineItems){
                    p_al.Add(li.Id);
                    p_al.Add(li.Quantity);
                    p_al.Add(li.Product.Id);
                    p_al.Add(li.Product.Name);
                    p_al.Add(li.Product.Description);
                    p_al.Add(li.Product.Category);
                    p_al.Add(li.Product.Price);
                }

            }
            return p_al;
        } 
        #nullable disable
        public Customer FromArrayList(ArrayList p_al){
            int i = 0;
            Id = (int)p_al[i++];
            Name = (string)p_al[i++];
            Phone = (string)p_al[i++];
            Email = (string)p_al[i++];
            Address = (string)p_al[i++];
            TotalSpent = (decimal)p_al[i++];
            for(int j = 0; j < (int)p_al[i++]; j++){
                Order order = new Order();
                order.Id = (int)p_al[i++];
                order.Active = (bool)p_al[i++];
                order.Address = (string)p_al[i++];
                for(int k = 0; k < (int)p_al[i++]; k++){
                    LineItem lineitem = new LineItem();
                    lineitem.Id = (int)p_al[i++];
                    lineitem.Quantity = (int)p_al[i++];
                        Product product = new Product();
                        product.Id = (int)p_al[i++];
                        product.Name = (string)p_al[i++];
                        product.Description = (string)p_al[i++];
                        product.Category = (string)p_al[i++];
                        product.Price = (decimal)p_al[i++];
                    lineitem.Product = product;
                    order.LineItems.Add(lineitem);
                }
                Orders.Add(order);
            }
            return this;
        }

       
    }

    public partial class Store : IClass
    {
        //Variables -----------------------------------------------------------------------------
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Name must be less than 50 characters.")]
        [RegularExpression(@"^[a-zA-Z,'.\s]+$", ErrorMessage = "Name must be letters, spaces, apostrophes,  periods, and commas only.")]
        public string Name { get; set; }

        [StringLength(50, ErrorMessage = "Address must be less than 50 characters.")]
        [RegularExpression(@"^[0-9]{1,6}[a-zA-Z\s,.'-]+[0-9]{5}$", ErrorMessage = "Address must be have a street number, street name, and zip code.")]
        [Required]
        public string Address { get; set; }

        [Required]
        [DataType(DataType.Currency), Column(TypeName = "decimal(18, 2)")]
        public decimal Expenses { get; set; }

        [Required]
        [DataType(DataType.Currency), Column(TypeName = "decimal(18, 2)")]
        public decimal Revenue { get; set; }

        [NotMapped]
        public decimal Profit {get => Revenue-Expenses; set => Profit = value;}

        public virtual List<InventoryItem> Inventory { get; set; }

        
        //Constructors ---------------------------------------------------------------------------
        public Store(){}
        public Store(int p_Id){this.Id = p_Id;}
        public Store(string p_name, string p_address){this.Name = p_name;this.Address = p_address; }
        public Store(string p_name, string p_address, decimal p_expenses):this(p_name, p_address){this.Expenses = p_expenses;}
        public Store(string p_name, string p_address, decimal p_expenses, decimal p_revenue):this(p_name, p_address, p_expenses){this.Revenue = p_revenue;}
        public Store(string p_name, string p_address, decimal p_expenses, decimal p_revenue, List<InventoryItem> p_Inventory):this(p_name, p_address, p_expenses, p_revenue){this.Inventory = p_Inventory;}
        public Store(string p_name, string p_address, decimal p_expenses, decimal p_revenue, List<InventoryItem> p_Inventory, int p_id):this(p_name, p_address, p_expenses, p_revenue, p_Inventory){this.Id = p_id;}
       
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
            foreach(var item in Inventory){p_al.Add(item.ToArrayList(p_al));}
            return p_al;
        }
        #nullable disable
        public Store FromArrayList(ArrayList p_al){
            int i = 0;

            Id = (int)p_al[i++];
            Name = (string)p_al[i++];
            Address = (string)p_al[i++];
            Expenses = (decimal)p_al[i++];
            Revenue = (decimal)p_al[i++];
            Profit = (decimal)p_al[i++];
            while(i < p_al.Count){
                InventoryItem inv = new InventoryItem();
                inv.FromArrayList((ArrayList)p_al[i++]);
                Inventory.Add(inv);
            }

            return this;
        }
            
    }

    public partial class Order : IClass
    {
        //Variables -----------------------------------------------------------------------------
        [Key]
        public int Id { get; set; }
        [Required]
        public int CustomerId { get; set; }

        [Required]
        [RegularExpression(@"^[0-9]{1,6}[a-zA-Z\s,.'-]+[0-9]{5}$", ErrorMessage = "Address must be have a street number, street name, and zip code.")]
        [StringLength(50, ErrorMessage = "Address must be less than 50 characters.")]
        public string Address { get; set; }

        [Required]
        public bool? Active { get; set; }
        
        [NotMapped]
        public decimal Total { get => CalculateTotalPrice(); set => Total = value; }


        public virtual List<LineItem> LineItems { get; set; }
        public virtual Customer Customer { get; set; }

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
            stringlist.Add($"{Total}");
            return stringlist;
        }
        #nullable enable
        public ArrayList ToArrayList(ArrayList? p_al){
            if(p_al == null){p_al = new ArrayList();}

            p_al.Add(Id);
            p_al.Add(Address);
            p_al.Add(Active);
            p_al.Add(Total);
            foreach(var item in LineItems){p_al.Add(item.ToArrayList(p_al));}

            return p_al;
        }
        #nullable disable
        public Order FromArrayList(ArrayList p_al){
            int i = 0;

            Id = (int)p_al[i++];
            Address = (string)p_al[i++];
            Active = (bool)p_al[i++];
            Total = (decimal)p_al[i++];
            while(i < p_al.Count){
                LineItem lineitem = new LineItem();
                lineitem.FromArrayList((ArrayList)p_al[i++]);
                LineItems.Add(lineitem);
            }
            
            return this;
        }
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
        public int OrderId { get; set; }

        [Required]
        [Range(1, 999, ErrorMessage = "Quantity must be between 1 and 999.")]
        public int Quantity { get; set; }

        [NotMapped]
        public decimal Total { get => CalculateTotalPrice(); set => Total = value; }

        public virtual Product Product { get; set; }
        public virtual Order Order { get; set; }


        //Constructors ---------------------------------------------------------------------------
        public LineItem(){}
        public LineItem(int p_Id):this(){this.Id = p_Id;}
        public LineItem(int p_Id, int p_quantity):this(p_Id){this.Quantity = p_quantity;}
        public LineItem(int p_Id, int p_quantity, decimal p_total):this(p_Id, p_quantity){this.Total = p_total;}
        public LineItem(int p_Id, int p_quantity, decimal p_total, Product p_product):this(p_Id, p_quantity, p_total){this.Product = p_product;}
        public LineItem(int p_Id, int p_quantity, Product p_product):this(p_Id, p_quantity){this.Product = p_product;}



        //Interface --------------------------------------------------------------------------------
        public string Identify() { return "LineItem"; }
        public List<string> ToStringList(){
            List<string> stringlist = new List<string>();
            stringlist.Add($"{Quantity}");
            stringlist.Add($"{Total}");
            return stringlist;
        
        //Turns itself into a list of variables    
        }
        #nullable enable
        public ArrayList ToArrayList(ArrayList? p_al){
            if(p_al == null){p_al = new ArrayList();}

            p_al.Add(Id);
            p_al.Add(Quantity);
            p_al.Add(Total);
            p_al.Add(Product.ToArrayList(p_al));

            return p_al;
        }
        #nullable disable
        //Unpacks itself from a list of variables
        public LineItem FromArrayList(ArrayList p_al){
            int i = 0;

            Id = (int)p_al[i++];
            Quantity = (int)p_al[i++];
            Total = (decimal)p_al[i++];
            Product = Product.FromArrayList((ArrayList)p_al[i++]);

            return this;
        }

        //Methods ---------------------------------------------------------------------------------
        public decimal CalculateTotalPrice(){
            return Product.Price * Quantity;
        }
    }
    public partial class InventoryItem : IClass
    {
        //Variables -----------------------------------------------------------------------------
        [Key]
        public int Id { get; set; }
        [Required]
        public int ProductId { get; set; }
        [Required]
        public int StoreId { get; set; }

        [Required]
        [Range(1, 999, ErrorMessage = "Quantity must be between 1 and 999.")]
        public int Quantity { get; set; }

        [NotMapped]
        public decimal Total { get => CalculateTotalPrice(); set => Total = value; }

        public virtual Product Product { get; set; }
        public virtual Store Store { get; set; }


        //Constructors ---------------------------------------------------------------------------
        public InventoryItem(){}
        public InventoryItem(int p_Id):this(){this.Id = p_Id;}
        public InventoryItem(int p_Id, int p_quantity):this(p_Id){this.Quantity = p_quantity;}
        public InventoryItem(int p_Id, int p_quantity, decimal p_total):this(p_Id, p_quantity){this.Total = p_total;}
        public InventoryItem(int p_Id, int p_quantity, decimal p_total, Product p_product):this(p_Id, p_quantity, p_total){this.Product = p_product;}
        public InventoryItem(int p_Id, int p_quantity, Product p_product):this(p_Id, p_quantity){this.Product = p_product;}



        //Interface --------------------------------------------------------------------------------
        public string Identify() { return "InventoryItem"; }
        public List<string> ToStringList(){
            List<string> stringlist = new List<string>();
            stringlist.Add($"{Quantity}");
            stringlist.Add($"{Total}");
            return stringlist;
        
       
        }
        #nullable enable //Turns itself into a list of variables    
        public ArrayList ToArrayList(ArrayList? p_al){
            if(p_al == null){p_al = new ArrayList();}

            p_al.Add(Id);
            p_al.Add(Quantity);
            p_al.Add(Total);
            p_al.Add(Product.ToArrayList(p_al));

            return p_al;
        }
        #nullable disable
        //Unpacks itself from a list of variables
        public InventoryItem FromArrayList(ArrayList p_al){
            int i = 0;

            Id = (int)p_al[i++];
            Quantity = (int)p_al[i++];
            Total = (decimal)p_al[i++];
            Product = Product.FromArrayList((ArrayList)p_al[i++]);

            return this;
        }

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
        [RegularExpression(@"^[a-zA-Z\s,.'-]+$", ErrorMessage = "Name must be letters, spaces, commas, periods, and apostrophes.")]
        [StringLength(50, ErrorMessage = "Name must be less than 50 characters.")]
        public string Name { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Description must be less than 50 characters.")]
        [RegularExpression(@"^[a-zA-Z\s,.%&$#'-]+$", ErrorMessage = "Description must be letters, percents, dollars, hashtags, commas, periods, and apostrophes.")]
        public string Description { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Category must be less than 50 characters.")]
        [RegularExpression(@"^[a-zA-Z\s,.'-]+$", ErrorMessage = "Category must be letters, commas, periods, and apostrophes.")]
        public string Category { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)"), DataType(DataType.Currency)]
        public decimal Price { get; set; }

        public List<LineItem> LineItems { get; set; }
        public List<InventoryItem> Inventory { get; set; }


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
        public Product FromArrayList(ArrayList p_al){
            int i = 0;

            Id = (int)p_al[i++];
            Name = (string)p_al[i++];
            Description = (string)p_al[i++];
            Category = (string)p_al[i++];
            Price = (decimal)p_al[i++];

            return this;
        }
    }


    
    //User has a username. Maybe an email and phone number. A password is only stored in the database through the business layer.
    //User is a vestigial limb now becuase I don't have time to make it work.
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
        #nullable enable
        public ArrayList ToArrayList(ArrayList? p_al){
            if(p_al == null){p_al = new ArrayList();}

            p_al.Add(Id);
            p_al.Add(Username);
            p_al.Add(Password);
            p_al.Add(Email);
            p_al.Add(Phone);

            return p_al;
        }
        #nullable disable
        public User FromArrayList(ArrayList p_al){
            int i = 0;

            Id = (int)p_al[i++];
            Username = (string)p_al[i++];
            Password = (string)p_al[i++];
            Email = (string)p_al[i++];
            Phone = (string)p_al[i++];

            return this;
        }
    }
}