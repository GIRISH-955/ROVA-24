using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ROVA_24.DTO.ReviewsDTO;
using ROVA_24.IServices;
using ROVA_24.Services;

namespace ROVA_24.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewsController : ControllerBase
    {
        private readonly IReviewsServices _reviewsServices;
        public ReviewsController(IReviewsServices reviewsServices)
        {
            _reviewsServices = reviewsServices;
        }
        [EnableCors("CORSPolicy")]
        [HttpPost("addreviews")]
        public async Task<IActionResult> addReviewsAsync(ReviewsRequestDTO reviewRequestDTO)
        {
            var Result = await _reviewsServices.AddReviewsAsync(reviewRequestDTO);
            if(Result.Success)
                return Ok(Result);
            if(Result.Success && Result.Message== "Error occured while adding reviews")
                return BadRequest(Result);
            return BadRequest(Result);
        }
        [EnableCors("CORSPolicy")]
        [HttpGet("getAllReviews")]
        public async Task<IActionResult> getAllReviews()
        {
            var result = await _reviewsServices.GetAllReviewsAsync();
            if (result.Success)
                return Ok(result);
            return BadRequest(result);
        }
        [EnableCors("CORSPolicy")]
        [HttpGet("getReviews{reviewId}")]
        public async Task<IActionResult> getReviewsById(int reviewId)
        {
            var result = await _reviewsServices.getReviewsByIdAsync(reviewId);
            if (result.Success)
                return Ok(result);
            if (result.Success && result.Message == "Address not found")
                return BadRequest(result);
            return BadRequest(result);
        }
        [EnableCors("CORSPolicy")]
        [HttpDelete("DeleteReviews")]
        public async Task<IActionResult> deleteReviewsById(int reviewId)
        {
            var result = await _reviewsServices.deleteReviewsByIdAsync(reviewId);
            if(result.Success)
                return Ok(result);
            if (result.Success && result.Message == "Reviews Does not exist")
                return BadRequest(result);
            return BadRequest(result);
        }
    }
}
