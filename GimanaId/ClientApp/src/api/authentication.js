import { catchBadResponses } from "./utils";

export async function logIn(username, password, rememberMe) {
    return await fetch(`/api/auth/login`, {
        method: "POST",
        headers: {
            accept: "application/json",
            "Content-Type": "application/json"
        },
        body: JSON.stringify({ username, password, rememberMe })
        // credentials: "omit" // We don't need the auth/session token cookies here
    });
}

export async function signUp(username, password, email) {
    return await fetch(`/api/auth/sign-up`, {
        method: "POST",
        headers: {
            accept: "application/json",
            "Content-Type": "application/json"
        },
        body: JSON.stringify({ username, password, email })
        // credentials: "omit"
    });
}

export async function getCurrentUserId() {
    const response = await fetch(`/api/users/get-user-id`, {
        headers: {
            "accept": "text/plain"
        }
    }).then(catchBadResponses);

    const data = await response.json();
    return data.id;
}

export async function getUserInfo(userId) {
    const response = await fetch(`/api/users/${userId}`, {
        headers: {
            "accept": "text/plain"
        }
    }).then(catchBadResponses);

    // Or it can also be done this way:
    // catchBadResponses(response);
    // But it would add one more line

    return await response.json();
}

export async function getCurrentUserInfo() {
    const id = await getCurrentUserId();
    return await getUserInfo(id);
}