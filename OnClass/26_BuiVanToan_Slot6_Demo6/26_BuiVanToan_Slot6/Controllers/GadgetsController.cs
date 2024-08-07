using _26_BuiVanToan_Slot6.Data.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace _26_BuiVanToan_Slot6.Controllers
{
    [Route("gadget")]
    [ApiController]
    public class GadgetsController : ODataController
    {
        private readonly MyWorldDbContext _myworldDbContext;
  
public GadgetsController(MyWorldDbContext myworldDbContext)
        {
            _myworldDbContext = myworldDbContext;
        }
        [EnableQuery]
        //[HttpGet("Get")]
public IActionResult Get()
        {
            return Ok(_myworldDbContext.Gadgets.AsQueryable());

        }
    }
}
