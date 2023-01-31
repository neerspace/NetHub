import React, {FC} from 'react';
import SvgSelector from '../../UI/SvgSelector/SvgSelector';
import classes from './Menu.module.sass';
import {IMenuItem} from "./MenuData";
import {useNavigate} from "react-router-dom";
import {ListItem, Text, UnorderedList, useColorModeValue} from "@chakra-ui/react";

interface IMenuGroupProps {
  groupName?: string,
  menuItems: IMenuItem[]
}

const MenuGroup: FC<IMenuGroupProps> = ({groupName, menuItems}) => {
  const navigate = useNavigate();

  const itemActiveBg = useColorModeValue('#896DC8', '#835ADF');
  const itemHoveredBg = useColorModeValue('#BBAFEA', '#333439');

  const isActive = (menuItem: IMenuItem): boolean => window.location.pathname === menuItem.link;
  const getItemHoveredBg = (menuItem: IMenuItem) => menuItem.isActive ? itemHoveredBg : '';

  return (
    <UnorderedList className={classes.menuPointContainer} marginInlineStart={0}>
      {groupName && <Text as={'p'}>{groupName}</Text>}
      {menuItems.map(menuItem =>
        <ListItem
          bg={isActive(menuItem) ? itemActiveBg : ''}
          sx={
            {
              _hover: {
                backgroundColor: menuItem.isActive ? getItemHoveredBg(menuItem) : ''
              }
            }
          }
          key={menuItem.itemName}
          className={`
            ${classes.menuPoint} 
            ${window.location.pathname === menuItem.link ? classes.menuPointActive : ''} 
            ${menuItem.isActive ? '' : classes.disabled}
          `}
          onClick={() => menuItem.isActive && navigate(menuItem.link)}
        >
          <SvgSelector id={menuItem.svgId}/>

          <Text as={'p'}>{menuItem.itemName}</Text>
        </ListItem>
      )}
    </UnorderedList>
  );
};

export default MenuGroup;
