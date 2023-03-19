import React, { FC, PropsWithChildren, ReactNode } from 'react';
import cl from "../Article.module.sass"
import { Text, useColorModeValue } from "@chakra-ui/react";

interface IThemeTagProps extends PropsWithChildren {
  value: string,
  children?: ReactNode,
  onClick?: (value: string) => void,
  active?: boolean,
  onHover?: boolean,
  isDisabled?: boolean
}

const Tag: FC<IThemeTagProps> = ({value, onClick, active, onHover, children, isDisabled}) => {
    const tagBgDefault = useColorModeValue('#896DC8', '#835ADF');
    const errorColor = useColorModeValue('error', 'errorDark');
    const disabledColor = useColorModeValue('#1F2023', '#1F2023');
    const tagBg = isDisabled ? disabledColor : active ? 'success' : tagBgDefault;

    return <Text
      cursor={isDisabled ? 'not-allowed' : 'pointer'}
      _hover={{bg: isDisabled ? disabledColor : (onHover ?? true) ? errorColor : tagBg}}
      as={'p'} onClick={() => onClick ? onClick(value) : {}} className={cl.themeTag}
      bg={tagBg}
      display={'block'}
      height={'fit-content'}
    >
      {children ?? value}
    </Text>
  }
;

export default Tag;
