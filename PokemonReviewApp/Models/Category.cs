namespace PokemonReviewApp.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }

        /**
         * For many-to-many relationships: EF Core 5.0+ can automatically configure these when you have 
         * navigation properties on both sides.
         */
        public ICollection<PokemonCategory> PokemonCategories { get; set; }
    }
}
