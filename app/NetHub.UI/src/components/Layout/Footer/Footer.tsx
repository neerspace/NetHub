import React from 'react';
import layoutClasses from "../Layout.module.sass";
import {Box, Link, Text, useColorMode, useColorModeValue} from "@chakra-ui/react";
import SvgSelector from "../../UI/SvgSelector/SvgSelector";
import cl from "./Footer.module.sass";
import {useNavigate} from "react-router-dom";

const Footer = () => {

  const navigate = useNavigate();
  const {colorMode} = useColorMode();

  const lineColor = useColorModeValue('#D0D0D0', 'lightDark');
  const linksColor = useColorModeValue('#838383', 'lightDark');


  return (
    <footer className={cl.footer}>

      <hr style={{border: `1px solid ${lineColor}`}}/>

      <Box className={cl.footerWrapper}>

        <Box className={layoutClasses.left}>
          <Link onClick={() => navigate('/')}>
            <SvgSelector className={cl.logo} id={colorMode === 'light' ? 'LightLogo' : 'DarkLogo'}/>
          </Link>
        </Box>

        <Box className={`${layoutClasses.center} ${cl.center}`}>
          <Box width={'fit-content'}>
            <Link fontWeight={'semibold'} color={linksColor}>
              Home
            </Link>
          </Box>
          <Box width={'fit-content'}>
            <Link onClick={() => navigate('/team')} fontWeight={'semibold'} color={linksColor}>
              Team
            </Link>
          </Box>
          <Box width={'fit-content'}>
            <Link fontWeight={'semibold'} color={linksColor}>
              Q/A
            </Link>
          </Box>
          <Box width={'fit-content'}>
            <Link fontWeight={'semibold'} color={linksColor}>
              E-mail
            </Link>
          </Box>
        </Box>

        <Box className={layoutClasses.right}>
          <Text
            as={'p'}
            fontWeight={'semibold'}
            className={cl.copyright} fontSize={14}
            width={'100%'}
            color={linksColor}
          >
            © 2022 NetHub inc. All rights reserved
          </Text>
        </Box>
      </Box>

    </footer>
  );
};

export default Footer;
