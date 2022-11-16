using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HRIS.Api.Models
{
    public class FilterSetting
    {
        public string FilterBy { get; set; }    
        public string FilterValue { get; set; }    
        public DateTime DateFrom { get; set; }    
        public DateTime DateTo { get; set; }    
    }
}