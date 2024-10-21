using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using P1Solution.Models;

namespace P1Solution.Controllers
{
    [Route("api/Order")]
    [ApiController]
    public class OrderController:ControllerBase
    {
        private readonly PRN231_P1Context _context = new PRN231_P1Context();
        private readonly IMapper _mapper;
        public OrderController(PRN231_P1Context  context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        //[HttpGet ("GetAllOrder")]
        //public IActionResult GetAllOrder()
        //{
        //    var o=_context.Orders.Include(x=>x.Customer).Include(x=>x.Employee)
        //        .ThenInclude(x=>x.Department).ToList();
        //    if (o.Count > 0)
        //    {
        //        var res = o.Select(x => new OrderDTO
        //        {
        //            orderId = x.OrderId,
        //            customerId= x.CustomerId,   
        //            customerName=x.Customer.CompanyName,
        //            employeeId=x.Employee.EmployeeId,
        //            employeeName= x.Employee.FirstName +" "+ x.Employee.LastName,
        //            employeeDepartmentId=x.Employee.Department.DepartmentId,
        //            employeeDepartmentName=x.Employee.Department.DepartmentName,
        //            orderDate=x.OrderDate,
        //            requiredDate=x.RequiredDate,
        //            shippedDate=x.ShippedDate,
        //            freight=x.Freight,
        //            shipName=x.ShipName,
        //            shipAddress=x.ShipAddress,
        //            shipCity=x.ShipCity,
        //            shipRegion=x.ShipRegion,
        //            shipPostalCode=x.ShipPostalCode,
        //            shipCountry=x.ShipCountry,
        //        }).ToList();
        //        return Ok(res);
        //    }
        //    else
        //    {
        //        return NotFound();
        //    }
        //}
        [HttpGet("GetAllOrder")]
        public  IActionResult GetAllOrder()
        {
            //var o = _context.Orders.Include(x => x.Customer).Include(x => x.Employee)
            //    .ThenInclude(x => x.Department).ToList();
            //if (o.Count > 0)
            //{
            //  return (Ok(_mapper.Map<List<OrderDTO>>(o)));
            //}
            //else
            //{
            //    return NotFound();
            //}
          List<Order> Orders =  _context.Orders
         .Include(o => o.Customer)
         .Include(o => o.Employee)
         .ThenInclude(o => o.Department).ToList();
            return Ok(_mapper.Map<List<OrderDTO>>(Orders));
        }
        //[HttpGet("getorderbydate/{From}/{To}")]
        //public IActionResult getorderbydate(DateTime From, DateTime To)
        //{
        //    var o = _context.Orders.Include(x => x.Customer).Include(x => x.Employee)
        //        .ThenInclude(x => x.Department)
        //        .Where(x=>x.OrderDate >= From && x.OrderDate<=To).ToList();
        //    if (o.Count > 0)
        //    {
        //        var res = o.Select(x => new OrderDTO
        //        {
        //            orderId = x.OrderId,
        //            customerId = x.CustomerId,
        //            customerName = x.Customer.CompanyName,
        //            employeeId = x.Employee.EmployeeId,
        //            employeeName = x.Employee.FirstName + " " + x.Employee.LastName,
        //            employeeDepartmentId = x.Employee.Department.DepartmentId,
        //            employeeDepartmentName = x.Employee.Department.DepartmentName,
        //            orderDate = x.OrderDate,
        //            requiredDate = x.RequiredDate,
        //            shippedDate = x.ShippedDate,
        //            freight = x.Freight,
        //            shipName = x.ShipName,
        //            shipAddress = x.ShipAddress,
        //            shipCity = x.ShipCity,
        //            shipRegion = x.ShipRegion,
        //            shipPostalCode = x.ShipPostalCode,
        //            shipCountry = x.ShipCountry,
        //        }).ToList();
        //        return Ok(res);
        //    }
        //    else
        //    {
        //        return NotFound();
        //    }
        //}
        [HttpGet("getorderbydate/{From}/{To}")]
        public IActionResult getorderbydate(DateTime From, DateTime To)
        {
            List<Order> o = _context.Orders.Include(x => x.Customer).Include(x => x.Employee)
                .ThenInclude(x => x.Department)
                .Where(x => x.OrderDate >= From && x.OrderDate <= To).ToList();
            if (o.Count > 0)
            {
              
                return Ok(_mapper.Map<List<OrderDTO>>(o));
            }
            else
            {
                return NotFound();
            }
        }
    }
}
