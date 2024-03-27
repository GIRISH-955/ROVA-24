using ROVA_24.DTO.AddressDTO;
using ROVA_24.DTO.ReviewsDTO;
using ROVA_24.ServiceResponse;

namespace ROVA_24.IServices
{
    public interface IReviewsServices
    {
        Task<ServiceResponse<ReviewsResponseDTO>> AddReviewsAsync(ReviewsRequestDTO request);
        Task<ServiceResponse<List<ReviewsResponseDTO>>> GetAllReviewsAsync();
        Task<ServiceResponse<ReviewsResponseDTO>> getReviewsByIdAsync(int reviewId);
        Task<ServiceResponse<string>> deleteReviewsByIdAsync(int addressId);
    }
}
