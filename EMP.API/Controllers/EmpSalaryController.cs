using EMP.API.Models;
using EMP.LIB.Data;
using EMP.LIB.Infrastructure;
using EMP.LIB.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EMP.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class EmpSalaryController : ControllerBase
    {
        private ApplicationDbContext _context;

        private IRepository<EmpSalarys> _empSalarys;
        public EmpSalaryController(IRepository<EmpSalarys> empSalarys, ApplicationDbContext context)
        {
            _empSalarys = empSalarys;

            _context = context;
        }

        // GET: api/<EmpSalaryController>
        [HttpGet]
        public IActionResult Get(int pagenum = 0, int pagesize = 10)
        {
            try
            {
                //// EXEC [SP_empsalary_getall_paging]   @PageNum=1,@PageSize=3;

                var empsalary_lst = _context.EmpSalarys.FromSqlRaw(String.Format(ConstVar.SP_empsalary_getall_paging, pagenum, pagesize)).ToList();

                return Ok(empsalary_lst);
            }
            catch { return BadRequest("Error"); }
        }

        // GET api/<EmpSalaryController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                if (id > 0)
                {
                    var emp = _empSalarys.GetById(id);

                    return Ok(emp);
                }
                return BadRequest("Invalid Id");
            }
            catch { return BadRequest("Error"); }
        }

        // POST api/<EmpSalaryController>
        [HttpPost]
        public IActionResult Post(EmpSalaryViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    DateTime ServerDateTime = DateTime.Now;

                    EmpSalarys emp_obj = new EmpSalarys();
                    emp_obj.EmpId = model.EmpId;
                    emp_obj.Year = model.Year;
                    emp_obj.Month = model.Month;
                    emp_obj.SalaryPaid = model.SalaryPaid;
                    emp_obj.CreatedDate = ServerDateTime;
                    emp_obj.UpdatedDate = ServerDateTime;

                    _empSalarys.Insert(emp_obj);
                    _empSalarys.Save();

                    return Ok("successfully created");
                }
                return BadRequest("Invalid Id");
            }
            catch { return BadRequest("Error"); }
        }

        // PUT api/<EmpSalaryController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, EmpSalaryViewModel model)
        {
            try
            {
                if (id > 0 && ModelState.IsValid)
                {
                    var emp_obj = _empSalarys.GetById(id);
                    if (emp_obj != null)
                    {
                        DateTime ServerDateTime = DateTime.Now;

                        emp_obj.EmpId = model.EmpId;
                        emp_obj.Year = model.Year;
                        emp_obj.Month = model.Month;
                        emp_obj.SalaryPaid = model.SalaryPaid;
                        emp_obj.UpdatedDate = ServerDateTime;
                        _empSalarys.Update(emp_obj);
                        _empSalarys.Save();

                        return Ok("successfully Updated");
                    }
                }
                return BadRequest("Invalid Id");
            }
            catch { return BadRequest("Error"); }
        }

        // DELETE api/<EmpSalaryController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                if (id > 0)
                {
                    _empSalarys.Delete(id);
                    _empSalarys.Save();
                    return Ok("successfully deleted");
                }
                return BadRequest("Invalid Id");
            }
            catch { return BadRequest("Error"); }
        }

    }
}
