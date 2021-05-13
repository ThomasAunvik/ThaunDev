import React from "react";
import { Route, Switch } from "react-router";
import HomeContainer from "./containers/HomeContainer";
import ProfileContainer from "./containers/ProfileContainer";
import SilentRefresh from "./containers/SilentRefresh";

export const Paths = {
  home: "/",
  silentRefresh: "/silent-refresh",

  profile: "/profile",
};

export const AppRouter = () => {
  return (
    <Switch>
      <Route exact path={Paths.home} component={HomeContainer} />
      <Route exact path={Paths.silentRefresh} component={SilentRefresh} />

      <Route exact path={Paths.profile} component={ProfileContainer} />
    </Switch>
  );
};
