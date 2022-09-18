import { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import FloraService from "../../repository/floraEducationRepository";

function RegisterForm() {

    const [email, setEmail] = useState();
    const [username, setUsername] = useState();
    const [password, setPassword] = useState();
    const [name, setName] = useState();
    const [surname, setSurname] = useState();
    const [loading, setLoading] = useState(true);
    const navigate = useNavigate();


    const handleRegister = async (event) => {
        event.preventDefault();
        setLoading(true);
        await FloraService.register(email, username, password, name, surname);
        setLoading(false);
        navigate("/login");
    }

    useEffect(() => {
        setLoading(false);
    }, [])

    return (
        <div className="container" style={{ marginTop: '100px' }}>
            <nav aria-label="breadcrumb">
                <ol class="breadcrumb">
                    <li class="breadcrumb-item"><a className='text-dark' style={{ textDecoration: "none" }} href="/home">Дома</a></li>
                    <li class="breadcrumb-item active" aria-current="page">Регистрација</li>
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
                        <div className="row justify-content-center">
                            <form className="col-lg-4 p-5 rounded shadow-sm" style={{ backgroundColor: "#C9F0B0" }} onSubmit={handleRegister}>
                                <div class="mb-3">
                                    <label for="email" class="form-label">Е-пошта</label>
                                    <input autoComplete="off" type="email" class="form-control" id="email" required onChange={(event) => setEmail(event.target.value)} />
                                </div>
                                <div class="mb-3">
                                    <label for="username" class="form-label">Корисничко име</label>
                                    <input autoComplete="off" type="text" class="form-control" id="username" required onChange={(event) => setUsername(event.target.value)} />
                                </div>
                                <div class="mb-3">
                                    <label for="password" class="form-label">Лозинка</label>
                                    <input autoComplete="off" type="password" class="form-control" id="password" required onChange={(event) => setPassword(event.target.value)} />
                                </div>
                                <div class="mb-3">
                                    <label for="name" class="form-label">Име</label>
                                    <input autoComplete="off" type="text" class="form-control" id="name" required onChange={(event) => setName(event.target.value)} />
                                </div>
                                <div class="mb-3">
                                    <label for="surname" class="form-label">Презиме</label>
                                    <input autoComplete="off" type="text" class="form-control" id="surname" required onChange={(event) => setSurname(event.target.value)} />
                                </div>
                                <div class="mb-3 text-danger">
                                    <small>*Сите полиња се задолжителни.</small>
                                </div>
                                <button type="submit" class="btn btn-success">Регистрација</button>
                            </form>
                        </div>
                    )
            }
        </div>
    );
}

export default RegisterForm;