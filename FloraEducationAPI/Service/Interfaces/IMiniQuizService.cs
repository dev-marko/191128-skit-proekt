using FloraEducationAPI.Domain.DTO;
using FloraEducationAPI.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FloraEducationAPI.Service.Interfaces
{
    public interface IMiniQuizService
    {
        MiniQuiz FetchMiniQuizById(Guid id);
        MiniQuiz FetchMiniQuizByPlantId(Guid plantId);
        List<MiniQuiz> FetchAllMiniQuizes();
        MiniQuiz CreateMiniQuiz(MiniQuizDTO miniQuizDTO);
        MiniQuiz UpdateMiniQuiz(MiniQuiz miniQuiz);
        MiniQuiz DeleteMiniQuiz(MiniQuiz miniQuiz);

        // QuizQuestions
        QuizQuestion AddQuestionToQuiz(QuizQuestionDTO quizQuestionDTO);
    }
}
