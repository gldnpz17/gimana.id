import logoStyles from "./logo.module.css";

const GimanaIdMainLogo = ({ className, ...props }) => (
    <div role="img" aria-label="Gimana.id" className={className}>
        <div
            id={logoStyles.gi}
        >Gi</div><div
            id={logoStyles.ma}
        >ma</div><div
            id={logoStyles.na}
        >na</div><div
            id={logoStyles.dotId}
        >.id</div>
    </div>
);

export default GimanaIdMainLogo;