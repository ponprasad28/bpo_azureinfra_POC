import React, { useEffect } from "react";
import { BrowserRouter as Router, Routes, Route, Navigate, useNavigate } from "react-router-dom";
import Layout from "./Layout";
import HomePage from "./Pages/HomePage";
import ProtectedPage from "./Pages/ProtectedPage";
import { useIsAuthenticated } from "@azure/msal-react";

// Custom wrapper for protected route logic
const ProtectedRoute = () => {
  const isAuthenticated = useIsAuthenticated();
  return isAuthenticated ? <ProtectedPage /> : <Navigate to="/" replace />;
};

function App() {
  return (
    <Router>
      <Routes>
        <Route path="/" element={<Layout />}>
          <Route index element={<HomePage />} />
          <Route path="protected" element={<ProtectedRoute />} />
        </Route>
      </Routes>
    </Router>
  );
}

export default App;
