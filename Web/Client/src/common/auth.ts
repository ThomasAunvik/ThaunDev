import {
  UserManager,
  UserManagerSettings,
  WebStorageStateStore,
} from "oidc-client";

export const getClientSettings = (): UserManagerSettings => ({
  authority: process.env.REACT_APP_AUTH,
  client_id: "thaun-dev-web",
  redirect_uri: process.env.REACT_APP_APP,
  post_logout_redirect_uri: process.env.REACT_APP_APP,
  response_type: "code",
  scope: "openid profile email thaun-dev-api roles",
  filterProtocolClaims: true,
  loadUserInfo: true,
  automaticSilentRenew: true,
  silent_redirect_uri: process.env.REACT_APP_APP + "/silent-refresh",
  userStore: new WebStorageStateStore({ store: localStorage }),
  revokeAccessTokenOnSignout: true,
  prompt: "login",
});

export const userManager = new UserManager(getClientSettings());

export const validateAuth = async () => {
  const user = await userManager.getUser();
  return user;
};

export const startAuthentication = async () => {
  await userManager.removeUser();
  await userManager.clearStaleState();

  await userManager.signinRedirect();
};

export const completeAuthentication = async () => {
  var user = await userManager.getUser();
  if (!user) {
    user = await userManager.signinCallback();
  }

  return user;
};

export const logout = async () => {
  await userManager.signoutRedirect();
  await userManager.removeUser();
};

export const renewToken = async () => {
  return await userManager.signinSilent();
};

export const signinSilent = async () => {
  return await userManager.signinSilentCallback();
};
