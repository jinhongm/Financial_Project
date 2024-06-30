using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend_api.Data;
using backend_api.Dtos.Stock;
using backend_api.Interfaces;
using backend_api.Mappers;
using backend_api.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
 

namespace backend_api.Controllers  // 确保这是正确的命名空间
{
    [Route("api/stock")]
    [ApiController]
    public class StockController : ControllerBase
    {
        private readonly ApplicationDBContext _context;
        private readonly IStockRepository _stockRepository;

        public StockController(ApplicationDBContext context, IStockRepository stockRepository)
        {
            _stockRepository = stockRepository;
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] QueryObject query)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var stocks = await _stockRepository.GetAllAsync(query);

            var stockDto = stocks.Select(s => s.ToStockDto());

            return Ok(stockDto);

        }

        // Database 才需要 await
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);           
            var stock = await _stockRepository.GetByIdAsync(id);
            if (stock == null)
            {
                return NotFound();
            }
            return Ok(stock.ToStockDto());
        }
        /*
        StockController 使用 _context （Entity Framework 的 DbContext）直接与数据库进行交互，获取 Stock 对象，然后使用 Mapper 将这些对象转换为 DTO，返回给前端。
        */
        /*
        方法返回 CreatedAtAction，这是 ASP.NET Core 提供的一种特殊类型的 ActionResult，用于在创建资源后返回一个 201（Created）状态码。这个方法需要几个参数：
            actionName: 调用以获取新创建的资源的控制器动作的名称。这里是 GetById，这意味着应该有一个名为 GetById 的方法能够接受一个 id 参数并返回相应的 Stock 实体。
            routeValues: 包含路由参数的对象，这里传递新创建的 Stock 实体的 Id。
            value: 要返回给客户端的内容，通常是新创建的资源的表示。这里，stockModel.ToStockDto() 应该是将 Stock 实体转换为更适合返回给客户端查看的格式。    
        */
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateStockRequestDto stockDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);           
            var stockModel = stockDto.ToStockFromCreateDTO();
            stockModel = await _stockRepository.CreateAsync(stockModel);
            return CreatedAtAction(nameof(GetById), new { id = stockModel.Id }, stockModel.ToStockDto());
        }
        
        // Database 需要 await
        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateStockRequestDto stockDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);           
            var stockModel = await _stockRepository.UpdateAsync(id, stockDto);
            if (stockModel == null)
            {
                return NotFound();
            }
            return Ok(stockModel.ToStockDto());
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);           
            var stockModel = await _stockRepository.DeleteAsync(id);
            if (stockModel == null)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}