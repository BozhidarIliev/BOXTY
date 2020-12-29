using AutoMapper.QueryableExtensions;
using Boxty.Common;
using Boxty.Data.Common.Repositories;
using Boxty.Data.Models;
using Boxty.Models;
using Boxty.Services.Data.Interfaces;
using Boxty.Services.Mapping;
using Boxty.Web.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Boxty.Services.Data.Tests
{
    public class ReservationServiceTests : BaseServiceTests
    {
        private IReservationService reservationService => this.ServiceProvider.GetRequiredService<IReservationService>();

        [Fact]
        public void AddReservation()
        {
            var reservation = new ReservationCreateInputModel
            {
                Id = 1,
                Name = "1",
                StartTime = DateTime.Now.AddDays(1),
                Duration = 1,
                Email = "a@a.a",
                Number = "08999999999",
                NumberOfSeats = 2,
            };

            reservationService.AddReservation(reservation);

            var actual = reservationService.GetReservationById(1);

            Assert.Equal(reservation.Id, actual.Id);
        }

        [Fact]
        public void RemoveReservation()
        {
            var reservation = new ReservationCreateInputModel
            {
                Id = 1,
                Name = "1",
                StartTime = DateTime.Now.AddDays(1),
                Duration = 1,
                Email = "a@a.a",
                Number = "08999999999",
                NumberOfSeats = 2,
            };

            reservationService.AddReservation(reservation);
            reservationService.RemoveReservation(1);

            var actual = reservationService.GetReservationById(1);

            Assert.Null(actual);
        }

    }
}
