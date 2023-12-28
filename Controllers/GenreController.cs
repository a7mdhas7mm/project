using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieStoreMvc.Models.Domain;
using MovieStoreMvc.Repositories.Abstract;
using System.Linq;

namespace MovieStoreMvc.Controllers
{
    [Authorize]
    public class GenreController : Controller
    {
        private readonly IGenreService _genreService;

        public GenreController(IGenreService genreService)
        {
            _genreService = genreService;
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(Genre model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var result = _genreService.Add(model);

            return HandleActionResult(result, "Added Successfully", model, nameof(Add));
        }

        public IActionResult Edit(int id)
        {
            var data = _genreService.GetById(id);
            return View(data);
        }

        [HttpPost]
        public IActionResult Update(Genre model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var result = _genreService.Update(model);

            return HandleActionResult(result, "Updated Successfully", model, nameof(GenreList));
        }

        public IActionResult GenreList()
        {
            var data = _genreService.List().ToList();
            return View(data);
        }

        public IActionResult Delete(int id)
        {
            var result = _genreService.Delete(id);

            if (result)
            {
                TempData["msg"] = "Deleted Successfully";
            }
            else
            {
                TempData["msg"] = "Error on server side";
            }

            return RedirectToAction(nameof(GenreList));
        }

        private IActionResult HandleActionResult(bool result, string successMessage, Genre model, string actionName)
        {
            if (result)
            {
                TempData["msg"] = successMessage;
                return RedirectToAction(actionName);
            }
            else
            {
                TempData["msg"] = "Error on server side";
                return View(model);
            }
        }
    }
}
