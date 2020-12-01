import styled from "styled-components";
import { Link } from "react-router-dom";
import { useState, useRef } from "react";

// The imported components starting with lowercase are going to be re-styled later
import stockCard from "./card";
import stockButton from "./button";

import LabeledInput from "./labeled-input";

const Card = styled(stockCard)`
    width: 25rem;
    padding: 2em;
    margin: 2.5rem 3.75rem;

    box-shadow: 0 0.5rem 1rem rgb(0 0 0 / 0.25);

    position: relative;
    z-index: 1;

    &::before {
        content: "";
        position: absolute;
        z-index: -1;
        width: 32.5rem;
        height: 100%;
        top: 8%;
        left: 50%;
        transform: translateX(-50%);
        background-color: rgb(255 255 255 / 0.2);
    }
`;

const MainForm = styled.form`
    display: flex;
    flex-direction: column;
    align-items: center;
`;

const FormTitle = styled.h1`
    text-align: center;
    font-size: 1.5em;

    margin: 0;
    margin-bottom: 0.25em;

    /* FIXME */
    font-weight: normal;
    &::first-line {
        font-weight: bold;
    }
`;

const AdditionalText = styled.p`
    text-align: center;
    font-size: 0.9em;

    margin: 0;
    margin-bottom: 2em;
`;

const Button = styled(stockButton)`
    margin-top: 1em;
`;

const AuthCard = ({ mode }) => {
    const refs = {
        username: useRef(null),
        password: useRef(null),
        email: useRef(null)
    };

    const callAuth = (apiUrl, jsonBody) => fetch(apiUrl, {
        method: "POST",
        headers: {
            "accept": "application/json",
            "Content-Type": "application/json"
        },
        body: JSON.stringify(jsonBody)
    });

    function handleClick(e, functionToExecute) {
        e.preventDefault();
        functionToExecute();
    }

    // doLogIn
    function logIn(e) {
        e.preventDefault();

        callAuth("api/Auth/login", {
            username: refs.username.current.value,
            password: refs.password.current.value
        }).then(response => response.json()).then(data => {
            localStorage.setItem("authToken", data.token);
        }, () => {
            alert("Login failed!");
        });
    }

    if (mode === "signup") {
        return (
            <Card>
                <FormTitle>Buat akun baru</FormTitle>
                <AdditionalText>Sudah pernah mendaftar? <Link to="/masuk">Klik di sini untuk masuk</Link>.</AdditionalText>
                <MainForm>
                    <LabeledInput name="email" title="Alamat e-mail" type="email" autoFocus inputRef={refs.email} />
                    <LabeledInput name="username" title="Username" type="text" autoComplete="username" inputRef={refs.username}/>
                    <LabeledInput name="password" title="Kata sandi" type="password" autoComplete="new-password" inputRef={refs.password} />
                    <LabeledInput name="repeat-password" title="Ulangi kata sandi" type="password" autoComplete="new-password" inputRef={refs.username} />
                    <Button backgroundColor="#23CC20" onClick={e => { e.preventDefault(); }}>Daftar</Button>
                </MainForm>
            </Card>
        );
    }

    return (
        <Card>
            <FormTitle>Selamat datang kembali!<br/>Silakan masuk.</FormTitle>
            <MainForm>
                <LabeledInput name="username" title="Username" type="text" autoComplete="username" autoFocus inputRef={refs.username} />
                <LabeledInput name="password" title="Kata sandi" type="password" autoComplete="current-password" inputRef={refs.password} />
                <Button backgroundColor="#23CC20" onClick={logIn}>Masuk</Button>
                <AdditionalText>Lupa kata sandi? <a href="#">Klik di sini</a>.</AdditionalText>
            </MainForm>
        </Card>
    );
}

export default AuthCard;