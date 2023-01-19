import React, {FC, PropsWithChildren} from 'react';
import FilledDiv from "../UI/FilledDiv";
import {BoxProps, Text, useColorModeValue} from '@chakra-ui/react';

interface IErrorBlockProps extends PropsWithChildren, BoxProps {
  children?: string
}

const ErrorBlock: FC<IErrorBlockProps> = (props) => {
  const {children: message, ...rest} = props;

  const dotsColor = useColorModeValue('#FFFFFF', '#1F2023');


  return (
    <FilledDiv
      {...rest}
      padding={'45px 0'}
      backgroundPosition={'center'}
      backgroundSize={'50px 50px'}
      backgroundImage={`radial-gradient(${dotsColor} 20%, transparent 25%)`}
      display={'flex'} alignItems={'center'} justifyContent={'center'}
      textAlign={'center'}
    >
      <Text
        as={'h1'} fontSize={24} fontWeight={'bold'}
      >{message ?? 'Упс, помилка при завантаженні інформації'} (×﹏×)</Text>
    </FilledDiv>
  );
};

export default ErrorBlock;
