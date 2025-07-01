// Layout.tsx
import React from "react";
import Navbar from "./components/Navbar";
import { Outlet } from "react-router-dom";

const Layout = () => {
  return (
    <div>
      <Navbar />
      <div style={{ padding: "20px" }}>
        <Outlet /> {/* Equivalent to @RenderBody() */}
      </div>
    </div>
  );
};

export default Layout;
