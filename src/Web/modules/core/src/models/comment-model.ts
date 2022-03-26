interface CommenterDTO {
  id: string;
  name: string;
  birthDate?: Date;
  gender?: string;
  country?: string;
}

export interface CommentModel {
  id?: string;
  movieId: string;
  commenterId?: string;
  text: string;
  createdAt?: string;
  commenter?: CommenterDTO;
}
