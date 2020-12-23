import { useEffect, useContext } from "react";
import { Redirect, useHistory, Link } from "react-router-dom";
import { createGlobalStyle } from "styled-components";

import s from "./dashboard.module.css";

import Button from "../../components/button";
import TestArticleCard from "../../components/article-entry-card";
import ArticleGridSection from "../../components/article-grid";

import genericAvatarImage from "../../assets/generic-avatar.png";

import AuthContext from "../../components/auth-context";

const GlobalStyle = createGlobalStyle`
    body {
        background-color: #ab1691;
    }
`;

const DashboardPage = () => {
    const { userInfo, refreshUserInfo } = useContext(AuthContext);

    // FIXME: Create a new "PrivateRoute" component instead to generalize the logic
    // if (!userInfo?.isLoggedIn) {
    //     return <Redirect to="/masuk" />;
    // }
    // Nope, apparently that's not how it works (Should it be inside an useEffect hook call?)
    // console.log(userInfo);

    const history = useHistory();

    useEffect(() => {
        refreshUserInfo();
    }, []);

    // TODO: Find out why the value is null at first, but then able to get the value after adding the null? propagation operator
    return (
        <>
            <GlobalStyle />
            {/* <div className={s.subHeader}>
                <h1><span>{userInfo?.username} &gt; </span>Dasbor Anda</h1>
                <Button backgroundColor="#23CC20" onClick={() => {
                    history.push("/artikel/buat-baru"); // FIXME, probably make a custom button-styled anchor/link instead
                }}>Tambah artikel baru</Button>
            </div> */}
            <section className={s.heroSection}>
                <img className={s.profileImage} src={genericAvatarImage} alt="Gambar profil" />
                <div>
                    <h1 className={s.userGreeting}>Hai, <b>{userInfo?.username}</b>.</h1>
                    <p className={s.userSupportingText}>
                        {userInfo?.contributedArticles.length > 0 ? `Anda telah berkontribusi ${userInfo.contributedArticles.length} artikel.` : "Ayo mulai berkontribusi sekarang!"}
                    </p>
                    <Button className={s.createNewArticleButton} onClick={() => {
                        history.push("/artikel/baru"); // FIXME, probably make a custom button-styled anchor/link instead
                    }}>Tambah artikel baru</Button>
                </div>
            </section>
            <ArticleGridSection
                sectionTitle="Kontribusi terakhir Anda"
                headingStyle={{
                    color: "white"
                }}
                articleList={userInfo?.contributedArticles}
            />
        </>
    )
}

export default DashboardPage;