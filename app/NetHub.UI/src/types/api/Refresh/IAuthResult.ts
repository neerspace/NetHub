export default interface IAuthResult {
  username: string,
  firstName: string,
  lastName?: string,
  token: string,
  tokenExpires: string,
  refreshTokenExpires: string,
  profilePhotoUrl: string | null
}
