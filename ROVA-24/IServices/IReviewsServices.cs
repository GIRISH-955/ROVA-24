using ROVA_24.DTO.ReviewsDTO;
using ROVA_24.ServiceResponse;

namespace ROVA_24.IServices
{
    public interface IReviewsServices
    {
        Task<ServiceResponse<ReviewsResponseDTO>> AddReviewsAsync(ReviewsRequestDTO request);
    }
}
