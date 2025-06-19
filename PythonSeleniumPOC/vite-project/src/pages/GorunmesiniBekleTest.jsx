import { useState } from "react";

export default function GorunmesiniBekleTest() {
    const [gorunmesiniBekle, setGorunmesiniBekle] = useState(false);

    const handleGorunmesiniBekle = () => {
        setTimeout(() => {
            setGorunmesiniBekle(true);
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
            <h1>Görünmesini Bekle Test</h1>
            <button
                id="btnGorunmesiniBekle"
                onClick={handleGorunmesiniBekle}
                style={{ padding: "8px 16px" }}
            >
                Mesajı Göster
            </button>
            {gorunmesiniBekle && (
                <div
                    id="divGorunmesiniBekle"
                    style={{ marginTop: "20px", fontSize: "16px", color: "green" }}
                >
                    Bu bir test mesajıdır.
                </div>
            )}
        </div>
    );
}
