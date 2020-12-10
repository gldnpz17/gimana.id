import { useState } from "react";

const TestArticleCollectionsPage = () => {
    const blankValues = {
        title: "",
        description: "",
        parts: []
    };

    const [values, setValues] = useState(blankValues);

    // const []

    // Set one part of the whole state object/value
    function setOneProperty(property) {
        setValues(currentValues => ({ ...currentValues, ...property }));
    }

    function onTitleChange(ev) {
        setOneProperty({ title: ev.target.value });
    }

    function onDescriptionChange(ev) {
        setOneProperty({ description: ev.target.value });
    }

    function addPart() {
        const currentParts = values.parts;
        currentParts.push({
            title: "",
            description: "",
            steps: [
                {
                    title: "Title",
                    description: "Deskription"
                }
            ]
        });
        setOneProperty({ parts: currentParts })
    }

    async function handleSubmit(ev) {
        ev.preventDefault();
        console.log("Submitting...");
        console.log(JSON.stringify(values));

        const submissionResponse = await fetch(`api/articles`, {
            method: "POST",
            headers: {
                accept: "application/json",
                "Content-Type": "application/json"
            },
            body: JSON.stringify(values)
        });

        if (submissionResponse.ok) {
            console.log("Article submitted successfully!");
            setValues(blankValues);
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
                        <input type="text" required value={values.title} onChange={onTitleChange} />
                    </label>
                    <label>Deskripsi
                        <textarea value={values.description} onChange={onDescriptionChange} />
                    </label>
                    <ul>
                        {values.parts.map((entry, indexNumber) => (
                            <li style={{ display: `block`, border: `2px solid black`, padding: `1em` }}>
                                <h1>Part {indexNumber + 1}</h1>
                                <label>Judul part:
                                <input type="text" required value={entry.title} onChange={ev => {
                                        const val = [...values.parts];
                                        val[indexNumber].title = ev.target.value;
                                        setOneProperty({ parts: val });
                                    }} />
                                </label>
                                <label>Deskripsi:
                                <textarea value={entry.description} onChange={ev => {
                                        const val = [...values.parts];
                                        val[indexNumber].description = ev.target.value;
                                        setOneProperty({ parts: val });
                                    }} />
                                </label>
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