export const msalConfig = {
  auth: {
    clientId: 'fe32b8b3-2f08-46ae-90bf-ad4ba320145a',
    authority: 'https://login.microsoftonline.com/46128a69-6beb-4b94-a807-e8e4aea19b5f',
    redirectUri: 'http://localhost:3000', // or your deployed URI
  },
    cache: {
    cacheLocation: "sessionStorage",
    storeAuthStateInCookie: false
  }
};

export const loginRequest = {
  scopes: ['api://a01c063c-3c58-4563-b93d-0ec07b33a93b/access_as_user'],
};
