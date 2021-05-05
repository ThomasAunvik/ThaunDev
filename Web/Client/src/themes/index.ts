import { Theme } from "@material-ui/core";
import { unstable_createMuiStrictModeTheme as createMuiTheme, createStyles, makeStyles } from "@material-ui/core";

const useDarkStyles = makeStyles((theme: Theme) => 
    createStyles({
        root: {
            backgroundColor: "#121212",
            color: "white"
        }
    })
)

export const darkTheme = {
    style: useDarkStyles,
    mui: createMuiTheme({
        palette: {
            type: "dark",
            primary: {
                main: "#303030",
            },
            secondary: {
                main: "#424242"
            },
            text: {
                primary: "#fff",
                secondary: "rgba(255, 255, 255, 0.7)",
                disabled: "rgba(255, 255, 255, 0.5)"
            },
            background: {
                default: "#303030",
                paper: "#424242"
            },
            divider: "rgba(255, 255, 255, 0.12)"
        }
    })
};