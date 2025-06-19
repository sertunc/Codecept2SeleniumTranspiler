import { useState } from "react";

export default function GizlenmesiniBekleTest() {
    const [gizlenmesiniBekle, setGizlenmesiniBekle] = useState(true);

    const handleGizlenmesiniBekle = () => {
        setTimeout(() => {
            setGizlenmesiniBekle(false);
        }, 2000); // 2 saniye sonra mesaj görünür hale gelir
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
            <h1>Gizlenmesini Bekle Test</h1>
            <button
                id="btnGizlenmesiniBekle"
                onClick={handleGizlenmesiniBekle}
                style={{ padding: "8px 16px" }}
            >
                Mesajı Gizle
            </button>
            {gizlenmesiniBekle && (
                <div
                    id="divGizlenmesiniBekle"
                    style={{ marginTop: "20px", fontSize: "16px", color: "green" }}
                >
                    Bu bir test mesajıdır.
                </div>
            )}
        </div>
    );
}
