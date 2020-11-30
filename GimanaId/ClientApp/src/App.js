import { BrowserRouter, Switch, Route } from "react-router-dom";
import { createGlobalStyle } from "styled-components";

import Header from "./components/header";

// Pages for each route
import authenticationPage from "./pages/sign-up";
import authExperimentPage from "./pages/authentication-experiment";

export default () => (
    <BrowserRouter>
        <Header />
        <Switch>
            <Route path="/daftar"                    component={authenticationPage} />
            <Route path="/authentication-experiment" component={authExperimentPage} />

            {/* 404 page */}
            <Route>Page not found.</Route>
        </Switch>
        {process.env.NODE_ENV === "development" ? <DebuggingOutlines /> : null}
    </BrowserRouter>
);

const DebuggingOutlines = createGlobalStyle`
    * {
        outline: 1px solid rgb(255 0 0 / 0.25);
    }
`;