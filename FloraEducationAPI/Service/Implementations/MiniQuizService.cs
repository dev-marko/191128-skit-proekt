using FloraEducationAPI.Domain.DTO;
using FloraEducationAPI.Domain.Models;
using FloraEducationAPI.Repository.Interfaces;
using FloraEducationAPI.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FloraEducationAPI.Service.Implementations
{
    public class MiniQuizService : IMiniQuizService
    {
        private readonly IRepository<MiniQuiz> miniQuizRepositoryGeneric;
        private readonly IRepository<QuizQuestion> questionsRepository;
        private readonly IMiniQuizRepository miniQuizRepository;
        private readonly IPlantService plantService;

        public MiniQuizService(IRepository<MiniQuiz> miniQuizRepositoryGeneric, IRepository<QuizQuestion> questionsRepository, IMiniQuizRepository miniQuizRepository, IPlantService plantService)
        {
            this.miniQuizRepositoryGeneric = miniQuizRepositoryGeneric;
            this.questionsRepository = questionsRepository;
            this.miniQuizRepository = miniQuizRepository;
            this.plantService = plantService;
        }

        public QuizQuestion AddQuestionToQuiz(QuizQuestionDTO quizQuestionDTO)
        {
            if (quizQuestionDTO == null)
            {
                throw new ArgumentNullException(null, "QuizQuestionDTO must not be null");
            }

            if (quizQuestionDTO.QuizId == Guid.Empty)
            {
                throw new ArgumentException("Invalid id value, Guid must not be empty");
            }

            string[] fields = new string[] { quizQuestionDTO.Question, quizQuestionDTO.Answer1, quizQuestionDTO.Answer2, quizQuestionDTO.Answer3, quizQuestionDTO.Answer4 };
            bool anyNull = fields.Any(f => string.IsNullOrEmpty(f));

            if (anyNull)
            {
                throw new ArgumentNullException(null, "Question and answers must not be null or empty");
            }

            if (quizQuestionDTO.CorrectAnswerIndex >= 4 || quizQuestionDTO.CorrectAnswerIndex < 0)
            {
                throw new InvalidOperationException("CorrectAnswerIndex must not be greater than 3 or less than 0");
            }

            var miniQuiz = miniQuizRepositoryGeneric.FetchById(quizQuestionDTO.QuizId);

            if (miniQuiz == null)
            {
                throw new ArgumentNullException(null, "Mini Quiz not found");
            }

            List<string> answers = new List<string>() 
            {
                quizQuestionDTO.Answer1,
                quizQuestionDTO.Answer2,
                quizQuestionDTO.Answer3,
                quizQuestionDTO.Answer4
            };

            var quizQuestions = new QuizQuestion
            {
                Quiz = miniQuiz,
                Question = quizQuestionDTO.Question,
                Answers = answers,
                CorrectAnswerIndex = quizQuestionDTO.CorrectAnswerIndex
            };

            return questionsRepository.Insert(quizQuestions);
        }

        public MiniQuiz CreateMiniQuiz(MiniQuizDTO miniQuizDTO)
        {
            if (miniQuizDTO == null)
            {
                throw new ArgumentNullException(null, "MiniQuizDTO must not be null");
            }

            if (miniQuizDTO.PlantId == Guid.Empty)
            {
                throw new ArgumentException("Invalid id value, Guid must not be empty");
            }

            if (string.IsNullOrEmpty(miniQuizDTO.Title))
            {
                throw new ArgumentNullException(null, "Title must not be null or empty");
            }

            Plant plant = plantService.FetchPlantById(miniQuizDTO.PlantId);

            if (plant == null)
            {
                throw new ArgumentNullException(null, "Plant not found");
            }

            var miniQuiz = new MiniQuiz
            {
                PlantId = plant.Id,
                Plant = plant,
                Title = miniQuizDTO.Title
            };

            return miniQuizRepositoryGeneric.Insert(miniQuiz);
        }

        public MiniQuiz DeleteMiniQuiz(MiniQuiz miniQuiz)
        {
            return miniQuizRepositoryGeneric.Delete(miniQuiz);
        }

        public List<MiniQuiz> FetchAllMiniQuizes()
        {
            return miniQuizRepositoryGeneric.FetchAll().ToList();
        }

        public MiniQuiz FetchMiniQuizById(Guid id)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentException("Invalid id value, Guid must not be empty");
            }

            return miniQuizRepositoryGeneric.FetchById(id);
        }

        public MiniQuiz FetchMiniQuizByPlantId(Guid plantId)
        {
            if (plantId == Guid.Empty)
            {
                throw new ArgumentException("Invalid id value, Guid must not be empty");
            }

            return miniQuizRepository.FetchMiniQuizByPlantId(plantId);
        }

        public MiniQuiz UpdateMiniQuiz(MiniQuiz miniQuiz)
        {
            return miniQuizRepositoryGeneric.Update(miniQuiz);
        }
    }
}
