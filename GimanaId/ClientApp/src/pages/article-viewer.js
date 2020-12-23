import { useParams, useHistory } from "react-router-dom";
import { useEffect, useState, useContext } from "react";
import { createGlobalStyle } from "styled-components";

import { fetchArticle } from "../api/article";

// import cm from "../components/article-common.module.css";
import c from "./article-common.module.css";

import AuthContext from "../components/auth-context";

import Button from "../components/button";

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

// const BodyBackgroundColor = createGlobalStyle`
//     body {
//         background-color: rgb(245 245 245);
//     }`;

const ArticlePage = () => {
    const { articleGuid } = useParams();

    const [data, setData] = useState(null);

    useEffect(() => {
        getArticleData(articleGuid, setData);
    }, []);

    const { userInfo } = useContext(AuthContext);
    const history = useHistory();

    const date = new Date(data?.dateCreated);

    return data ? (
        <article className={c.pageWrapper}>
            <section className={c.heroSection}>
                <div className={c.heroTexts}>
                    <h1 className={c.heroTitle}>{data.title || "Terjadi eror dalam memuat konten artikel ini."}</h1>
                    <p>Dibuat pada {date.toLocaleString("id-ID")}</p>
                    <p className={c.heroDescription}>{data.description || `${data.error.status}: ${data.error.statusText}`}</p>
                    {userInfo?.isLoggedIn && userInfo.contributedArticles.filter(item => item.id === articleGuid).length > 0 ? <Button onClick={() => {
                        history.push(`/artikel/${articleGuid}/edit`);
                    }}>Edit artikel ini</Button> : null}
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
            <section className={c.articleContributorsSection}>
                <h2>Kontributor artikel ini</h2>
                <p>Say thanks to them!</p>
                <ul className={c.articleContributorsContainer}>
                    {data.contributors.map(user => (
                        <li className={c.articleContributorWrapper}>
                            <img
                                className={c.articleContributorAvatarImage}
                                src="https://source.unsplash.com/random"
                                alt={user.username}
                                title={user.username}
                                data-username={user.username}
                            />
                            <div className={c.articleContributorUsername}>{user.username}</div>
                        </li>
                    ))}
                </ul>
            </section>
        </article>
    ) : (
        <p>Memuat konten artikel dengan (gu)id {articleGuid}...</p>
    );
};

export default ArticlePage;

export { getArticleData };