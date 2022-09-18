using FloraEducationAPI.Domain.DTO;
using FloraEducationAPI.Domain.Enumerations;
using FloraEducationAPI.Domain.Models;
using FloraEducationAPI.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FloraEducationAPI.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlantController : ControllerBase
    {
        private readonly IPlantService plantService;
        private readonly IMiniQuizService miniQuizService;
        private readonly ICommentService commentService;
        private readonly IUserLikedPlantsService userLikedPlantsService;

        public PlantController(IPlantService plantService, IMiniQuizService miniQuizService, ICommentService commentService, IUserLikedPlantsService userLikedPlantsService)
        {
            this.plantService = plantService;
            this.miniQuizService = miniQuizService;
            this.commentService = commentService;
            this.userLikedPlantsService = userLikedPlantsService;
        }

        [HttpGet("categories")]
        public IActionResult GetAllPlantTypes()
        {
            return Ok(JsonConvert.SerializeObject(new { categories = Enum.GetNames(typeof(PlantType))}));
        }

        [HttpGet]
        public IActionResult GetAllPlantsByType([FromQuery] string query)
        {
            if (Enum.TryParse<PlantType>(query, out PlantType plantType))
            {
                return Ok(plantService.FetchAllPlantsByType(plantType));
            } else
            {
                return NotFound(new { error = "Category not found" });
            }

        }

        [HttpGet("{plantId}")]
        public IActionResult GetPlant(Guid plantId)
        {
            if (plantId == null || !plantService.PlantExists(plantId))
            {
                return NotFound(new { error = "Plant not found" });
            }

            return Ok(plantService.FetchPlantById(plantId));
        }

        [HttpPost]
        public IActionResult AddPlant([FromBody] Plant plant)
        {
            return Ok(plantService.CreatePlant(plant));
        }

        [HttpGet("{plantId}/mini-quiz")]
        public IActionResult GetMiniQuizForPlant(Guid plantId)
        {
            var miniQuiz = miniQuizService.FetchMiniQuizByPlantId(plantId);

            if (miniQuiz != null)
            {
                return Ok(miniQuiz);
            }

            return NotFound(JsonConvert.SerializeObject(new { error = "Mini quiz not found" }));
        }

        [HttpPost("{plantId}/mini-quiz")]
        public IActionResult AddMiniQuizForPlant(Guid plantId, [FromBody] MiniQuizDTO miniQuizDTO)
        {
            miniQuizDTO.PlantId = plantId;
            return Ok(miniQuizService.CreateMiniQuiz(miniQuizDTO));
        }

        [HttpPost("{plantId}/mini-quiz/question")]
        public IActionResult AddQuestionToMiniQuiz(Guid plantId, [FromBody] QuizQuestionDTO quizQuestionDTO)
        {
            var miniQuiz = miniQuizService.FetchMiniQuizByPlantId(plantId);
            quizQuestionDTO.QuizId = miniQuiz.Id;
            return Ok(miniQuizService.AddQuestionToQuiz(quizQuestionDTO));
        }

        [HttpPost("{plantId}/add-comment")]
        public IActionResult AddCommentToPlant([FromBody] CommentDTO commentDTO)
        {
            return Ok(commentService.AddCommentToPlant(commentDTO));
        }

        [HttpPost("add-to-liked")]
        public IActionResult AddPlantToLikedPlants([FromBody] UserLikedPlantDTO userLikedPlantDTO)
        {
            return Ok(userLikedPlantsService.AddPlantToLikedPlants(userLikedPlantDTO));
        }
    }
}
