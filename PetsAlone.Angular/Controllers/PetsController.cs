using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PetsAlone.Core;
using System;
using System.Linq;

namespace PetsAlone.Angular.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PetsController : ControllerBase
    {
        private readonly PetsDbContext _petsDbContext;
        public PetsController(PetsDbContext petsDbContext)
        {
            _petsDbContext = petsDbContext;
        }

        [HttpGet("all")]
        public object GetAll([FromQuery] int? petType)
        {
            var petsService = new PetsService();
            var result = petsService.GetAll(_petsDbContext);

            result = result.Where(p => p.PetType.HasValue && (int)p.PetType == petType.Value).ToList();

            result = result.OrderByDescending(p => p.MissingSince).ToList();

            var petTypes = Enum.GetValues(typeof(PetType))
                               .Cast<PetType>()
                               .Select(e => new
                               {
                                   value = (int)e,
                                   text = e.ToString()
                               })
                               .ToList();

            return new
            {
                pets = result,
                petTypes = petTypes,
                selectedPetType = petType
            };
        }

        [HttpPost("create")]
        [Authorize]
        public IActionResult Create(My_Pet_Class pet)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _petsDbContext.Pets.Add(pet);
            _petsDbContext.SaveChanges();

            return Ok(new { success = true });
        }

        public enum PetType
        {
            Cat = 1,
            Dog = 2,
            Hamster = 3,
            Bird = 4,
            Rabbit = 5,
            Fish = 6,
            Lizard = 7,
            Horse = 8,
            Gerbil = 9,
            Tortoise = 10,
            Ferret = 11
        }
    }
}
