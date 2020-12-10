import { useState } from "react";

import styles from "./labeled-input.module.css";

const LabeledInput = ({ name,
                        title,
                        type,
                        inputRef,
                        customError, // or `customMessage` or `accompanyingMessage`?
                        onBlur,
                        ...rest }) => {
    const [showInvalidity, setShowInvalidity] = useState(false);

    return (
        <div className={styles.wrapper}>
            <label className={styles.accompanyingLabel} htmlFor={name}>{title}</label>
            <input
                className={[styles.authInputField, showInvalidity ? styles.showInvalidity : null].join(" ")}
                name={name}
                type={type}
                ref={inputRef}
                onBlur={ev => {
                    showInvalidity || setShowInvalidity(true);
                    onBlur && onBlur(ev);
                }}
                {...rest}
            />
            <p className={[styles.accompanyingMessage, styles.invalidMessage].join(" ")}>Isian tidak valid</p>
            {customError ?
                <p
                    className={styles.accompanyingMessage}
                    style={{ color: customError.color ?? `grey` }}
                >
                    {customError.message}
                </p>
            : null}
        </div>
    );
}

export default LabeledInput;