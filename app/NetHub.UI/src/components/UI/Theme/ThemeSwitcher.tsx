import React, {useMemo} from 'react';
import {Box, Switch, useColorMode, useColorModeValue} from "@chakra-ui/react";
import SvgSelector from "../SvgSelector/SvgSelector";
import cl from "../../Layout/Header/Header.module.sass";

const ThemeSwitcher = () => {

  const {toggleColorMode, colorMode} = useColorMode();

  const isSwitched = useMemo(() => colorMode !== 'light', [colorMode]);
  const switcherColor = useColorModeValue('', cl.svgWhite);

  return (
    <Box display={'flex'} alignItems={'center'}>
      <SvgSelector id={'Sun'} className={switcherColor}/>
      <Switch onChange={toggleColorMode} defaultChecked={isSwitched} size='md' mx={2}/>
      <SvgSelector id={'Moon'} className={switcherColor}/>
    </Box>
  );
};

export default ThemeSwitcher;