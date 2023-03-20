import React, {FC, PropsWithChildren} from 'react';
import {Box} from "@chakra-ui/react";

interface IBarWrapperProps extends PropsWithChildren {
  className?: string
}

const BarWrapper: FC<IBarWrapperProps> = ({children, className}) => {
  return (
    <Box className={className}>
      {children}
    </Box>
  );
};

export default BarWrapper;
