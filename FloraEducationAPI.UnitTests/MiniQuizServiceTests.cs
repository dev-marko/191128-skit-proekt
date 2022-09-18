using FloraEducationAPI.Domain.DTO;
using FloraEducationAPI.Domain.Models;
using FloraEducationAPI.Repository.Interfaces;
using FloraEducationAPI.Service.Implementations;
using FloraEducationAPI.Service.Interfaces;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace FloraEducationAPI.UnitTests
{
    [TestFixture]
    public class MiniQuizServiceTests
    {
        private Mock<IRepository<MiniQuiz>> mockMiniQuizGenericRepository;
        private Mock<IRepository<QuizQuestion>> mockQuizQuestionRepository;
        private Mock<IPlantService> mockPlantService;
        private Mock<IMiniQuizRepository> mockMiniQuizRepository;
        private IMiniQuizService miniQuizService;

        [OneTimeSetUp]
        public void Setup()
        {
            mockMiniQuizGenericRepository = new Mock<IRepository<MiniQuiz>>(MockBehavior.Loose);
            mockQuizQuestionRepository = new Mock<IRepository<QuizQuestion>>(MockBehavior.Loose);
            mockPlantService = new Mock<IPlantService>(MockBehavior.Loose);
            mockMiniQuizRepository = new Mock<IMiniQuizRepository>(MockBehavior.Loose);
            miniQuizService = new MiniQuizService(mockMiniQuizGenericRepository.Object, mockQuizQuestionRepository.Object, mockMiniQuizRepository.Object, mockPlantService.Object);
        }

        [Test]
        public void FetchAllMiniQuizes_MiniQuizFound_ReturnList()
        {
            IEnumerable<MiniQuiz> miniQuizes = new List<MiniQuiz> {
                new MiniQuiz
                {
                    Id = Guid.NewGuid()
                }
            };

            mockMiniQuizGenericRepository.Setup(m => m.FetchAll()).Returns(miniQuizes);

            var actual = miniQuizService.FetchAllMiniQuizes();

            Assert.IsNotEmpty(actual);
        }

        [Test]
        public void FetchAllMiniQuizes_MiniQuizNotFound_ReturnEmptyList()
        {
            IEnumerable<MiniQuiz> miniQuizes = new List<MiniQuiz> {};

            mockMiniQuizGenericRepository.Setup(m => m.FetchAll()).Returns(miniQuizes);

            var actual = miniQuizService.FetchAllMiniQuizes();

            Assert.IsEmpty(actual);
        }

        [Test]
        public void FetchMiniQuizById_IdIsEmpty_ReturnArgumentException()
        {
            Guid id = Guid.Empty;
            var ex = Assert.Throws<ArgumentException>(() => miniQuizService.FetchMiniQuizById(id));
            Assert.That(ex.Message, Is.EqualTo("Invalid id value, Guid must not be empty"));
        }

        [Test]
        public void FetchMiniQuizByPlantId_IdIsEmpty_ReturnArgumentException()
        {
            Guid plantId = Guid.Empty;
            var ex = Assert.Throws<ArgumentException>(() => miniQuizService.FetchMiniQuizByPlantId(plantId));
            Assert.That(ex.Message, Is.EqualTo("Invalid id value, Guid must not be empty"));
        }

        [Test]
        public void CreateMiniQuiz_MiniQuizDTOIsNull_ReturnArgumentNullException()
        {
            MiniQuizDTO miniQuizDTO = null;
            var ex = Assert.Throws<ArgumentNullException>(() => miniQuizService.CreateMiniQuiz(miniQuizDTO));
            Assert.That(ex.Message, Is.EqualTo("MiniQuizDTO must not be null"));
        }

        [Test]
        public void CreateMiniQuiz_PlantIdIsEmpty_ReturnArgumentException()
        {
            MiniQuizDTO miniQuizDTO = new MiniQuizDTO 
            {
                PlantId = Guid.Empty,
                Title = null
            };

            var ex = Assert.Throws<ArgumentException>(() => miniQuizService.CreateMiniQuiz(miniQuizDTO));
            Assert.That(ex.Message, Is.EqualTo("Invalid id value, Guid must not be empty"));
        }

        [Test]
        public void CreateMiniQuiz_TitleIsNull_ReturnArgumentNullException()
        {
            MiniQuizDTO miniQuizDTO = new MiniQuizDTO
            {
                PlantId = Guid.NewGuid(),
                Title = null
            };

            var ex = Assert .Throws<ArgumentNullException>(() => miniQuizService.CreateMiniQuiz(miniQuizDTO));
            Assert.That(ex.Message, Is.EqualTo("Title must not be null or empty"));
        }

        [Test]
        public void CreateMiniQuiz_TitleIsEmpty_ReturnArgumentNullException()
        {
            MiniQuizDTO miniQuizDTO = new MiniQuizDTO
            {
                PlantId = Guid.NewGuid(),
                Title = string.Empty
            };

            var ex = Assert.Throws<ArgumentNullException>(() => miniQuizService.CreateMiniQuiz(miniQuizDTO));
            Assert.That(ex.Message, Is.EqualTo("Title must not be null or empty"));
        }

        [Test]
        public void CreateMiniQuiz_PlantNotFound_ReturnArgumentNullException()
        {
            MiniQuizDTO miniQuizDTO = new MiniQuizDTO
            {
                PlantId = Guid.NewGuid(),
                Title = "title"
            };

            mockPlantService.Setup(m => m.FetchPlantById(It.IsAny<Guid>())).Returns((Plant) null);

            var ex = Assert.Throws<ArgumentNullException>(() => miniQuizService.CreateMiniQuiz(miniQuizDTO));
            Assert.That(ex.Message, Is.EqualTo("Plant not found"));
        }

        [Test]
        public void CreateMiniQuiz_Success_ReturnMiniQuizEntity()
        {
            var plantId = Guid.NewGuid();

            MiniQuizDTO miniQuizDTO = new MiniQuizDTO
            {
                PlantId = plantId,
                Title = "title"
            };

            var plant = new Plant
            {
                Id = plantId
            };

            var miniQuiz = new MiniQuiz
            {
                Id = Guid.NewGuid(),
                PlantId = plantId,
                Plant = plant,
                Title = "title"
            };

            mockPlantService.Setup(m => m.FetchPlantById(plantId))
                .Returns(plant);

            mockMiniQuizGenericRepository.Setup(m => m.Insert(It.IsAny<MiniQuiz>()))
                .Returns(miniQuiz);

            var actual = miniQuizService.CreateMiniQuiz(miniQuizDTO);
            var expected = miniQuiz;

            Assert.AreEqual(expected.Id, actual.Id);
            Assert.AreEqual(expected.PlantId, actual.PlantId);
            Assert.AreEqual(expected.Plant, actual.Plant);
            Assert.AreEqual(expected.Title, actual.Title);
        }


        [Test]
        public void UpdateMiniQuiz_MiniQuizIsNull_ReturnArgumentNullException()
        {
            MiniQuiz miniQuiz = null;

            mockMiniQuizGenericRepository.Setup(m => m.Update(miniQuiz)).Throws<ArgumentNullException>();

            Assert.Throws<ArgumentNullException>(() => miniQuizService.UpdateMiniQuiz(miniQuiz));
        }

        [Test]
        public void UpdateMiniQuiz_MiniQuizIsNotNull_ReturnMiniQuizEntity()
        {
            MiniQuiz miniQuiz = new MiniQuiz { };

            mockMiniQuizGenericRepository.Setup(m => m.Update(miniQuiz)).Returns(miniQuiz);

            var actual = miniQuizService.UpdateMiniQuiz(miniQuiz);

            Assert.IsNotNull(actual);
        }

        [Test]
        public void DeleteMiniQuiz_MiniQuizIsNull_ReturnArgumentNullException()
        {
            MiniQuiz miniQuiz = null;

            mockMiniQuizGenericRepository.Setup(m => m.Delete(miniQuiz)).Throws<ArgumentNullException>();

            Assert.Throws<ArgumentNullException>(() => miniQuizService.DeleteMiniQuiz(miniQuiz));
        }

        [Test]
        public void DeleteMiniQuiz_MiniQuizIsNotNull_ReturnMiniQuizEntity()
        {
            MiniQuiz miniQuiz = new MiniQuiz { };

            mockMiniQuizGenericRepository.Setup(m => m.Delete(miniQuiz)).Returns(miniQuiz);

            var actual = miniQuizService.DeleteMiniQuiz(miniQuiz);

            Assert.IsNotNull(actual);
        }

        [Test]
        public void AddQuestionToQuiz_QuizQuestionDTOIsNull_ReturnArgumentNullException()
        {
            QuizQuestionDTO quizQuestionDTO = null;
            var ex = Assert.Throws<ArgumentNullException>(() => miniQuizService.AddQuestionToQuiz(quizQuestionDTO));
            Assert.That(ex.Message, Is.EqualTo("QuizQuestionDTO must not be null"));
        }

        [Test]
        public void AddQuestionToQuiz_QuizIdIsEmpty_ReturnArgumentException()
        {
            QuizQuestionDTO quizQuestionDTO = new QuizQuestionDTO
            {
                QuizId = Guid.Empty
            };

            var ex = Assert.Throws<ArgumentException>(() => miniQuizService.AddQuestionToQuiz(quizQuestionDTO));
            Assert.That(ex.Message, Is.EqualTo("Invalid id value, Guid must not be empty"));
        }

        [Test]
        public void AddQuestionToQuiz_QuestionOrAnwersAllAreNull_ReturnArgumentNullException()
        {
            QuizQuestionDTO quizQuestionDTO = new QuizQuestionDTO
            {
                QuizId = Guid.NewGuid(),
                Question = null,
                Answer1 = null,
                Answer2 = null,
                Answer3 = null,
                Answer4 = null,
                CorrectAnswerIndex = 0
            };

            var ex = Assert.Throws<ArgumentNullException>(() => miniQuizService.AddQuestionToQuiz(quizQuestionDTO));
            Assert.That(ex.Message, Is.EqualTo("Question and answers must not be null or empty"));
        }

        [Test]
        public void AddQuestionToQuiz_QuestionOrAnwersAllAreEmpty_ReturnArgumentNullException()
        {
            QuizQuestionDTO quizQuestionDTO = new QuizQuestionDTO
            {
                QuizId = Guid.NewGuid(),
                Question = string.Empty,
                Answer1 = string.Empty,
                Answer2 = string.Empty,
                Answer3 = string.Empty,
                Answer4 = string.Empty,
                CorrectAnswerIndex = 0
            };

            var ex = Assert.Throws<ArgumentNullException>(() => miniQuizService.AddQuestionToQuiz(quizQuestionDTO));
            Assert.That(ex.Message, Is.EqualTo("Question and answers must not be null or empty"));
        }

        [Test]
        public void AddQuestionToQuiz_QuestionOrAnwersAnyAreNull_ReturnArgumentNullException()
        {
            QuizQuestionDTO quizQuestionDTO = new QuizQuestionDTO
            {
                QuizId = Guid.NewGuid(),
                Question = "quesion",
                Answer1 = null,
                Answer2 = null,
                Answer3 = "answer3",
                Answer4 = null,
                CorrectAnswerIndex = 0
            };

            var ex = Assert.Throws<ArgumentNullException>(() => miniQuizService.AddQuestionToQuiz(quizQuestionDTO));
            Assert.That(ex.Message, Is.EqualTo("Question and answers must not be null or empty"));
        }

        [Test]
        public void AddQuestionToQuiz_QuestionOrAnwersAnyAreEmpty_ReturnArgumentNullException()
        {
            QuizQuestionDTO quizQuestionDTO = new QuizQuestionDTO
            {
                QuizId = Guid.NewGuid(),
                Question = "question",
                Answer1 = string.Empty,
                Answer2 = string.Empty,
                Answer3 = "answer3",
                Answer4 = string.Empty,
                CorrectAnswerIndex = 0
            };

            var ex = Assert.Throws<ArgumentNullException>(() => miniQuizService.AddQuestionToQuiz(quizQuestionDTO));
            Assert.That(ex.Message, Is.EqualTo("Question and answers must not be null or empty"));
        }

        [Test]
        public void AddQuestionToQuiz_CorrectAnswerIndexIsGreaterThan3_ReturnInvalidOperationException()
        {
            QuizQuestionDTO quizQuestionDTO = new QuizQuestionDTO
            {
                QuizId = Guid.NewGuid(),
                Question = "question",
                Answer1 = "bla",
                Answer2 = "bla",
                Answer3 = "bla",
                Answer4 = "bla",
                CorrectAnswerIndex = 4
            };

            var ex = Assert.Throws<InvalidOperationException>(() => miniQuizService.AddQuestionToQuiz(quizQuestionDTO));
            Assert.That(ex.Message, Is.EqualTo("CorrectAnswerIndex must not be greater than 3 or less than 0"));
        }

        [Test]
        public void AddQuestionToQuiz_CorrectAnswerIndexIsLessThan0_ReturnInvalidOperationException()
        {
            QuizQuestionDTO quizQuestionDTO = new QuizQuestionDTO
            {
                QuizId = Guid.NewGuid(),
                Question = "question",
                Answer1 = "bla",
                Answer2 = "bla",
                Answer3 = "bla",
                Answer4 = "bla",
                CorrectAnswerIndex = -1
            };

            var ex = Assert.Throws<InvalidOperationException>(() => miniQuizService.AddQuestionToQuiz(quizQuestionDTO));
            Assert.That(ex.Message, Is.EqualTo("CorrectAnswerIndex must not be greater than 3 or less than 0"));
        }

        [Test]
        public void AddQuestionToQuiz_MiniQuizNotFound_ReturnArgumentNullException()
        {
            QuizQuestionDTO quizQuestionDTO = new QuizQuestionDTO
            {
                QuizId = Guid.NewGuid(),
                Question = "question",
                Answer1 = "bla",
                Answer2 = "bla",
                Answer3 = "bla",
                Answer4 = "bla",
                CorrectAnswerIndex = 0
            };

            mockMiniQuizGenericRepository.Setup(m => m.FetchById(It.IsAny<Guid>())).Returns((MiniQuiz) null);

            var ex = Assert.Throws<ArgumentNullException>(() => miniQuizService.AddQuestionToQuiz(quizQuestionDTO));
            Assert.That(ex.Message, Is.EqualTo("Mini Quiz not found"));
        }

        [Test]
        public void AddQuestionToQuiz_MiniQuizFound_ReturnQuizQuestionEntity()
        {
            var quizId = Guid.NewGuid();

            QuizQuestionDTO quizQuestionDTO = new QuizQuestionDTO
            {
                QuizId = quizId,
                Question = "question",
                Answer1 = "bla",
                Answer2 = "bla",
                Answer3 = "bla",
                Answer4 = "bla",
                CorrectAnswerIndex = 0
            };

            var miniQuiz = new MiniQuiz
            {
                Id = quizId
            };

            List<string> answers = new List<string>()
            {
                quizQuestionDTO.Answer1,
                quizQuestionDTO.Answer2,
                quizQuestionDTO.Answer3,
                quizQuestionDTO.Answer4,
            };

            var quizQuestion = new QuizQuestion
            {
                Id = Guid.NewGuid(),
                Quiz = miniQuiz,
                Question = quizQuestionDTO.Question,
                Answers = answers,
                CorrectAnswerIndex = quizQuestionDTO.CorrectAnswerIndex
            };

            mockMiniQuizGenericRepository.Setup(m => m.FetchById(quizId))
                .Returns(miniQuiz);

            mockQuizQuestionRepository.Setup(m => m.Insert(It.IsAny<QuizQuestion>()))
                .Returns(quizQuestion);

            var actual = miniQuizService.AddQuestionToQuiz(quizQuestionDTO);
            var expected = quizQuestion;

            Assert.AreEqual(expected.Id, actual.Id);
            Assert.AreEqual(expected.Quiz.Id, actual.Quiz.Id);
            Assert.AreEqual(expected.Question, actual.Question);
            Assert.AreEqual(expected.Answers, actual.Answers);
            Assert.AreEqual(expected.CorrectAnswerIndex, actual.CorrectAnswerIndex);
        }
    }
}
