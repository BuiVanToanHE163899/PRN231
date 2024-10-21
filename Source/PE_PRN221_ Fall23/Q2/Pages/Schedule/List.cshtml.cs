using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Q2.Models;

namespace Q2.Pages.Schedule
{
    public class ListModel : PageModel
    {
        private readonly PE_PRN_Fall2023B1Context _context;
        public ListModel(PE_PRN_Fall2023B1Context context)
        {
            _context = context;
        }
        public void OnGet(string date)
        {
            var datenow = new DateTime();
            if (date == null)
            {
                datenow = new DateTime(2023, 10, 24);
                ViewData["datenow"] = datenow;
            }
            else
            {
                datenow = DateTime.Parse(date);
                ViewData["datenow"] = datenow;
            }

            var rooms = _context.Rooms.ToList();
            ViewData["rooms"] = rooms;

            var timeSlots = _context.TimeSlots.ToList();
            ViewData["timeslots"] = timeSlots;

            var schedules = _context.Schedules
                .Include(s => s.Room)
                .Include(s => s.TimeSlot)
                .Include(s => s.Movie)
                .Where(s => datenow >= s.StartDate && datenow <= s.EndDate)
                .OrderBy(s => s.RoomId)
                .ToList();
            ViewData["schedules"] = schedules;
        }
    }
}
