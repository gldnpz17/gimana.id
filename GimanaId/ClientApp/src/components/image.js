import { useState, useRef, useEffect } from "react";

import styles from "./image.module.css";

const Image = ({ onLoad, className, ...props }) => {
    // The stathe whether the image has been loaded or not
    const [isLoaded, setLoaded] = useState(false);

    // [on]ImageCompletedLoading / onImageFinishedLoading
    function onImageLoad(ev) {
        setLoaded(true);

        if (onLoad) {
            onLoad(ev);
        }
    }

    // Manual override in case the image has been cached/loaded in the first place
    const imgRef = useRef();
    useEffect(() => {
        if (imgRef.current?.complete && !isLoaded) {
            setLoaded(true);
        }
    }, []);

    const classes = [styles.imageElement]; /*[
        isLoaded ? null : "loading",
        className ? className : null
    ].join(" ");*/

    if (!isLoaded) {
        classes.push(styles.loading);
    }
    
    if (className) {
        classes.push(className);
    }

    return (
        <img className={classes.join(" ")} onLoad={onImageLoad} {...props} />
    );
};

export default Image;