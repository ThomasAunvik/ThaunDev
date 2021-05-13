import {
  ListItem,
  ListItemIcon,
  ListItemText,
  SvgIconTypeMap,
} from "@material-ui/core";
import { OverridableComponent } from "@material-ui/core/OverridableComponent";
import React from "react";
import { useHistory } from "react-router-dom";

export interface IProps {
  title: string;
  Icon?: OverridableComponent<SvgIconTypeMap<{}, "svg">>;
  path?: string;

  roles?: string[];

  onClick?: () => void;
}

const DrawerItem = (props: IProps) => {
  const { title, Icon, path, onClick } = props;

  const history = useHistory();

  const handleClick = () => {
    if (path === undefined) return;
    history.push(path);
  };

  return (
    <ListItem
      button
      onClick={() => {
        handleClick();
        if (onClick) onClick();
      }}
    >
      {Icon !== undefined && (
        <ListItemIcon>
          <Icon />
        </ListItemIcon>
      )}
      <ListItemText primary={title} />
    </ListItem>
  );
};

export default DrawerItem;
