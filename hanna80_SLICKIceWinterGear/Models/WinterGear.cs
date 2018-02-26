using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace hanna80_SLICKIceWinterGear.Models
{
    public class WinterGear
    {
		public int Id { get; set; }

		[Required(ErrorMessage = "A name is required for this item.")]
		public string Name { get; set; }

		public int Weight { get; set; }

		[Required(ErrorMessage = "A gear type is required for this item.")]
		public string GearType { get; set; }

		public int Condition { get; set; }
    }
}
