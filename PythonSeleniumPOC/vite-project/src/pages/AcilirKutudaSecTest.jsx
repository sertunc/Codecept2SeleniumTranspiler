import { useState } from "react";

export default function AcilirKutudaSecTest() {
    const [selectedOption, setSelectedOption] = useState("");

    const handleSelectChange = (e) => {
        setSelectedOption(e.target.value);
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
            <h1>Seçim Yap</h1>
            <select
                id="dropdown"
                value={selectedOption}
                onChange={handleSelectChange}
                style={{ marginBottom: "10px", padding: "8px", width: "200px" }}
            >
                <option value="">Bir seçenek seçin</option>
                <option value="option1">Seçenek 1</option>
                <option value="option2">Seçenek 2</option>
                <option value="option3">Seçenek 3</option>
            </select>
            <label
                id="selectedOptionLabel"
                style={{ marginTop: "20px", fontSize: "16px", color: "blue" }}
            >
                {selectedOption ? `Seçilen seçenek: ${selectedOption}` : "Henüz bir seçenek seçilmedi."}
            </label>
        </div>
    );
}
