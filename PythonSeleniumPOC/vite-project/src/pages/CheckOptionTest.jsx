import { useState } from "react";

export default function CheckOptionTest() {
    const [checked, setChecked] = useState(false);

    const handleCheckboxChange = () => {
        setChecked(!checked);
    };

    return (
        <div
            style={{
                display: "flex",
                flexDirection: "column",
                alignItems: "center",
                marginTop: "50px",
            }}
        >
            <label>
                <input id="checkbox1" type="checkbox" checked={checked} onChange={handleCheckboxChange} />
                Kullanım koşullarını kabul ediyorum
            </label>
        </div>
    );
}
