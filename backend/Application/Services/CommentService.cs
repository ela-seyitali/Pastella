using Pastella.Backend.Core.DTOs;
using Pastella.Backend.Core.Entities;
using Pastella.Backend.Core.Interfaces;
using Pastella.Backend.Infrastructure.Repositories;

namespace Pastella.Backend.Application.Services
{
    public class CommentService : ICommentService
    {
        private readonly IRepository<Comment> _commentRepository;
        private readonly CommentRepository _commentRepo;

        public CommentService(IRepository<Comment> commentRepository)
        {
            _commentRepository = commentRepository;
            _commentRepo = (CommentRepository)commentRepository;
        }

        public async Task<CommentDto?> CreateComment(CreateCommentDto createCommentDto, int userId)
        {
            var comment = new Comment
            {
                UserId = userId,
                CakeId = createCommentDto.CakeId,
                Rating = createCommentDto.Rating,
                Message = createCommentDto.Message,
                CreatedDate = DateTime.UtcNow
            };

            await _commentRepository.Create(comment);

            // Get the created comment with user info
            var createdComment = await _commentRepository.GetById(comment.Id);
            if (createdComment == null) return null;

            return new CommentDto
            {
                Id = createdComment.Id,
                Rating = createdComment.Rating,
                Message = createdComment.Message,
                CreatedDate = createdComment.CreatedDate,
                UserName = createdComment.User.FullName, // FullName olarak değiştirildi
                CakeId = createdComment.CakeId
            };
        }

        public async Task<IEnumerable<CommentDto>> GetCakeComments(int cakeId)
        {
            var comments = await _commentRepo.GetCakeComments(cakeId);
            return comments.Select(c => new CommentDto
            {
                Id = c.Id,
                Rating = c.Rating,
                Message = c.Message,
                CreatedDate = c.CreatedDate,
                UserName = c.User.FullName, // FullName olarak değiştirildi
                CakeId = c.CakeId
            });
        }

        public async Task<bool> DeleteComment(int commentId, int userId)
        {
            var comment = await _commentRepository.GetById(commentId);
            if (comment == null || comment.UserId != userId) return false;

            await _commentRepository.Delete(commentId);
            return true;
        }
    }
}