export interface UserModel {
  id?: string;
  username?: string;
  email: string;
  password?: string;
  birthDate?: string;
  gender?: 'm' | 'f' | 'n';
  country?: string;
  roles?: string[];
  createdAt?: string;
}
