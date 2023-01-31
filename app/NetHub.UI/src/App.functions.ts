import {JWTStorage} from "./utils/localStorageProvider";
import {isAccessTokenExpired, isAccessTokenValid, isRefreshTokenValid} from "./utils/JwtHelper";
import jwtDecode from "jwt-decode";
import IJwtPayload from "./types/IJwtPayload";
import {userApi} from "./api/api";

// export async function check() {
//   if (isAccessTokenValid()) {
//     return jwtDecode<IJwtPayload>(JWTStorage.getAccessToken()!);
//   } else if (isAccessTokenExpired()) {
//     if (isRefreshTokenValid()) {
//       if (await userApi.refresh()) {
//         return jwtDecode<IJwtPayload>(JWTStorage.getAccessToken()!);
//       }
//     }
//   } else {
//     return null;
//   }
// }
