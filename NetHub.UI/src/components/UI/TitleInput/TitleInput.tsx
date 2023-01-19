import React, {FC} from 'react';
import cl from './TitleInput.module.sass';
import {FormControl, FormErrorMessage, Input, InputProps, Text} from '@chakra-ui/react';

interface ITitleInputProps extends InputProps {
  title: string
  errorMessage?: string
}

const TitleInput: FC<ITitleInputProps> = (props) => {
  const {title, isInvalid, errorMessage, ...rest} = props;


  return (
    <div className={cl.titleInput}>
      <Text as={'p'}>{title}</Text>
      <FormControl isInvalid={isInvalid}>
        <Input{...rest}/>
        {isInvalid && !!errorMessage
          ? <FormErrorMessage>{errorMessage}</FormErrorMessage>
          : null}
      </FormControl>
    </div>
  );
};

export default TitleInput;
