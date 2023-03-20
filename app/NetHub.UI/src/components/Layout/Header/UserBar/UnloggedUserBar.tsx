import React, {FC} from 'react';
import {Link} from 'react-router-dom';
import SvgSelector from '../../../UI/SvgSelector/SvgSelector';
import cl from '../Header.module.sass';
import {Box} from "@chakra-ui/react";

const UnloggedUserBar: FC = () => {
  return (
    <Box className={cl.loggingLinks}>
      <SvgSelector id="PersonOutlineOutlinedIcon"/>
      <Link to="/login">Sign up / Login</Link>
    </Box>
  );
};

export default UnloggedUserBar;
