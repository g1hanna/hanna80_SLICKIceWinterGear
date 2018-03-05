using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using hanna80_SLICKIceWinterGear.DAL;
using hanna80_SLICKIceWinterGear.Models;
using hanna80_SLICKIceWinterGear.Utilities;
using Microsoft.AspNetCore.Hosting;
//using PagedList;

namespace hanna80_SLICKIceWinterGear.Controllers
{
    public class WinterGearController : Controller
    {
		private DataSettings _dataSettings;
		private readonly IHostingEnvironment _hostingEnvironment;

		public WinterGearController(IHostingEnvironment env, DataSettings dataSettings)
		{
			_dataSettings = new DataSettings(env.ContentRootPath + dataSettings.DataFilePath);
			_hostingEnvironment = env;
		}

		[HttpGet]
		public ActionResult Index(SearchViewModel searchModel)
		{
			WinterGearRepository gearRepository = new WinterGearRepository(_dataSettings);

			IEnumerable<WinterGear> gearItems;

			using (gearRepository)
			{
				gearItems = gearRepository.SelectAll() as IList<WinterGear>;
			}

			if (!string.IsNullOrWhiteSpace(searchModel.SearchCriteria))
			{
				gearItems = gearItems.Where(item => item.Name.ToUpper().Contains(searchModel.SearchCriteria.ToUpper()));
			}

			if (!string.IsNullOrWhiteSpace(searchModel.GearTypeFilter))
			{
				gearItems = gearItems.Where(item => item.GearType == searchModel.GearTypeFilter);
			}

			switch (searchModel.SortOrder)
			{
				case "name":
					// order names ascending
					gearItems = gearItems.OrderBy(i => i.Name);
					break;
				case "condition":
					// order condition descending
					gearItems = gearItems.OrderByDescending(i => i.Condition);
					break;
				case "weight":
					// order weight ascending
					gearItems = gearItems.OrderBy(i => i.Weight);
					break;
				case "gearType":
					// order gearType ascending
					gearItems = gearItems.OrderBy(i => i.GearType);
					break;
				default:
					// order id ascending
					gearItems = gearItems.OrderBy(i => i.Id);
					break;
			}

			if (searchModel.FlipOrder)
			{
				gearItems = gearItems.Reverse();
			}

			int pageNumber = (searchModel.PageNumber ?? 1);

			int itemsPerPage = (searchModel.PageSize ?? 10);

			IPaginator<WinterGear> paginator = new Paginator<WinterGear>(gearItems, itemsPerPage, pageNumber);

			ViewBag.paginator = paginator;

			ViewBag.sortOrder = searchModel.SortOrder;
			ViewBag.flipOrder = searchModel.FlipOrder;
			ViewBag.gearTypes = listOfGearTypes();
			ViewBag.gearTypeFilter = searchModel.GearTypeFilter;
			ViewBag.searchCriteria = searchModel.SearchCriteria;
			ViewBag.isSearch = true;

			return View(paginator.GetItems());
		}

		//// GET: WinterGear
		//[HttpGet]
  //      public ActionResult Index(SortViewModel sortModel)
  //      {
		//	WinterGearRepository repository = new WinterGearRepository(_dataSettings);

		//	IEnumerable<WinterGear> winterGearItems;
		//	using (repository)
		//	{
		//		winterGearItems = repository.SelectAll() as IList<WinterGear>;
		//	}

		//	switch (sortModel.SortOrder)
		//	{
		//		case "name":
		//			// order names ascending
		//			winterGearItems = winterGearItems.OrderBy(i => i.Name);
		//			break;
		//		case "condition":
		//			// order condition descending
		//			winterGearItems = winterGearItems.OrderByDescending(i => i.Condition);
		//			break;
		//		case "weight":
		//			// order weight ascending
		//			winterGearItems = winterGearItems.OrderBy(i => i.Weight);
		//			break;
		//		case "gearType":
		//			// order gearType ascending
		//			winterGearItems = winterGearItems.OrderBy(i => i.GearType);
		//			break;
		//		default:
		//			// order id ascending
		//			winterGearItems = winterGearItems.OrderBy(i => i.Id);
		//			break;
		//	}

		//	if (sortModel.FlipOrder)
		//	{
		//		winterGearItems = winterGearItems.Reverse();
		//	}

		//	ViewBag.sortOrder = sortModel.SortOrder;
		//	ViewBag.flipOrder = sortModel.FlipOrder;
		//	ViewBag.gearTypes = listOfGearTypes();
		//	ViewBag.isSearch = false;

		//	int pageNumber = (sortModel.PageNumber ?? 1);

		//	IPaginator<WinterGear> paginator = new Paginator<WinterGear>(winterGearItems, 10, pageNumber);

		//	ViewBag.paginator = paginator;

		//	return View(paginator.GetItems());
  //      }

		[NonAction]
		private IEnumerable<string> listOfGearTypes()
		{
			IEnumerable<WinterGear> gearItems;

			using (WinterGearRepository gearRepository = new WinterGearRepository(_dataSettings))
			{
				gearItems = gearRepository.SelectAll() as IList<WinterGear>;
			}

			IEnumerable<string> gearTypes = gearItems.Select(gearItem => gearItem.GearType).Distinct().OrderBy(gt => gt);

			return gearTypes;
		}

		//[HttpPost]
		//public ActionResult Index(SearchViewModel searchModel)
		//{
		//	WinterGearRepository gearRepository = new WinterGearRepository(_dataSettings);

		//	IEnumerable<WinterGear> gearItems;

		//	using (gearRepository)
		//	{
		//		gearItems = gearRepository.SelectAll() as IList<WinterGear>;
		//	}

		//	if (!string.IsNullOrWhiteSpace(searchModel.SearchCriteria))
		//	{
		//		gearItems = gearItems.Where(item => item.Name.ToUpper().Contains(searchModel.SearchCriteria.ToUpper()));
		//	}

		//	if (!string.IsNullOrWhiteSpace(searchModel.GearTypeFilter))
		//	{
		//		gearItems = gearItems.Where(item => item.GearType == searchModel.GearTypeFilter);
		//	}

		//	switch (searchModel.SortOrder)
		//	{
		//		case "name":
		//			// order names ascending
		//			gearItems = gearItems.OrderBy(i => i.Name);
		//			break;
		//		case "condition":
		//			// order condition descending
		//			gearItems = gearItems.OrderByDescending(i => i.Condition);
		//			break;
		//		case "weight":
		//			// order weight ascending
		//			gearItems = gearItems.OrderBy(i => i.Weight);
		//			break;
		//		case "gearType":
		//			// order gearType ascending
		//			gearItems = gearItems.OrderBy(i => i.GearType);
		//			break;
		//		default:
		//			// order id ascending
		//			gearItems = gearItems.OrderBy(i => i.Id);
		//			break;
		//	}

		//	if (searchModel.FlipOrder)
		//	{
		//		gearItems = gearItems.Reverse();
		//	}

		//	int pageNumber = (searchModel.PageNumber ?? 1);

		//	IPaginator<WinterGear> paginator = new Paginator<WinterGear>(gearItems, 10, pageNumber);
		//	ViewBag.paginator = paginator;

		//	ViewBag.sortOrder = searchModel.SortOrder;
		//	ViewBag.flipOrder = searchModel.FlipOrder;
		//	ViewBag.gearTypes = listOfGearTypes();
		//	ViewBag.gearTypeFilter = searchModel.GearTypeFilter;
		//	ViewBag.searchCriteria = searchModel.SearchCriteria;
		//	ViewBag.isSearch = true;

		//	return View(gearItems);
		//}

		// GET: WinterGear/Details/5
		[HttpGet]
        public ActionResult Details(int id)
        {
			WinterGearRepository repository = new WinterGearRepository(_dataSettings);

			WinterGear winterGearItem;
			using (repository)
			{
				winterGearItem = repository.SelectOne(id);
			}

			return View(winterGearItem);
        }

		// GET: WinterGear/Create
		[HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        // POST: WinterGear/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(WinterGear item)
        {
            try
            {
				WinterGearRepository repository = new WinterGearRepository(_dataSettings);

				using (repository)
				{
					repository.Insert(item);
				}

				return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
				ViewData["errorOccurred"] = true;
				ViewData["errorMessage"] = ex.Message;

				return View();
            }
        }

		// GET: WinterGear/Edit/5
		[HttpGet]
        public ActionResult Edit(int id)
        {
			WinterGearRepository repository = new WinterGearRepository(_dataSettings);

			WinterGear winterGearItem;
			using (repository)
			{
				winterGearItem = repository.SelectOne(id);
			}

			return View(winterGearItem);
        }

        // POST: WinterGear/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, WinterGear item)
        {
            try
            {
				WinterGearRepository repository = new WinterGearRepository(_dataSettings);

				WinterGear updateItem = new WinterGear()
				{
					Id = id,
					Name = item.Name,
					Weight = item.Weight,
					GearType = item.GearType,
					Condition = item.Condition,
				};

				using (repository)
				{
					repository.Update(updateItem);
				}

				return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
				ViewData["errorOccurred"] = true;
				ViewData["errorMessage"] = ex.Message;

                return View();
            }
        }

		// GET: WinterGear/Delete/5
		[HttpGet]
        public ActionResult Delete(int id)
        {
			WinterGearRepository repository = new WinterGearRepository(_dataSettings);

			WinterGear winterGearItem;
			using (repository)
			{
				winterGearItem = repository.SelectOne(id);
			}

			return View(winterGearItem);
        }

        // POST: WinterGear/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, WinterGear item)
        {
            try
            {
				WinterGearRepository repository = new WinterGearRepository(_dataSettings);

				using (repository)
				{
					repository.Delete(id);
				}

				return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
				ViewData["errorOccurred"] = true;
				ViewData["errorMessage"] = ex.Message;

				return View();
            }
        }
    }
}