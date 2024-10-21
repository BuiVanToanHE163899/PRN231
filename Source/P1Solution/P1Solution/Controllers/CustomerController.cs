using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using P1Solution.Models;

namespace P1Solution.Controllers
{
    [Route("api/Customer")]
    [ApiController]
    public class CustomerController : Controller
    {
        private readonly PRN231_P1Context _context = new PRN231_P1Context();
        [HttpPost("Delete/{CustomerId}")]
        public IActionResult Delete(string CustomerId)
        {
            var c=_context.Customers.Include(x=>x.Orders)
                .SingleOrDefault(x=>x.CustomerId == CustomerId);
            if (c == null)
            {
                return NotFound();
            }else {
                try
                {
                    returnDel res = new returnDel();
                    foreach (var x in c.Orders)
                    {
                        var details= _context.OrderDetails.Where(m=>m.OrderId==x.OrderId).ToList();
                        _context.OrderDetails.RemoveRange(details);
                        res.orderDetailDeletedCount += _context.SaveChanges();
                    }
                    _context.Orders.RemoveRange(c.Orders);
                    res.orderDeletedCount += _context.SaveChanges();
                    _context.Customers.Remove(c);
                    res.customerDeletedCount += _context.SaveChanges();
                    return Ok(res);
                }
                catch (Exception ex)
                {
                    return Conflict("There was an unknown error when performing data deletion.");
                }
            }
        }
    }
    public class returnDel
    {
        public int customerDeletedCount { get; set; }
        public int orderDeletedCount { get; set; }
        public int orderDetailDeletedCount { get; set; }
    }
}
