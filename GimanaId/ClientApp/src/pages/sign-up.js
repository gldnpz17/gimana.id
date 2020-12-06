import styled from "styled-components";
import { Link } from "react-router-dom";

import AuthCard from "../components/auth-card";

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

export default () => {
    return (
        <PageContainer>
            <ExplanationPart>
                <CatchPhrase>Ingin berkontribusi?</CatchPhrase>
                <CatchPhrase>Silakan membuat akun terlebih dahulu.</CatchPhrase>
                <CompanionText>
                    Data yang anda masukkan akan dijaga sesuai dengan <Link to="/privasi">kebijakan privasi kami</Link>.
                    Dengan berkontribusi, Anda turut serta dalam mengembangkan koleksi yang dimiliki oleh <b>gimana.id</b>.
                </CompanionText>
            </ExplanationPart>
            <EntryPart>
                <AuthCard />
            </EntryPart>
        </PageContainer>
    );
};