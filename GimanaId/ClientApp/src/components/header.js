import { Link, useHistory } from "react-router-dom";
import { useContext, createContext, useState } from "react";
import { createGlobalStyle } from "styled-components";

import AuthContext from "./auth-context";
import Logo from "./logo-in-pure-html-css";

import headerStyles from "./header.module.css";

import { ReactComponent as SearchIcon } from "../assets/svg/search.svg";
import genericAvatarImage from "../assets/generic-avatar.png";

const SearchBox = () => {
    const history = useHistory();

    function handleHeaderSearch(ev) {
        ev.preventDefault();

        history.push({
            pathname: "/artikel",
            state: {
                preinputtedSearchQuery: ev.target[0].value
            }
        });
    }

    return (
        <form className={headerStyles.searchBoxFormWrapper} onSubmit={handleHeaderSearch}>
            <SearchIcon className={headerStyles.searchBoxIcon} />
            <label htmlFor="header-search-box" className={headerStyles.visuallyHiddenSearchBoxLabel}>Cari artikel</label>
            <input name="header-search-box" type="search" placeholder="Cari di sini" className={headerStyles.searchInput} />
        </form>
    )
};

const Header = () => {
    const { userInfo } = useContext(AuthContext);

    function testHandleLogOut(e) {
        e.preventDefault();

        fetch("/api/auth/logout", {
            method: "POST"
        }).then(() => {
            window.location.reload();
        }, reason => {
            alert(`Error logging you out. Reason: ${reason}`);
        });
    }

    const searchBoxTempStyles = {
        border: "none",
        fontSize: "inherit",
        fontFamily: "inherit",
        padding: "0.5em",
        backgroundColor: "rgb(245 245 245)",
        borderRadius: "0.2rem"
    };

    // const [customClassName, setCustomClassName] = useState(null);

    // const classNamesArray = [headerStyles.mainContainer];
    // if (customClassName) {
    //     classNamesArray.push(customClassName);
    // }

    // console.log(customClassName);

    return (
        <header className={`gid-main-page-header`}>
            {/* Is it a good practice to put multiple nav elements (in one page / containing element)? */}
            <nav className={headerStyles.part}>
                <Link to="/" className={headerStyles.logoWrapperAnchor}><Logo /></Link>
                {/* Is it okay to put the search box inside <nav> element? */}
                <SearchBox />
                <Link to="/artikel" className={headerStyles.navItem}>Jelajahi artikel</Link>
                {/* <Link to="/tentang" className={headerStyles.navItem}>Tentang kami</Link> */}
                {process.env.NODE_ENV === "development" ? (
                    <a href="/swagger" className={headerStyles.navItem}>Go to Swagger</a>
                ) : null}
            </nav>
            <nav className={headerStyles.part}>
                {userInfo ? (
                    userInfo.isLoggedIn ? (
                        <>
                            <Link to="/anda" className={headerStyles.userProfileNavItem}>
                                <img className={headerStyles.userAvatarImage} src={genericAvatarImage} alt={userInfo.username} />
                                <span>{userInfo.username}</span>
                            </Link>
                            <Link to="#keluar-dari-akun" className={headerStyles.navItem} onClick={testHandleLogOut}>Keluar</Link>
                        </>
                    ) : (
                            <>
                                <Link to="/masuk" className={headerStyles.navItem}>Masuk</Link>
                                <Link to="/daftar" className={headerStyles.catchyRegisterNavItem}>Daftar</Link>
                            </>
                        )
                ) : <p>Loading user info...</p>}

            </nav>
        </header>
    );
}

export default Header;

export const CustomizeHeaderStyle = createGlobalStyle`
    .gid-main-page-header {
        /* $ {props => {
            const {
                headerBackgroundColor,
                headerShadow,
                navTextColor,
                navHighlightBackgroundColor,
                searchBoxTextColor,
                searchBoxPlaceholderColor,
                searchBoxBackgroundColor
            } = props;

            return css
                $ {headerBackgroundColor ? css
        }} */

        ${props => props.cssStyles}
    }
`;

export { css } from "styled-components";