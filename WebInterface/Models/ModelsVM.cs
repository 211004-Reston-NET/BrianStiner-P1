using Models;

namespace WebInterface.Models{
    
    public class CustomerVM{
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public decimal TotalSpent { get; set; }

        public CustomerVM(){ }
        
        public CustomerVM(Customer customer){
            Id = customer.Id;
            Name = customer.Name;
            Address = customer.Address;
            Phone = customer.Phone;
            Email = customer.Email;
            TotalSpent = customer.TotalSpent;
        }

        public Customer MapToModel(){
            return new Customer{
                Id = this.Id,
                Name = this.Name,
                Address = this.Address,
                Phone = this.Phone,
                Email = this.Email,
                TotalSpent = this.TotalSpent
            };
        }
    }

    public class StoreVM{
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public decimal Expenses { get; set; }
        public decimal Revenue { get; set; }
        public decimal Profit { get; set; }

        public StoreVM(){ }
        
        public StoreVM(Store p_store){
            this.Id = p_store.Id;
            this.Name = p_store.Name;
            this.Address = p_store.Address;
            this.Expenses = p_store.Expenses;
            this.Revenue = p_store.Revenue;
            this.Profit = p_store.Profit;
        }

        public Store MapToModel(){
            return new Store{
                Id = this.Id,
                Name = this.Name,
                Address = this.Address,
                Expenses = this.Expenses,
                Revenue = this.Revenue,
            };
        }
    }

    public class OrderVM{
        public int Id { get; set; }
        public string Address { get; set; }
        public bool Active { get; set; }
        public decimal Total { get; set; }

        public OrderVM(){ }
        
        public OrderVM(Order order){
            Id = order.Id;
            Address = order.Address;
            Active = (bool)order.Active;
            Total = order.Total;
        }

        public Order MapToModel(){
            return new Order{
                Id = Id,
                Address = Address,
                Active = Active,
            };
        }
    }

    public class LineItemVM{
        public int Id { get; set; }
        public int Quantity { get; set; }
        public decimal Total { get; set; }
        public int ProductId { get; set; }

        public LineItemVM(){ }

        public LineItemVM(LineItem lineItem){
            Id = lineItem.Id;
            Quantity = lineItem.Quantity;
            Total = lineItem.Total;
            ProductId = lineItem.ProductId;
        }

        public LineItem MapToModel(){
            return new LineItem{
                Id = Id,
                Quantity = Quantity,
                ProductId = ProductId
            };
        }
    }

    public class ProductVM{
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }

        public ProductVM(){ }

        public ProductVM(Product product){
            Id = product.Id;
            Name = product.Name;
            Price = product.Price;
            Description = product.Description;
            Category = product.Category;
        }

        public Product MapToModel(){
            return new Product{
                Id = Id,
                Name = Name,
                Price = Price,
                Description = Description,
                Category = Category
            };
        }
    }
    public class UserVM{
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public bool LoggedIn { get; set; }
        public UserVM(){ }

        public UserVM(User user){
            Username = user.Username;
            Password = user.Password;
            Email = user.Email;
            Phone = user.Phone;
        }

        public User MapToModel(){
            return new User{
                Username = Username,
                Password = Password,
                Email = Email,
                Phone = Phone
            };
        }
    }

    public class NewUserVM{
        public string Username { get; set; }
        public string Password1 { get; set; }
        public string Password2 { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

        public NewUserVM(){ }
        
    }
}   