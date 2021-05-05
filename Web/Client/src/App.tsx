import {
  createStyles,
  makeStyles,
  Theme,
  ThemeProvider,
} from "@material-ui/core";
import React from "react";
import ApplicationBar from "./components/AppBar/ApplicationBar";
import { darkTheme } from "./themes";
import { AppRouter } from "./router";
import "./styles/App.css";
import { Router } from "react-router";
import { createBrowserHistory } from "history";
import { RecoilRoot } from "recoil";
import AppRoot from "./components/AppRoot/UserRoot";

const history = createBrowserHistory();

export const appEnv = {
  API: process.env.REACT_APP_API,
};

const App = () => {
  const useThemeStyle = darkTheme.style();

  const styles = useStyles();

  return (
    <div className={useThemeStyle.root + " " + styles.appRoot}>
      <RecoilRoot>
        <AppRoot>
          <ThemeProvider theme={darkTheme.mui}>
            <Router history={history}>
              <ApplicationBar />
              <div className={styles.contentRoot}>
                <AppRouter />
              </div>
            </Router>
          </ThemeProvider>
        </AppRoot>
      </RecoilRoot>
    </div>
  );
};

export default App;

const useStyles = makeStyles((theme: Theme) =>
  createStyles({
    contentRoot: {
      paddingLeft: "1em",
    },
    appRoot: {
      height: "100vh",
    },
  })
);
