using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using SammysAuto.Data;
using SammysAuto.Models;

namespace SammysAuto.Controllers
{
    public class ServiceTypesController : Controller
    {
        private readonly ApplicationDbContext _db;

        public ServiceTypesController(ApplicationDbContext db)
        {
            this._db = db;
        }

        //GET : /ServiceTypes
        public IActionResult Index()
        {
            return View(this._db.ServiceTypes.ToList());
        }

        //GET: Services/Create
        public IActionResult Create()
        {
            return View();
        }

        //POST: /ServiceTypes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ServiceType serviceType)
        {
            if(ModelState.IsValid)
            {
                this._db.Update(serviceType);
                await this._db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(serviceType);
        }

        //GET : /ServiceTypes/Details
        public async Task<IActionResult> Details(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }
            var serviceType = await this._db.ServiceTypes.SingleOrDefaultAsync(m=>m.Id == id);
            if(serviceType == null)
            {
                return NotFound();
            }
            return View(serviceType);
        }

        protected override void Dispose(bool disposing)
        {
            if(disposing)
            {
                this._db.Dispose();
            }
        }
    }
}
