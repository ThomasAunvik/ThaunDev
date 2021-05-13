import { Button } from "@material-ui/core";
import React from "react";
import { startAuthentication } from "../common/auth";

const NotLoggedInContainer = () => {
  return (
    <div>
      <h1>You are not logged in!</h1>
      <Button variant="outlined" onClick={() => startAuthentication()}>
        Log In
      </Button>
    </div>
  );
};

export default NotLoggedInContainer;
