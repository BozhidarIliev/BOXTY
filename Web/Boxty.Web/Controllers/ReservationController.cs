namespace Boxty.Web.Controllers
{
    using System.Threading.Tasks;
    using Boxty.Common;
    using Boxty.Services.Data.Interfaces;
    using Boxty.Web.ViewModels;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

    [Authorize(Roles="manager, admin")]
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
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PersonName,PersonEmail,PersonNumber,NumberOfSeats,StartTime,NumberOfHours")] ReservationInputModel model)
        {
            if (ModelState.IsValid)
            {
                await reservationService.AddReservation(model);
            }

            return View(model);
        }

        public IActionResult Edit(int id)
        {
            var reservation = reservationService.GetReservationById(id);

            if (reservation == null)
            {
                return NotFound();
            }

            return View(reservation);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PersonName,PersonEmail,PersonNumber,NumberOfSeats,StartTime,NumberOfHours")] ReservationInputModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    model.Id = id;
                    await reservationService.Update(model);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReservationExists(model.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return RedirectToAction(nameof(Index));
            }

            return View(model);
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

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await reservationService.RemoveReservation(id);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ActionName("Update")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfirmReservation(int id)
        {
            await reservationService.ConfirmReservation(id);
            return RedirectToAction(nameof(Index));
        }


        private bool ReservationExists(int id)
        {
            return reservationService.GetReservationById(id) != null;
        }
    }
}
