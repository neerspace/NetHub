import React, {FC} from 'react';
import {Box, BoxProps, defineStyleConfig, useStyleConfig} from "@chakra-ui/react";
import {mode} from "@chakra-ui/theme-tools";

interface IFilledDivProps extends BoxProps {
  variant?: 'clickable'
}

const FilledDiv: FC<IFilledDivProps> = (props) => {
  const {variant, ...rest} = props;
  const styles = useStyleConfig('FilledDiv', {variant})

  return (
    <Box __css={styles} {...rest}>
    </Box>
  );
};

export default FilledDiv

export const FilledDivConfig = defineStyleConfig({
  baseStyle: (props: any) => ({
    padding: '20px',
    background: mode('#F3EEFF','#333439')(props),
    width: '100%',
    height: 'fit-content',
    borderRadius: '12px',
  }),
  variants: {
    clickable: {
      _hover: {
        backgroundColor: '#8267be',
        transition: '0.2s'
      }
    }
  }
});
