using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MarketAnalyst.Core.Data.General
{
    public class Test
    {
        [Key]
        public Guid IdLicense { get; set; }

        public int TotalLicence { get; set; }

        public int CurrentLicence { get; set; }
        
    }
}
