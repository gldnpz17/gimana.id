import { catchBadResponses } from "./utils";

export async function fetchArticleList() {
    const response = await fetch(`/api/articles`).then(catchBadResponses);
    return await response.json();
}

export async function fetchArticle(articleGuid) {
    const response = await fetch(`/api/articles/${articleGuid}`).then(catchBadResponses);
    return await response.json();
}