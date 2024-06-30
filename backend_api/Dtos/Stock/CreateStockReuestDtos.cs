using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using backend_api.Dtos.Comment;
// DTO (Data Transfer Object)
// DTO 用于封装数据并将其传输层与业务逻辑分离。它帮助在不同层之间传递数据，而不需要暴露数据库实体。
// StockDto 和 CreateStockRequestDto 是 DTO 的例子，其中 StockDto 用于数据的读操作，而 CreateStockRequestDto 用于封装创建新股票时来自客户端的数据。
namespace backend_api.Dtos.Stock
{
    public class CreateStockRequestDto
    {
        [Required]
        [MaxLength(10, ErrorMessage = "Symbol cannot be over 10 over characters")]
        public string Symbol { get; set; } = string.Empty;
        [Required]
        [MaxLength(10, ErrorMessage = "Company Name cannot be over 10 over characters")]
        public string CompanyName { get; set; } = string.Empty;
        [Required]
        [Range(1, 1000000000)]
        public decimal Purchase { get; set; }
        [Required]
        [Range(0.001, 100)]
        public decimal LastDiv { get; set; }
        [Required]
        [MaxLength(10, ErrorMessage = "Industry cannot be over 10 characters")]
        public string Industry { get; set; } = string.Empty;
        [Range(1, 5000000000)]
        public long MarketCap { get; set; }
    }
}