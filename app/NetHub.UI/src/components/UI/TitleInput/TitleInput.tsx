import React, { FC } from 'react';
import cl from '../UiComps.module.sass';
import { Box, FormControl, FormErrorMessage, Input, InputProps, Text } from '@chakra-ui/react';

interface ITitleInputProps extends InputProps {
  title: string
  errorMessage?: string
}

const TitleInput: FC<ITitleInputProps> = (props) => {
  const {title, isInvalid, errorMessage, ...rest} = props;

  return <Box className={cl.titleInput} {...rest}>
    <Text as={'p'}>{title}</Text>
    <FormControl isInvalid={isInvalid}>
      <Input{...rest}/>
      {isInvalid && !!errorMessage
        ? <FormErrorMessage>{errorMessage}</FormErrorMessage>
        : null}
    </FormControl>
  </Box>
};

export default TitleInput;
