namespace ROVA_24.DTO.ReviewsDTO
{
    public class ReviewsRequestDTO
    {
        public int CustomerId { get; set; }
        public int ProductId { get; set; }
        public string Comment { get; set; }
        public string Rating { get; set; }
    }

    public class ReviewsResponseDTO
    {
        public int reviewId { get; set; }
        public int CustomerId { get; set; }
        public int ProductId { get; set; }
        public string Comment { get; set; }
        public string Rating { get; set; }
    }
}
