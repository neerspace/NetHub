import React, {FC} from 'react';
import {Link} from 'react-router-dom';
import SvgSelector from '../../UI/SvgSelector/SvgSelector';
import classes from './Header.module.sass';

const UnloggedUserBar: FC = () => {
  return (
    <div className={classes.loggingLinks}>
      <SvgSelector id="PersonOutlineOutlinedIcon"/>
      <Link to="/login">Sign up / Login</Link>
    </div>
  );
};

export default UnloggedUserBar;
