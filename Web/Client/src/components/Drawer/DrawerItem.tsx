import {
  ListItem,
  ListItemIcon,
  ListItemText,
  SvgIconTypeMap,
} from "@material-ui/core";
import { OverridableComponent } from "@material-ui/core/OverridableComponent";
import React from "react";

export interface IProps {
  title: string;
  Icon?: OverridableComponent<SvgIconTypeMap<{}, "svg">>;

  roles?: string[];
}

const DrawerItem = (props: IProps) => {
  const { title, Icon } = props;

  return (
    <ListItem button>
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
