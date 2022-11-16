using HRIS.Api.Models;
using HRIS.Api.Models.RequestModels;
using HRIS.Api.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace HRIS.Api.Controllers
{
    public class EmployeeController : ApiController
    {
        [HttpPost]
        [Route("GetEmployees")]
        public IHttpActionResult GetEmployees(FilterSetting filterSetting)
        {
            return Ok(new List<Employee>());    
        }

        [HttpGet]
        [Route("GetEmployee")]
        public IHttpActionResult GetEmployee(Guid internalID)
        {
            try
            {
                return Ok(EmployeeService.GetEmployee(internalID));
            }
            catch (Exception ex)
            {
                //TODO: Error Log must be here  
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("SaveEmployee")]
        public IHttpActionResult SaveEmployee(EmployeeRequest employeeRequest)
        {
            try
            {
                var result = EmployeeService.SaveEmployee(employeeRequest);
                //TODO: Successfully Saved must be logged here
                return Ok(result);
            }
            catch (Exception ex)
            {
                //TODO: Error Log must be here
                return BadRequest(ex.Message);
            }
        }

    }
}