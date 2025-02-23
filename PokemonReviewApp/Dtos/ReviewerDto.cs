using PokemonReviewApp.Models;

namespace PokemonReviewApp.Dtos
{
    public class ReviewerDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        // Use 'ReviewDto' here to avoid object loops
        //public ICollection<ReviewDto> Reviews { get; set; }
    }
}
