// Authentication logics go here

function getUserId(authToken) {
    return fetch("api/users/get-user-id", {
        method: "GET",
        headers: {
            "accept": "text/plain",
            "Auth-Token": authToken
        }
    }).then(response => response.json()).then(data => data.id);
}

function getUserInfo(userId, authToken) {
    return fetch("api/users/get-user-id", {
        method: "GET",
        headers: {
            "accept": "text/plain",
            "Auth-Token": authToken
        }
    }).then(response => response.json());
}

export { getUserId, getUserInfo };