import styled from "styled-components";

const Wrapper = styled.h1`
    font-family: "Fredoka One";
    font-weight: normal;

    display: block;
    margin: 0 1em;

    color: darkgreen;
`;

const Logo = () => (
    <Wrapper>Gimana.id</Wrapper>
);

export default Logo;