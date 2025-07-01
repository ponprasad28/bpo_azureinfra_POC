import React from "react";
import { useIsAuthenticated, useMsal } from "@azure/msal-react";
import { useNavigate } from "react-router-dom";

const Navbar = () => {
  const { instance } = useMsal();
  const isAuthenticated = useIsAuthenticated();
  const navigate = useNavigate();

  const handleLogin = async () => {
    try {
      await instance.loginPopup();
      navigate("/protected"); // Redirect to protected page after login
    } catch (error) {
      console.error("Login failed", error);
    }
  };

  const handleLogout = async () => {
    await instance.logoutPopup();
    navigate("/"); // Redirect to home after logout
  };

  return (
    <nav style={{ padding: "10px", borderBottom: "1px solid #ccc" }}>
      <span style={{ marginRight: "10px" }}>My App</span>
      {!isAuthenticated ? (
        <button onClick={handleLogin}>Login</button>
      ) : (
        <button onClick={handleLogout}>Logout</button>
      )}
    </nav>
  );
};

export default Navbar;
