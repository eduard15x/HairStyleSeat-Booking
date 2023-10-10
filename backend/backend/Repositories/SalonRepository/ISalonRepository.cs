﻿using backend.Dtos.Salon;
using backend.Models.Salon;

namespace backend.Repositories.SalonRepository
{
    public interface ISalonRepository
    {
        Task<GetSingleSalonDto> CreateNewSalon(CreateNewSalonDto newSalonDetails);
        Task<GetSingleSalonDto> UpdateSalon(UpdateSalonDto updateSalonDto);
        Task<List<Salon>> GetAllSalons();
        Task<GetSingleSalonDto> GetSingleSalonDetails(int salonId);
    }
}
