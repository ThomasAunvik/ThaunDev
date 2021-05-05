import { atom } from "recoil";
import { IUser } from "../models/user";

export const current = atom<IUser | null>({
  key: "currentUserState",
  default: null,
});
