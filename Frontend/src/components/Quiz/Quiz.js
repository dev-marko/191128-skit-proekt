import { useEffect, useState } from "react";
import { useNavigate, useParams } from "react-router-dom";
import FloraService from "../../repository/floraEducationRepository";
import QuizQuestions from "./QuizQuestions";
import EndScreenPassed from "./EndScreenPassed";
import EndScreenFailed from "./EndScreenFailed";
import { QuizContext } from "../../providers/quizProvider";

function Quiz() {

    const params = useParams();
    const plantId = params.plantId;
    const navigate = useNavigate();
    const username = localStorage.getItem('username');
    const [miniQuiz, setMiniQuiz] = useState();
    const [gameState, setGameState] = useState("quiz");
    const [score, setScore] = useState(0);
    const [questionsLength, setQuestionsLength] = useState(0);
    const [loading, setLoading] = useState(true);

    useEffect(() => {
        if (username === null) {
            navigate('/login');
            return;
        }
        FloraService.fetchMiniQuizByPlantId(plantId)
            .then((data) => {
                setMiniQuiz(data.data);
                setLoading(false);
            })

    }, []);

    useEffect(() => {
        if (gameState === "endScreenPassed") {
            FloraService.addBadge(username, miniQuiz.plant.name);
        }
    }, [gameState]);

    return (
        <div>
            {
                loading ?
                (
                    <div className="row justify-content-center">
                        <div class="spinner-border" role="status">
                            <span class="visually-hidden">Loading...</span>
                        </div>
                    </div>
                )
                :
                (
                    <QuizContext.Provider value={{ gameState, setGameState, score, setScore, questionsLength, setQuestionsLength, miniQuiz, setMiniQuiz}}>
                        {gameState === "quiz" && <QuizQuestions miniQuiz={miniQuiz}/>}
                        {gameState === "endScreenPassed" && <EndScreenPassed />}
                        {gameState === "endScreenFailed" && <EndScreenFailed />}
                        {gameState === "exitQuiz" && navigate(`/plants/${plantId}`)}
                    </QuizContext.Provider>
                )
            }
        </div>
    );
}

export default Quiz;