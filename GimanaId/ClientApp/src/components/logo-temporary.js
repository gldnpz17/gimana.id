import styled from "styled-components";
import { Link } from "react-router-dom";

const LinkWrapper = styled(Link)`
    display: block;
    margin: 0 1em !important; /* FIXME */

    h1 {
        margin: 0;

        font-family: "Fredoka One";
        font-weight: normal;
        
        color: #3399D2;
    }

    &, &:hover {
        text-decoration: none !important; /* FIXME */
    }

    &:hover {
        filter: brightness(1.1);
    }
`;

export default () => (
    <LinkWrapper to="/">
        <h1>Gimana.id</h1>
    </LinkWrapper>
);