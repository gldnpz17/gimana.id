import { ReactComponent as SearchIcon } from "../assets/svg/search.svg";

import c from "./article-search-box.module.css";

const ArticleSearchBox = ({ title, className, ...props }) => (
    <div className={[c.containerBox, className].join(" ")}>
        <SearchIcon className={c.icon} role="presentation" />
        <label htmlFor="article-search-box" className={c.visuallyHiddenLabel}>{title || "Cari (daftar) artikel"}</label>
        <input type="search" name="article-search-box" className={c.searchInput} placeholder={title || "Cari"} {...props} />
    </div>
);

export default ArticleSearchBox;