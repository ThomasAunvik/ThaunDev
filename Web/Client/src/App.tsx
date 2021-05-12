import {
  createStyles,
  makeStyles,
  Theme,
  ThemeProvider,
} from "@material-ui/core";
import { createBrowserHistory } from "history";
import React from "react";
import { Router } from "react-router";
import ApplicationBar from "./components/AppBar/ApplicationBar";
import AppRoot from "./components/AppRoot/UserRoot";
import AuthContext from "./containers/AuthContext";
import { AppRouter } from "./router";
import "./styles/App.css";
import { darkTheme } from "./themes";

export const history = createBrowserHistory({ basename: "/" });

export const appEnv = {
  API: process.env.REACT_APP_API,
};

const App = () => {
  const useThemeStyle = darkTheme.style();

  const styles = useStyles();

  return (
    <div className={useThemeStyle.root + " " + styles.appRoot}>
      <AppRoot>
        <ThemeProvider theme={darkTheme.mui}>
          <Router history={history}>
            <AuthContext>
              <div>
                <ApplicationBar />
                <div className={styles.contentRoot}>
                  <AppRouter />
                </div>
              </div>
            </AuthContext>
          </Router>
        </ThemeProvider>
      </AppRoot>
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
