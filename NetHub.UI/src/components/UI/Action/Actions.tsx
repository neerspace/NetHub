import React, {FC} from 'react';
import {BoxProps, useColorModeValue} from "@chakra-ui/react";
import FilledDiv from "../FilledDiv";

interface IActionProps extends BoxProps {

}

const Actions: FC<IActionProps> = (props) => {
  const blockColor = useColorModeValue('#FFFFFF', '#EFEFEF');

  const {children, ...rest} = props

  return (
    <FilledDiv
      onClick={(e) => e.stopPropagation()}
      padding={'4px 10px'}
      width={'fit-content'}
      height={'35px'}
      bg={blockColor}
      {...rest}
    >
      {children}
    </FilledDiv>
  );
};

export default Actions;