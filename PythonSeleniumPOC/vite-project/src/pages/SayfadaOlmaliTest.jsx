import { useState } from "react";

export default function SayfadaOlmaliTest() {
  const [username, setUsername] = useState("");
  const [password, setPassword] = useState("");
  const [message, setMessage] = useState("");

  const handleLogin = () => {
    if (username === "sertunc" && password === "123") {
      setMessage("Giriş Başarılı");
    } else {
      setMessage("Hatalı Giriş");
    }
  };


  return (
    <div style={{ display: "flex", flexDirection: "column" }}>
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
  );
}
