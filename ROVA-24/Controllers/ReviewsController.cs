using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ROVA_24.DTO.ReviewsDTO;
using ROVA_24.DTO.UpdateAddressDTO;
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
            if (Result.Success)
                return Created("", new { status = "success",  Result.Data.reviewId, message = "Reviews added successfully" });
            if (!Result.Success && Result.Message == "Error occured while adding reviews")
                return BadRequest(new { status = "error", message = "Error occured while adding reviews" });
            return BadRequest(new { status = "error", message = "Invalid request payload" });
        }

        [EnableCors("CORSPolicy")]
        [HttpGet("getAllReviews")]
        public async Task<IActionResult> getAllReviews()
        {
            var result = await _reviewsServices.GetAllReviewsAsync();
            if (result.Success)
                return Ok(result.Data);
            return BadRequest(new { status = "error", message = result.Message });
        }

        [EnableCors("CORSPolicy")]
        [HttpGet("getReviews{reviewId}")]
        public async Task<IActionResult> getReviewsById(int reviewId)
        {
            var result = await _reviewsServices.getReviewsByIdAsync(reviewId);
            if (result.Success)
                return Ok(result);
            if (!result.Success && result.Message == "Reviews not found")
                return BadRequest(new { status = "error", message = "Reviews not found" });
            return BadRequest(new { status = "error", message = "Invalid request payload" });
        }
        [EnableCors("CORSPolicy")]
        [HttpPut("{reviewId}")]
        public async Task<IActionResult> updateReviewsById(int reviewId, [FromBody] ReviewsRequestDTO reviewsRequestDTO)
        {
            var result = await _reviewsServices.updateReviewsByIdAsync(reviewId, reviewsRequestDTO);
            if (result.Success)
                return Ok(new { status = "success", message = "Reviews Update successfully" });
            if (!result.Success && result.Message == "Reviews does not exist")
                return BadRequest(new { status = "error", message = "Reviews does not exist" });
            if (!result.Success && result.Message == "Product Does not exist")
                return BadRequest(new { status = "error", message = "Product Does not exist" });
            if (!result.Success && result.Message == "Customer does not exist")
                return BadRequest(new { status = "error", message = "Customer does not exist" });
            if (!result.Success && result.Message == "Error occured while updating")
                return BadRequest(new { status = "error", message = "Error occured while updating" });
            return BadRequest(new { status = "error", message = "Invalid request payload" });
        }

        [EnableCors("CORSPolicy")]
        [HttpDelete("DeleteReviews")]
        public async Task<IActionResult> deleteReviewsById(int reviewId)
        {
            var result = await _reviewsServices.deleteReviewsByIdAsync(reviewId);
            if (result.Success)
                return Ok(new { status = "success", message = "Reviews  deleted." });
            if (result.Success && result.Message == "Reviews Does not exist")
                return BadRequest(new { status = "error", message = "Reviews Does not exist" });
            return BadRequest(new { status = "error", message = "Invalid request payload" });
        }
    }
}
