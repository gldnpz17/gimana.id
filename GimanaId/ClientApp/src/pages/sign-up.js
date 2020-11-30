import styled from "styled-components";
import { Link } from "react-router-dom";

import stockCard from "../components/card";
import stockButton from "../components/button";
import LabeledInput from "../components/labeled-input";

const PageContainer = styled.div`
    min-height: 90vh;

    background-color: #3399D2; /* Subject to change */

    display: flex;
    align-items: center;
`;

const ExplanationPart = styled.div`
    flex-grow: 1;

    color: white;
`;

const EntryPart = styled.div`
    flex-grow: 0;
    flex-shrink: 0;
`;

const CatchPhrase = styled.h1`
    font-weight: normal;
    font-family: "Red Hat Display", sans-serif;

    &:first-of-type {
        font-weight: bold;
    }
`;

const CompanionText = styled.p`
    font-family: "Red Hat Text", sans-serif;
`;

const AuthCard = styled(stockCard)`
    width: 25rem;
    padding: 2em;
    margin: 2rem 10rem;

    box-shadow: 0 0.5rem 1rem rgb(0 0 0 / 0.25);

    position: relative;
    z-index: 1;

    &::before {
        content: "";
        position: absolute;
        z-index: -1;
        width: 130%;
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

export default () => {
    return (
        <PageContainer>
            <ExplanationPart>
                <CatchPhrase>Ingin berkontribusi?</CatchPhrase>
                <CatchPhrase>Silakan membuat akun terlebih dahulu.</CatchPhrase>
                <CompanionText>
                    Data yang anda masukkan akan dijaga sesuai dengan <u>kebijakan privasi kami</u>.
                    Dengan berkontribusi, Anda turut serta dalam mengembangkan koleksi yang dimiliki oleh <b>gimana.id</b>.
                </CompanionText>
            </ExplanationPart>
            <EntryPart>
                <AuthCard>
                    <FormTitle>Buat akun baru</FormTitle>
                    <AdditionalText>Sudah pernah mendaftar? <Link to="/masuk">Klik di sini untuk masuk</Link>.</AdditionalText>
                    <MainForm>
                        <LabeledInput name="email"           title="Alamat e-mail"            type="email"    autoFocus />
                        <LabeledInput name="username"        title={<i>Username</i>}   type="text"     autoComplete="username" />
                        <LabeledInput name="password"        title="Kata sandi"        type="password" autoComplete="new-password" />
                        <LabeledInput name="repeat-password" title="Ulangi kata sandi" type="password" autoComplete="new-password" />
                        <Button backgroundColor="#23CC20" onClick={e => { e.preventDefault(); }}>Daftar</Button>
                    </MainForm>
                </AuthCard>
            </EntryPart>
        </PageContainer>
    );
};