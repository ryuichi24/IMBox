export interface UserModel {
  id?: string;
  username: string;
  email: string;
  password: string;
  birthDate: Date;
  gender: 'm' | 'f' | 'n';
  country: string;
  roles: string[];
}
