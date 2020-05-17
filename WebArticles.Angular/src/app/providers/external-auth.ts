import { AuthServiceConfig, GoogleLoginProvider, FacebookLoginProvider } from "angularx-social-login";

let config = new AuthServiceConfig([
    {
      id: GoogleLoginProvider.PROVIDER_ID,
      provider: new GoogleLoginProvider("309087529769-7357dag56agntfingo03fjlpe3e92i00.apps.googleusercontent.com")
    },
    {
      id: FacebookLoginProvider.PROVIDER_ID,
      provider: new FacebookLoginProvider("2623267997891850")
    }
  ])

export function provideConfig() {
    return config;
}

export const ExternalAuthProvider = {
    provide: AuthServiceConfig, useFactory: provideConfig
}
