import styled, { css } from "styled-components";

export default styled.div`
    border: 0.25rem solid ${props => props.borderColor || "black"};
    background-color: white;

    ${props => props.width ? css`width: ${props.width};` : null}
    ${props => props.height ? css`height: ${props.height};` : null}
`;