import styled from "styled-components";
import { useState, useEffect } from "react";
import { useHistory } from "react-router-dom";

import c from "./home.module.css";

import ArticleSearchBox from "../components/article-search-box";
import ArticleCard from "../components/article-entry-card";
import ArticleGrid from "../components/article-grid";

// Testing for the new logo concept/WIP (Logo in pure HTML and CSS)
import TestLogo from "../components/logo-in-pure-html-css";

import { fetchArticleList } from "../api/article";

const Landing = () => {
    const history = useHistory();

    function handleSearch(ev) {
        ev.preventDefault();
        console.log(ev);
        history.push({
            pathname: "/artikel",
            state: {
                preinputtedSearchQuery: ev.target[0].value // HACKY METHOD, probably use a Ref to the searchInput DOM element instead?
            }
        });
    }

    return (
        <section className={c.landingSection}>
            <h1 className={c.mainHeroText}>Bingung caranya melakukan sesuatu?</h1>
            <p className={c.complementaryHeroText}>Ayo pelajari di sini!</p>
            {/* [Ayo] tanyakan di sini! */}
            <form className={c.hiddenFormWrapper} onSubmit={handleSearch}>
                <ArticleSearchBox className={c.mainSearchInput} title="Cara ..." autoFocus />
            </form>
        </section>
    );
}

// or: Newest(Submitted)Articles
const MostPopularArticles = ({ data }) => (
    <section className={c.popularArticlesSection}>
        <h1 className={c.articlesSectionHeading}>Artikel terbaru</h1>
        <div className={c.articlesContainer}>
            {data.map(article => (
                <ArticleCard
                    path={"/artikel/" + article.id}
                    title="Lorem ipsum!"
                    featuredImageUrl="https://source.unsplash.com/random"
                />
            ))}
        </div>
    </section>
)

const HomePage = () => {
    const [articleList, setArticleList] = useState([]);

    async function getData() {
        try {
            const retrievedData = await fetchArticleList();
            setArticleList(retrievedData);
        }
        catch (err) {
            console.error(err);
            alert("Terjadi eror dalam meminta daftar artikel: " + `${err.status} ${err.statusText}`);
        }
    }

    useEffect(() => {
        getData();
    }, []);


    return (
        <>
            <div className={c.coloredSections}>
                <Landing />
                <ArticleGrid
                    sectionTitle="Artikel terbaru"
                    headingStyle={{ color: "white" }}
                    articleList={articleList}
                />
            </div>
        </>
    );
}

export default HomePage;