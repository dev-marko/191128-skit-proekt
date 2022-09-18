import { useEffect, useState } from "react";

function AboutUs() {

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
                                    <li class="breadcrumb-item active" aria-current="page">За нас</li>
                                </ol>
                            </nav>
                            <div className="row justify-content-center">
                                <div className="col-md-12 p-4 rounded" style={{ backgroundColor: "#C9F0B0" }}>
                                    <h1 className="text-center mb-5">За нас</h1>
                                    <hr />
                                    <div className="vstack gap-3">
                                        <p className="fs-5">
                                            Флора Едукација претставува македонски систем за изучување на домашно градинарство.
                                            Системот е изграден со цел да го надополни недостатокот на едукативна содржина на едно место, за садење и одржување на растенија на македонски јазик.
                                            Целна група на системот се сите возрасни групи кои имаат желба да научат основно земјоделие и имаат познавање од македонски јазик.
                                            Двете главни видови на информации кои ги нуди системот се: садење и одржување. Системот е изграден врз принципи на едноставност и брзина.
                                            Секој корисник со само неколку чекори би требало да пристапи до било кое растение и да ги има посакуваните информации.
                                        </p>
                                        <p className="fs-5">
                                            Моментално системот е само <b>прототип</b> изработен од страна на Марко Спасеновски за целите
                                            на предметот "Дизајн на интеракцијата човек-компјутер". Сепак, постои желба истиов систем во иднина
                                            да се доработи и да стане реален продукт кој ќе биде бесплатно достапен (open-source) за сите.
                                        </p>
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

export default AboutUs;