using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hanna80_SLICKIceWinterGear.Models
{
    public class SortViewModel
    {
		public int? PageNumber { get; set; }

		public int? PageSize { get; set; }

		public string SortOrder { get; set; }		

		public bool FlipOrder { get; set; }
    }
}
