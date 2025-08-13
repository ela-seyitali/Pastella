using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Pastella.Backend.Core.DTOs;
using Pastella.Backend.Application.Services;
using System.Security.Claims;

namespace Pastella.Backend.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CommentController : ControllerBase
    {
        private readonly ICommentService _commentService;

        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateComment([FromBody] CreateCommentDto createCommentDto)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
            var result = await _commentService.CreateComment(createCommentDto, userId);
            
            if (result != null)
            {
                return Ok(new { Message = "Yorum başarıyla eklendi", CommentId = result.Id });
            }
            return BadRequest(new { Message = "Yorum eklenemedi" });
        }

        [HttpGet("cake/{cakeId}")]
        public async Task<IActionResult> GetCakeComments(int cakeId)
        {
            var comments = await _commentService.GetCakeComments(cakeId);
            return Ok(comments);
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteComment(int id)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
            var result = await _commentService.DeleteComment(id, userId);
            
            if (result)
            {
                return Ok(new { Message = "Yorum silindi" });
            }
            return NotFound(new { Message = "Yorum bulunamadı veya silme yetkiniz yok" });
        }
    }
}