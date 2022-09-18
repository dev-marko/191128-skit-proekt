import { useNavigate, useSearchParams } from 'react-router-dom'
import { useState, useEffect } from 'react';
import FloraService from '../../repository/floraEducationRepository';
import PlantCard from './PlantCard';

function Plants() {
    const navigate = useNavigate();
    const [searchParams] = useSearchParams();
    const category = searchParams.get('query');

    const [plants, setPlants] = useState([]);
    const [loading, setLoading] = useState(true);

    useEffect(() => {
        FloraService.fetchPlantsByCategory(category)
            .then((data) => {
                setPlants(data.data);
                setLoading(false);
            })
    }, [category]);

    return (
        <div className="container" style={{ marginTop: '100px' }}>
            <nav aria-label="breadcrumb">
                <ol class="breadcrumb">
                    <li class="breadcrumb-item"><a className='text-dark' style={{textDecoration: "none"}} href="/home">Дома</a></li>
                    <li class="breadcrumb-item"><a className='text-dark' style={{textDecoration: "none"}} href="/plant-categories">Сите растенија</a></li>
                    <li class="breadcrumb-item active" aria-current="page">{category}</li>
                </ol>
            </nav>
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
                        <div className="row">
                            <div className="col-lg-12 col-md-5 ps-3 pt-3 m-0">
                                <button onClick={() => {navigate(`/plant-categories`)}} className="btn btn-success"><i class="bi bi-arrow-left-circle"></i> Назад</button>
                            </div>
                        </div>
                        <div className="row mt-3 justify-content-center text-center">
                            <h2>{category}</h2>
                            {
                                category !== "Цвеќиња" ?
                                    (
                                        <h3 className='col mt-5 text-center'>
                                            😳 Овој дел не е имплементиран во овој прототип. Пренасочете се кон делот за "<a href='/plants?query=Цвеќиња'>Цвеќиња</a>".
                                        </h3>
                                    )
                                    :
                                    (
                                        plants.map((term) => {

                                            const imageUri = `./images/plants/${term.name.toLowerCase()}.png`

                                            return (
                                                <PlantCard key={term.id} image={imageUri} term={term} />
                                            );
                                        })
                                    )
                            }
                        </div>
                    </>
                    )
            }
        </div>
    );
}

export default Plants;