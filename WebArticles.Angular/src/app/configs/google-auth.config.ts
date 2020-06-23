import { AuthConfig } from "angular-oauth2-oidc";

export const authConfig: AuthConfig = {
    issuer: 'https://accounts.google.com',
    redirectUri: window.location.origin + '/main',
    clientId: "309087529769-a9dcutl25531q0ollm1jh47ab7mnuqk0.apps.googleusercontent.com",
    scope: 'openid profile email',
    strictDiscoveryDocumentValidation: false,
    skipIssuerCheck: true,
    showDebugInformation: true
}
