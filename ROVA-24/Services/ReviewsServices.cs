using ROVA_24.DTO.AddressDTO;
using ROVA_24.DTO.ReviewsDTO;
using ROVA_24.DTO.UpdateAddressDTO;
using ROVA_24.IRepository;
using ROVA_24.IServices;
using ROVA_24.Models;
using ROVA_24.Repository;
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
                if (existingCustomer != null)
                {
                    var review = new Reviews
                    {
                        CustomerId = request.CustomerId,
                        ProductId = request.ProductId,
                        Comment = request.Comment,
                        Rating = request.Rating,
                    };
                    var result = await _reviewsRepository.addReviewsAsync(review);
                    if (result != null)
                    {
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
                            Success = true,
                            Data = response,
                            Status = System.Net.HttpStatusCode.OK,
                            Message = "Reviews added successfully"
                        };
                    }
                }
                return new ServiceResponse<ReviewsResponseDTO>
                {
                    Success = false,
                    Status = System.Net.HttpStatusCode.BadRequest,
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
        public async Task<ServiceResponse<List<ReviewsResponseDTO>>> GetAllReviewsAsync()
        {
            try
            {
                var reviews = await _reviewsRepository.getAllReviewsAsync();

                var response = reviews.Select(r => new ReviewsResponseDTO
                {
                    reviewId = r.reviewId,
                    CustomerId = r.CustomerId,
                    ProductId = r.ProductId,
                    Comment = r.Comment,
                    Rating = r.Rating
                }).ToList();

                return new ServiceResponse<List<ReviewsResponseDTO>>
                {
                    Success = true,
                    Data = response,
                    Status = System.Net.HttpStatusCode.OK,
                    Message = "Reviews retrieved successfully"
                };
            }
            catch (Exception ex)
            {
                return new ServiceResponse<List<ReviewsResponseDTO>>()
                {
                    Status = System.Net.HttpStatusCode.BadGateway,
                    Message = ex.Message
                };
            }
        }
        public async Task<ServiceResponse<ReviewsResponseDTO>> getReviewsByIdAsync(int reviewId)
        {
            try
            {
                var response = await _reviewsRepository.getReviewsById(reviewId);
                if (response != null)
                {
                    var reviewsdto = new ReviewsResponseDTO
                    {
                        reviewId = response.reviewId,
                        CustomerId = response.CustomerId,
                        ProductId = response.ProductId,
                        Comment = response.Comment,
                        Rating = response.Rating
                    };
                    return new ServiceResponse<ReviewsResponseDTO>
                    {
                        Success = true,
                        Data = reviewsdto,
                        Status = System.Net.HttpStatusCode.OK,
                        Message = "Reviews fetched successfully"
                    };

                }
                return new ServiceResponse<ReviewsResponseDTO>
                {
                    Success = false,
                    Status = System.Net.HttpStatusCode.BadRequest,
                    Message = "Reviews not found"
                };

            }
            catch (Exception ex)
            {
                return new ServiceResponse<ReviewsResponseDTO>
                {
                    Message = ex.Message,
                };
            }
        }
        public async Task<ServiceResponse<ReviewsRequestDTO>> updateReviewsByIdAsync(int reviewId, ReviewsRequestDTO reviewsRequestDTO)
        {
            var existingReviews = await _reviewsRepository.getReviewsById(reviewId);
            if (existingReviews == null)
            {
                return new ServiceResponse<ReviewsRequestDTO>
                {
                    Status = System.Net.HttpStatusCode.BadRequest,
                    Message = "Reviews does not exist"
                };
            }

            var existingCustomer = await _reviewsRepository.checkExistingCustomerByIdAsync(reviewsRequestDTO.CustomerId);
            if (existingCustomer == null)
            {
                return new ServiceResponse<ReviewsRequestDTO>
                {
                    Status = System.Net.HttpStatusCode.BadRequest,
                    Message = "Customer does not exist"
                };
            }
            var existingProduct = await _reviewsRepository.checkExistingProductIdAsync(reviewsRequestDTO.ProductId);
            if (existingProduct == null)
            {
                return new ServiceResponse<ReviewsRequestDTO>
                {
                    Status = System.Net.HttpStatusCode.BadRequest,
                    Message = "Product Does not exist"
                };
            }
            existingReviews.Comment = reviewsRequestDTO.Comment;
            existingReviews.Rating = reviewsRequestDTO.Rating;

            var result = await _reviewsRepository.updateCustomerReviewsAsync(existingReviews);
            if (result.Success)
            {
                return new ServiceResponse<ReviewsRequestDTO>
                {
                    Success = true,
                    Data = reviewsRequestDTO,
                    Status = System.Net.HttpStatusCode.OK,
                    Message = " Updated successfully"
                };
            }
            return new ServiceResponse<ReviewsRequestDTO>
            {
                Success = true,
                Status = System.Net.HttpStatusCode.BadRequest,
                Message = "Error occured while updating"
            };
        }
        public async Task<ServiceResponse<string>> deleteReviewsByIdAsync(int reviewId)
        {
            var existingReviews = await _reviewsRepository.getReviewsById(reviewId);
            if (existingReviews != null)
            {
                var deletionResult = await _reviewsRepository.deleteReviewsByIdAsync(reviewId);
                if (deletionResult)
                {
                    return new ServiceResponse<string>
                    {
                        Success = true,
                        Status = System.Net.HttpStatusCode.OK,
                        Message = "Reviews  deleted."
                    };
                }
            }
            return new ServiceResponse<string>
            {
                Success = false,
                Status = System.Net.HttpStatusCode.BadRequest,
                Message = "Reviews Does not exist"
            };
        }
    }

}
