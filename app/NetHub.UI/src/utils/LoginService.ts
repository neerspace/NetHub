import { auth, facebookProvider, googleProvider } from '../api/firebase';
import { signInWithPopup } from 'firebase/auth';
import IProviderTokenResponse from '../types/IProviderTokenResponse';
import { SsoRequest } from '../types/schemas/Sso/SsoSchema';
import { ProviderType } from '../api/_api';


export default class LoginService {
  static async ProviderHandle(provider: ProviderType): Promise<SsoRequest> {
    switch (provider) {
      case ProviderType.Google:
        return await LoginService.googleHandle();
      case ProviderType.Telegram:
        return await LoginService.telegramHandle();
      case ProviderType.Facebook:
        return await LoginService.facebookHandle();
      default:
        throw new Error('Such provider doesn\'t implemented yet');
    }
  }

  private static async googleHandle(): Promise<SsoRequest> {
    googleProvider.addScope('profile');
    googleProvider.addScope('email');
    const credential = await signInWithPopup(auth, googleProvider);
    //@ts-ignore
    const tokenResponse: IProviderTokenResponse = credential._tokenResponse;

    return {
      username: tokenResponse.email.replace(/@.*$/, ''),
      firstName: tokenResponse.firstName ?? '',
      lastName: tokenResponse.lastName ?? '',
      middleName: null,
      profilePhotoUrl: tokenResponse.photoUrl,
      email: tokenResponse.email,
      providerMetadata: {
        //@ts-ignore
        token: credential._tokenResponse.oauthIdToken,
      },
      providerKey: tokenResponse.localId,
      provider: ProviderType.Google,
    };
  }

  private static async telegramHandle(): Promise<SsoRequest> {

    return new Promise((resolve, reject) => {
      window.Telegram.Login.auth(
        { bot_id: import.meta.env.VITE_TELEGRAM_BOT_ID, request_access: true },
        (data: any) => {
          console.log(data);
          if (!data) {
            console.log('error');
            reject('Telegram login failed');
          }

          setTimeout(() => {
            const request: SsoRequest = {
              username: data.username ?? '',
              firstName: data.first_name ?? '',
              lastName: data.last_name ?? '',
              middleName: null,
              profilePhotoUrl: data.photo_url,
              email: data.email ?? '',
              providerMetadata: {
                id: data.id.toString(),
                username: data.username,
                auth_date: data.auth_date.toString(),
                hash: data.hash,
                first_name: data.first_name ?? null,
                last_name: data.last_name ?? null,
                photo_url: data.photo_url ?? null,
              },
              provider: ProviderType.Telegram,
              providerKey: data.id.toString()
            };
            resolve(request);
          }, 100);

          // resolve({username: data.username ?? data.first_name ?? data.last_name, profilePhoto: data.photo_url});
        }
      );
    });
  }

  private static async facebookHandle(): Promise<SsoRequest> {
    const credential = await signInWithPopup(auth, facebookProvider);

    //@ts-ignore
    const tokenResponse: IProviderTokenResponse = credential._tokenResponse;
    return {
      username: tokenResponse.email?.replace(/@.*$/, '') ?? '',
      firstName: tokenResponse.firstName ?? '',
      lastName: tokenResponse.lastName ?? '',
      middleName: null,
      profilePhotoUrl: tokenResponse.photoUrl,
      email: tokenResponse.email ?? null,
      providerMetadata: {
        //@ts-ignore
        token: credential._tokenResponse.oauthAccessToken,
      },
      providerKey: tokenResponse.localId,
      provider: ProviderType.Facebook,
    };
  }
}
