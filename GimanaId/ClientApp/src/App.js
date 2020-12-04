import { BrowserRouter, Switch, Route } from "react-router-dom";
import { useState, useEffect } from "react";
import { createGlobalStyle } from "styled-components";

import { AuthProvider } from "./components/authentication-context";
import { getUserId } from "./authentication";

import Header from "./components/header";

// Pages for each route
import authenticationPage from "./pages/sign-up";
import authExperimentPage from "./pages/authentication-experiment";

export default () => {
    const [userInfo, setUserInfo] = useState({
        isLoggedIn: false
    });

    useEffect(() => {
        const storedToken = localStorage.getItem("authToken");
        if (storedToken) {
            getUserId(storedToken).then(id => {
                setUserInfo({
                    isLoggedIn: true,
                    userId: id
                });
            })
        }
    }, [])

    return (
        <BrowserRouter>
            <AuthProvider value={userInfo}>
                <Header />
                <Switch>
                    <Route path="/daftar"                    component={authenticationPage} />
                    <Route path="/authentication-experiment" component={authExperimentPage} />

                    {/* 404 page */}
                    <Route>Page not found.</Route>
                </Switch>
                {process.env.NODE_ENV === "development" ? <DebuggingOutlines /> : null}
            </AuthProvider>
        </BrowserRouter>
    )
};

const DebuggingOutlines = createGlobalStyle`
    * {
        outline: 1px solid rgb(255 0 0 / 0.25);
    }
`;