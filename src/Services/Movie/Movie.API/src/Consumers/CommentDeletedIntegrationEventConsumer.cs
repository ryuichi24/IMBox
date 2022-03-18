using System.Threading.Tasks;
using IMBox.Services.IntegrationEvents;
using IMBox.Services.Movie.Domain.Repositories;
using MassTransit;

namespace IMBox.Services.Movie.API.Consumers
{
    public class CommentDeletedIntegrationEventConsumer : IConsumer<CommentDeletedIntegrationEvent>
    {
        private readonly IMovieRepository _movieRepository;

        public CommentDeletedIntegrationEventConsumer(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }

        public async Task Consume(ConsumeContext<CommentDeletedIntegrationEvent> context)
        {
            var message = context.Message;

            var existingMovie = await _movieRepository.GetByIdAsync(message.MovieId);

            if (existingMovie == null) return;

            existingMovie.DecrementCommentCount();

            await _movieRepository.UpdateAsync(existingMovie);
        }
    }
}