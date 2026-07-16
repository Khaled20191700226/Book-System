using BookSystem.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.Text.Json.Serialization;

namespace BookSystem.Models
{
    public class Customer
    {
        public int Id { get; set; }             // رقم العميل (المفتاح الأساسي)
        public string Name { get; set; }        // اسم العميل
        public string Email { get; set; }       // البريد الإلكتروني

        [ValidateNever]
        // علاقة واحد إلى متعدد: العميل الواحد ممكن يكون ليه أكتر من طلب
       [JsonIgnore]
        public ICollection<Order>? Orders { get; set; } = new List<Order>();


    }
}

