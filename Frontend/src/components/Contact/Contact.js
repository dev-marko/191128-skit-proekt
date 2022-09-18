import { useEffect, useState } from "react";

function Contact() {

    const [loaded, setLoaded] = useState(false);

    useEffect(() => {
        setLoaded(true);
    }, []);

    return (
        <div>
            {
                loaded ?
                    (
                        <div className="container" style={{ marginTop: '100px', paddingBottom: '35px' }}>
                            <nav aria-label="breadcrumb">
                                <ol class="breadcrumb">
                                    <li class="breadcrumb-item"><a className='text-dark' style={{ textDecoration: "none" }} href="/home">Дома</a></li>
                                    <li class="breadcrumb-item active" aria-current="page">Контакт</li>
                                </ol>
                            </nav>
                            <div className="row justify-content-center">
                                <div className="col-md-4 p-4 rounded" style={{ backgroundColor: "#C9F0B0" }}>
                                    <h1 className="text-center mb-5">Контакт</h1>
                                    <hr />
                                    <div className="vstack">
                                        <div className="mb-3 text-center">
                                            <img className="border border-dark rounded-5"  style={{maxWidth: "250px"}} src={require("./images/marko.jpg")} alt="avatar"/>
                                        </div>
                                        <p className="fs-5">
                                            <b>Автор:</b> Марко Спасеновски
                                        </p>
                                        <p className="fs-5">
                                            <b>Факултет:</b> Факултет за информатички науки и компјутерско инженерство
                                        </p>
                                        <p className="fs-5">
                                            <b>Смер:</b> Софтверско инженерство и информациски системи
                                        </p>
                                        <p className="fs-5">
                                            <b>Број на индекс:</b> 191128
                                        </p>
                                        <div className="hstack justify-content-evenly">
                                            <div><a target="_blank" style={{color: "black"}} href="https://github.com/dev-marko"><i className="bi bi-github fs-1"></i></a></div>
                                            <div><a target="_blank" href="https://twitter.com/devmarko22"><i className="bi bi-twitter fs-1"></i></a></div>
                                            <div><a target="_blank" href="https://mspasenovski.hashnode.dev/"><img style={{maxWidth: "125px"}} src={require("./images/hashnode.png")} alt="hashnode"/></a></div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    )
                    :
                    (
                        <div className="spinner">

                        </div>
                    )
            }
        </div>
    );
}

export default Contact;