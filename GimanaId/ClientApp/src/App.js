import { BrowserRouter, Switch, Route } from "react-router-dom";

import Header from "./components/header";
import AuthExperimentPage from "./components/authentication-experiment";

const App = () => (
    <BrowserRouter>
        <Header />
        <Switch>
            <Route path="/authentication-experiment" component={AuthExperimentPage} />
        </Switch>
    </BrowserRouter>
);

export default App;
