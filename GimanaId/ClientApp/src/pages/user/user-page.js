import { Route, NavLink, useRouteMatch, Switch } from "react-router-dom";
// import { createGlobalStyle } from "styled-components";

import c from "./user-page.module.css";

import { CustomizeHeaderStyle, css } from "../../components/header";

// Temporarily use this (page) component, so that we have anything to show (not blank page)
import PreviousOldFormerTestDashboardPage from "./dashboard";
import Contributions from "./contributions"; // contrib

// const CustomHeaderColor = createGlobalStyle``;

const TabLinkItem = ({ children, ...props }) => (
    <li className={c.tabItemWrapper}>
        <NavLink className={c.tabItem} activeClassName={c.activeTab} {...props}>
            {children}
        </NavLink>
    </li>
);

const UserPage = () => {
    const { path: currentTabUrlPath } = useRouteMatch();

    return (
        <>
            {/* <CustomizeHeader className={c.customMainHeaderBgColor} /> */}
            <CustomizeHeaderStyle cssStyle={css`
                background-color: #991683;
                color: white;`}
            />
            <div className={c.header}>
                <ul className={c.tabsContainer}>
                    <TabLinkItem exact to={currentTabUrlPath}>Dasbor</TabLinkItem>
                    <TabLinkItem to={`${currentTabUrlPath}/kontribusi`}>(Artikel) Kontribusi Anda</TabLinkItem>
                    <TabLinkItem to={`${currentTabUrlPath}/profil`}>Pengaturan Profil</TabLinkItem>
                </ul>
            </div>
            <div className={c.contentWrapper}>
                <Switch>
                    <Route exact path={currentTabUrlPath} component={PreviousOldFormerTestDashboardPage} />
                    <Route path={currentTabUrlPath + "/kontribusi"} component={Contributions} />
                    <Route path={currentTabUrlPath + "/profil"}>Atur serta-merta/seluk beluk tentang profil kamu di sini!</Route>
                </Switch>
            </div>
        </>
    )
}

export default UserPage;