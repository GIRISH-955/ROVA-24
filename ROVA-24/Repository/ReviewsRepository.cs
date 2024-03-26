using Microsoft.EntityFrameworkCore;
using ROVA_24.Data;
using ROVA_24.IRepository;
using ROVA_24.Models;

namespace ROVA_24.Repository
{
    public class ReviewsRepository:IReviewsRepository
    {
        private readonly Rova_23DBContext _dbContext;
        public ReviewsRepository(Rova_23DBContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<bool> checkExistingCustomerByIdAsync(int customerId)
        {
            var isExist = await _dbContext.Customers.AnyAsync(c => c.CustomerId==customerId);
            return isExist;
        }
        public async Task<bool> checkExistingProductIdAsync(int productId)
        {
            var isExist = await _dbContext.Products.AnyAsync(p => p.ProductId == productId);
            return isExist;
        }
        public async Task<Reviews> addReviewsAsync(Reviews reviews)
        {
            _dbContext.Add(reviews);
            await _dbContext.SaveChangesAsync();
            return reviews;
        }
    }
}
