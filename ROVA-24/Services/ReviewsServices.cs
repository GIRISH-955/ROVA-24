using ROVA_24.DTO.ReviewsDTO;
using ROVA_24.DTO.UpdateAddressDTO;
using ROVA_24.IRepository;
using ROVA_24.IServices;
using ROVA_24.Models;
using ROVA_24.ServiceResponse;

namespace ROVA_24.Services
{
    public class ReviewsServices : IReviewsServices
    {
        private readonly IReviewsRepository _reviewsRepository;
        public ReviewsServices(IReviewsRepository reviewsRepository)
        {
            _reviewsRepository = reviewsRepository;
        }
        public async Task<ServiceResponse<ReviewsResponseDTO>> AddReviewsAsync(ReviewsRequestDTO request)
        {
            try
            {
                var existingCustomer = await _reviewsRepository.checkExistingCustomerByIdAsync(request.CustomerId);
                if (existingCustomer == null)
                {
                    return new ServiceResponse<ReviewsResponseDTO>
                    {
                        Status = System.Net.HttpStatusCode.BadRequest,
                        Message = "Customer does not exist"
                    };
                }
                var existingProduct = await _reviewsRepository.checkExistingProductIdAsync(request.CustomerId);
                if (existingProduct == null)
                {
                    return new ServiceResponse<ReviewsResponseDTO>
                    {
                        Status = System.Net.HttpStatusCode.BadRequest,
                        Message = "Product does not exist"
                    };
                }
                if (existingProduct && existingCustomer != null)
                {
                    var review = new Reviews
                    {
                        CustomerId = request.CustomerId,
                        ProductId = request.ProductId,
                        Comment = request.Comment,
                        Rating = request.Rating,
                    };
                    var result = await _reviewsRepository.addReviewsAsync(review);
                    var response = new ReviewsResponseDTO
                    {
                        reviewId = result.reviewId,
                        CustomerId = result.CustomerId,
                        ProductId = result.ProductId,
                        Comment = result.Comment,
                        Rating = result.Rating,
                    };
                    return new ServiceResponse<ReviewsResponseDTO>
                    {
                        Data = response,
                        Message = "Reviews added successfully"
                    };
                }
                return new ServiceResponse<ReviewsResponseDTO>
                {
                    Message = "Error occured while adding reviews"
                };
            }
            catch (Exception ex) 
            {
                return new ServiceResponse<ReviewsResponseDTO>()
                {
                    Status = System.Net.HttpStatusCode.BadGateway,
                    Message = ex.Message
                };
            }
        }
    }
}
