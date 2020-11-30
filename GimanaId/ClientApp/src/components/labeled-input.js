import styled from "styled-components";

const Wrapper = styled.div`
    margin: 0.5em;
    width: 100%;
`;

const Label = styled.label`
    display: block;

    font-size: inherit;

    margin: 0;
    margin-bottom: 0.25em;
`;

const Input = styled.input`
    display: block;
    font-family: inherit;
    width: 100%; /* FIXME? */

    &[type=text], &[type=password], &[type=email] {
        border: none;
        padding: 0.8em;
        background-color: rgb(0 0 0 / 0.075);

        transition: background-color 0.2s, box-shadow 0.1s;

        &:focus {
            /* Some resets */
            border: none;
            outline: none;

            box-shadow: 0 0 0.05em 0.1em #3399D2;
            background-color: white;
        }
    }
`;

export default ({ name, title, type, ...rest }) => (
    <Wrapper>
        <Label htmlFor={name}>{title}</Label>
        <Input name={name} type={type} {...rest} />
    </Wrapper>
);