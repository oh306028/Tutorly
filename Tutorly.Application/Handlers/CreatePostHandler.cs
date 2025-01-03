﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tutorly.Application.Commands;
using Tutorly.Application.Interfaces;
using Tutorly.Application.Services;
using Tutorly.Domain.Models;
using Tutorly.Domain.ModelsExceptions;

namespace Tutorly.Application.Handlers
{
    public class CreatePostHandler : IHandler<CreatePost>
    {
        private readonly IRepository<Post> _postRepository;
        private readonly IRepository<Tutor> _tutorRepository;
        private readonly IRepository<Category> _categoryRepository;
        private readonly PostCreatedService _postCreatedService;

        public CreatePostHandler(IRepository<Post> postRepository, IRepository<Tutor> tutorRepository, IRepository<Category> categoryRepository, PostCreatedService postCreatedService)
        {
            _postRepository = postRepository;
            _tutorRepository = tutorRepository;
            _categoryRepository = categoryRepository;
            _postCreatedService = postCreatedService;
        }

        //TO DO:
        //AFTER JWT AUTHORIZATION NO MORE NEED TO TAKE TUTOR FROM REPOSITORY -> TAKE IT FROM USERCONTEXT ID
        public async Task Handle(CreatePost command)
        {
            var tutor = await _tutorRepository.GetByIdAsync(command.TutorId);

            if (tutor is null)
                throw new NotFoundException("Tutor not found");

            var category = await _categoryRepository.GetByIdAsync(command.CategoryId);

            if(category is null)
                throw new NotFoundException("Category not found");

            if (command.MaxStudentAmount <= 0)
                throw new OutOfSpaceException("Cannot create post for 0 or less students"); 

            Post newPost = new(
                tutor,
                command.MaxStudentAmount,
                category, 
                command.HappensOn,
                command.HappensAt,
                command.IsRemotely, 
                command.IsAtStudentPlace,
                command.AddressId,
                command.Description);


            await _postRepository.AddAsync(newPost);

            _postCreatedService.CreatedPostId = newPost.Id;


        }
    }
}
