import { catchBadResponses } from "./utils";

export async function fetchArticle(articleGuid) {
    const response = await fetch(`/api/articles/${articleGuid}`).then(catchBadResponses);
    return await response.json();
}