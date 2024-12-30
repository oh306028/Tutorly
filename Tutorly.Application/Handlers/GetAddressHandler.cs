using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tutorly.Application.Interfaces;
using Tutorly.Application.Queries;
using Tutorly.Domain.Models;
using Tutorly.Domain.ModelsExceptions;

namespace Tutorly.Application.Handlers
{
    public class GetAddressHandler : IQueryHandler<GetAddress, Address>
    {
        private readonly IRepository<Address> _addressRepository;
        private readonly IRepository<Post> _postRepository;
        public GetAddressHandler(IRepository<Address> addressRepository, IRepository<Post> postRepository)
        {
            _addressRepository = addressRepository;
            _postRepository = postRepository;
        }

        public async Task<Address> HandleAsync(GetAddress query)
        {
            var post = await _postRepository.GetByIdAsync(query.PostId);
            if (post is null)
                throw new NotFoundException("Post not found");


            var address = await _addressRepository.GetByIdAsync((int)post.AddressId);
            if (address is null)
                throw new NotFoundException("The post has no address or is remotely working");

            return address;
        }

    }
}
