// Authentication logics go here
// Create a utils/ directory/file instead?

function catchErrors(response) {
    if (!response.ok) {
        const { status, statusText } = response;
        throw { status, statusText }; // Or throw the whole/entire response object instead?
    }

    return response;
}

export async function logIn(username, password) {
    return await fetch(`api/auth/login`, {
        method: "POST",
        headers: {
            accept: "application/json",
            "Content-Type": "application/json"
        },
        body: JSON.stringify({ username, password })
        // credentials: "omit" // We don't need the auth/session token cookies here
    });
}

export async function signUp(username, password, email) {
    return await fetch(`api/auth/sign-up`, {
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
    const response = await fetch(`api/users/get-user-id`, {
        headers: {
            "accept": "text/plain"
        }
    }).then(res => catchErrors(res));

    const data = await response.json();
    return data.id;
}

export async function getUserInfo(userId) {
    const response = await fetch(`api/users/${userId}`, {
        headers: {
            "accept": "text/plain"
        }
    }).then(res => catchErrors(res));

    return await response.json();
}

export async function getCurrentUserInfo() {
    const id = await getCurrentUserId();
    return await getUserInfo(id);
}