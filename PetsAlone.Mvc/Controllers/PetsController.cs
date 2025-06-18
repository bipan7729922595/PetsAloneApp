using System;
using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PetsAlone.Core;
using PetsAlone.Mvc.Constant;
using PetsAlone.Mvc.Models;

namespace PetsAlone.Mvc.Controllers
{
    public class PetsController : Controller
    {
        private readonly PetsDbContext _petsDbContext;
        public PetsController(PetsDbContext petsDbContext)
        {
            _petsDbContext = petsDbContext;
        }

        public IActionResult Index(int? petType)
        {
            var service = new PetsService();
            var result = service.GetAll(_petsDbContext);

            if (petType.HasValue)
            {
                result = result.Where(p => (int)p.PetType == petType.Value).ToList();
            }

            result = result.OrderByDescending(p => p.MissingSince).ToList();

            ViewBag.PetTypes = Enum.GetValues(typeof(PetType))
                                   .Cast<PetType>()
                                   .Select(e => new SelectListItem
                                   {
                                       Value = ((int)e).ToString(),
                                       Text = e.ToString()
                                   })
                                   .ToList();

            ViewBag.SelectedPetType = petType?.ToString();

            return View(result);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [Authorize]
        public IActionResult Create()
        {
            ViewBag.PetTypes = Enum.GetValues(typeof(PetType))
                .Cast<PetType>()
                .Select(e => new SelectListItem { Value = ((int)e).ToString(), Text = e.ToString() })
                .ToList();

            return View();
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public IActionResult Create(My_Pet_Class pet)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.PetTypes = Enum.GetValues(typeof(PetType))
                    .Cast<PetType>()
                    .Select(e => new SelectListItem { Value = ((int)e).ToString(), Text = e.ToString() })
                    .ToList();

                return View(pet);
            }

            _petsDbContext.Pets.Add(pet);
            _petsDbContext.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}