using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;
using todo_api.Data;
using todo_api.Models;
using todo_api.Models.Domain;

namespace todo_api.Controllers
{
    public class Employees : Controller
    {
        private readonly MVCDemoDbContext _mvcDemoDBContext;

        public Employees(MVCDemoDbContext mvcDemoDBContext)
        {
            this._mvcDemoDBContext = mvcDemoDBContext;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var employess = await _mvcDemoDBContext.Employees.ToListAsync();
            return View(employess);
        }

        [HttpGet]
        public IActionResult AddEmployee()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> View(UpdateEmployeeViewModel updateEmployeeReq)
        {
            var emp = await _mvcDemoDBContext.Employees.FindAsync(updateEmployeeReq.Id);
            if(emp != null)
            {
                emp.Name = updateEmployeeReq.Name;
                await _mvcDemoDBContext.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return await Task.Run(() => View("View",emp));
        }

        [HttpGet]
        public async Task<IActionResult> View(Guid id)
        {
            var result = await _mvcDemoDBContext.Employees.FirstOrDefaultAsync(x => x.Id == id);
            if (result != null)
            {
                var _viewModel = new UpdateEmployeeViewModel()
                {
                    Id = result.Id,
                    Name = result.Name
                };
                return await Task.Run(() => View("View", _viewModel));
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddEmployeeViewModel addEmployeeReq)
        {
            var employee = new Employee()
            {
                Id = Guid.NewGuid(),
                Name = addEmployeeReq.Name
            };
            await _mvcDemoDBContext.Employees.AddAsync(employee);
            await _mvcDemoDBContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(UpdateEmployeeViewModel delReq)
        {
            var emp = await _mvcDemoDBContext.Employees.FindAsync(delReq.Id);
            if(emp != null)
            {
                _mvcDemoDBContext.Employees.Remove(emp);
                await _mvcDemoDBContext.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
    }

  
}
