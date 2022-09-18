import { useContext } from "react";
import { useNavigate } from "react-router-dom";
import { QuizContext } from "../../providers/quizProvider";

function EndScreenPassed() {
    
    const navigate = useNavigate();
    const {miniQuiz, questionsLength, score} = useContext(QuizContext);

    const handlePlantRedirect = () => {
        navigate(`/plants/${miniQuiz.plant.id}`);
    }

    const handleMyProfileRedirect = () => {
        navigate('/my-profile');
    }

    return (
        <div className="container" style={{ marginTop: '100px' }}>
            <div className="row justify-content-center">
                <div className="col-md-8 p-4 rounded" style={{ backgroundColor: "#C9F0B0" }}>
                    <h1 className="text-center">🎉 ЧЕСТИТКИ! 🎉</h1>
                    <h3 className="text-center">Резултат: {score}/{questionsLength}</h3>
                    <p className="mt-5 fs-5 text-center">Успешно го положивте мини квизот на темa "<b>{miniQuiz.plant.name}</b>"!</p>
                    <p className="text-center">Како награда за вашиот труд, на вашиот профил е додаден "<b>{miniQuiz.plant.name}</b>" беџ.</p>
                    <div className="hstack mt-5 justify-content-center gap-3">
                        <button onClick={handlePlantRedirect} className="btn btn-danger">Излез</button>
                        <button onClick={handleMyProfileRedirect} className="btn btn-success">Мој профил</button>
                    </div>
                </div>
            </div>
        </div>
    );
}

export default EndScreenPassed;