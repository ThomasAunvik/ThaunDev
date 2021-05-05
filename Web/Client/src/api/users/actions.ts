import { fetchObj } from "..";
import { IUser } from "../../models/user";
import { queryCurrentUser } from "./queries";

export const fetchCurrentUser = async () => {
  return await fetchObj<IUser>(queryCurrentUser);
};
