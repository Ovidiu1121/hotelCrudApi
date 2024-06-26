﻿using HotelCrudApi.Dto;
using HotelCrudApi.Hotels.Model;
using HotelCrudApi.Hotels.Repository.interfaces;
using HotelCrudApi.Hotels.Service.interfaces;
using HotelCrudApi.System.Constant;
using HotelCrudApi.System.Exceptions;

namespace HotelCrudApi.Hotels.Service
{
    public class HotelCommandService: IHotelCommandService
    {
        private IHotelRepository _repository;

        public HotelCommandService(IHotelRepository repository)
        {
            _repository = repository;
        }

        public async Task<HotelDto> CreateHotel(CreateHotelRequest request)
        {
            HotelDto hotel = await _repository.GetByNameAsync(request.Name);

            if (hotel!=null)
            {
                throw new ItemAlreadyExists(Constants.HOTEL_ALREADY_EXIST);
            }

            hotel=await _repository.CreateHotel(request);
            return hotel;
        }

        public async Task<HotelDto> DeleteHotel(int id)
        {
            HotelDto hotel = await _repository.GetByIdAsync(id);

            if (hotel==null)
            {
                throw new ItemDoesNotExist(Constants.HOTEL_DOES_NOT_EXIST);
            }

            await _repository.DeleteHotel(id);
            return hotel;
        }

        public async Task<HotelDto> UpdateHotel(int id, UpdateHotelRequest request)
        {
            HotelDto hotel = await _repository.GetByIdAsync(id);

            if (hotel==null)
            {
                throw new ItemDoesNotExist(Constants.HOTEL_DOES_NOT_EXIST);
            }

            hotel = await _repository.UpdateHotel(id, request);
            return hotel;
        }
    }
}
