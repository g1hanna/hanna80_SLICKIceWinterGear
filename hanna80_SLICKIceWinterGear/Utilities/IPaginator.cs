﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hanna80_SLICKIceWinterGear.Utilities
{
    public interface IPaginator<T>
    {
		int CurrentPageNumber { get; set; }
		int PageCount { get; }
		int ItemsPerPage { get; set; }
		int ItemCount { get; }

		IEnumerable<T> GetItems();
		string RenderPaginatorNav(Func<int, string> actionGenerator);
		string RenderPaginatorSizeControl(Func<int, string> actionGenerator, int pageSize = 10);
    }
}
