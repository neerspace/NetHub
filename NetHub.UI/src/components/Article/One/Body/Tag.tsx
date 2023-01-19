import React, {FC, PropsWithChildren, ReactNode} from 'react';
import cl from "../ArticleInfo.module.sass"
import {Text, useColorModeValue} from "@chakra-ui/react";

interface IThemeTagProps extends PropsWithChildren {
  value: string,
  children?: ReactNode,
  onClick?: (value: string) => void,
  active?: boolean,
  onHover?: boolean
}

const Tag: FC<IThemeTagProps> = ({value, onClick, active, onHover, children}) => {
    const tagBgDefault = useColorModeValue('#896DC8', '#835ADF');
    const tagBg = active ? 'success' : tagBgDefault;
    const errorColor = useColorModeValue('error', 'errorDark');

    return (
        <Text
          _hover={{bg: (onHover ?? true) ? errorColor : tagBg}}
          as={'p'} onClick={() => onClick ? onClick(value) : {}} className={cl.themeTag}
          bg={tagBg}
          display={'block'}
          height={'fit-content'}
        >
          {children ?? value}
        </Text>
    );
  }
;

export default Tag;
