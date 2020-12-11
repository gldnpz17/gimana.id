import { useParams } from "react-router-dom";
import { useEffect, useState } from "react";

const ArticlePage = () => {
    const { articleGuid } = useParams();

    const [articleData, setArticleData] = useState(null);

    // async function getArticleData() {
    async function fetchArticleData() {
        try {
            const response = await fetch(`/api/articles/${articleGuid}`);
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

    return (
        <div>
            {articleData ? (
                articleData.err ? <p>Terjadi eror dalam memuat artikel: {articleData.err}</p> : (
                    <>
                        <h1>{articleData.title}</h1>
                        <p>{articleData.description}</p>
                        {articleData.parts?.map(part => (
                            <article key={part.partNumber}>
                                <h2>Bagian {part.partNumber}: {part.title}</h2>
                                <p>{part.description}</p>
                                <ol>
                                    {part.steps?.map(step => (
                                        <li key={step.stepNumber}>
                                            <h3>{step.title}</h3>
                                            <p>{step.description}</p>
                                        </li>
                                    ))}
                                </ol>
                            </article>
                        ))}
                    </>
                )
            ) : <p>Memuat artikel...</p>}
        </div>
    );
};

export default ArticlePage;