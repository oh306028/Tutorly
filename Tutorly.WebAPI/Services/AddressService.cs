using AutoMapper;
using Tutorly.Application.Commands;
using Tutorly.Application.Dtos.CreateDtos;
using Tutorly.Application.Dtos.DisplayDtos;
using Tutorly.Application.Handlers;
using Tutorly.Application.Interfaces;
using Tutorly.Application.Queries;
using Tutorly.Domain.Models;
using Tutorly.Domain.ModelsExceptions;

namespace Tutorly.WebAPI.Services
{
    public interface IAddressService
    {
        Task<AddressDto> GetPostAddressAsync(int id);
        Task CreateAddressAsync(int postId, CreateAddressDto dto);
    }



    public class AddressService : IAddressService
    {
        private readonly IQueryHandler<GetAddress, Address> _getAddressQueryHandler;
        private readonly IMapper _mapper;
        private readonly IHandler<CreateAddress> _createAddressHandler;

        public AddressService(IQueryHandler<GetAddress, Address> getAddressQueryHandler, IMapper mapper, IHandler<CreateAddress> createAddressHandler)
        {
            _getAddressQueryHandler = getAddressQueryHandler;
            _mapper = mapper;
            _createAddressHandler = createAddressHandler;
        }

        public async Task<AddressDto> GetPostAddressAsync(int id)   
        {
            var query = new GetAddress(id);
            var address = await _getAddressQueryHandler.HandleAsync(query);

            var result = _mapper.Map<AddressDto>(address);

            return result;

        }

        public async Task CreateAddressAsync(int postId, CreateAddressDto dto)
        {
            var command = new CreateAddress(postId, dto.City, dto.Street, dto.Number);
            await _createAddressHandler.Handle(command);
        }


    }
}
