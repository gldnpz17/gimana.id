import { useState, useRef, useEffect } from "react";

const TestArticleCollectionsPage = () => {
    const blanks = {
        meta: {
            title: "",
            description: ""
        }
    };

    blanks.step = {
        // stepNumber: null,
        title: "",
        description: ""
    };

    blanks.part = {
        // partNumber: null,
        title: "",
        description: "",
        steps: [blanks.step]
    };

    const [mainArticleData, setMainArticleData] = useState(blanks.meta);
    const [parts, setParts] = useState([blanks.part]);

    const [particularTextAreaHeight, setParticularTextAreaHeight] = useState("auto");
    // const descTextArea = useRef(); // reminder: use ref for the timeout state in signup page

    // useEffect(() => {
    //     // setParticularTextAreaHeight("auto"); // Find out why we need this
    //     setParticularTextAreaHeight(descTextArea.current.scrollHeight + "px");
    // }, [mainArticleData.description]);


    // Set one part of the whole state object/value
    function partiallyUpdateStateObject(stateSetterFunction, newProperty) {
        stateSetterFunction(unchangedValues => ({
            ...unchangedValues,
            ...newProperty
        }));
    }

    const [numberofRows, setNumberofRows] = useState(1);

    function addPart() {
        const currentParts = [...parts];
        currentParts.push(blanks.part);
        setParts(currentParts);
    }

    function addStep(partIndexNumber) {
        const currentParts = [...parts];
        currentParts[partIndexNumber].steps.push(blanks.step);
        setParts(currentParts);
    }

    async function handleSubmit(ev) {
        ev.preventDefault();
        console.log("Submitting...");

        const submissionResponse = await fetch(`api/articles`, {
            method: "POST",
            headers: {
                accept: "application/json",
                "Content-Type": "application/json"
            },
            body: JSON.stringify({
                ...mainArticleData,
                parts: parts
            })
        });

        if (submissionResponse.ok) {
            console.log("Article submitted successfully!");
            setMainArticleData(blanks.meta);
            setParts([blanks.part]);
        }
        else {
            console.log(submissionResponse);
        }
    }

    return (
        <div>
            <div className="articles-collection-container">
                <form onSubmit={handleSubmit}>
                    <label>Judul
                        <input type="text" required value={mainArticleData.title} onChange={ev => {
                            partiallyUpdateStateObject(setMainArticleData, { title: ev.target.value });
                        }} />
                    </label>
                    <label>Deskripsi
                        <textarea style={{ height: particularTextAreaHeight }} value={mainArticleData.description} onChange={ev => {
                            // setParticularTextAreaHeight("auto");
                            // ev.target.style.height = "auto";
                            partiallyUpdateStateObject(setMainArticleData, { description: ev.target.value });
                            // setParticularTextAreaHeight(ev.target.scrollHeight + "px");
                            // ev.target.style.height = ev.target.scrollHeight + 4 + "px";

                            // const?

                            if (ev.target.style.height !== ev.target.scrollHeight + 4 + "px") {
                                ev.target.style.height = "auto";
                                ev.target.style.height = ev.target.scrollHeight + 4 + "px";
                            }

                            console.log(ev);
                        }} />
                    </label>
                    <ul>
                        {parts.map((part, partIndex) => (
                            // FIXME on the `key` prop (I don't know if using the index number alone is suitable)
                            <li style={{ display: `block`, border: `2px solid black`, padding: `1em` }}>
                                <h1>Part {partIndex + 1}</h1>
                                <label>Judul part:
                                    <input type="text" required value={part.title} onChange={ev => {
                                        const val = [...parts];
                                        val[partIndex].title = ev.target.value;
                                        setParts(val);
                                    }} />
                                </label>
                                <label>Deskripsi:
                                    <textarea style={{ display: "block", resize: "none" }} rows={numberofRows} value={part.description} onChange={ev => {
                                        const val = [...parts];
                                        val[partIndex].description = ev.target.value;
                                        setParts(val);
                                        console.log(ev.target.scrollHeight);
                                        setNumberofRows(Math.floor(ev.target.scrollHeight / 19));
                                    }} />
                                </label>
                                <ul>
                                    {part.steps.map((step, stepIndex) => (
                                        <li>
                                            <h2>Step {stepIndex + 1}</h2>
                                            <label>Judul step:
                                                <input type="text" required value={step.title} onChange={ev => {
                                                    const partsVal = [...parts];
                                                    partsVal[partIndex].steps[stepIndex].title = ev.target.value;
                                                    setParts(partsVal);
                                                }} />
                                            </label>
                                            <label>Deskripsi step:
                                                <textarea value={step.description} onChange={ev => {
                                                    const partsVal = [...parts];
                                                    partsVal[partIndex].steps[stepIndex].description = ev.target.value;
                                                    setParts(partsVal);
                                                }} />
                                            </label>
                                        </li>
                                    ))}
                                </ul>
                                <button type="button" onClick={() => { addStep(partIndex) }}>Add step</button>
                            </li>
                        ))}
                    </ul>
                    <button type="button" onClick={addPart}>Add part</button>
                    <button>Save article</button>
                </form>
            </div>
        </div>
    );
}

export default TestArticleCollectionsPage;