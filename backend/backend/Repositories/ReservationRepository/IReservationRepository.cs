﻿using backend.Dtos.Reservation;

namespace backend.Repositories.ReservationRepository
{
    public interface IReservationRepository
    {
        Task<GetReservationDetailsCustomerDto> MakeReservation(CreateReservationDto createReservationDto);
        Task<List<GetReservationDetailsCustomerDto>> GetAllCustomerReservations(int customerId);
        Task<GetReservationDetailsCustomerDto> GetCustomerReservationDetails(int customerId, int reservationId);
    }
}