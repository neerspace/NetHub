export default interface IUserInfoResponse {
  id: number,
  userName: string,
  firstName: string,
  lastName: string,
  middleName?: string,
  email: string,
  profilePhotoUrl?: string,
  emailConfirmed: boolean,
  description?: string,
  registered: string
}

export interface IPrivateUserInfoResponse {
  id: number,
  userName: string,
  profilePhotoUrl?: string
}
