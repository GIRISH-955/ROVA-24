using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ROVA_24.DTO.ReviewsDTO;
using ROVA_24.IServices;

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
    }
}
