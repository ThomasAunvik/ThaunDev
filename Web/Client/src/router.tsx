import React from "react";
import { Route, Switch } from "react-router";
import HomeContainer from "./containers/HomeContainer";
import SilentRefresh from "./containers/SilentRefresh";

export const Paths = {
  home: "/",
  silentRefresh: "/silent-refresh",
};

export const AppRouter = () => {
  return (
    <Switch>
      <Route path={Paths.home} component={HomeContainer} />
      <Route path={Paths.silentRefresh} component={SilentRefresh} />
    </Switch>
  );
};
