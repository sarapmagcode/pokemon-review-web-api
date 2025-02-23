namespace PokemonReviewApp.Models
{
    public class Pokemon
    {
        // Actual Columns/Properties on the database
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Birthdate { get; set; }

        // Navigation Properties - represent relationships between entities
        public ICollection<Review> Reviews { get; set; }
        public ICollection<PokemonOwner> PokemonOwners { get; set; }
        public ICollection<PokemonCategory> PokemonCategories { get; set; }
    }
}
