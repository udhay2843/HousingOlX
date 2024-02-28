export interface UserFromRegister {
  username: string;
  email?: string;
  password: string;
  mobile?: number;
}
export interface UserFromLogin {
  username: string;
  password: string;
  token:string;

}
