using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace hanna80_SLICKIceWinterGear.Models
{
	[XmlRoot("WinterGearItems")]
    public class WinterGearItems
    {
		[XmlElement("WinterGear")]
		public List<WinterGear> Items;
    }
}
