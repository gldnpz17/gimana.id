import { BrowserRouter, Switch, Route } from "react-router-dom";

import Header from "./components/header";

// Pages for each route
import registerPage from "./pages/sign-up";
import authExperimentPage from "./pages/authentication-experiment";

const App = () => (
    <BrowserRouter>
        <Header />
        <Switch>
            <Route path="/daftar"                    component={registerPage} />
            <Route path="/authentication-experiment" component={authExperimentPage} />

            {/* 404 page */}
            <Route>Page not found.</Route>
        </Switch>
    </BrowserRouter>
);

export default App;
