interface MovieDTO {
  description: string;
  id: string;
  title: string;
}

export interface MemberModel {
  id?: string;
  name: string;
  description: string;
  headshotUrl: string;
  birthDate: Date | string;
  role: string;
  movies: MovieDTO[];
}
