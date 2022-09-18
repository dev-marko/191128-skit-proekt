using FloraEducationAPI.Domain.DTO;
using FloraEducationAPI.Domain.Models;
using FloraEducationAPI.Domain.Models.Authentication;
using FloraEducationAPI.Repository.Interfaces;
using FloraEducationAPI.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FloraEducationAPI.Service.Implementations
{
    public class CommentService : ICommentService
    {
        private readonly IRepository<Comment> commentRepository;
        private readonly IPlantService plantService;
        private readonly IUserService userService;

        public CommentService(IRepository<Comment> commentRepository, IPlantService plantService, IUserService userService)
        {
            this.commentRepository = commentRepository;
            this.plantService = plantService;
            this.userService = userService;
        }

        public Comment AddCommentToPlant(CommentDTO commentDTO)
        {
            if (commentDTO == null)
            {
                throw new ArgumentNullException(null, "CommentDTO object must not be null");
            }

            if (string.IsNullOrEmpty(commentDTO.Content))
            {
                throw new ArgumentNullException(null, "Content must not be null or empty");
            }

            if (string.IsNullOrEmpty(commentDTO.Username))
            {
                throw new ArgumentNullException(null, "Username must not be null or empty");
            }

            if (commentDTO.PlantId == Guid.Empty)
            {
                throw new ArgumentException("Invalid id, guid must not be empty");
            }

            Plant plant = plantService.FetchPlantById(commentDTO.PlantId);

            if (plant == null)
            {
                throw new ArgumentNullException(null, "Plant not found");
            }

            User user = userService.FetchUserByUsername(commentDTO.Username);

            if (user == null)
            {
                throw new ArgumentNullException(null, "User not found");
            }

            Comment comment = new Comment
            {
                Plant = plant,
                Author = user,
                Content = commentDTO.Content
            };

            return commentRepository.Insert(comment);
        }
    }
}
