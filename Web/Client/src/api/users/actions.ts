import { editObj, fetchObj } from "..";
import { IImage } from "../../models/image";
import { IUser } from "../../models/user";
import {
  mutationChangeProfilePicture,
  mutationEditUser,
  queryCurrentUser,
} from "./queries";

export const fetchCurrentUser = async (network?: boolean) => {
  return await fetchObj<IUser>(queryCurrentUser, "current", network);
};

export const editUser = async (id: number, user: IUser) => {
  var variables = new Map<string, any>();
  variables = variables.set("id", id);
  variables = variables.set("user", user);

  return await editObj<IUser>(mutationEditUser, "edituser", variables);
};

export const editProfilePicture = async (id: number, image: string) => {
  var variables = new Map<string, any>();
  variables = variables.set("id", id);
  variables = variables.set("image", image);

  return await editObj<IImage>(
    mutationChangeProfilePicture,
    "editprofilepicture",
    variables
  );
};
