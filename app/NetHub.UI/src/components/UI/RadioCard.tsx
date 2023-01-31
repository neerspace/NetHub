import {Box, useColorModeValue, useRadio, UseRadioProps} from "@chakra-ui/react";
import {FC, PropsWithChildren} from "react";

interface IRadioCardProps extends UseRadioProps, PropsWithChildren {
}

const RadioCard: FC<IRadioCardProps> = ({children, ...rest}) => {
  const {getInputProps, getCheckboxProps} = useRadio(rest)

  const input = getInputProps()
  const checkbox = getCheckboxProps()

  return (
    <Box as='label'>
      <input {...input} />
      <Box
        {...checkbox}
        cursor='pointer'
        borderWidth='1px'
        borderRadius='12px'
        minW={'116px'}
        _checked={{
          bg: useColorModeValue('#896DC8', '#835ADF'),
          color: 'white',
          transition: '0.2s'
        }}
        color={useColorModeValue('#000000', '#323232')}
        bg={'#F3EEFF'}
        textAlign={'center'}
        _hover={{
          bg: '#BBAFEA',
          transition: '0.2s'
        }}
        px={5}
        py={3}
      >
        {children}
      </Box>
    </Box>
  )
}

export default RadioCard;
