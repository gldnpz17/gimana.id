import c from "./article-grid.module.css";

import ArticleCard from "./article-entry-card";

const ArticleGridSection = ({ sectionTitle, headingStyle, articleList, ...rest }) => (
    <section className={c.mainContainer} {...rest}>
        <h1 className={c.headingText} style={headingStyle}>{sectionTitle}</h1>
        <div className={c.gridContainer}>
            {articleList?.map(item => (
                <ArticleCard
                    path={`/artikel/${item.id}`}
                    title={item.title}
                    featuredImageUrl={item.heroImage.base64EncodedData}
                />
            ))}
        </div>
        {/* Probably put additional children here if needed? */}
    </section>
);

export default ArticleGridSection;