using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend_api.Models;
using backend_api.Dtos.Comment;
using backend_api.Helpers;

namespace backend_api.Interfaces
{
    public interface ICommentRepository 
    {
        Task<List<Comment>> GetAllAsync(CommentQueryObject commentQueryObject);

        public Task<Comment?> GetByIdAsync(int id);

        public Task<Comment> CreateAsync(Comment commentModel);


        public Task<Comment?> DeleteAsync(int id);

        public Task<Comment?> UpdateAsync(int id, UpdateCommentRequestDto commentRequest);

    }
}