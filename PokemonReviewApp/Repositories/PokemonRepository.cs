using PokemonReviewApp.Data;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Repositories
{
    public class PokemonRepository : IPokemonRepository
    {
        private readonly DataContext _context;

        public PokemonRepository(DataContext context)
        {
            _context = context;
        }

        public bool CreatePokemon(int ownerId, int categoryId, Pokemon pokemon)
        {
            var pokemonOwnerEntity = _context.Owners.Where(o => o.Id == ownerId).FirstOrDefault();
            var category = _context.Categories.Where(c => c.Id == categoryId).FirstOrDefault();

            // PokemonOwners (Join Table)
            var pokemonOwner = new PokemonOwner
            {
                Owner = pokemonOwnerEntity,
                Pokemon = pokemon
            };
            _context.PokemonOwners.Add(pokemonOwner);

            // PokemonCategories (Join Table)
            var pokemonCategory = new PokemonCategory
            {
                Category = category,
                Pokemon = pokemon
            };
            _context.PokemonCategories.Add(pokemonCategory);

            // Pokemon (Table)
            _context.Pokemons.Add(pokemon);

            return Save();
        }

        public bool DeletePokemon(Pokemon pokemon)
        {
            _context.Pokemons.Remove(pokemon);
            return Save();
        }

        public Pokemon GetPokemon(int id)
        {
            return _context.Pokemons.Where(p => p.Id == id).FirstOrDefault();
        }

        public Pokemon GetPokemon(string name)
        {
            return _context.Pokemons.Where(p => p.Name == name).FirstOrDefault();
        }

        public decimal GetPokemonRating(int pokeId)
        {
            var reviews = _context.Reviews.Where(r => r.Pokemon.Id == pokeId);
            
            if (reviews.Count() <= 0)
            {
                return 0;
            }

            return (decimal)reviews.Sum(r => r.Rating) / reviews.Count();
        }

        public ICollection<Pokemon> GetPokemons()
        {
            return _context.Pokemons.OrderBy(p => p.Id).ToList();
        }

        public bool PokemonExists(int pokeId)
        {
            return _context.Pokemons.Any(p => p.Id == pokeId);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0;
        }

        public bool UpdatePokemon(int ownerId, int categoryId, Pokemon pokemon)
        {
            // TODO: Handle owner and category

            _context.Pokemons.Update(pokemon);
            return Save();
        }
    }
}
