using Pastella.Backend.Core.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pastella.Backend.Application.Services
{
    public interface ICommentService
    {
        Task<CommentDto?> CreateComment(CreateCommentDto createCommentDto, int userId);
        Task<IEnumerable<CommentDto>> GetCakeComments(int cakeId);
        Task<bool> DeleteComment(int commentId, int userId);
    }
}