namespace Boxty.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Boxty.Services.Data.Interfaces;
    using Boxty.Web.ViewModels;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

    [Authorize(Roles="admin")]
    public class ReservationController : Controller
    {
        private readonly IReservationService reservationService;

        public ReservationController(IReservationService reservationService)
        {
            this.reservationService = reservationService;
        }

        public IActionResult Index()
        {
            return View(reservationService.GetAllReservations<ReservationViewModel>());
        }

        public IActionResult Details(int id)
        {
            var reservation = reservationService.GetReservationById(id);
            if (reservation == null)
            {
                return NotFound();
            }

            return View(reservation);
        }

        [AllowAnonymous]
        public IActionResult Create()
        {
            var model = new ReservationCreateInputModel();
            model.StartTime = DateTime.Now;
            return this.View(model);
        }

        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ReservationCreateInputModel model)
        {
            if (!ModelState.IsValid)
            {
                model.StartTime = DateTime.Now;

                return this.View(model);
            }

            await reservationService.AddReservation(model);

            return View("RequestSent");
        }

        public IActionResult Delete(int id)
        {
            var reservation = reservationService.GetReservationById(id);
            if (reservation == null)
            {
                return NotFound();
            }

            return View(reservation);
        }

        public async Task<IActionResult> Confirm(int id)
        {
            await reservationService.ConfirmReservation(id);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await reservationService.RemoveReservation(id);
            return RedirectToAction(nameof(Index));
        }

        private bool ReservationExists(int id)
        {
            return reservationService.GetReservationById(id) != null;
        }
    }
}
