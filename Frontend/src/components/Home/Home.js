function Home() {
    return (
        <div className="container" style={{ marginTop: '100px', paddingBottom: '35px' }}>
            <div className="row rounded" style={{ backgroundColor: "#C9F0B0" }}>
                <div className="col-lg-6 col-md-6 p-4">
                    <img className="img-fluid" src={require('./images/lady-holding-plant.jpg')} alt="lady holding a plant" />
                </div>
                <div className="col-lg-6 col-md-6 p-4">
                    <h3 className='p-2 text-center'>
                        Добредојдовте на Флора Едукација!
                    </h3>
                    <hr />
                    <p className='mt-5 p-2 text-start fs-5'>
                        Флора Едукација претставува веб страница наменета
                        за изучување на домашно градинарство. Страницата
                        е наменета за сите возрасни групи и има цел да ги
                        подобри градинарските вештини на граѓаните во
                        Македонија!
                    </p>
                </div>
            </div>
            <div className='row my-4 gap-3 d-flex justify-content-between text-center'>
                <div className='col-lg-3 col-md-5 rounded' style={{ backgroundColor: "#C9F0B0" }}>
                    <img className='img-fluid w-50 p-2' src={require('./images/garden-showel.png')} alt="garden shovel" />
                    <p className='p-2'>Научете да садите голем број на разновидни растенија!</p>
                </div>
                <div className='col-lg-3 col-md-5 rounded' style={{ backgroundColor: "#C9F0B0" }}>
                    <img className='img-fluid w-50 p-2' src={require('./images/garden-sheers.png')} alt="garden sheers" />
                    <p className='p-2'>Совети за одржување на вашите насади!</p>
                </div>
                <div className='col-lg-3 col-md-5 rounded' style={{ backgroundColor: "#C9F0B0" }}>
                    <img className='img-fluid w-50 p-2' src={require('./images/handshake.png')} alt="handshake" />
                    <p className='p-2'>Комуницирајте со други корисници и решавајте ги проблемите заедно!</p>
                </div>
            </div>
        </div>
    );
}

export default Home;