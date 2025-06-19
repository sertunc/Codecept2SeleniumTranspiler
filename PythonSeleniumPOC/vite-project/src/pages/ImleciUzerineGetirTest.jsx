import { useState } from "react";

export default function ImleciUzerineGetirTest() {
    const [hoverMessage, setHoverMessage] = useState(false);
    const handleMouseEnter = () => setHoverMessage(true);
    const handleMouseLeave = () => setHoverMessage(false);

    return (
        <div
            style={{
                display: "flex",
                flexDirection: "column",
                alignItems: "center",
                marginTop: "50px",
            }}
        >
            <h1>İmleci Üzerine Getir Testi</h1>
            <button
                id="hoverButton"
                onMouseEnter={handleMouseEnter}
                onMouseLeave={handleMouseLeave}
                style={{ padding: "8px 16px" }}
            >
                İmleci Üzerine Getir
            </button>
            {hoverMessage && (
                <div
                    id="hoverMessage"
                    style={{ marginTop: "20px", fontSize: "16px", color: "orange" }}
                >
                    İmleç butonun üzerine geldi!
                </div>
            )}
        </div>
    );
}
