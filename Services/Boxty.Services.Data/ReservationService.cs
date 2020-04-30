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

namespace Boxty.Services.Data
{
    public class ReservationService : IReservationService
    {
        private readonly IDeletableEntityRepository<Reservation> reservationRepository;
        private readonly ITableService tableService;

        public ReservationService(IDeletableEntityRepository<Reservation> reservationRepository, ITableService tableService)
        {
            this.reservationRepository = reservationRepository;
            this.tableService = tableService;
        }

        public IEnumerable<T> GetAllReservations<T>()
        {
            return reservationRepository.All().To<T>();
        }

        public Reservation GetReservationById(int id)
        {
            return reservationRepository.All().FirstOrDefault(x => x.Id == id);
        }

        public async Task AddReservation(ReservationInputModel model)
        {
            var table = CheckForAvailablity(model);
            if (table != null)
            {
                var reservation = new Reservation
                {
                    NumberOfSeats = model.NumberOfSeats,
                    StartTime = model.StartTime,
                    EndTime = model.StartTime.AddHours(model.NumberOfHours),
                    PersonEmail = model.PersonEmail,
                    PersonName = model.PersonName,
                    PersonNumber = model.PersonNumber,
                    Table = table,
                };

                await reservationRepository.AddAsync(reservation);
                await reservationRepository.SaveChangesAsync();
            }
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

        public async Task Update(ReservationInputModel model)
        {
            var table = CheckForAvailablity(model);
            if (table != null)
            {
                var reservation = new Reservation
                {
                    Id = model.Id,
                    NumberOfSeats = model.NumberOfSeats,
                    StartTime = model.StartTime,
                    EndTime = model.StartTime.AddHours(model.NumberOfHours),
                    PersonEmail = model.PersonEmail,
                    PersonName = model.PersonName,
                    PersonNumber = model.PersonNumber,
                    Table = table,
                };
                reservationRepository.Update(reservation);
                await reservationRepository.SaveChangesAsync();
            }
        }

        public Table CheckForAvailablity(ReservationInputModel model)
        {
             var items = reservationRepository.All()
                .Where(x =>
                (x.StartTime <= model.StartTime.AddHours(model.NumberOfHours) && (x.EndTime >= model.StartTime)));
            if (items != null)
            {
                var tables = tableService.GetTablesByNumberOfSeats(model.NumberOfSeats);
                foreach (var table in tables)
                {
                    if (!items.Any(x => x.TableId == table.Id))
                    {
                        return table;
                    }
                }
            }

            return null;
        }

        public async Task ConfirmReservation(int id)
        {
            reservationRepository.All().FirstOrDefault(x => x.Id == id).Confirmed = true;
            await reservationRepository.SaveChangesAsync();
        }
    }
}
