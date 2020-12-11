import styled from "styled-components";
import { Fragment } from "react";

const LandingSection = styled.section`
    width: 100%;
    height: 80vh;
    padding: 2rem;

    display: flex;
    flex-direction: column;
    align-items: center;
    justify-content: center;

    background: linear-gradient(45deg, #ab1691, #d81bb7); /* Temporary styling */
    color: white;
`;

const MainHeroText = styled.h1`
    font-size: 2.5em;

    margin: 0;
    margin-bottom: 0.5em;
`;

const ComplementaryHeroText = styled.p`
    font-family: "Barlow", sans-serif;
    font-size: 1.5em;

    margin: 0;
    margin-bottom: 2em;
`;

const MainSearchInput = styled.input`
    display: block;
    border: none;
    padding: 1em;

    font-size: 1em;
    font-family: inherit;

    width: 100%;
    max-width: 600px;

    border-radius: 0.5em;
    box-shadow: 0 0.4em 1em rgb(0 0 0 / 0.25);

    &:focus {
        /* Override user-agent focus style/ring */
        outline: none;
        box-shadow: 0 0 0 0.2em #90CAF9,
                    0 0.4em 1em rgb(0 0 0 / 0.25);
    }
`;

const Landing = () => (
    <LandingSection>
        <MainHeroText>Bingung caranya melakukan sesuatu?</MainHeroText>
        <ComplementaryHeroText>Ayo pelajari di sini!</ComplementaryHeroText>
        {/* [Ayo] tanyakan di sini! */}
        <MainSearchInput type="text" placeholder="Cara ..." autoFocus />
    </LandingSection>
);

const HomePage = () => (
    <Fragment>
        <Landing />
    </Fragment>
);

export default HomePage;