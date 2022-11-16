using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HRIS.Api.Models.RequestModels
{
    public class EmployeeRequest
    {   
        public Request inputRequest { get; set; }
        public Employee inputEmployee { get; set; } 
    }
}