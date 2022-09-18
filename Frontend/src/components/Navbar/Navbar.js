import { useContext } from "react";
import { useNavigate } from "react-router-dom";
import { UserContext } from "../../providers/userProvider";

function Navbar() {

    const { isLoggedIn, setIsLoggedIn } = useContext(UserContext);
    const navigate = useNavigate();

    const logout = (event) => {
        event.preventDefault();
        window.localStorage.removeItem('username');
        setIsLoggedIn(false);
        navigate('/home');
    }

    return (
        <nav class="navbar navbar-expand-lg fixed-top" style={{ backgroundColor: "#92D866" }}>
            <div class="container-fluid">
                <a className="navbar-brand fs-4" href="/home">Флора Едукација</a>
                <button class="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbar-toggler" aria-controls="navbarTogglerDemo02" aria-expanded="false" aria-label="Toggle navigation">
                    <span class="navbar-toggler-icon"></span>
                </button>
                <div class="collapse ms-5 navbar-collapse" id="navbar-toggler">
                    <ul class=" navbar-nav text-dark fw-bold me-auto mb-2 mb-lg-0">
                        <li className="nav-item">
                            <a className="nav-link" aria-current="page" href="/home">Дома</a>
                        </li>
                        <li className="nav-item">
                            <a className="nav-link" href="/plant-categories">Сите растенија</a>
                        </li>
                        <li className="nav-item">
                            <a className="nav-link" href="/about-us">За нас</a>
                        </li>
                        <li className="nav-item">
                            <a className="nav-link" href="/contact">Контакт</a>
                        </li>
                        <li className="nav-item">
                            <a className="nav-link" href="/documentation">Упатство</a>
                        </li>
                        {
                            isLoggedIn ?
                                (
                                    <>
                                        <li className="nav-item">
                                            <a className="nav-link" href="/my-profile">Мој профил</a>
                                        </li>
                                        <li className="nav-item">
                                            <a className="nav-link" href="#" role="button" onClick={logout}>Одјава</a>
                                        </li>
                                    </>
                                )
                                :
                                (
                                    <>
                                        <li className="nav-item">
                                            <a className="nav-link" href="/login">Најава</a>
                                        </li>
                                        <li className="nav-item">
                                            <a className="nav-link" href="/register">Регистрација</a>
                                        </li>
                                    </>
                                )
                        }
                    </ul>
                </div>
            </div>
        </nav>
    );
}

export default Navbar;