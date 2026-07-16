using BookSystem.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.Text.Json.Serialization;

namespace BookSystem.Models
{
    public class OrderItem
    {
        public int Id { get; set; }                // رقم العنصر داخل الطلب
        public int Quantity { get; set; }          // الكمية المطلوبة
        public decimal UnitPrice { get; set; }     // سعر الكتاب الواحد

        // العلاقة مع الطلب
        public int OrderId { get; set; }
       
        [ValidateNever]
        public Order Order { get; set; }

        // العلاقة مع الكتاب
        public int BookId { get; set; }
        [ValidateNever]
        public Book Book { get; set; }
    }
}

