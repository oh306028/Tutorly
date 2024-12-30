using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tutorly.Application.Authorization;
using Tutorly.Application.Commands;
using Tutorly.Application.Interfaces;
using Tutorly.Domain.Models;
using Tutorly.Domain.ModelsExceptions;
using Tutorly.WebAPI.Services;

namespace Tutorly.Application.Handlers
{
    public class CreateAddressHandler : IHandler<CreateAddress>
    {
        private readonly IRepository<Address> _addressRepository;
        private readonly IRepository<Post> _postRepository;
        private readonly IAuthorizationService _authorizationService;
        private readonly IUserContextService _userContextService;

        public CreateAddressHandler(IRepository<Address> addressRepository, IRepository<Post> postRepository, IAuthorizationService authorizationService,
            IUserContextService userContextService)
        {
            _addressRepository = addressRepository;
            _postRepository = postRepository;
            _authorizationService = authorizationService;
            _userContextService = userContextService;
        }

        public async Task Handle(CreateAddress command)
        {
            var post = await _postRepository.GetByIdAsync(command.PostId);

            if (post is null)
                throw new NotFoundException("Post not found");

            var user = _userContextService.User;
            var authenticationResult = await _authorizationService.AuthorizeAsync(user, post, new ResourceAvaibilityRequirement());

            if (!authenticationResult.Succeeded)
                throw new ForbidException("Cannot add address to the post that current user is not owner of");

            var address = new Address()
            {
                City = command.City,
                Street = command.Street,
                Number = command.Number
            };


            await _addressRepository.AddAsync(address);


        }

    }
}
