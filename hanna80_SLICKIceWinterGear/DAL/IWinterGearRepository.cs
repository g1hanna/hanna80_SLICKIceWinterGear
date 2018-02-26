using hanna80_SLICKIceWinterGear.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hanna80_SLICKIceWinterGear.DAL
{
	public interface IWinterGearRepository
	{
		IEnumerable<WinterGear> SelectAll();
		WinterGear SelectOne(int id);
		void Insert(WinterGear gearItem);
		void Update(WinterGear gearItem);
		void Delete(int id);
	}
}
