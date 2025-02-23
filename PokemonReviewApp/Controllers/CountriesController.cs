﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PokemonReviewApp.Dtos;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Models;
using PokemonReviewApp.Repositories;

namespace PokemonReviewApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountriesController : ControllerBase
    {
        private readonly ICountryRepository _countryRepository;
        private readonly IMapper _mapper;

        public CountriesController(ICountryRepository countryRepository, IMapper mapper)
        {
            _countryRepository = countryRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<CountryDto>))]
        public IActionResult GetCountries()
        {
            var categories = _mapper.Map<List<CountryDto>>(_countryRepository.GetCountries());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(categories);
        }

        [HttpGet("{countryId}")]
        [ProducesResponseType(200, Type = typeof(CountryDto))]
        [ProducesResponseType(400)]
        public IActionResult GetCountry(int countryId)
        {
            if (!_countryRepository.CountryExists(countryId))
            {
                return NotFound();
            }

            var country = _mapper.Map<CountryDto>(_countryRepository.GetCountry(countryId));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(country);
        }

        [HttpGet("/owners/{ownerId}")]
        [ProducesResponseType(200, Type = typeof(CountryDto))]
        public IActionResult GetCountryByOwner(int ownerId)
        {
            var country = _mapper.Map<CountryDto>(
                _countryRepository.GetCountryByOwner(ownerId));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(country);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateCountry([FromBody] CountryDto countryCreate)
        {
            if (countryCreate == null)
            {
                return BadRequest(ModelState);
            }

            var country = _countryRepository.GetCountries()
                .Where(c => c.Name.Trim().ToUpperInvariant() == countryCreate.Name.Trim().ToUpperInvariant())
                .FirstOrDefault();

            if (country != null)
            {
                ModelState.AddModelError("", "Country already exists.");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var countryMap = _mapper.Map<Country>(countryCreate);
            if (!_countryRepository.CreateCountry(countryMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving.");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created!");
        }

        [HttpPut("{countryId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateCountry(int countryId, [FromBody] CountryDto updatedCountry)
        {
            if (updatedCountry == null)
            {
                return BadRequest(ModelState);
            }

            if (countryId != updatedCountry.Id)
            {
                return BadRequest(ModelState);
            }

            if (!_countryRepository.CountryExists(countryId))
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var countryMap = _mapper.Map<Country>(updatedCountry);

            if (!_countryRepository.UpdateCountry(countryMap))
            {
                ModelState.AddModelError("", "Something went wrong updating country");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{countryId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult DeleteCountry(int countryId)
        {
            if (!_countryRepository.CountryExists(countryId))
            {
                return NotFound();
            }

            var countryToDelete = _countryRepository.GetCountry(countryId);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!_countryRepository.DeleteCountry(countryToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting country");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
    }
}
