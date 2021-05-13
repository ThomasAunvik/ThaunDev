import { useEffect } from "react";
import { useRecoilState } from "recoil";
import qs from "query-string";
import { completeAuthentication } from "../common/auth";
import { current } from "../states/users";
import { fetchCurrentUser } from "../api/users/actions";
import { Paths } from "../router";
import { useHistory } from "react-router-dom";
import CookieConsent from "react-cookie-consent";
import { useCallback } from "react";

export interface IProps {
  children: JSX.Element;
}

const AuthContext = (props: IProps) => {
  const { children } = props;

  const history = useHistory();
  const setUser = useRecoilState(current)[1];

  const completeAuth = () => {
    const params = qs.parse(history?.location?.search);
    if (params.code) {
      completeAuthentication().then((user) => {
        history.push(Paths.home);
        if (!user) return;
        fetchCurrentUser().then((current) => {
          setUser(current);
        });
      });
    }
  };

  const authenticationCallback = useCallback(completeAuth, [history, setUser]);

  useEffect(() => {
    authenticationCallback();
  }, [authenticationCallback]);

  return (
    <div>
      <CookieConsent>
        <span>This website uses cookies to enhance the user experience.</span>
      </CookieConsent>
      {children}
    </div>
  );
};

export default AuthContext;
