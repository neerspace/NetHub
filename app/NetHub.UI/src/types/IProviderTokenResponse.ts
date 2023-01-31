export default interface IProviderTokenResponse {
  email: string,
  emailVerified: boolean,
  firstName?: string,
  lastName?: string,
  photoUrl: string | null,
  localId: string
}
