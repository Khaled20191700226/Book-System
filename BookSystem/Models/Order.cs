using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.Text.Json.Serialization;

namespace BookSystem.Models
{
    public class Order
    {


        public int Id { get; set; }                // رقم الطلب (Primary Key)
        public DateTime OrderDate { get; set; }    // تاريخ الطلب
        public decimal TotalAmount { get; set; }   // إجمالي السعر
        //fk
        public int CustomerId { get; set; }
        [ValidateNever]
        public Customer Customer { get; set; }

        [JsonIgnore]
        public ICollection<OrderItem>? OrderItems { get; set; } = new List<OrderItem>();


    }
}
