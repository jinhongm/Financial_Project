using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend_api.Dtos.Comment;
using backend_api.Interfaces;
using backend_api.Mappers;
using backend_api.Models;
using backend_api.Data;
using backend_api.Mappers;
using backend_api.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace backend_api.Controllers
{
    [Route("api/comment")]
    public class CommentController : ControllerBase
    {
        // private readonly ApplicationDBContext _context;
        private readonly ICommentRepository _commentRepository;
        private readonly IStockRepository _stockRepo;

        // 泛型的用法  UserManager是一个泛型类， AppUser是它的类型参数
        private readonly UserManager<AppUser> _userManager;

        private readonly IFMPService _fmpService;
        public CommentController(ApplicationDBContext context, ICommentRepository commentRepository, IStockRepository stockRepo,
        UserManager<AppUser> userManager, IFMPService fmpService)
        {
            // _context = context;
            _commentRepository = commentRepository;
            _stockRepo = stockRepo;
            _userManager = userManager;
            _fmpService = fmpService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var comments = await _commentRepository.GetAllAsync();
            var commentDto = comments.Select( s => s.ToCommentDto());
            return Ok(commentDto);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);            
            var comment = await _commentRepository.GetByIdAsync(id);
            if (comment == null){
                return null;
            }
            return Ok(comment.ToCommentDto());
        }

        [HttpPost("{symbol:alpha}")]
        public async Task<IActionResult> Create([FromRoute] string symbol, [FromBody] CreateCommentDto commentDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);            
            var stock = await _stockRepo.GetBySymbolAsync(symbol);
            if (stock == null){
                stock = await _fmpService.FindStockBySymbolAsync(symbol);
                if (stock == null){
                    return BadRequest("Stock does not exist");
                }
                else{
                    await _stockRepo.CreateAsync(stock);
                }
            }
            // User is inherited from ClaimsPrincipal, which is defined in the Controller and ControllerBase.
            // Identity is used to provide middleware for registering, logging in, logging out, and identity management.
            var username = User.GetUsername();
            // Every HTTP Request will include a User
            var appUser = await _userManager.FindByNameAsync(username);

            var commentModel = commentDto.ToCommentFromCreateDTO(stock.Id);
            commentModel.AppUserId = appUser.Id;
            commentModel = await _commentRepository.CreateAsync(commentModel);
            return CreatedAtAction(nameof(GetById), new { id = commentModel.Id }, commentModel.ToCommentDto());
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);            
            var commentModel = await _commentRepository.DeleteAsync(id);
            if (commentModel == null) {
                return NotFound();
            }
            return NoContent();
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateCommentRequestDto commentDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var commentModel = await _commentRepository.UpdateAsync(id, commentDto);
            if (commentModel == null)
            {
                return NotFound();
            }
            return Ok(commentModel.ToCommentDto());
        }
    }
}