import React, { useEffect, useState } from "react";
import { useMsal } from "@azure/msal-react";
import { loginRequest } from "../authConfig";
import { LogInfoDTO } from "../DTO/LogInfoDTO";

const ProtectedPage = () => {
  const {instance, accounts } = useMsal();
  const [logins, setLogins] = useState<LogInfoDTO[]>([]);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    const fetchData = async () => {
      try {
        const result = await instance.acquireTokenSilent({
          ...loginRequest,
          account: accounts[0],
        });

        const response = await fetch("https://backendbpo-atcjd6ahftgyejcu.canadacentral-01.azurewebsites.net/api/loginfo", {
          headers: {
            Authorization: `Bearer ${result.accessToken}`,
          },
        });

        if (!response.ok) {
          throw new Error("Failed to fetch data");
        }

        const data = await response.json();
        setLogins(data);
      } catch (error) {
        console.error("Error fetching data:", error);
      } finally {
        setLoading(false);
      }
    };

    fetchData();
  }, [instance, accounts]);

  return (
    <div>
      {loading ? (
        <p>Loading data...</p>
      ) : (
        <table border={1} cellPadding={8} style={{ width: "100%", marginTop: "20px" }}>
          <thead>
            <tr>
              <th>User Name</th>
              <th>Email</th>
              <th>Login Time</th>
              <th>Login From</th>
            </tr>
          </thead>
          <tbody>
            {logins.map((log, index) => (
              <tr key={index}>
                <td>{log.userName}</td>
                <td>{log.userEmail}</td>
                <td>{new Date(log.loginTime).toLocaleString()}</td>
                <td>{log.loginFrom === 1 ? 'Revit' : 'Website'}</td>
              </tr>
            ))}
          </tbody>
        </table>
      )}
    </div>
  );
};

export default ProtectedPage;
