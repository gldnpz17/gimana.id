import styled from "styled-components";
import { Link } from "react-router-dom";
import { Fragment, useContext } from "react";

import authContext from "../utils/auth-context";
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
    const authStatus = useContext(authContext);

    function testHandleLogOut(e) {
        e.preventDefault();

        fetch("api/auth/logout", {
            method: "POST"
        }).then(() => {
            window.location.reload();
        }, reason => {
            alert(`Error logging you out. Reason: ${reason}`);
        });
    }
    
    return (
        <MainContainer>
            <Part className="left">
                <Logo />
                <a href="#">Jelajahi topik</a>
                <a href="#">Tentang kami</a>
                <Link to="/daftar">Ingin berkontribusi?</Link>
            </Part>
            <Part className="right">
                {authStatus && (
                    authStatus.isLoggedIn ? (
                        <Fragment>
                            <p>Halo, <b>{authStatus.username}</b>!</p>
                            <Link to="articles-exp">Test tambah artikel baru</Link>
                            <a href="#" onClick={testHandleLogOut}>Keluar dari akun</a>
                        </Fragment>
                    ) : (
                        <Fragment>
                            <Link to="/masuk">Masuk</Link>
                            <Link to="/daftar">Daftar</Link>
                        </Fragment>
                    )
                )}

            </Part>
        </MainContainer>
    );
}

export default Header;