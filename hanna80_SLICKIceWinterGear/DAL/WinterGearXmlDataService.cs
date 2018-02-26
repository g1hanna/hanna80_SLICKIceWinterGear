using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;
using hanna80_SLICKIceWinterGear.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace hanna80_SLICKIceWinterGear.DAL
{
	public class WinterGearXmlDataService : IWinterGearDataService, IDisposable
	{
		private DataSettings _settings;

		public WinterGearXmlDataService(DataSettings settings)
		{
			_settings = settings;
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
				}

				// free unmanaged resources (unmanaged objects) and override a finalizer below.
				

				// set large fields to null.

				disposedValue = true;
			}
		}

		// override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
		~WinterGearXmlDataService()
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

		public List<WinterGear> Read()
		{
			WinterGearItems itemsObject;
			
			StreamReader sReader = new StreamReader(_settings.DataFilePath);

			XmlSerializer deserializer = new XmlSerializer(typeof(WinterGearItems));

			using (sReader)
			{
				object xmlObject = deserializer.Deserialize(sReader);

				itemsObject = (WinterGearItems)xmlObject;
			}

			return itemsObject.Items;
		}

		public void Write(List<WinterGear> gearItems)
		{
			StreamWriter sWriter = new StreamWriter(_settings.DataFilePath, false);

			XmlSerializer serializer = new XmlSerializer(typeof(List<WinterGear>), new XmlRootAttribute("WinterGearItems"));

			using (sWriter)
			{
				serializer.Serialize(sWriter, gearItems);
			}
		}
	}
}
