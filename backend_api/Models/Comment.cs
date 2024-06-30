using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace backend_api.Models
{
    [Table("Comments")]
    public class Comment
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        // 这是一个可空的整型属性（int?），表示这个属性可以接受 null 值。StockId 作为外键，用于在数据库中关联 Comment 和 Stock 表。
        public int? StockId { get; set; }
        // Navigation Property
        // 这是一个导航属性，它允许在实体框架（Entity Framework）中直接通过 Comment 实例访问相关联的 Stock 实例。
        public Stock? Stock { get; set; }
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }
    }
}