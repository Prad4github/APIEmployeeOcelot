using EmployeeManagement.IConfig;
using EmployeeManagement.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace EmployeeManagement.Controllers
{
    [ApiController]
    [Route("/api/employee")]
    //[Authorize]
    public class EmployeeController : ControllerBase
    {

        private readonly IUnitOfWork _unitOfWork;

        public EmployeeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet("allEmployee")]
        public async Task<IActionResult> Get()
        {
            var users = await _unitOfWork.Employee.All();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmployee(Guid id)
        {
            var item = await _unitOfWork.Employee.GetById(id);

            if (item == null)
                return NotFound();

            return Ok(item);
        }

        [HttpPost]
        public async Task<IActionResult> CreateEmployee(Employee employee)
        {
            if (ModelState.IsValid)
            {
                employee.Id = Guid.NewGuid();

                await _unitOfWork.Employee.Upsert(employee);
                await _unitOfWork.CompleteAsync();

                return CreatedAtAction("GetEmployee", new { employee.Id }, employee);
            }

            return new JsonResult("Somethign Went wrong") { StatusCode = 500 };
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(Guid id)
        {
            var item = await _unitOfWork.Employee.GetById(id);

            if (item == null)
                return BadRequest();

            await _unitOfWork.Employee.Delete(id);
            await _unitOfWork.CompleteAsync();

            return Ok(item);
        }
    }
}
