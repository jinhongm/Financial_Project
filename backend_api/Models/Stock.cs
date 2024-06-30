using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace backend_api.Models
{   // 在C#中，get 和 set 是属性（Properties）的访问器（Accessors），用于封装类或结构中字段的访问。
    [Table("Stocks")]
    public class Stock
    {
        // 这是Stock类的主键。在数据库中，它通常会被设置为自动增长的标识列，用于唯一标识每条记录。
        public int Id { get; set; }

        // 表示股票的代码，如 "AAPL" 代表苹果公司。初始化为一个空字符串，防止该字段为null。
        public string Symbol {get; set;} = string.Empty;
        // TypeName："decimal(18,2)" 表示该列在数据库中将被设置为 decimal 类型，其中18是数字的总位数（精度），2是小数点后的位数（小数精度）。
        public string CompanyName { get; set; } = string.Empty;
        
        // 表示股票的购买价
        [Column(TypeName = "decimal(18,2)")]

        public decimal Purchase { get; set;}
        // 表示最后一次分红
        [Column(TypeName = "decimal(18,2)")]

        public decimal LastDiv { get; set; }
        // I表示公司所属的行业
        public string? Industry { get; set; } 
        // 表示公司的市值。
        public long MarketCap { get; set; }

        public List<Comment> Comments { get; set; } = new List<Comment>();

        public List<Portfolio> Portfolios = new List<Portfolio>();
    }
}