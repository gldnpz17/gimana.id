import { useEffect, useContext } from "react";
import { Redirect, useHistory } from "react-router-dom";

import s from "./dashboard.module.css";

import Button from "../components/button";

import AuthContext from "../utils/auth-context";

const DashboardPage = () => {
    const userInfo = useContext(AuthContext);

    // FIXME: Create a new "PrivateRoute" component instead to generalize the logic
    // if (!userInfo?.isLoggedIn) {
    //     return <Redirect to="/masuk" />;
    // }
    // Nope, apparently that's not how it works (Should it be inside an useEffect hook call?)
    // console.log(userInfo);

    const histo = useHistory(); // FIXME, this is just a quick and dirty way to get it to navigate

    // TODO: Find out why the value is null at first, but then able to get the value after adding the null? propagation operator
    return (
        <>
            <div className={s.subHeader}>
                <h1><span>{userInfo?.username} &gt; </span>Dasbor Anda</h1>
                <Button backgroundColor="#23CC20" onClick={() => {
                    histo.push("/articles-exp"); // FIXME, probably make a custom button-styled anchor/link instead
                }}>Tambah artikel baru</Button>
            </div>
            <section className={s.heroSection}>
                <img className={s.profileImage} src="https://source.unsplash.com/random" alt="Gambar profil" />
                <div>
                    <h1 className={s.userGreeting}>Hai, <b>{userInfo?.username}</b>.</h1>
                    <p className={s.userSupportingText}>Ayo mulai berkontribusi sekarang!</p>
                </div>
            </section>
            <section className={s.userContributionsSection}>
                <h1>Kontribusi Anda</h1>
                <ul>
                    {/* FIXME: This won't automatically refresh after adding an entry unless full page reload */}
                    {/* Probably refresh global userInfo state on each route visit OR provide a state-refresh action context? */}
                    {userInfo?.contributedArticles.map(entry => (
                        <li key={entry.id}>
                            {/* TODO: Change all of this to using <Link /> */}
                            <a href={`#${entry.id}`}>{entry.title}</a>
                        </li>
                    ))}
                </ul>
            </section>
        </>
    )
}

export default DashboardPage;