using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AmistServer.Object;
using CustomerCors.Interface;

namespace AmistServer.API
{
    public class EmployeeController: MISAEntityController<Employee>
    {
        IEmployeeService _employeeService;
        IEmployeeRepo _employeeRepo;
        public EmployeeController(IEmployeeService employeeService,
             IEmployeeRepo employeeExcute):base(employeeService, employeeExcute)
        {
            _employeeService = employeeService;
            _employeeRepo = employeeExcute;
        }
        [HttpGet("Pagging")]
        public IActionResult Get(int index,int numRecord)
        {
            if(numRecord == 10)
            {
                var result = _employeeRepo.GetPageTen(index);
                if (result.isValid == false)
                {
                    return BadRequest(result);
                }
                return Ok(result);
            }
            else 
            {
                var result = _employeeRepo.GetPageTwenty(index);
                if (result.isValid == false)
                {
                    return BadRequest(result);
                }
                return Ok(result);
            }
            
        }
        [HttpGet("NewCode")]
        public IActionResult Get()
        {
            var result = _employeeRepo.GetNewEmployeeCode();
            if(result.isValid == false)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
        [HttpGet("Find")]
        public IActionResult Get(string keyWord)
        {
            var result = _employeeRepo.GetEmployeeByKey(keyWord);
            if (result.isValid == false)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
    }
}
