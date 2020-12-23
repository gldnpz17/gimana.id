import { Link } from "react-router-dom";
// import { useState, useRef, useEffect } from "react";

import c from "./article-entry-card.module.css";

import Image from "./image";

const ArticleCard = ({ path, title, viewsCount, lastUpdated, featuredImageUrl, ...rest }) => (
    <Link to={path} className={c.entryWrapper}>
        <article className={c.card}>
            <Image className={c.featuredImage} src={featuredImageUrl} alt={title} />
            <h2 className={c.articleTitle}>{title}</h2>
        </article>
    </Link>
);

export default ArticleCard;