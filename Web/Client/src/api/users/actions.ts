import { editObj, fetchObj } from "..";
import { IUser } from "../../models/user";
import { mutationEditUser, queryCurrentUser } from "./queries";

export const fetchCurrentUser = async () => {
  return await fetchObj<IUser>(queryCurrentUser, "current");
};

export const editUser = async (id: number, user: IUser) => {
  var variables = new Map<string, any>();
  variables = variables.set("id", id);
  variables = variables.set("user", user);

  return await editObj<IUser>(mutationEditUser, "edituser", variables);
};
