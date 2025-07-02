import React from "react";
import { useIsAuthenticated, useMsal } from "@azure/msal-react";
import { useNavigate } from "react-router-dom";
import { loginRequest } from "../authConfig";
import { LogInfoDTO } from "../DTO/LogInfoDTO";
import { getAccessToken, getSignedInUser } from "../auth/msalHelper";


const Navbar = () => {
  const { instance } = useMsal();
  const isAuthenticated = useIsAuthenticated();
  const navigate = useNavigate();

  const handleLogin = async () => {
    try {
      await instance.loginPopup();

      const user = getSignedInUser();
      if (!user) throw new Error("User not found after login");

      const accessToken = await getAccessToken();

      const dto = {
        userName: user.name ?? user.username,
        userEmail: user.username,
        loginTime: new Date().toISOString(),
        loginFrom: 2
      };

      const response = await fetch("https://backendbpo-atcjd6ahftgyejcu.canadacentral-01.azurewebsites.net/api/loginfo", {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
          "Authorization": `Bearer ${accessToken}`
        },
        body: JSON.stringify(dto)
      });

      if (!response.ok) {
        const errorText = await response.text();
        throw new Error(`Backend error: ${response.status} - ${errorText}`);
      }

      const result = await response.text();
      console.log("Backend response:", result);


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
