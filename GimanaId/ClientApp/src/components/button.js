import styled from "styled-components";

// FIXME/TODO: Apply box-shadow to a pseudoelement instead to avoid performance issues
const initialBoxShadow = "0 0 0 0.15em inset rgb(0 0 0 / 0.1)";

export default styled.button`
    display: block;
    border: none;
    outline: none;

    margin: 0;
    padding: 0.5em 1em;

    font-size: inherit;
    font-family: inherit;
    font-weight: 500;

    background-color: ${props => props.backgroundColor ? props.backgroundColor : "skyblue"};
    color: ${props => props.textColor ? props.textColor : "white"};
    box-shadow: ${initialBoxShadow}, 0 0.1em 0.25em rgb(0 0 0 / 0.25);

    cursor: pointer;
    transition: filter 0.1s, box-shadow 0.4s, transform 0.15s;

    &:hover {
        filter: brightness(1.1);
        box-shadow: ${initialBoxShadow}, 0 0.25em 0.5em rgb(0 0 0 / 0.5);
    }

    &:active {
        filter: brightness(0.9);
        transform: translateY(5%);
    }
`;