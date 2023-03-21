import { defineStyle, defineStyleConfig } from "@chakra-ui/react";
import { mode } from "@chakra-ui/theme-tools";

const textAreaBaseStyle = defineStyle((props) => ({
  color: '#757575',
  backgroundColor: `${mode('#FFFFFF', '#EFEFEF')(props)} !important`,
  border: '1px solid',
  borderColor: `${mode('gray.200', '#EFEFEF')(props)} !important`,
  _placeholder: {
    color: mode('#B1BAC5', '#757575')(props)
  },
  _focus: {
    borderColor: mode('gray.200', '#EFEFEF')(props),
    boxShadow: 0
  },
  _focusVisible: {
    border: 0,
    boxShadow: '0 0 0 0',
    borderColor: mode('gray.200', '#EFEFEF')(props),
  }
}));
export const textareaTheme = defineStyleConfig({
  baseStyle: textAreaBaseStyle
})