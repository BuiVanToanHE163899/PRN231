using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Q2.Models;

namespace Q2.Pages.Movie
{
    public class IndexModel : PageModel
    {
        private readonly PE_PRN_Fall2023B1Context _context;
        public IndexModel(PE_PRN_Fall2023B1Context context)
        {
            _context = context;
        }
        public void OnGet(int? id)
        {
            string errormess = string.Empty;
            if (id == null)
            {
                errormess = "không tồn tại id của movie";
            }
            else
            {
                var movie = _context.Movies.Include(m => m.Director).FirstOrDefault(m => m.Id == id);
                if (movie != null)
                {
                    ViewData["movie"] = movie;
                }
                else
                {
                    errormess = $"không tồn tại movie có id = {id}";
                }
                var stars = _context.MovieStars.Include(m => m.Movie).Include(m => m.Star)
                            .Where(m => m.MovieId == id).ToList();
                ViewData["stars"] = stars;
            }
            ViewData["error"] = errormess;
        }
    }
}
