import { useCallback, useEffect } from "react";
import { useRecoilState } from "recoil";
import { fetchCurrentUser } from "../../api/users/actions";
import { validateAuth } from "../../common/auth";
import { current } from "../../states/users";

export interface IProps {
  children: JSX.Element;
}

const AppRoot = (props: IProps) => {
  const { children } = props;

  const [user, setUser] = useRecoilState(current);

  const fetchUser = useCallback(fetchCurrentUser, []);
  useEffect(() => {
    if (user !== null) return;
    validateAuth().then((authUser) => {
      if (authUser === null) return;

      fetchUser().then((u) => {
        if (u === null) return;

        setUser(u);
      });
    });
  }, [fetchUser, setUser, user]);

  return children;
};

export default AppRoot;
