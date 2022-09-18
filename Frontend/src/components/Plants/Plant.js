import { useEffect, useState } from "react";
import { useNavigate, useParams } from "react-router-dom";
import FloraService from "../../repository/floraEducationRepository";

function Plant() {

    const params = useParams();
    const navigate = useNavigate();
    const plantId = params.plantId;
    const [plant, setPlant] = useState([]);
    const [loading, setLoading] = useState(true);
    const [text, setText] = useState();
    const [comment, setComment] = useState();
    const username = localStorage.getItem('username');

    useEffect(() => {
        FloraService.fetchPlantById(plantId)
            .then((data) => {
                setPlant(data.data);
                setText(data.data.description);
                setLoading(false);
            })
    }, [plantId])

    const like = async () => {
        if (username === null) {
            navigate('/login');
            return;
        }

        await FloraService.likePlant(username, plantId);

        const alertPlaceholder = document.getElementById('liveAlertPlaceholder');

        const alert = (message) => {
            const wrapper = document.createElement('div')
            wrapper.innerHTML = [
                `<div class="alert alert-success alert-dismissible" role="alert">`,
                `   <div>${message}</div>`,
                '   <button type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Close"></button>',
                '</div>'
            ].join('')

            alertPlaceholder.append(wrapper)
        }

        alert('<i class="bi bi-check-circle"></i> Успешно додадено во омилени растенија.');
    }

    const handleComment = async () => {
        if (username === null) {
            navigate('/login');
            return;
        }

        await FloraService.addComment(plantId, username, comment);
    }

    return (
        <div className="container" style={{ marginTop: '100px', paddingBottom: '35px' }}>
            <nav aria-label="breadcrumb">
                <ol class="breadcrumb">
                    <li class="breadcrumb-item"><a className='text-dark' style={{ textDecoration: "none" }} href="/home">Дома</a></li>
                    <li class="breadcrumb-item"><a className='text-dark' style={{ textDecoration: "none" }} href="/plant-categories">Сите растенија</a></li>
                    <li class="breadcrumb-item"><a className='text-dark' style={{ textDecoration: "none" }} href={`/plants?query=${plant.type}`}>{plant.type}</a></li>
                    <li class="breadcrumb-item active" aria-current="page">{plant.name}</li>
                </ol>
            </nav>
            <div id="liveAlertPlaceholder"></div>
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
                        <>
                            <div className="row rounded-top" style={{ backgroundColor: "#C9F0B0" }}>
                                <div className="col-lg-12 col-md-5 ps-3 pt-3 m-0">
                                    <button onClick={() => {navigate(`/plants?query=${plant.type}`)}} className="btn btn-success"><i class="bi bi-arrow-left-circle"></i> Назад</button>
                                </div>
                            </div>
                            <div className="row rounded-bottom" style={{ backgroundColor: "#C9F0B0" }}>
                                <div className="col-lg-6 col-md-6 px-4 pb-4">
                                    <h2 className='p-2 text-center'>
                                        {plant.name}
                                    </h2>
                                    <hr />
                                    <img className="img-fluid d-block mx-auto rounded w-75" src={require(`./images/plants/${plant.id}.jpg`)} alt="{plant.name}" />
                                    <div class="d-flex pt-2 hstack gap-3 justify-content-center flex-xl-row flex-lg-column flex-md-column flex-sm-column flex-column">
                                        <button onClick={() => like()} type="button" class="btn btn-danger">
                                            <i class="bi bi-heart"></i> Ми се допаѓа
                                        </button>
                                        <button onClick={() => { setText(plant.description) }} className="btn btn-success">Опис</button>
                                        <button onClick={() => { setText(plant.predispositions) }} className="btn btn-success">Предуслови</button>
                                        <button onClick={() => { setText(plant.planting) }} className="btn btn-success">Садење</button>
                                        <button onClick={() => { setText(plant.maintenance) }} className="btn btn-success">Одржување</button>
                                    </div>
                                </div>
                                <div className="col-lg-6 col-md-6 px-4 pb-4">
                                    <h2 className='p-2 text-center'>
                                        Краток опис
                                    </h2>
                                    <hr />
                                    <p className='p-2 text-start fs-5'>
                                        {text}
                                    </p>
                                </div>
                            </div>
                            <div className="row rounded mt-4" style={{ backgroundColor: "#C9F0B0" }}>
                                <div className="col-lg-12 col-md-5 p-4 justify-content-center">
                                    <div className="hstack justify-content-center gap-3">
                                        <p className="fs-5 m-0">
                                            Тестирајте го вашето знаење на темата со краток квиз!
                                        </p>
                                        <a className="btn btn-info" href={`/mini-quiz/${plant.id}`}>Мини-квиз <i class="bi bi-patch-question"></i></a>
                                    </div>
                                </div>
                            </div>
                            <div className="row rounded mt-4" style={{ backgroundColor: "#C9F0B0" }}>
                                <div className="col-lg-12 col-md-5 p-4 justify-content-center">
                                    <h2><i class="bi bi-chat-square-text"></i> Коментари:</h2>
                                    {
                                        plant.comments.map((term) => {
                                            return (
                                                <div className="m-3 p-4 rounded-pill bg-light">
                                                    <p className="p-3 m-0">{term.author.username} коментирал:</p>
                                                    <hr className="m-0" />
                                                    <p className="p-3 m-0">{term.content}</p>
                                                </div>
                                            );
                                        })
                                    }
                                </div>
                            </div>
                            <div className="row rounded mt-4" style={{ backgroundColor: "#C9F0B0", marginBottom: "50px" }}>
                                <div className="col-lg-12 col-md-5 p-4">
                                    <form className="hstack justify-content-center gap-3" onSubmit={handleComment}>
                                        <div class="form-floating w-75">
                                            <textarea class="form-control" placeholder="Напишете коментар..." id="comment" required onChange={(event) => setComment(event.target.value)}></textarea>
                                        </div>
                                        <button type="submit" className="btn btn-primary">Коментирај <i class="bi bi-send"></i></button>
                                    </form>
                                </div>
                            </div>
                        </>
                    )
            }
        </div>
    );
}

export default Plant;