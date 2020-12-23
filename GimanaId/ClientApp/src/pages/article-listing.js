import { useLocation } from "react-router-dom";
import { useEffect, useState } from "react";

import c from "./article-listing.module.css";

import SearchBox from "../components/article-search-box";
import ArticleSearchBox from "../components/article-search-box";
import ArticleEntryCard from "../components/article-entry-card";

import { fetchArticleList } from "../api/article";

const TestingTheArticleListingPage = () => {
    const location = useLocation();

    useEffect(() => {
        console.log(location.state);
    }, []);

    return <p>{location.state?.preinputtedSearchQuery || "Nothing search query already provided!"}</p>
}

// or 'Explore articles' page
const ArticleListingPage = () => {
    const location = useLocation();

    const [searchQuery, setSearchQuery] = useState("");
    const [articleList, setArticleList] = useState([]);

    async function getData() {
        try {
            const retrievedData = await fetchArticleList();
            setArticleList(retrievedData);
        }
        catch (err) {
            console.error(err);
            alert("Terjadi eror dalam mengambil/mendapatkan daftar artikel: " + `${err.status} ${err.statusText}`);
        }
    }

    useEffect(() => {
        getData();

        if (location.state?.preinputtedSearchQuery) {
            setSearchQuery(location.state.preinputtedSearchQuery);
        }
    }, []);

    return (
        <div className={c.pageWrapper}>
            <h1 className={c.mainPageTitle}>Jelajahi artikel</h1>
            <ArticleSearchBox
                className={c.searchBox}
                title="Cari artikel"
                autoFocus
                value={searchQuery}
                onChange={ev => { setSearchQuery(ev.target.value) }}
            />
            <div className={c.articleGrid}>
                {articleList
                    .filter(item => searchQuery !== "" ? item.title.toLowerCase().includes(searchQuery.toLowerCase()) : true)
                    .map(article => (
                        <ArticleEntryCard
                            path={`/artikel/${article.id}`}
                            title={article.title}
                            featuredImageUrl={article.heroImage.base64EncodedData}
                        />
                    ))}
            </div>
        </div>
    )
}

export default ArticleListingPage;