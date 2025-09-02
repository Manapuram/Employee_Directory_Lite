using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace EmployeeApi {
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeesController : ControllerBase {
        private static List<Employee> Employees = new List<Employee> {
            new Employee { Id = 1, Name = "John Doe", Email = "john@example.com", Position = "Developer" },
            new Employee { Id = 2, Name = "Jane Smith", Email = "jane@example.com", Position = "Manager" }
        };

        [HttpGet]
        public ActionResult<IEnumerable<Employee>> Get() => Employees;

        [HttpPost]
        public ActionResult<Employee> Post([FromBody] Employee employee) {
            employee.Id = Employees.Count > 0 ? Employees.Max(e => e.Id) + 1 : 1;
            Employees.Add(employee);
            return CreatedAtAction(nameof(Get), new { id = employee.Id }, employee);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id) {
            var emp = Employees.FirstOrDefault(e => e.Id == id);
            if (emp == null) return NotFound();
            Employees.Remove(emp);
            return NoContent();
        }
    }
}
