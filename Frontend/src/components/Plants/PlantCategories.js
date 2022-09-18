import { useEffect, useState } from "react";
import PlantCategoryCard from "./PlantCategoryCard";
import FloraService from "../../repository/floraEducationRepository"
import { useNavigate } from "react-router-dom";

function PlantCategories() {

    const navigate = useNavigate();
    const [categories, setCategories] = useState([]);
    const [loading, setLoading] = useState(true);

    useEffect(() => {
        FloraService.fetchPlantCategories()
            .then((data) => {
                setCategories(data.data.categories);
                setLoading(false);
            })
    }, [])

    return (
        <div className="container" style={{ marginTop: '100px', paddingBottom: '35px' }}>
            <nav aria-label="breadcrumb">
                <ol class="breadcrumb">
                    <li class="breadcrumb-item"><a className='text-dark' style={{textDecoration: "none"}} href="/home">Дома</a></li>
                    <li class="breadcrumb-item active" aria-current="page">Сите растенија</li>
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
                                <button onClick={() => {navigate(`/home`)}} className="btn btn-success"><i class="bi bi-arrow-left-circle"></i> Назад</button>
                            </div>
                        </div>
                        <div className="row mt-5 justify-content-center text-center">
                            <h3>Категории</h3>
                            {
                                categories.map((term) => {
                                    const imageUri = `./images/categories/${term.toLowerCase()}.png`

                                    return (
                                        <PlantCategoryCard key={term} image={imageUri} term={term} />
                                    );
                                })
                            }
                        </div>
                    </>
                    )
            }
        </div>
    );


}

export default PlantCategories;