import React, { ReactNode, useCallback, useEffect } from "react";
import { useRecoilState } from "recoil";
import { fetchCurrentUser } from "../../api/users/actions";
import { current } from "../../states/users";

export interface IProps {
  children?: ReactNode;
}

const AppRoot = (props: IProps) => {
  const { children } = props;

  const [user, setUser] = useRecoilState(current);

  const fetchUser = useCallback(fetchCurrentUser, []);
  useEffect(() => {
    if (user !== null) return;

    fetchUser().then((u) => {
      if (u === null) return;

      setUser(u);
    });
  }, [fetchUser, setUser, user]);

  return <>{children}</>;
};

export default AppRoot;
