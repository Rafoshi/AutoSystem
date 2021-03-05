using AutoSystem.Models;
using AutoSystem.Repostory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AutoSystem.Controllers
{
    public class HomeController : Controller
    {
        Queries queries = new Queries();

        public ActionResult Index()
        {
            Vehicle vehicle = new Vehicle();

            return View(vehicle);
        }

        public ActionResult RentList(Rent rent)
        {
            ViewBag.ListRent = queries.RentListVehicle();

            return View(rent);
        }

        public ActionResult DeleteRent(int rentID)
        {
            queries.DeleteRent(rentID);
            return RedirectToAction("RentList");
        }

        public ActionResult DeleteRentWithTax(int rentID)
        {
            queries.DeleteRentWithTax(rentID);
            return RedirectToAction("Index");
        }

        public ActionResult List()
        {
            ViewBag.ListVehicle = queries.ListVehicle();

            return View("List");
        }

        public ActionResult Edit(int vehicleID)
        {
            Vehicle vehicle = new Vehicle();
            vehicle = queries.GetVehicleById(vehicleID);
            return View(vehicle);
        }

        public ActionResult Rent()
        {
            Rent rent = new Rent();
            ViewBag.ListVehicle = queries.ListVehicle();
            return View(rent);
        }

        public ActionResult DeleteVehicle(int vehicleID)
        {
            queries.DeleteVehicle(vehicleID);
            return RedirectToAction("List");
        }

        [HttpPost]
        public ActionResult RegisterVehicle(Vehicle vehicle)
        {
            queries.RegisterVehicle(vehicle);

            return RedirectToAction("List");
        }

        [HttpPost]
        public ActionResult UpdateVehicle(Vehicle vehicle)
        {
            queries.UpdateVehicle(vehicle);

            return RedirectToAction("List");
        }

        [HttpPost]
        public ActionResult RegisterRent(Rent rent)
        {
            try
            {
                queries.RegisterRent(rent);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return RedirectToAction("Index");
            }
            return RedirectToAction("RentList");
        }
    }
}