import React from "react";
import { Route, Switch } from "react-router";
import HomeContainer from "./containers/HomeContainer";

export const Paths = {
    home: "/"
};

export const AppRouter = () => {
    return (<Switch>
        <Route path={Paths.home} component={HomeContainer} />
    </Switch>);
}