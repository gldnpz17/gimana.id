import { useParams } from "react-router-dom";
import { useEffect, useState } from "react";
import { createGlobalStyle } from "styled-components";

// import {}

import c from "./article.module.css";

// Global styles specific to this page
const ArticlePageGlobalStyle = createGlobalStyle`
    body {
        background-color: #3399D2; /* Subject to change */
    }
`;

const ArticlePage = () => {
    const { articleGuid } = useParams();

    const mockOrStubArticleData = {
        "id": "string",
        "title": "string",
        "description": "string",
        "heroImage": {
            "fileFormat": "string",
            "base64EncodedData": "string"
        },
        "parts": [
            {
                "partNumber": 1,
                "title": "string",
                "description": "string",
                "steps": [
                    {
                        "stepNumber": 1,
                        "title": "string",
                        "description": "string"
                    }
                ]
            }
        ],
        "contributors": [
            {
                "id": "string",
                "username": "string",
                "profilePicture": {
                    "fileFormat": "string",
                    "base64EncodedData": "string"
                }
            }
        ]
    };

    const [articleData, setArticleData] = useState(null); // previously/originally was null

    async function fetchArticleData() {
        try {
            const response = await fetch(`/api/articles/${articleGuid}`);

            // FIXME: Place all of this in the "utils/authentication.js" file (unified in a single file)
            if (!response.ok) {
                throw response;
            }

            const data = await response.json();
            setArticleData(data);
        }
        catch (err) {
            setArticleData({
                error: err
            });
            console.log(err);
        }
    }

    useEffect(() => {
        fetchArticleData();
    }, [])

    return articleData ? (
        <article className={c.pageWrapper}>
            <ArticlePageGlobalStyle />
            <section className={c.heroSection}>
                <div className={c.heroTexts}>
                    <h1 className={c.heroTitle}>{articleData.title || "Terjadi eror dalam memuat konten artikel ini."}</h1>
                    <p className={c.heroDescription}>{articleData.description || `${articleData.error.status}: ${articleData.error.statusText}`}</p>
                </div>
                <img className={c.heroImage} src="https://source.unsplash.com/random" alt="Hero image" />
            </section>
            {articleData.parts?.map(part => (
                <section key={`part${part.partNumber}`} className={c.partWrapper}>
                    <h1 className={c.partTitle}> {/* or h2? */}
                        Bagian {part.partNumber}
                        <br />
                        {part.title}
                    </h1>
                    <ul className={c.stepsContainer}>
                        {part.steps?.map(step => (
                            <li key={`part${part.partNumber}-step${step.stepNumber}`} className={c.stepWrapper}>
                                <div className={c.stepNumberMarker}>{step.stepNumber}</div>
                                <p className={c.stepText}>
                                    {step.title}
                                    <br />
                                    {step.description}
                                </p>
                            </li>
                        ))}
                    </ul>
                </section>
            ))}
        </article>
    ) : (
        <p>Memuat konten artikel dengan (gu)id {articleGuid}...</p>
    );
};

export default ArticlePage;