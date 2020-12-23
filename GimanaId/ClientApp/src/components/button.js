import styled from "styled-components";
import { button } from "./button.module.css";

// FIXME/TODO: Apply box-shadow to a pseudoelement instead to avoid performance issues
const initialBoxShadow = "0 0 0 0.15em inset rgb(0 0 0 / 0.1)";

const oldButton = styled.button`
    display: block;
    border: none;

    margin: 0;
    padding: 0.5em 1em;
    border-radius: 0.2rem;

    font-size: inherit;
    font-family: "Barlow", "Nunito Sans", sans-serif;
    font-weight: 600;

    background-color: ${props => props.backgroundColor ? props.backgroundColor : "skyblue"};
    color: ${props => props.textColor ? props.textColor : "white"};
    box-shadow: ${initialBoxShadow}, 0 0.1em 0.25em rgb(0 0 0 / 0.25);

    &:not([disabled]) {
        cursor: pointer;
        transition: filter 0.1s, box-shadow 0.1s, transform 0.1s;

        &:hover, &:focus {
            box-shadow: 0 0 0 0.15em inset rgb(255 255 255 / 0.5), /*${initialBoxShadow}*/0 0.1em 0.25em rgb(0 0 0 / 0.25);
        }

        &:focus {
            outline: none;
        }

        /* &:hover {
            box-shadow: 0 0 0 0.15em inset rgb(255 255 255 / 0.5)/*, 0 0.25em 0.5em rgb(0 0 0 / 0.5);
        } */

        &:hover {
            filter: brightness(1.1);
        }

        &:active {
            filter: brightness(0.9);
            transform: translateY(2%);
        }
    }

    &[disabled] {
        background-color: grey;
        color: lightgrey;
    }
`;

const Button = ({ className, style, backgroundColor, textColor, ...props }) => (
    <button
        className={[button, className].join(" ")}
        style={{
            backgroundColor: backgroundColor || undefined,
            color: textColor || undefined,
            ...style
        }}
        {...props} />
);

export default Button;