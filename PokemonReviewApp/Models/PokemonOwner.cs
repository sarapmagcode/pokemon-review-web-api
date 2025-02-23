namespace PokemonReviewApp.Models
{
    /// <summary>
    /// Join table between [Pokemon] and [Owner]
    /// </summary>
    public class PokemonOwner
    {
        public int PokemonId { get; set; }
        public Pokemon Pokemon { get; set; }

        public int OwnerId { get; set; }
        public Owner Owner { get; set; }
    }
}
