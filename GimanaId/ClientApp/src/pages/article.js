import { useParams } from "react-router-dom";
import { useEffect, useState } from "react";

import { fetchArticle } from "../api/article";

// import cm from "../components/article-common.module.css";
import c from "./article-common.module.css";

// // Move these to a separate article-common.js file?
// const HeroSection = ({ children, ...props }) => (
//     <section {...props}>
//         {children}
//     </section>
// );

// const Text = ({ mode, component, className, rows, children, onChange, ...rest }) => {
//     if (mode === "edit" || mode === "new") {
//         return <textarea className={[c.editable, className].join(" ")} rows={rows} value={children} onChange={onChange} />;
//     }
//     const Component = component;
//     return <Component className={className} {...rest}>{children}</Component>;
// }

async function getArticleData(articleGuid, stateSetter) {
    try {
        const jsonResponse = await fetchArticle(articleGuid);
        stateSetter(jsonResponse);
    }
    catch (err) {
        stateSetter({
            error: err
        });
        console.log(err);
    }
}

const ArticlePage = () => {
    const { articleGuid } = useParams();

    const [data, setData] = useState(null);

    useEffect(() => {
        getArticleData(articleGuid, setData);
    }, []);

    return data ? (
        <article className={c.pageWrapper}>
            <section className={c.heroSection}>
                <div className={c.heroTexts}>
                    <h1
                        // m/ode="edit"
                        // component="div"
                        className={c.heroTitle}
                        // onChange={({ target: { value } }) => {
                        //     const prev = data;
                        //     prev.title = value;
                        //     setData(prev);
                        // }}
                    >{data.title || "Terjadi eror dalam memuat konten artikel ini."}</h1>
                    <p className={c.heroDescription}>{data.description || `${data.error.status}: ${data.error.statusText}`}</p>
                </div>
                <img className={c.heroImage} src="https://source.unsplash.com/random" alt="Hero image" />
            </section>
            {data.parts?.map(part => (
                <section key={`part${part.partNumber}`} className={c.partCard}>
                    <div className={c.partHeading}>
                        <h2 className={c.partNumber}>Bagian {part.partNumber}</h2>
                        <p className={c.partTitle}>{part.title}</p>
                    </div>
                    <ul className={c.stepsContainer}>
                        {part.steps?.map(step => (
                            <li key={`part${part.partNumber}-step${step.stepNumber}`} className={c.stepItemContainer}>
                                {Math.random() < 0.25 ? (
                                    <img className={c.stepImage} src="https://source.unsplash.com/random" alt={step.title} />
                                ) : null}
                                <div className={c.stepExplanationWrapper}>
                                    <div className={c.stepNumberMarker}>{step.stepNumber}</div>
                                <p className={c.stepText}>
                                        {step.title}
                                        <br />
                                        {step.description}
                                    </p>
                                </div>
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

export { getArticleData };