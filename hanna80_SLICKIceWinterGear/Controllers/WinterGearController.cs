using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using hanna80_SLICKIceWinterGear.DAL;
using hanna80_SLICKIceWinterGear.Models;
using Microsoft.AspNetCore.Hosting;

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

        // GET: WinterGear
		[HttpGet]
        public ActionResult Index(string sortOrder, bool flipOrder = false)
        {
			WinterGearRepository repository = new WinterGearRepository(_dataSettings);

			IEnumerable<WinterGear> winterGearItems;
			using (repository)
			{
				winterGearItems = repository.SelectAll() as IList<WinterGear>;
			}

			switch (sortOrder)
			{
				case "name":
					// order names ascending
					winterGearItems = winterGearItems.OrderBy(i => i.Name);
					break;
				case "condition":
					// order condition descending
					winterGearItems = winterGearItems.OrderByDescending(i => i.Condition);
					break;
				case "weight":
					// order weight ascending
					winterGearItems = winterGearItems.OrderBy(i => i.Weight);
					break;
				case "gearType":
					// order gearType ascending
					winterGearItems = winterGearItems.OrderBy(i => i.GearType);
					break;
				default:
					// order id ascending
					winterGearItems = winterGearItems.OrderBy(i => i.Id);
					break;
			}

			if (flipOrder)
			{
				winterGearItems = winterGearItems.Reverse();
			}

			ViewBag.sortOrder = sortOrder;
			ViewBag.flipOrder = flipOrder;
			ViewBag.gearTypes = listOfGearTypes();
			ViewBag.viewMode = "sort";

			return View(winterGearItems);
        }

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

		[HttpPost]
		public ActionResult Index(string searchCriteria, string gearTypeFilter, string sortOrder, bool flipOrder = false)
		{
			WinterGearRepository gearRepository = new WinterGearRepository(_dataSettings);

			IEnumerable<WinterGear> gearItems;

			using (gearRepository)
			{
				gearItems = gearRepository.SelectAll() as IList<WinterGear>;
			}

			if (!string.IsNullOrWhiteSpace(searchCriteria))
			{
				gearItems = gearItems.Where(item => item.Name.ToUpper().Contains(searchCriteria.ToUpper()));
			}

			if (!string.IsNullOrWhiteSpace(gearTypeFilter))
			{
				gearItems = gearItems.Where(item => item.GearType == gearTypeFilter);
			}

			if (flipOrder)
			{
				gearItems = gearItems.Reverse();
			}

			ViewBag.sortOrder = sortOrder;
			ViewBag.flipOrder = flipOrder;
			ViewBag.gearTypes = listOfGearTypes();
			ViewBag.gearTypeFilter = gearTypeFilter;
			ViewBag.searchCriteria = searchCriteria;
			ViewBag.viewMode = "filter";

			return View(gearItems);
		}

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