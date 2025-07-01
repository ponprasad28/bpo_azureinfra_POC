import React from "react";
import { useMsal } from "@azure/msal-react";

const ProtectedPage = () => {
  const { accounts } = useMsal();
  const user = accounts[0];
  
  return (
    <div>
      <h2>Welcome to the Protected Page!</h2>
      {user && (
        <p>
          Hello, <strong>{user.name}</strong> ({user.username})
        </p>
      )}
      <p>You can only see this if you're logged in.</p>
    </div>
  );
};

export default ProtectedPage;
