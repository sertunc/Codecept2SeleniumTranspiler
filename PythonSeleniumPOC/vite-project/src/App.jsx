import { Routes, Route, useNavigate } from "react-router-dom";
import SayfadaOlmaliTest from "./pages/SayfadaOlmaliTest";
import GizlenmesiniBekleTest from "./pages/GizlenmesiniBekleTest";
import GorunmesiniBekleTest from "./pages/GorunmesiniBekleTest";
import CheckOptionTest from "./pages/CheckOptionTest";
import AcilirKutudaSecTest from "./pages/AcilirKutudaSecTest";
import ImleciUzerineGetirTest from "./pages/ImleciUzerineGetirTest";

function HomePage() {
  const navigate = useNavigate();

  const handleClick = () => {
    const buttonId = document.activeElement.id;
    if (buttonId === "") {
      alert("button id bulunamadı.");
      return;
    }

    navigate(`/${buttonId}`);
  };

  return (
    <>
      <h2>Test Listesi</h2>
      <div style={{ display: "flex", flexDirection: "column", gap: "15px" }}>
        <button id="sayfada-olmali-test" onClick={handleClick}>Sayfada Olmalı Test</button>
        <button id="gizlenmesini-bekle-test" onClick={handleClick}>Gizlenmesini Bekle Test</button>
        <button id="gorunmesini-bekle-test" onClick={handleClick}>Gorunmesini Bekle Test</button>
        <button id="check-option-test" onClick={handleClick}>Check Option Test</button>
        <button id="acilir-kutuda-sec-test" onClick={handleClick}>Acilir Kutuda Sec Test</button>
        <button id="imleci-uzerine-getir-test" onClick={handleClick}>Imleci Üzerine Getir Test</button>
      </div>
    </>
  );
}

export default function App() {
  return (
    <Routes>
      <Route path="/" element={<HomePage />} />
      <Route path="/sayfada-olmali-test" element={<SayfadaOlmaliTest />} />
      <Route path="/gizlenmesini-bekle-test" element={<GizlenmesiniBekleTest />} />
      <Route path="/gorunmesini-bekle-test" element={<GorunmesiniBekleTest />} />
      <Route path="/check-option-test" element={<CheckOptionTest />} />
      <Route path="/acilir-kutuda-sec-test" element={<AcilirKutudaSecTest />} />
      <Route path="/imleci-uzerine-getir-test" element={<ImleciUzerineGetirTest />} />
    </Routes>
  );
}
