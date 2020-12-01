import styled from "styled-components";
import { Link } from "react-router-dom";
import { Fragment, useContext } from "react";

import AuthContext from "./authentication-context";
import Logo from "./logo-temporary";

const MainContainer = styled.header`
    width: 100%;
    padding: 1rem;

    background-color: white;
    box-shadow: 0 0 1rem rgb(0 0 0 / 0.25);

    display: flex;
    align-items: center;
    justify-content: space-between;
`;

const Part = styled.div`
    display: flex;
    align-items: center;

    /* FIXME: Separate the behavior between left part and right part */
    &.left, &.right {
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

const Header = () => {
    const authStatus = useContext(AuthContext);
    
    return (
        <MainContainer>
            <Part className="left">
                <Logo />
                <a href="#">Jelajahi topik</a>
                <a href="#">Tentang kami</a>
                <Link to="/daftar">Ingin berkontribusi?</Link>
            </Part>
            <Part className="right">
                {authStatus.isLoggedIn ? (
                    <p>User Id: {authStatus.userId}</p>
                ) : (
                    <Fragment>
                        <Link to="/masuk">Masuk</Link>
                        <Link to="/daftar">Daftar</Link>
                    </Fragment>
                )}

            </Part>
        </MainContainer>
    );
}

export default Header;