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
        description: "",
        image: {
            fileFormat: "",
            base64EncodedData: null
        }
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
    const [articleFeaturedImage, setArticleFeaturedImage] = useState(null);
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

                // Make sure to properly sort the steps for each part beforehand
                /*const sortedParts = */data.parts.sort(
                    (a, b) => a.partNumber - b.partNumber
                ); // Apparently it does in-place modification as well, so we don't need to assign the result to a new variable

                for (let i = 0; i < data.parts.length; i++) {
                    data.parts[i].steps.sort(
                        (a, b) => a.stepNumber - b.stepNumber
                    );
                }

                setParts(data.parts);
            });
        }
    }, []);

    function confirmExit() {
        const areYouSure = window.confirm("Anda yakin ingin keluar? Segala perubahan yang anda buat akan terhapus.");

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
                    heroImage: {
                        fileFormat: "",
                        base64EncodedData: articleFeaturedImage
                    },
                    parts: parts
                })
            }
        );

        if (submissionResponse.ok) {
            alert("Artikel berhasil disimpan!");

            let id;
            if (mode === "edit") {
                id = articleGuid;
            }
            else {
                const responseData = await submissionResponse.json();
                id = responseData.id;
            }
            history.push(`/artikel/${id}`);
        }
        else {
            console.error(submissionResponse);
        }
    }

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
                    <Button backgroundColor="#c6262e" style={{ marginRight: "1rem" }} onClick={confirmExit}>Batalkan perubahan</Button>
                    <Button backgroundColor="#68b723" onClick={testHandleSubmit}>Simpan artikel</Button>
                </div>
            </div>
            <section className={viewer.heroSection}>
                <div className={viewer.heroTexts}>
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
                <img
                    className={[
                        viewer.heroImage,
                        editor.heroImageUpload
                    ].join(" ")}
                    src={articleFeaturedImage || "hey you aren't supposed to read this (this is a workaround so that the ::before pseudoelement can be shown)"}
                    alt="Hero image"
                    onClick={ev => {
                        ev.target.nextSibling.click();
                    }}
                />
                <input type="file" accept="image/*" style={{ display: "none" }} onChange={ev => {
                    const fileReader = new FileReader();
                    fileReader.onloadend = event => {
                        setArticleFeaturedImage(event.target.result);
                    };
                    fileReader.readAsDataURL(ev.target.files[0]);
                }} />
            </section>
            {parts.map((part, i) => (
                <section className={viewer.partCard}>
                    <div className={viewer.partHeading}>
                        <div className={editor.partActions}>
                            <h2 className={viewer.partNumber}>Bagian {part.partNumber || i + 1}</h2>
                            <Button onClick={() => {
                                const prev = [...parts];
                                prev.splice(i, 1);
                                setParts(prev);
                            }} backgroundColor="red">Hapus part ini</Button>
                        </div>

                        <Editable
                            className={viewer.partTitle}
                            placeholder={`Penjelasan part ke-${i + 1}`}
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
                                <img
                                    className={[
                                        viewer.stepImage,
                                        step.image.base64EncodedData ? null : editor.stepImageUpload
                                    ].join(" ")}
                                    src={step.image.base64EncodedData || "hey, you aren't supposed to be here / read this"}
                                    onClick={ev => {
                                        // console.log(ev);
                                        ev.target.nextSibling.click();
                                    }}
                                />
                                <input type="file" accept="image/*" style={{ display: "none" }} onChange={ev => {
                                    const fileReader = new FileReader();
                                    fileReader.onloadend = event => {
                                        const prevParts = [...parts];
                                        // console.log(prev[i]);
                                        prevParts[i].steps[j].image.base64EncodedData = event.target.result;
                                        console.log(event.target.result);
                                        setParts(prevParts);
                                    };
                                    fileReader.readAsDataURL(ev.target.files[0]);
                                    // console.log("yo");
                                }} />
                                <div className={viewer.stepExplanationWrapper}>
                                    <div className={viewer.stepNumberMarker}>{step.stepNumber || j + 1}</div>
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
                </section>
            ))}
            <section role="button" className={`${viewer.partCard} ${editor.addPartButton}`} onClick={() => {
                const prevPartsArr = [...parts];
                prevPartsArr.push(blankPartItem);
                setParts(prevPartsArr);
            }}>
                + Tambah bagian (part) baru
            </section>
        </article>
    )
}

export default ArticleEditorPage;