import { createContext } from "react";

const AuthContext = createContext({
    userInfo: null,
    refreshUserInfo: () => {}
});

export default AuthContext;

export const AuthProvider = AuthContext.Provider;
export const AuthConsumer = AuthContext.Consumer;