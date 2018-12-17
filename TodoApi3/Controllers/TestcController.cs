//加入、選擇控制器、MVC(控制器)、讀寫動作的MVC控制器
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TodoApi3.Controllers
{
    public class TestcController : Controller
    {
        // GET: Testc
        public ActionResult Index()
        {
            return View();
        }

        // GET: Testc/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Testc/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Testc/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Testc/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Testc/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Testc/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Testc/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}