import React from 'react';
import cl from "./Dynamic.module.sass";

const Flag = () => {
  return (
    <div className={cl.flag}>
      <div className={cl.flagBlue}></div>
      <div className={cl.flagYellow}></div>
    </div>
  );
};

export default Flag;
