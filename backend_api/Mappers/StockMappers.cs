using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend_api.Dtos;
using backend_api.Models;
using backend_api.Dtos.Stock;


// Mapper 用于转换 DTO 和业务模型之间的数据。这样可以确保视图和外部 API 不直接与数据模型交互，增加了数据处理的灵活性和安全性。
// StockMappers 类中的方法，如 ToStockDto 和 ToStockFromCreateDTO，就是进行这种转换的地方。它们分别将 Stock 对象转换为 StockDto，以及从 CreateStockRequestDto 创建 Stock 对象。
namespace backend_api.Mappers
{
    public static class StockMappers
    {
        public static StockDto ToStockDto(this Stock stockModel){
            return new StockDto
            {
                Id = stockModel.Id,
                Symbol = stockModel.Symbol,
                CompanyName = stockModel.CompanyName,
                Purchase = stockModel.Purchase,
                LastDiv = stockModel.LastDiv,
                Industry = stockModel.Industry,
                MarketCap = stockModel.MarketCap,
                Comments = stockModel.Comments.Select(c => c.ToCommentDto()).ToList()

            };
        }

        public static Stock ToStockFromCreateDTO(this CreateStockRequestDto stockDto)
        {
            return new Stock
            {
                Symbol = stockDto.Symbol,
                CompanyName = stockDto.CompanyName,
                Purchase = stockDto.Purchase,
                LastDiv = stockDto.LastDiv,
                Industry = stockDto.Industry,
                MarketCap = stockDto.MarketCap,
            };
        }
    }
}