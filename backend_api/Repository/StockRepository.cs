using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend_api.Data;
using backend_api.Dtos.Stock;
using backend_api.Interfaces;
using backend_api.Models;
using backend_api.Helpers;
using Microsoft.EntityFrameworkCore;


namespace backend_api.Repository
{
    public class StockRepository : IStockRepository
    {
        private readonly ApplicationDBContext _context;
        public StockRepository(ApplicationDBContext context){
            _context = context;
        }
        // 这里使用了 .Include(c => c.Comments) 来加载 Stock 实体关联的 Comments 导航属性。因为 FindAsync 不支持 .Include，所以你不能使用 FindAsync 来实现相同的效果。如果你尝试使用 FindAsync 加载导航属性，你会发现它无法做到，因为 FindAsync 只能加载实体本身而不加载其关联的导航属性。
        public async Task<List<Stock>> GetAllAsync(QueryObject query)
        {
            IQueryable<Stock> queryable = _context.Stocks.Include(c => c.Comments);

            if (!string.IsNullOrWhiteSpace(query.CompanyName))
            {
                queryable = queryable.Where(s => s.CompanyName.Contains(query.CompanyName));
            }
            if (!string.IsNullOrWhiteSpace(query.Symbol))
            {
                queryable = queryable.Where(s => s.Symbol.Contains(query.Symbol));
            }

            // 确保query.SortBy不为空，并且检查是否是按Symbol排序
            if (!string.IsNullOrWhiteSpace(query.SortBy) && query.SortBy.Equals("Symbol", StringComparison.OrdinalIgnoreCase))
            {
                    // 根据isDescending布尔值决定使用OrderBy还是OrderByDescending
                queryable = query.isDescending ? queryable.OrderByDescending(s => s.Symbol) : queryable.OrderBy(s => s.Symbol);
            }

            var skipNumber = (query.PageNumber - 1) * query.PageSize;

            // 这里使用ToListAsync来执行实际的数据库查询，并且是异步的
            return await queryable.Skip(skipNumber).Take(query.PageSize).ToListAsync();
        }
        // 在 Entity Framework Core 中，Include 方法用于指定在查询数据库时应该加载的关联实体或导航属性。这个方法是处理关系型数据的一部分，允许你在检索主实体时同时拉取与之关联的其他实体。
        // 这种机制称为“饥饿加载”（Eager Loading），即主动加载与主实体有关系的其他实体，以避免后续单独查询它们时的多次数据库访问。
        // 这里使用了 .Include(c => c.Comments) 来加载 Stock 实体关联的 Comments 导航属性。因为 FindAsync 不支持 .Include，所以你不能使用 FindAsync 来实现相同的效果。
        // 如果你尝试使用 FindAsync 加载导航属性，你会发现它无法做到，因为 FindAsync 只能加载实体本身而不加载其关联的导航属性。
        public async Task<Stock?> GetByIdAsync(int id)
        {
            return await _context.Stocks.Include(c => c.Comments).FirstOrDefaultAsync(x => x.Id == id);
    
        }
        // _stockRepository.CreateAsync(stockDto) 方法应该异步地将数据添加到数据库中，
        // 并在成功后返回创建的 Stock 实体。这个实体中应该包含数据库自动生成的 Id，这个 Id 是在数据被保存到数据库时由数据库自动设置的（例如，如果使用的是 SQL Server，Id 可以设置为自增字段）。
        public async Task<Stock> CreateAsync(Stock stockDto){
            await _context.Stocks.AddAsync(stockDto);
            await _context.SaveChangesAsync();
            return stockDto;
        }

        public async Task<Stock?> UpdateAsync(int id, UpdateStockRequestDto stockDto){
            var stockModel = await _context.Stocks.FirstOrDefaultAsync(x => x.Id == id);
            if (stockModel == null){
                return null;
            }
            stockModel.Symbol = stockDto.Symbol;
            stockModel.CompanyName = stockDto.CompanyName;
            stockModel.Purchase = stockDto.Purchase;
            stockModel.LastDiv = stockDto.LastDiv;
            stockModel.Industry = stockDto.Industry;
            stockModel.MarketCap = stockDto.MarketCap;
            await _context.SaveChangesAsync();
            return stockModel;
        }

        public async Task<Stock?> DeleteAsync(int id){
            var existing_Model = await _context.Stocks.FirstOrDefaultAsync( x => x.Id == id);
            if (existing_Model == null)
            {
                return null;
            }
            _context.Stocks.Remove(existing_Model);
            await _context.SaveChangesAsync();
            return existing_Model;
        }

        public Task<bool> StockExists(int id)
        {
            return _context.Stocks.AnyAsync(s => s.Id == id);
        }

        public async Task<Stock?> GetBySymbolAsync(string symbol){
            return await _context.Stocks.FirstOrDefaultAsync(s => s.Symbol == symbol);
        }
    }
}