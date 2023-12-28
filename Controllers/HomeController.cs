using Microsoft.AspNetCore.Mvc;
using MovieStoreMvc.Repositories.Abstract;

namespace MovieStoreMvc.Controllers
{
    public class HomeController : Controller
    {

        private readonly IMovieService _movieService;

        // Constructor injection for movie service


        public HomeController(IMovieService movieService)
        {
            _movieService = movieService;
        }


        // Action to display movies based on search term and current page
        public IActionResult Index(string term = "", int currentPage = 1)



        {
            // Retrieve movies based on the search term and page
            var movies = _movieService.List(term, true, currentPage);

            // Return the view with the list of movies
            return View(movies);
        }


        // Action to display details of a specific movie based on its ID
        public IActionResult MovieDetail(int movieId)
        {



            // Retrieve the movie details using its ID
            var movie = _movieService.GetById(movieId);

            // Return the view with the movie details
            return View(movie);
        }


        // Action to display information about the application
        public IActionResult About()
        {
            return View();
        }

        // Additional action methods or code can be added here if needed
    }
}
