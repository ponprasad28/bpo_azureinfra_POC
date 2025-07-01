// src/auth/msalHelper.ts
import { AccountInfo, InteractionRequiredAuthError, PublicClientApplication } from "@azure/msal-browser";
import { loginRequest } from "../authConfig"; // contains scopes
import { msalInstance } from "./msalInstance"; // your singleton

export const getAccessToken = async (): Promise<string> => {
    const accounts = msalInstance.getAllAccounts();
    if (accounts.length === 0) throw new Error("No user account found");

    try {
        const result = await msalInstance.acquireTokenSilent({
            ...loginRequest,
            account: accounts[0]
        });
        return result.accessToken;
    } catch (error) {
        if (error instanceof InteractionRequiredAuthError) {
            const result = await msalInstance.acquireTokenPopup(loginRequest);
            return result.accessToken;
        } else {
            throw error;
        }
    }
};

export const getSignedInUser = (): AccountInfo | null => {
    const accounts = msalInstance.getAllAccounts();
    return accounts.length > 0 ? accounts[0] : null;
};
