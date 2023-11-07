﻿using backend.Dtos.Reservation;

namespace backend.Repositories.ReservationRepository
{
    public interface IReservationRepository
    {
        Task<GetReservationDetailsCustomerDto> MakeReservation(CreateReservationDto createReservationDto);
        Task<List<GetReservationDetailsCustomerDto>> GetAllCustomerReservations(int customerId);
        Task<List<GetReservationDetailsForSalonDto>> GetAllSalonReservations(int salonAffiliateId);
        Task<GetReservationDetailsCustomerDto> GetCustomerReservationDetails(int customerId, int reservationId);
        Task<string> ConfirmReservation(int reservationId, int salonOwnerId);
        Task<string> CancelReservation(int reservationId, int customerId);
    }
}
