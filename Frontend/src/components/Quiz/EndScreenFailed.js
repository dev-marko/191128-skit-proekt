import { useContext } from "react";
import { useNavigate } from "react-router-dom";
import { QuizContext } from "../../providers/quizProvider";

function EndScreenFailed() {

    const navigate = useNavigate();
    const {miniQuiz, questionsLength, score, setScore, setGameState} = useContext(QuizContext);

    const handlePlantRedirect = () => {
        navigate(`/plants/${miniQuiz.plant.id}`);
    }

    const restartQuiz = () => {
        setScore(0);
        setGameState("quiz");
    }

    return (
        <div className="container" style={{ marginTop: '100px' }}>
            <div className="row justify-content-center">
                <div className="col-md-8 p-4 rounded" style={{ backgroundColor: "#C9F0B0" }}>
                    <h1 className="text-center">😔 НЕУСПЕШНО! 😔</h1>
                    <h3 className="text-center">Резултат: {score}/{questionsLength}</h3>
                    <p className="mt-5 fs-5 text-center">За жал, не постигнавте доволен број на поени за да го положите мини квизот на темa "<b>{miniQuiz.plant.name}</b>".</p>
                    <p className="text-center"><i>За да го положите квизот, потребно е да одговорите точно на барем половина од прашањата.</i></p>
                    <div className="hstack mt-5 justify-content-center gap-3">
                        <button onClick={handlePlantRedirect} className="btn btn-danger">Излез</button>
                        <button onClick={restartQuiz} className="btn btn-success">Обидете се повторно 💪</button>
                    </div>
                </div>
            </div>
        </div>
    );
}

export default EndScreenFailed;