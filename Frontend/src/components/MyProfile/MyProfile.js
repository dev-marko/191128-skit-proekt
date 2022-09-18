import { useEffect, useState } from "react";
import FloraService from "../../repository/floraEducationRepository";

function MyProfile() {

    const [user, setUser] = useState();
    const [loaded, setLoaded] = useState(false);

    useEffect(() => {
        FloraService.fetchUser(localStorage.getItem('username'))
            .then((data) => {
                setUser(data.data);
                setLoaded(true);
            });
    }, []);

    console.log(user);

    return (
        <div>
            {
                loaded ?
                    (
                        <div className="container" style={{ marginTop: '100px', paddingBottom: '35px' }}>
                            <nav aria-label="breadcrumb">
                                <ol class="breadcrumb">
                                    <li class="breadcrumb-item"><a className='text-dark' style={{ textDecoration: "none" }} href="/home">Дома</a></li>
                                    <li class="breadcrumb-item active" aria-current="page">Мој профил</li>
                                </ol>
                            </nav>
                            <div className="row justify-content-center">
                                <div className="col-md-4 p-4 rounded" style={{ backgroundColor: "#C9F0B0" }}>
                                    <h2 className="text-center mb-5">Mој профил</h2>
                                    <p><b>Е-пошта</b>: {user.email}</p>
                                    <p><b>Корисничко име</b>: {user.username}</p>
                                    <p><b>Име</b>: {user.name}</p>
                                    <p><b>Презиме</b>: {user.surname}</p>
                                    <p className="m-0"><b>Беџови</b>:</p>
                                    <ul>
                                        {
                                            user.badges.map((term) => {
                                                return (
                                                    <li>{term.badge.name}</li>
                                                );
                                            })
                                        }
                                    </ul>
                                    <p className="m-0"><b>Омилени растенија:</b></p>
                                    <ul>
                                        {
                                            user.likedPlants.map((term) => {
                                                return (
                                                    <li>{term.plant.name}</li>
                                                );
                                            })
                                        }
                                    </ul>
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

export default MyProfile;