import styled from "styled-components";

import AuthCard from "../components/auth-card";

const PageContainer = styled.div`
    min-height: 90vh;

    background: linear-gradient(45deg, #ab1691, #d81bb7); /* Subject to change */

    display: flex;
    flex-direction: column;
    align-items: center;
    justify-content: center;
`;

const LoginPage = () => (
    <PageContainer>
        <AuthCard mode="login" />
    </PageContainer>
);

export default LoginPage;