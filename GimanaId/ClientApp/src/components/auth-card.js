import styled from "styled-components";

import Card from "./card";
import Button from "./button";

const AuthCard = styled(Card)`
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

const AuthForm = styled.form`
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

const FinePrint = styled.p`
    text-align: center;
    font-size: 0.9em;

    margin: 0;
    margin-bottom: 2em;
`;

const AuthButton = styled(Button)`
    margin-top: 1em;
`;


export default AuthCard;

export { AuthForm, FormTitle, FinePrint, AuthButton };