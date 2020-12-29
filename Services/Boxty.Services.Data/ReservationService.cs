using Boxty.Data.Common.Repositories;
using Boxty.Data.Models;
using Boxty.Models;
using Boxty.Services.Data.Interfaces;
using Boxty.Services.Interfaces;
using Boxty.Services.Mapping;
using Boxty.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Boxty.Services.Data
{
    public class ReservationService : IReservationService
    {
        private readonly IDeletableEntityRepository<Reservation> reservationRepository;

        public ReservationService(IDeletableEntityRepository<Reservation> reservationRepository)
        {
            this.reservationRepository = reservationRepository;
        }

        public IEnumerable<T> GetAllReservations<T>()
        {
            return reservationRepository.All().To<T>();
        }

        public Reservation GetReservationById(int id)
        {
            return reservationRepository.All().FirstOrDefault(x => x.Id == id);
        }

        public async Task AddReservation(ReservationCreateInputModel model)
        {
            var reservation = new Reservation
            {
                StartTime = model.StartTime,
                NumberOfSeats = model.NumberOfSeats,
                Duration = model.Duration,
                PersonEmail = model.Email,
                PersonName = model.Name,
                PersonNumber = model.Number,
            };

            await reservationRepository.AddAsync(reservation);
            await reservationRepository.SaveChangesAsync();
        }

        public async Task RemoveReservation(int id)
        {
            var item = reservationRepository.All().FirstOrDefault(x => x.Id == id);
            if (item != null)
            {
                reservationRepository.Delete(item);
                await reservationRepository.SaveChangesAsync();
            }
        }

        public async Task Update(ReservationCreateInputModel model)
        {
            var reservation = new Reservation
            {
                Id = model.Id,
                NumberOfSeats = model.NumberOfSeats,
                Duration = model.Duration,
                StartTime = model.StartTime,
                PersonEmail = model.Email,
                PersonName = model.Name,
                PersonNumber = model.Number,
            };
            reservationRepository.Update(reservation);
            await reservationRepository.SaveChangesAsync();
        }

        public async Task ConfirmReservation(int id)
        {
            reservationRepository.All().FirstOrDefault(x => x.Id == id).Confirmed = true;
            await reservationRepository.SaveChangesAsync();
        }
    }
}
