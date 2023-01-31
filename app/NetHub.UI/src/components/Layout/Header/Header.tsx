import React from 'react';
import SvgSelector from '../../UI/SvgSelector/SvgSelector';
import cl from './Header.module.sass'
import layoutClasses from '../Layout.module.sass'
import LoggedUserBar from './LoggedUserBar';
import UnloggedUserBar from './UnloggedUserBar';
import {useNavigate} from "react-router-dom";
import {
  Box,
  Button,
  Input,
  InputGroup,
  InputLeftElement,
  Link,
  useColorMode,
  useColorModeValue
} from "@chakra-ui/react";
import {SearchIcon} from "@chakra-ui/icons";
import {useAppStore} from "../../../store/config";
import ThemeSwitcher from "../../UI/Theme/ThemeSwitcher";

const Header: React.FC = () => {

  const {colorMode} = useColorMode();
  const isLogin = useAppStore(state => state.isLogin);
  const navigate = useNavigate();
  const [searchValue, setSearchValue] = React.useState<string>('');
  const headerBackgroundColor = useColorModeValue("#FFFFFF", '#323232')

  return (
    <header className={cl.header} style={{backgroundColor: headerBackgroundColor}}>
      <Box className={layoutClasses.left}>
        <Link onClick={() => navigate('/')}>
          <SvgSelector className={cl.logo} id={colorMode === 'light' ? 'LightLogo' : 'DarkLogo'}/>
        </Link>
      </Box>
      <Box className={layoutClasses.center} style={{justifyContent: 'center'}}>
        <Box className={cl.headerCenter}>

          <InputGroup width={'55%'}>
            <InputLeftElement
              pointerEvents='none'
              children={<SearchIcon color={useColorModeValue('#B1BAC5', '#757575')}/>}
            />
            <Input
              variant={'outline'}
              value={searchValue}
              placeholder={'Пошук'}
              onChange={(event) => setSearchValue(event.target.value)}
            />
          </InputGroup>

          <Button p={'11px 34px'} onClick={() => navigate('/articles/add')}>
            Створити
            <SvgSelector id={'DriveFileRenameOutlineIcon'}/>
          </Button>
          {/*<Switch id="email-alerts" />*/}
          <ThemeSwitcher/>
        </Box>
      </Box>
      <Box className={layoutClasses.right}>
        <Box className={cl.userEntry}>
          {isLogin ? <LoggedUserBar/> : <UnloggedUserBar/>}
        </Box>
      </Box>
    </header>
  );
};

export default Header;
