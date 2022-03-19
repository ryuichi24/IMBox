namespace IMBox.Services.Rating.API.DTOs
{
    public record RatingDTO
    {
        public MovieDTO Movie { get; init; }
        public int Rating { get; init; }
    }
}