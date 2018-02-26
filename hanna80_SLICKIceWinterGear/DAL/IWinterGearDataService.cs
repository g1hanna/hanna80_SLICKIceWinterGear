using hanna80_SLICKIceWinterGear.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hanna80_SLICKIceWinterGear.DAL
{
    public interface IWinterGearDataService
    {
		List<WinterGear> Read();
		void Write(List<WinterGear> gearItems);
    }
}
