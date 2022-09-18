import { useContext, useState } from "react";
import { useNavigate } from "react-router-dom";
import { UserContext } from "../../providers/userProvider";
import FloraService from "../../repository/floraEducationRepository";

function LoginForm() {

    const [username, setUsername] = useState();
    const [password, setPassword] = useState();
    const [loading, setLoading] = useState(false);
    const navigate = useNavigate();

    const { setIsLoggedIn } = useContext(UserContext);

    const handleLogin = async (event) => {
        event.preventDefault();
        setLoading(true);
        const result = await FloraService.login(username, password);

        if (result === undefined) {
            localStorage.setItem('username', undefined);
            setIsLoggedIn(false);
            setLoading(false);
            navigate("/login");
        } else {
            localStorage.setItem('username', username);
            setIsLoggedIn(true);
            setLoading(false);
            navigate("/home");
        }
    }

    return (
        <div className="container" style={{ marginTop: '100px' }}>
            <nav aria-label="breadcrumb">
                <ol class="breadcrumb">
                    <li class="breadcrumb-item"><a className='text-dark' style={{ textDecoration: "none" }} href="/home">Дома</a></li>
                    <li class="breadcrumb-item active" aria-current="page">Најава</li>
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
                            <form className="col-lg-4 p-5 rounded shadow-sm" style={{ backgroundColor: "#C9F0B0" }} onSubmit={handleLogin}>
                                <div class="mb-3">
                                    <label for="username" class="form-label">Корисничко име</label>
                                    <input autoComplete="off" type="text" class="form-control" id="username" required onChange={(event) => setUsername(event.target.value)} />
                                </div>
                                <div class="mb-3">
                                    <label for="password" class="form-label">Лозинка</label>
                                    <input autoComplete="off" type="password" class="form-control" id="password" required onChange={(event) => setPassword(event.target.value)} />
                                </div>
                                {
                                    localStorage.getItem('username') === "undefined" ? <div className="mb-2"><small className="text-danger"> Погрешно корисничко име или лозинка. Обидете се повторно.</small></div> : null
                                }
                                <button type="submit" class="btn btn-success" id="login-button" data-test="login-button">Најава</button>
                            </form>
                        </div>
                    )
            }
        </div>
    );
}

export default LoginForm;