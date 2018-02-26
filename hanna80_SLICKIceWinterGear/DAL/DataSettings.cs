using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hanna80_SLICKIceWinterGear.DAL
{
    public class DataSettings
    {
		private string _dataFilePath;

		public string DataFilePath => _dataFilePath;

		public DataSettings(string dataFilePath)
		{
			_dataFilePath = dataFilePath;
		}
    }
}
