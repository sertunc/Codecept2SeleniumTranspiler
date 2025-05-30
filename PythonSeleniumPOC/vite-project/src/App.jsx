import { useState } from "react";
import "./App.css";

function App() {
  const [username, setUsername] = useState("");
  const [password, setPassword] = useState("");
  const [message, setMessage] = useState("");
  const [gizlenmesiniBekle, setGizlenmesiniBekle] = useState(true);
  const [gorunmesiniBekle, setGorunmesiniBekle] = useState(false);
  const [checked, setChecked] = useState(false);
  const [selectedOption, setSelectedOption] = useState("");
  const [hoverMessage, setHoverMessage] = useState(false);
  const handleMouseEnter = () => setHoverMessage(true);
  const handleMouseLeave = () => setHoverMessage(false);

  const handleLogin = () => {
    if (username === "sertunc" && password === "123") {
      setMessage("Giriş Başarılı");
    } else {
      setMessage("Hatalı Giriş");
    }
  };

  const handleGizlenmesiniBekle = () => {
    setTimeout(() => {
      setGizlenmesiniBekle(false);
    }, 2000); // 2 saniye sonra mesaj görünür hale gelir
  };

  const handleGorunmesiniBekle = () => {
    setTimeout(() => {
      setGorunmesiniBekle(true);
    }, 2000); // 2 saniye sonra mesaj görünür hale gelir
  };

  const handleCheckboxChange = () => {
    setChecked(!checked);
  };

  const handleSelectChange = (e) => {
    setSelectedOption(e.target.value);
  };

  return (
    <>
      <div
        style={{
          display: "flex",
          flexDirection: "column",
          alignItems: "center",
          marginTop: "50px",
        }}
      >
        <h1>Giriş Yap</h1>
        <input
          id="username"
          type="text"
          placeholder="Kullanıcı Adı"
          value={username}
          onChange={(e) => setUsername(e.target.value)}
          style={{ marginBottom: "10px", padding: "8px", width: "200px" }}
        />
        <input
          id="password"
          type="password"
          placeholder="Parola"
          value={password}
          onChange={(e) => setPassword(e.target.value)}
          style={{ marginBottom: "10px", padding: "8px", width: "200px" }}
        />
        <button id="btnLogin" onClick={handleLogin} style={{ padding: "8px 16px" }}>
          Giriş
        </button>
        <label
          id="lblResult"
          style={{
            marginTop: "20px",
            fontSize: "16px",
            color: message === "Giriş Başarılı" ? "green" : "red",
          }}
        >
          {message}
        </label>
      </div>
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
          İmleci Üzerime Getir
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
    </>
  );
}

export default App;
