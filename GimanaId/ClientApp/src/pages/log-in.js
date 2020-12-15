import styled from "styled-components";
import { useRef } from "react";

import { logIn } from "../api/authentication";

import AuthCard, { FinePrint, AuthForm, FormTitle, AuthButton } from "../components/auth-card";
import LabeledInput from "../components/labeled-input";

const PageContainer = styled.div`
    min-height: 90vh;

    background: linear-gradient(45deg, #ab1691, #d81bb7); /* Subject to change */

    display: flex;
    flex-direction: column;
    align-items: center;
    justify-content: center;
`;

const LoginCard = () => {
    const refs = {
        username: useRef(null),
        password: useRef(null),
        rememberMe: useRef(null)
    };

    async function doLogin(ev) {
        ev.preventDefault();

        try {
            await logIn(
                refs.username.current.value,
                refs.password.current.value,
                refs.rememberMe.current.checked
            );
            window.location.reload();
        }
        catch (err) {
            console.error(err);
            alert("Error logging you in");
        }
    }
    
    return (
        <AuthCard>
            <FormTitle>Selamat datang kembali!<br />Silakan masuk.</FormTitle>
            <AuthForm onSubmit={doLogin}>
                <LabeledInput
                    name="username" title="Username" type="text" autoComplete="username" autoFocus
                    inputRef={refs.username}
                />
                <LabeledInput
                    name="password" title="Kata sandi" type="password" autoComplete="current-password"
                    inputRef={refs.password}
                />
                <label><input type="checkbox" ref={refs.rememberMe} />Masuk otomatis pada kunjungan berikutnya</label>
                <AuthButton backgroundColor="#23CC20" onClick={e => { /*setSubmitAttempt(true)*/ }}>Masuk</AuthButton>
                <FinePrint>Lupa kata sandi? <a href="#">Klik di sini</a>.</FinePrint>
            </AuthForm>
        </AuthCard>
    );
}

const LoginPage = () => (
    <PageContainer>
        <LoginCard />
    </PageContainer>
);

export default LoginPage;