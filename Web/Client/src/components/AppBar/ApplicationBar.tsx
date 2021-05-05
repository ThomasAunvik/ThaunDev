import { AppBar, Button, createStyles, IconButton, makeStyles, Theme, Toolbar, Typography } from '@material-ui/core';
import MenuIcon from '@material-ui/icons/Menu';
import React from 'react'
import { useState } from 'react';
import AppDrawer from '../Drawer/AppDrawer';

const ApplicationBar = () => {
    const styles = useStyles();

    const [drawerOpen, setDrawerOpen] = useState<boolean>(false);
    
    return (<div className={styles.root}>
        <AppBar position="static">
            <Toolbar>
                <IconButton 
                    edge="start" 
                    className={styles.menuButton} 
                    color="inherit" 
                    aria-label="menu"
                    onClick={() => setDrawerOpen(true)}
                >
                    <MenuIcon />
                </IconButton>
                <Typography variant="h6" className={styles.title}>
                    Home
                </Typography>
                <Button color="inherit">Login</Button>
            </Toolbar>
        </AppBar>

        <AppDrawer isOpen={drawerOpen} setIsOpen={setDrawerOpen} />
    </div>)
}

const useStyles = makeStyles((theme: Theme) => 
    createStyles({
        root: {
            flexGrow: 1
        },
        menuButton: {
            marginRight: theme.spacing(2)
        },
        title: {
            flexGrow: 1
        }
    })
)

export default ApplicationBar;