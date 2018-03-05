using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hanna80_SLICKIceWinterGear.Models
{
    public class SearchViewModel
    {
		public int? PageNumber { get; set; }

		public int? PageSize { get; set; }

		public string SortOrder { get; set; }

		public bool FlipOrder { get; set; }

		public string SearchCriteria { get; set; }

		public string GearTypeFilter { get; set; }
	}
}
