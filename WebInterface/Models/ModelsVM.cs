using Models;

namespace WebInterface.Models{
    
    public class CustomerVM{
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        
        public CustomerVM(Customer customer){
            Id = customer.Id;
            Name = customer.Name;
            Address = customer.Address;
            Phone = customer.Phone;
            Email = customer.Email;
        }

        public Customer MapToModel(){
            return new Customer{
                Id = Id,
                Name = Name,
                Address = Address,
                Phone = Phone,
                Email = Email
            };
        }
    }

    public class StoreVM{
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        
        public StoreVM(Store store){
            Id = store.Id;
            Name = store.Name;
            Address = store.Address;
        }

        public Store MapToModel(){
            return new Store{
                Id = Id,
                Name = Name,
                Address = Address
            };
        }
    }

    public class OrderVM{
        public int Id { get; set; }
        public string Address { get; set; }
        public bool Active { get; set; }
        public decimal Total { get; set; }
        
        public OrderVM(Order order){
            Id = order.Id;
            Address = order.Address;
            Active = order.Active;
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
}   