export function catchBadResponses(response) {
    if (!response.ok) {
        throw response;
    }

    return response;
}