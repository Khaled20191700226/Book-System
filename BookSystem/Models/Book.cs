using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace BookSystem.Models
{
    public class Book
    {
        public int Id { get; set; }             // رقم الكتاب (Primary Key)
        public string Title { get; set; }       // عنوان الكتاب
        public string Author { get; set; }      // المؤلف
        public decimal Price { get; set; }      // السعر
        [JsonIgnore]
        // العلاقة: الكتاب ممكن يظهر في أكتر من عنصر طلب
        public ICollection<OrderItem>? OrderItems { get; set; } = new List<OrderItem>();
    
    }

}



