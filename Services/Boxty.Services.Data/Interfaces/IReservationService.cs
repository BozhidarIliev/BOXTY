using Boxty.Data.Models;
using Boxty.Models;
using Boxty.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Boxty.Services.Data.Interfaces
{
    public interface IReservationService
    {
        IEnumerable<T> GetAllReservations<T>();
        Reservation GetReservationById(int id);
        Task AddReservation(ReservationInputModel model);
        Task Update(ReservationInputModel model);
        Table CheckForAvailablity(ReservationInputModel model);
        Task RemoveReservation(int id);
        Task ConfirmReservation(int id);
    }
}
