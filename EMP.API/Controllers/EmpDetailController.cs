using EMP.API.Models;
using EMP.LIB.Models;
using EMP.LIB.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using EMP.LIB.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EMP.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class EmpDetailController : ControllerBase
    {
        private ApplicationDbContext _context;

        private IRepository<EmpDetails> _empDetails;
        public EmpDetailController(IRepository<EmpDetails> empDetails, ApplicationDbContext context)
        {
            _empDetails = empDetails;

            _context = context;
        }

        // GET: api/<EmpDetailController>
        [HttpGet]
        public IActionResult Get(int pagenum = 0, int pagesize = 10)
        {
            try
            {
                //// EXEC [empdetails_getall_paging]   @PageNum=1,@PageSize=3;

                var empdetails_lst = _context.EmpDetails.FromSqlRaw(String.Format(ConstVar.SP_empdetails_getall_paging, pagenum, pagesize)).ToList();

                return Ok(empdetails_lst);

            }
            catch { return BadRequest("Error"); }
        }

        // GET api/<EmpDetailController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                if (id > 0)
                {
                    var emp = _empDetails.GetById(id);

                    return Ok(emp);
                }
                return BadRequest("Invalid Id");
            }
            catch { return BadRequest("Error"); }
        }

        // POST api/<EmpDetailController>
        [HttpPost]
        public IActionResult Post(EmpDetailViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    DateTime ServerDateTime = DateTime.Now;

                    EmpDetails emp_obj = new EmpDetails();
                    emp_obj.Name = model.Name;
                    emp_obj.Email = model.Email;
                    emp_obj.Designation = model.Designation;
                    emp_obj.CreatedDate = ServerDateTime;
                    emp_obj.UpdatedDate = ServerDateTime;

                    _empDetails.Insert(emp_obj);
                    _empDetails.Save();

                    return Ok("successfully created");
                }
                return BadRequest("Invalid Id");
            }
            catch { return BadRequest("Error"); }
        }

        // PUT api/<EmpDetailController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, EmpDetailViewModel model)
        {
            try
            {
                if (id > 0 && ModelState.IsValid)
                {
                    var emp_obj = _empDetails.GetById(id);
                    if (emp_obj != null)
                    {
                        DateTime ServerDateTime = DateTime.Now;

                        emp_obj.Name = model.Name;
                        emp_obj.Email = model.Email;
                        emp_obj.Designation = model.Designation;
                        emp_obj.UpdatedDate = ServerDateTime;
                        _empDetails.Update(emp_obj);
                        _empDetails.Save();

                        return Ok("successfully Updated");
                    }
                }
                return BadRequest("Invalid Id");
            }
            catch { return BadRequest("Error"); }
        }

        // DELETE api/<EmpDetailController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                if (id > 0)
                {
                    _empDetails.Delete(id);
                    _empDetails.Save();
                    return Ok("successfully deleted");
                }
                return BadRequest("Invalid Id");
            }
            catch { return BadRequest("Error"); }
        }
    }
}
