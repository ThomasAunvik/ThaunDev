import { IImage } from "./image";

export interface IUser {
  id: number;

  firstName: string;
  lastName: string;
  username: string;
  email: string;

  profilePicture: IImage;
}
