using ROVA_24.Models;
using ROVA_24.ServiceResponse;

namespace ROVA_24.IRepository
{
    public interface IReviewsRepository
    {
        Task<bool> checkExistingCustomerByIdAsync(int customerId);
        Task<bool> checkExistingProductIdAsync(int productId);
        Task<Reviews> addReviewsAsync(Reviews reviews);
        Task<List<Reviews>> getAllReviewsAsync();
        Task<Reviews> getReviewsById(int reviewsId);
        Task<ServiceResponse<Reviews>> updateCustomerReviewsAsync(Reviews reviews);
        Task<bool> deleteReviewsByIdAsync(int reciewId);
    }
}
