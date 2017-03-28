using Movie_Catalog.Data;
using Movie_Catalog.Models;
using Movie_Catalog.Models.Identity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Movie_Catalog.Controllers
{
    public class HomeController : Controller
    {
        [AllowAnonymous]
        public ActionResult Login()
        {
            var model = new LoginViewModel();

            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var userManager = HttpContext.GetOwinContext().GetUserManager<UserManager<AppUser>>();
            var authManager = HttpContext.GetOwinContext().Authentication;

            // attempt to load the user with this password
            AppUser user = userManager.Find(model.UserName, model.Password);

            // user will be null if the password or user name is bad
            if (user == null)
            {
                ModelState.AddModelError("", "Invalid username or password");

                return View(model);
            }
            else
            {
                // successful login, set up their cookies and send them on their way
                var identity = userManager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie);
                authManager.SignIn(new AuthenticationProperties { IsPersistent = model.RememberMe }, identity);

                if (!string.IsNullOrEmpty(returnUrl))
                    return Redirect(returnUrl);
                else
                    return RedirectToAction("Index");
            }
        }

        [Authorize]
        public ActionResult ChangePassword()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult Index()
        {
            var repository = new MovieRepository();
            var model = repository.GetAllMovies();

            return View(model);
        }
        
        [Authorize(Roles="admin")]
        public ActionResult AddMovie()
        {
            var repository = new MovieRepository();
            AddMovieViewModel model = new AddMovieViewModel();

            model.Genres = from g in repository.GetGenres()
                           select new SelectListItem { Text = g.GenreType, Value = g.GenreId.ToString() };

            model.Ratings = from r in repository.GetRatings()
                            select new SelectListItem { Text = r.RatingName, Value = r.RatingId.ToString() };

            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public ActionResult AddMovie(AddMovieViewModel model)
        {
            var repository = new MovieRepository();

            if (ModelState.IsValid)
            {
                Movie movie = new Movie();
                movie.Title = model.Title;
                movie.RatingId = model.SelectedRatingId;
                movie.GenreId = model.SelectedGenreId;

                repository.MovieInsert(movie);
                return RedirectToAction("EditMovie", new { id = movie.MovieId });
            }

            // validation failed, return them to the view
            model.Genres = from g in repository.GetGenres()
                           select new SelectListItem { Text = g.GenreType, Value = g.GenreId.ToString() };

            model.Ratings = from r in repository.GetRatings()
                            select new SelectListItem { Text = r.RatingName, Value = r.RatingId.ToString() };
            return View(model);
        }

        [Authorize(Roles = "admin")]
        public ActionResult EditMovie(int id)
        {
            var repository = new MovieRepository();
            var movie = repository.GetMovieById(id);

            // if movie doesn't exist go home
            if (movie == null)
            {
                return RedirectToAction("Index");
            }

            EditMovieViewModel model = new EditMovieViewModel();
            model.MovieId = movie.MovieId;
            model.Title = movie.Title;
            model.SelectedGenreId = movie.GenreId;
            model.SelectedRatingId = movie.RatingId;

            model.Genres = from g in repository.GetGenres()
                           select new SelectListItem { Text = g.GenreType, Value = g.GenreId.ToString() };

            model.Ratings = from r in repository.GetRatings()
                            select new SelectListItem { Text = r.RatingName, Value = r.RatingId.ToString() };

            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public ActionResult EditMovie(EditMovieViewModel model)
        {
            var repository = new MovieRepository();

            if (ModelState.IsValid)
            {
                Movie movie = new Movie();
                movie.MovieId = model.MovieId;
                movie.Title = model.Title;
                movie.RatingId = model.SelectedRatingId;
                movie.GenreId = model.SelectedGenreId;

                repository.MovieEdit(movie);
                return RedirectToAction("Index");
            }

            // validation failed, return them to the view
            model.Genres = from g in repository.GetGenres()
                           select new SelectListItem { Text = g.GenreType, Value = g.GenreId.ToString() };

            model.Ratings = from r in repository.GetRatings()
                            select new SelectListItem { Text = r.RatingName, Value = r.RatingId.ToString() };

            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public ActionResult DeleteMovie(int id)
        {
            var repository = new MovieRepository();
            repository.MovieDelete(id);

            return RedirectToAction("Index");
        }
    }
}