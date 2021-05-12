import { useEffect } from "react";
import { useRecoilState } from "recoil";
import qs from "query-string";
import { completeAuthentication } from "../common/auth";
import { current } from "../states/users";
import { fetchCurrentUser } from "../api/users/actions";
import { Paths } from "../router";
import { useHistory } from "react-router-dom";

export interface IProps {
  children: JSX.Element;
}

const AuthContext = (props: IProps) => {
  const { children } = props;

  const history = useHistory();
  const setUser = useRecoilState(current);

  useEffect(() => {
    const params = qs.parse(history?.location?.search);
    if (params.code) {
      completeAuthentication().then((user) => {
        history.push(Paths.home);
        if (!user) return;
        fetchCurrentUser().then((current) => {
          setUser[1](current);
        });
      });
    }
  }, [history, setUser]);

  return children;
};

export default AuthContext;
