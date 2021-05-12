import React, { useEffect } from "react";
import { signinSilent } from "../common/auth";

const SilentRefresh = () => {
  useEffect(() => {
    signinSilent();
  }, []);

  return <div></div>;
};

export default SilentRefresh;
