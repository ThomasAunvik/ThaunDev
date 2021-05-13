import {
  createStyles,
  List,
  makeStyles,
  SwipeableDrawer,
  Theme,
} from "@material-ui/core";
import HomeIcon from "@material-ui/icons/Home";
import React, { useCallback, useEffect, useState } from "react";
import { Paths } from "../../router";
import DrawerItem, { IProps as DrawerItemProps } from "../Drawer/DrawerItem";

export interface IProps {
  isOpen: boolean;
  setIsOpen: (value: boolean) => void;
}

const AppDrawer = (props: IProps) => {
  const { isOpen, setIsOpen } = props;

  const styles = useStyles();

  const [drawerItems, setDrawerItems] = useState<DrawerItemProps[]>();

  const genToolbarItems = useCallback((): DrawerItemProps[] => {
    return [
      {
        title: "Home",
        Icon: HomeIcon,
        path: Paths.home,
      },
    ];
  }, []);

  useEffect(() => {
    setDrawerItems(genToolbarItems());
  }, [genToolbarItems]);

  return (
    <SwipeableDrawer
      open={isOpen}
      onOpen={() => setIsOpen(true)}
      onClose={() => setIsOpen(false)}
    >
      <div className={styles.drawerRoot}>
        <List component="nav">
          {drawerItems?.map((i) => (
            <DrawerItem
              key={"drawer-item-" + i.title}
              {...i}
              onClick={() => setIsOpen(false)}
            />
          ))}
        </List>
      </div>
    </SwipeableDrawer>
  );
};

export default AppDrawer;

const useStyles = makeStyles((theme: Theme) =>
  createStyles({
    drawerRoot: {
      width: "175px",
    },
  })
);
