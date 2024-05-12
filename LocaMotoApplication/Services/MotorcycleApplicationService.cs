using AutoMapper;
using LocaMoto.Application.DTOs.Requests;
using LocaMoto.Application.DTOs.Responses;
using LocaMoto.Application.Interfaces;
using LocaMoto.Domain.Entities;
using LocaMoto.Domain.Interfaces.Services;

namespace LocaMoto.Application.Services
{
    public sealed class MotorcycleApplicationService(IMotorcycleService motorcycleService,
        IMapper mapper) : IMotorcycleApplicationService
    {
        private readonly IMotorcycleService _motorcycleService = motorcycleService;
        private readonly IMapper _mapper = mapper;

        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            try
            {
                var motorcycle = await _motorcycleService.GetByIdAsync(id, cancellationToken);

                if (motorcycle is not null)
                    await _motorcycleService.DeleteAsync(motorcycle, cancellationToken);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<IEnumerable<MotorcycleResponseDto>> GetAllAsync(CancellationToken cancellationToken)
        {
            var motorcycles = await _motorcycleService.GetAllAsync(cancellationToken);

            return _mapper.Map<IEnumerable<MotorcycleResponseDto>>(motorcycles);
        }

        public async Task<IEnumerable<MotorcycleResponseDto>> FindByLicensePlateAsync(string description, CancellationToken cancellationToken)
        {
            try
            {
                var motorcycle = await _motorcycleService.FindByLicensePlateAsync(description, cancellationToken);

                return _mapper.Map<IEnumerable<MotorcycleResponseDto>>(motorcycle);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<MotorcycleResponseDto> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            try
            {
                var motorcycle = await _motorcycleService.GetByIdAsync(id, cancellationToken);

                return _mapper.Map<MotorcycleResponseDto>(motorcycle);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task SaveAsync(MotorcycleRequestDto motorcycleRequestDto, CancellationToken cancellationToken)
        {
            try
            {
                var licensePlateExists = await LicensePlateExists(motorcycleRequestDto.LicensePlate, cancellationToken);
                
                if (licensePlateExists)
                    throw new Exception("License plate already registered");
                                
                var motorcycle = _mapper.Map<Motorcycle>(motorcycleRequestDto);
                motorcycle.Id = Guid.NewGuid();
                motorcycle.CreationDate = DateTime.Now;
                
                await _motorcycleService.AddAsync(motorcycle, cancellationToken);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task UpdateAsync(MotorcycleRequestDto motorcycleRequestDto, CancellationToken cancellationToken)
        {
            try
            {
                var motorcycleWithSameLicensePlate = await _motorcycleService
                    .FindByLicensePlateAsync(motorcycleRequestDto.LicensePlate, cancellationToken);
                
                if(motorcycleWithSameLicensePlate != null && 
                    motorcycleRequestDto.Id != motorcycleWithSameLicensePlate.Id)
                    throw new Exception("License plate already registered to another motorcycle");

                var motorcycle = _mapper.Map<Motorcycle>(motorcycleRequestDto);

                await _motorcycleService.UpdateAsync(motorcycle, cancellationToken);
            }
            catch (Exception)
            {

                throw;
            }
        }

        private async Task<bool> LicensePlateExists(string description, CancellationToken cancellationToken)
        {
            var motorcycle = await _motorcycleService.FindByLicensePlateAsync(description, cancellationToken);

            if (motorcycle != null)
                return true;

            return false;
        }               
    }
}
