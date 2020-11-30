import styled from "styled-components";
import { Link } from "react-router-dom";

import stockCard from "../components/card";

const PageContainer = styled.div`
    min-height: 100vh;

    background-color: #3399D2; /* Subject to change */

    display: flex;
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
    height: 50rem;
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
                    <h1>Buat akun baru</h1>
                    <p>Sudah pernah mendaftar? <Link to="/masuk">Klik di sini untuk masuk</Link>.</p>
                    <form>
                        <label htmlFor="email">E-mail</label>
                        <input name="email" type="text" autoFocus />

                        <label htmlFor="username"><i>Username</i></label>
                        <input name="username" type="text" autoComplete="username" />

                        <label htmlFor="password">Kata sandi</label>
                        <input name="password" type="password" autoComplete="new-password" />

                        <label htmlFor="repeat-password">Ulangi kata sandi</label>
                        <input name="repeat-password" type="password" autoComplete="new-password" />

                        <button onClick={e => { e.preventDefault(); }}>Daftar</button>
                    </form>
                </AuthCard>
            </EntryPart>
        </PageContainer>
    );
};