// const ArticleEntry = ()
import { Link, useHistory } from "react-router-dom";
import { useContext, useEffect, useState } from "react";

import c from "./contributions.module.css";

import AuthContext from "../../components/auth-context";

import Image from "../../components/image";
import ArticleSearchBox from "../../components/article-search-box";

import { ReactComponent as PencilIcon } from "../../assets/svg/pencil-alt.svg";


const ArticleEntryCardWithActions = ({ articleUrlPath, title, viewsCount, lastUpdated, featuredImageUrl, ...rest }) => {
    // const history = useHistory();

    return (
        <article className={c.articleCard} {...rest}>
            <Link to={articleUrlPath} className={c.linkWrapper}>
                <Image className={c.featuredImage} src={featuredImageUrl} alt={title} />
                <div className={c.textContainer}>
                    <h2>{title}</h2>
                    <p>Terakhir diedit pada {lastUpdated}</p>
                    <p>Dilihat {viewsCount} kali</p>
                </div>

                {/* <button onClick={() => { history.push(`${articleUrlPath}/edit`) }}>Edit</button> */}
                {/* TODO: Replace the above with an imported SVG icon (probably place it inside /assets) */}
            </Link>
            {/* Link role="button"? */}
            <Link to={`${articleUrlPath}/edit`} className={c.actionButton}>
                <PencilIcon title="Edit artikel" />
            </Link>
        </article>
    );
}

const ContributionsPage = () => {
    const { userInfo, refreshUserInfo } = useContext(AuthContext);
    // const (auth)context = useContext(AuthCOntext);

    useEffect(() => {
        refreshUserInfo();
    }, []);

    const [searchQuery, setSearchQuery] = useState("");

    // function applySearchQuery()

    return (
        <section className={c.contentWrapper}>
            {/* <label htmlFor="search-box" style={{ display: "none" }}>Cari artikel kontribusi Anda</label>
            <input
                name="search-box"
                type="search"
                placeholder="Cari..."
                className={c.searchBox}
                value={searchQuery}
                onChange={ev => { setSearchQuery(ev.target.value) }} /> */}
            <ArticleSearchBox
                className={c.searchBox}
                title="Cari artikel kontribusi kamu"
                autoFocus
                value={searchQuery}
                onChange={ev => { setSearchQuery(ev.target.value) }}
            />
            <div className={c.articleGrid}>
                {userInfo?.contributedArticles
                    .filter(item => searchQuery !== "" ? item.title.toLowerCase().includes(searchQuery.toLowerCase()) : true)
                    .map(article => (
                        <ArticleEntryCardWithActions
                            articleUrlPath={`/artikel/${article.id}`}
                            title={article.title}
                            featuredImageUrl="https://source.unsplash.com/random"
                            lastUpdated="x"
                            viewsCount="n"
                        />
                    ))}
            </div>
        </section>
    )
}

export default ContributionsPage;