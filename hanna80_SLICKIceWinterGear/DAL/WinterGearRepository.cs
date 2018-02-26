using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using hanna80_SLICKIceWinterGear.Models;
using Microsoft.AspNetCore.Http;

namespace hanna80_SLICKIceWinterGear.DAL
{
	public class WinterGearRepository : IWinterGearRepository, IDisposable
	{
		private List<WinterGear> _gearItems;
		private DataSettings _settings;

		public WinterGearRepository(DataSettings dataSettings)
		{
			_settings = dataSettings;

			WinterGearXmlDataService gearDataService = new WinterGearXmlDataService(_settings);

			using (gearDataService)
			{
				_gearItems = gearDataService.Read() as List<WinterGear>;
			}
		}

		#region IDisposable Support
		private bool disposedValue = false; // To detect redundant calls

		protected virtual void Dispose(bool disposing)
		{
			if (!disposedValue)
			{
				if (disposing)
				{
					// dispose managed state (managed objects).
					_gearItems = null;
				}

				// free unmanaged resources (unmanaged objects) and override a finalizer below.

				// set large fields to null.

				disposedValue = true;
			}
		}

		// override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
		~WinterGearRepository()
		{
			// Do not change this code. Put cleanup code in Dispose(bool disposing) above.
			Dispose(false);
		}

		// This code added to correctly implement the disposable pattern.
		public void Dispose()
		{
			// Do not change this code. Put cleanup code in Dispose(bool disposing) above.
			Dispose(true);
			// uncomment the following line if the finalizer is overridden above.
			GC.SuppressFinalize(this);
		}
		#endregion

		public void Delete(int id)
		{
			WinterGear item = _gearItems.Where(i => i.Id == id).FirstOrDefault();

			if (item != null)
			{
				_gearItems.Remove(item);
			}

			reorderItems();

			Save();
		}

		public void Insert(WinterGear gearItem)
		{
			int nextItemId = getNextItemId();

			// insert a cloned item with an updated id
			WinterGear insertedItem = new WinterGear()
			{
				Id = nextItemId,
				Condition = gearItem.Condition,
				GearType = gearItem.GearType,
				Name = gearItem.Name,
				Weight = gearItem.Weight
			};

			_gearItems.Add(insertedItem);

			reorderItems();

			Save();
		}

		private int getNextItemId()
		{
			return _gearItems.OrderByDescending(i => i.Id).FirstOrDefault().Id + 1;
		}

		public IEnumerable<WinterGear> SelectAll()
		{
			return _gearItems;
		}

		public WinterGear SelectOne(int id)
		{
			WinterGear selectedItem = _gearItems.Where(i => i.Id == id).FirstOrDefault();

			return selectedItem;
		}

		public void Update(WinterGear gearItem)
		{
			WinterGear item = _gearItems.Where(i => i.Id == gearItem.Id).FirstOrDefault();

			if (item != null)
			{
				_gearItems.Remove(item);
				_gearItems.Add(gearItem);
			}

			reorderItems();

			Save();
		}

		public void Save()
		{
			WinterGearXmlDataService gearDataService = new WinterGearXmlDataService(_settings);

			using (gearDataService)
			{
				gearDataService.Write(_gearItems);
			}
		}

		private void reorderItems()
		{
			_gearItems.Sort((i1, i2) => i1.Id.CompareTo(i2.Id));
		}
	}
}
