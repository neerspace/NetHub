import React, {FC} from 'react';
import MenuGroup from './MenuGroup';
import classes from './Menu.module.sass';
import {firstGroup, secondGroup, thirdGroup} from "./MenuData";

const Menu: FC = () => {
  return (
    <div className={classes.menuItems}>
      <MenuGroup
        menuItems={firstGroup}
      />
      <MenuGroup
        groupName={'Бібліотека'}
        menuItems={secondGroup}
      />
      <MenuGroup
        groupName={'Профіль'}
        menuItems={thirdGroup}
      />
    </div>
  );
};

export default Menu;
