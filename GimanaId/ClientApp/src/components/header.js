import styled from "styled-components";
import { Link } from "react-router-dom";

import Logo from "./logo-temporary";

const MainContainer = styled.header`
    width: 100%;
    padding: 1rem;

    background-color: white;
    box-shadow: 0 0 1rem rgb(0 0 0 / 0.25);

    display: flex;
    align-items: center;
`;

const Part = styled.div`
    display: flex;
    align-items: center;

    &.left {
        a {
            color: #0a0a0a;
            text-decoration: none;
            margin: 0 0.5em;

            &:hover {
                text-decoration: underline;
            }
        }
    }
`;

const Header = () => (
    <MainContainer>
        <Part className="left">
            <Logo />
            <a href="#">Jelajahi topik</a>
            <a href="#">Tentang kami</a>
            <a href="#">Ingin berkontribusi?</a>
        </Part>
    </MainContainer>
);

export default Header;