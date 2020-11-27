import { useState, useRef } from "react";

const AuthenticationExperimentPage = () => {
    const [signUpResult, setSignUpResult] = useState("Try signing up!");
    const [logInResult, setLogInResult] = useState("Try logging in!");
    const [token, setToken] = useState(null);

    const inputRefs = {
        signUp: {
            uname: useRef(null),
            email: useRef(null),
            pass: useRef(null)
        },
        logIn: {
            uname: useRef(null),
            pass: useRef(null)
        }
    }

    function signUp(u, p, m) {
        fetch("api/Auth/sign-up", {
            method: "POST",
            headers: {
                "accept": "application/json",
                "Content-Type": "application/json"
            },
            body: JSON.stringify({
                username: u,
                password: p,
                email: m
            })
        }).then(response => {
            setSignUpResult(response.status + ", " + response.statusText + ". View response details in console log (devtools)");
            console.log(response);
        });
    }

    function logIn(u, p) {
        fetch("api/Auth/login", {
            method: "POST",
            headers: {
                "accept": "text/plain",
                "Content-Type": "application/json"
            },
            body: JSON.stringify({
                username: u,
                password: p
            })
        }).then(response => {
            setLogInResult(response.status + ", " + response.statusText);
            console.log(response);
            return response.json();
        }).then(returnedData => {
            setToken(returnedData.token);
            console.log(returnedData);
        }, () => {
            setToken("log in error");
        });
    }

    return (
        <div>
            <div style={{ marginTop: `2rem` }}>
                <form>
                    <h1>Sign-up test</h1>
                    <p>Testing the sign-up API mechanism</p>

                    <label htmlFor="username">Username:</label>
                    <input name="username" type="text" autoComplete="username" autoFocus ref={inputRefs.signUp.uname} />

                    <label htmlFor="email">E-mail:</label>
                    <input name="email" type="text" ref={inputRefs.signUp.email} />

                    <label htmlFor="password">Passphrase:</label>
                    <input name="password" type="password" autoComplete="new-password" ref={inputRefs.signUp.pass} />

                    <button onClick={e => {
                        e.preventDefault();
                        signUp(
                            inputRefs.signUp.uname.current.value,
                            inputRefs.signUp.pass.current.value,
                            inputRefs.signUp.email.current.value
                        );
                    }}>Create account! アカウント作り！</button>
                </form>
                <p><b><i>{signUpResult}</i></b></p>
            </div>
            <div style={{ marginTop: `5rem` }}>
                <form>
                    <h1>Login test</h1>
                    <p>Just another experiment with the javascript fetch() API</p>

                    <label htmlFor="username">Username</label>
                    <input name="username" type="text" autoComplete="username" ref={inputRefs.logIn.uname} />

                    <label htmlFor="password">Password</label>
                    <input name="password" type="password" autoComplete="current-password" ref={inputRefs.logIn.pass} />

                    <button onClick={e => {
                        e.preventDefault();
                        logIn(
                            inputRefs.logIn.uname.current.value,
                            inputRefs.logIn.pass.current.value
                        );
                    }}>ログイン！</button>
                </form>
                <p><b><i>{logInResult}</i></b></p>
                <p><i>Token: {token || "(waiting for log in)"}</i></p>
            </div>
        </div>
    );
}

export default AuthenticationExperimentPage;