﻿namespace PokemonReviewApp.Models
{
    /// <summary>
    /// Join table between [Pokemon] and [Category]
    /// </summary>
    public class PokemonCategory
    {
        public int PokemonId { get; set; }
        public Pokemon Pokemon { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
