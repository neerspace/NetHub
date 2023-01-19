import React from 'react';
import classes from "./Layout.module.sass";

const Flag = () => {
  return (
    <div className={classes.flag}>
      <div className={classes.flagBlue}></div>
      <div className={classes.flagYellow}></div>
    </div>
  );
};

export default Flag;
