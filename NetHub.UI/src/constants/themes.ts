import {extendTheme, StyleFunctionProps} from "@chakra-ui/react";
import {mode} from '@chakra-ui/theme-tools';
import {FilledDivConfig} from "../components/UI/FilledDiv";

const styles = {
  global: (props: StyleFunctionProps) => ({
    body: {
      bg: mode('#FFFFFF', '#1F2023')(props),
      color: mode('#242D35', '#FFFFFF')(props),
    },
    h2: {
      color: mode('#838383', '#EFEFEF')(props)
    },
    p: {
      color: mode('#323232', "#EFEFEF")(props)
    },
    a: {
      color: mode('#323232', "#EFEFEF")(props)
    }
  })
};

const components = {
  Skeleton: {
    baseStyle: {
      borderRadius: '12px',
      width: '100%'
    }
  },
  Input: {
    baseStyle: (props: StyleFunctionProps) => ({
      field: {
        color: '#757575',
        bg: mode('#FFFFFF', '#EFEFEF')(props),
        border: '1px solid',
        borderColor: mode('gray.200', '#EFEFEF')(props),
        _placeholder: {
          color: mode('#B1BAC5', '#757575')(props)
        },
        _invalid: {
          border: '2px solid',
          borderColor: mode('error', 'errorDark')(props),
        }
      }
    }),
    variants: (props: StyleFunctionProps) => ({
      outline: {
        field: {
          bg: mode('#FFFFFF', '#EFEFEF')(props),
        },
      },
    })
  },
  FormError:{
    baseStyle: {
      text: {
        color: 'red'
      },
    },
  },
  Button: {
    baseStyle: (props: StyleFunctionProps) => ({
      background: mode('#896DC8', "#835ADF")(props),
      py: '5px',
      px: '15px',
      color: mode('#FFFFFF', '#EFEFEF')(props),
      borderRadius: '12px',
      textAlign: 'center',
      _hover: {
        background: '#BBAFEA'
      }
    }),
    variants: (props: StyleFunctionProps) => ({
      solid: {
        background: mode('#896DC8', '#835ADF')(props),
        color: mode('#FFFFFF', '#EFEFEF')(props),
        py: '5px',
        px: '15px',
        borderRadius: '12px',
        textAlign: 'center',
      }
    }),
  },
  Text: {
    baseStyle: {
      wordWrap: 'none'
    }
  },
  FilledDiv: FilledDivConfig,
  Select: {
    baseStyle: (props: StyleFunctionProps) => ({
      field: {
        background: mode('#FFFFFF', '#EFEFEF')(props),
        color: '#1F2023',
      },
      icon: {
        color: '#1F2023'
      },
    }),
    variants: (props: StyleFunctionProps) => ({
      filled: {
        _hover: {
          background: mode('#FFFFFF', '#EFEFEF')(props)
        }
      }
    }),
    defaultProps: {
      variant: 'filled'
    }
  },
  Switch: {
    baseStyle: (props: StyleFunctionProps) => ({
      track: {
        _checked: {
          bg: mode('#896DC8', '#835ADF')(props)
        }
      }
    })
  }
};

const colors = {
  violetLight: '#F3EEFF',
  purpleLight: '#896DC8',
  purpleDark: '#835ADF',
  whiteLight: '#FFFFFF',
  whiteDark: '#EFEFEF',
  error: '#DF2638',
  errorDark: '#FF331F',
  success: '#09A552',
  warning: '#FEA613'
};

const config = {
  initialColorMode: localStorage.getItem('chakra-ui-color-mode') || 'dark',
};


const theme = extendTheme({
  config,
  styles,
  components,
  colors,
});

export default theme;
