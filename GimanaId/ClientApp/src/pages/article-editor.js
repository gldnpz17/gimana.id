import { useParams, useHistory } from "react-router-dom";
import { useState, useEffect, useRef } from "react";

import { getArticleData } from "./article-viewer";

// import cm from "../components/article-common.module.css";
// import c from "./article-editor.module.css";
// import tempS from "./article.module.css";

import editor from "./article-editor.module.css"; // Editor-specific styles(heet)
import viewer from "./article-common.module.css"; // Styles inherited from the regular viewer, to achieve the WYSIWYG look-n-feel


import Button from "../components/button";

const FlexibleTextArea = ({ className, onLoad, onChange, ...props }) => {
    function handleResizing(ev) {
        const oldHeight = ev.target.style.height;
        // const newHeight = ;
        // if (oldHeight !== ev.target.scrollHeight + "px") {
        ev.target.style.height = "auto";

        let newHeight = `calc(${ev.target.scrollHeight}px + 0.2em)`;
        ev.target.style.maxHeight = newHeight;
        ev.target.style.height = newHeight;

        ev.target.style.maxHeight = "";
        // console.log(ev);
        // }
    }

    return (
        <textarea
            className={[editor.autoGrowingTextArea, className].join(" ")}
            // rows={1} // FIXME?
            onLoad={ev => {
                handleResizing(ev);
                onLoad && onLoad(ev);
            }}
            onChange={ev => {
                handleResizing(ev);
                onChange && onChange(ev);
            }}
            {...props}
        />
    );
}

const Editable = ({ rows, className, ...rest }) => (
    <textarea
        rows={rows}
        className={className}
        {...rest}
    />
);

const ArticleEditorPage = ({ mode }) => {
    const { articleGuid } = useParams();

    const blankStepItem = {
        title: "",
        description: ""
    };

    const blankPartItem = {
        title: "",
        description: "",
        steps: [
            blankStepItem
        ]
    };

    const [articleTitle, setArticleTitle] = useState("");
    const [articleDescription, setArticleDescription] = useState("");
    const [parts, setParts] = useState([blankPartItem]);

    const originalArticleTitle = useRef(null);

    // function partiallyUpdateStateArrayValue(stateSetterFunction, ) {}

    const history = useHistory();

    useEffect(() => {
        if (mode === "edit") {
            getArticleData(articleGuid, data => {
                // Add some conditionals here for error-checking
                // FIXME
                if (data.error) {
                    alert("Error fetching existing article data!");
                    return; // Early return in case there's an error
                }

                originalArticleTitle.current = data.title;
                setArticleTitle(data.title);
                setArticleDescription(data.description);
                setParts(data.parts);
            });
        }
    }, []);

    function confirmExit() {
        const areYouSure = window.confirm("Anda yakin ingin keluar? Segala perubahan yang anda buat akan terhapus/hilang.");

        if (areYouSure) {
            history.push("/anda/kontribusi");
        }
    }

    async function testHandleSubmit(ev) {
        ev.preventDefault();

        const submissionResponse = await fetch(
            `/api/articles${mode === "edit" ? "/" + articleGuid : ""}`,
            {
                method: mode === "edit" ? "PUT" : "POST",
                headers: {
                    accept: "application/json",
                    "Content-Type": "application/json"
                },
                body: JSON.stringify({
                    title: articleTitle,
                    description: articleDescription,
                    parts: parts
                })
            }
        );

        if (submissionResponse.ok) {
            alert("Article submitted/updated successfully!");
            history.push(mode === "edit" ? `/artikel/${articleGuid}` : `/anda/kontribusi`);
        }
        else {
            console.log(submissionResponse);
        }
    }

    const  [uploadedImageTemporaryLocalUri, setUploadedImageTemporaryLocalUri] = useState("https://source.unsplash.com/random");

    return (
        <article className={viewer.pageWrapper}>
            <div className={editor.actionStrip}>
                <p>
                    {mode === "edit" ? (
                        <>Mengedit artikel <b>{originalArticleTitle.current}</b></>
                    ) : (
                            <>Artikel baru</>
                        )}
                </p>
                <div className={editor.actionButtons}>
                    <Button backgroundColor="darkred" onClick={confirmExit}>Buang perubahaan</Button>
                    <Button backgroundColor="green" onClick={testHandleSubmit}>Simpan perubahan</Button>
                </div>
            </div>
            <section className={viewer.heroSection}>
                <div>
                    <Editable
                        className={viewer.heroTitle}
                        placeholder="Judul artikel..."
                        rows={1}
                        value={articleTitle}
                        onChange={ev => { setArticleTitle(ev.target.value) }}
                    />
                    <Editable
                        className={viewer.heroDescription}
                        placeholder="Deskripsi..."
                        value={articleDescription}
                        onChange={ev => { setArticleDescription(ev.target.value) }}
                    />
                </div>
                <img className={viewer.heroImage} src={uploadedImageTemporaryLocalUri} alt="Hero image" />
                <input type="file" accept="image/*" onChange={ev => {
                    setUploadedImageTemporaryLocalUri(
                        URL.createObjectURL(ev.target.files[0])
                    );
                }} />
            </section>
            {parts.map((part, i) => (
                <section className={viewer.partCard}>
                    <div className={viewer.partHeading}>
                        <div className={editor.partActions}>
                            <h2 className={viewer.partNumber}>Bagian {i + 1}</h2>
                            <Button onClick={() => {
                                const prev = [...parts];
                                prev.splice(i, 1);
                                setParts(prev);
                            }} backgroundColor="red">Hapus part ini</Button>
                        </div>

                        <Editable
                            className={viewer.partTitle}
                            placeholder={`Penjelasan step ke-${i + 1}`}
                            rows={1}
                            value={part.title}
                            onChange={ev => {
                                const prevArray = [...parts];
                                prevArray[i].title = ev.target.value;
                                setParts(prevArray);
                            }}
                        />
                    </div>
                    <ul className={viewer.stepsContainer}>
                        {part.steps?.map((step, j) => (
                            <li className={viewer.stepItemContainer}>
                                <div className={viewer.stepExplanationWrapper}>
                                    <div className={viewer.stepNumberMarker}>{j + 1}</div>
                                    <div style={{ flexGrow: "1" }}>
                                        <Editable
                                            placeholder={`Judul step ke-${j + 1}...`}
                                            rows={1}
                                            value={step.title}
                                            onChange={ev => {
                                                const prevPartsArray = [...parts];
                                                prevPartsArray[i].steps[j].title = ev.target.value;
                                                setParts(prevPartsArray);
                                            }}
                                        />
                                        <Editable
                                            placeholder={`Deskripsi step ke-${j + 1}...`}
                                            value={step.description}
                                            onChange={ev => {
                                                const prevPartsArray = [...parts];
                                                prevPartsArray[i].steps[j].description = ev.target.value;
                                                setParts(prevPartsArray);
                                            }}
                                        />
                                    </div>
                                    <Button style={{ marginLeft: '1rem' }} onClick={ev => {
                                        const prev = [...parts];
                                        prev[i].steps.splice(j, 1);
                                        setParts(prev);
                                    }} backgroundColor="red">X</Button>
                                </div>
                            </li>
                        ))}
                    </ul>
                    <div role="button" className={`${viewer.stepExplanationWrapper} ${editor.addStepButton}`} onClick={ev => {
                        const prev = [...parts];
                        prev[i].steps.push(blankStepItem);
                        setParts(prev);
                    }}>
                        <div className={viewer.stepNumberMarker}>+</div>
                        <p>Tambah step</p>
                    </div>
                    {/* <Button style={{ margin: '0 auto' }}>Tambah step</Button> */}
                </section>
            ))}
            <section role="button" className={`${viewer.partCard} ${editor.addPartButton}`} onClick={() => {
                const prevPartsArr = [...parts];
                prevPartsArr.push(blankPartItem);
                setParts(prevPartsArr);
            }}>
                + Tambah part baru
            </section>
            {/* <Button style={{ margin: '0 auto' }} backgroundColor="green">Tambah part</Button> */}
        </article>
    )
}

export default ArticleEditorPage;