import { Route, Redirect } from "react-router-dom";

import { AuthConsumer } from "./auth-context";

export { Route as RegularRoute };

export const PrivateRoute = props => (
    <AuthConsumer>
        {({ userInfo }) => userInfo?.isLoggedIn ? <Route {...props} /> : <Redirect to="/masuk" />}
    </AuthConsumer>
);

// Unauthenticated, public-only route
export const PublicOnlyRoute = props => (
    <AuthConsumer>
        {authContext => authContext.userInfo?.isLoggedIn ? <Redirect to="/anda" /> : <Route {...props} />}
    </AuthConsumer>
);