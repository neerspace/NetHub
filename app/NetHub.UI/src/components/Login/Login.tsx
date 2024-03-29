import {
  Accordion,
  AccordionButton,
  AccordionItem,
  AccordionPanel,
  Box,
  Button,
  Text,
  useColorModeValue
} from '@chakra-ui/react';
import React, {useRef, useState} from 'react';
import {useNavigate} from 'react-router-dom';
import {z as u} from 'zod';
import useCustomSnackbar from '../../hooks/useCustomSnackbar';
import {useDebounce} from '../../hooks/useDebounce';
import {SsoRequest, SsoRequestSchema} from '../../types/schemas/Sso/SsoSchema';
import {usernameDebounce} from '../../utils/debounceHelper';
import LoginService from '../../utils/LoginService';
import TitleInput from '../UI/TitleInput/TitleInput';
import FacebookLoginButton from './Buttons/FacebookLoginButton';
import GoogleLoginButton from './Buttons/GoogleLoginButton';
import TelegramLoginButton from './Buttons/TelegramLoginButton';
import {_jwtApi, _usersApi} from "../../api";
import {JwtAuthenticateRequest, ProviderType, SsoType} from '../../api/_api';
import {JWTStorage} from "../../utils/localStorageProvider";
import {useAppStore} from "../../store/store";

interface ISecondStep {
  isExpanded: boolean,
  enableEmail: boolean
}

const Login = () => {
  const [registrationStep, setRegistrationStep] = useState<ISecondStep>({
    isExpanded: false,
    enableEmail: false
  });
  const [request, setRequest] = useState<SsoRequest>({
    username: '',
    firstName: '',
    lastName: '',
    email: ''
  } as SsoRequest);
  const {login} = useAppStore();
  const {enqueueError, enqueueSuccess, enqueueSnackBar} = useCustomSnackbar('info');
  const navigate = useNavigate();
  const accordionButtonRef = useRef<HTMLButtonElement | null>(null);
  const [errors, setErrors] = useState<u.ZodFormattedError<{
    middleName?: string | undefined,
    providerMetadata?: any,
    username: string,
    email: string,
    firstName: string,
    lastName: string,
    profilePhotoUrl: string | null,
    provider: ProviderType,
    type: 'register' | 'login',
    providerKey: string
  }
  >>({_errors: []});

  const debounceLogic = async (username: string | null) => await usernameDebounce(username, setErrors, errors);
  const debounce = useDebounce(debounceLogic, 1000);

  const validate = async () => {
    const validationResult = await SsoRequestSchema.safeParseAsync(request);

    if (!validationResult.success) {
      const errors = validationResult.error.format();
      setErrors(errors);
      return;
    }

    setErrors({_errors: []});

    return validationResult.success;
  };

  const updateRequest = (key: string, value: string | undefined) => {
    setRequest((prev: SsoRequest) => {
      return {...prev, [ key ]: value};
    });
  };

  const firstStep = async (provider: ProviderType) => {
    let isExpanded = registrationStep.isExpanded;

    if (isExpanded) {
      accordionButtonRef.current?.click();
      isExpanded = !isExpanded;
    }

    setRegistrationStep({isExpanded: false, enableEmail: false});

    const providerRequest = await LoginService.ProviderHandle(provider);
    setRequest(providerRequest);

    const {isProviderRegistered} = await _usersApi.checkIfExists(providerRequest.providerKey, provider);

    if (!isProviderRegistered) {
      if (!isExpanded) {
        accordionButtonRef.current?.click();
        isExpanded = true;
      }
      setRegistrationStep({isExpanded, enableEmail: !providerRequest.email});
      // accordionButtonRef.current?.click();
      return;
    }
    enqueueSnackBar('Завантаження...');
    const user = await _jwtApi.authenticate(
      new JwtAuthenticateRequest({...providerRequest, type: SsoType.Login}))
    JWTStorage.setTokensData(user);
    enqueueSuccess('Авторизовано!');

    login(user);
    navigate('/');
  };

  const secondStep = async () => {
    if (!await validate())
      return;
    try {
      enqueueSnackBar('Завантаження...');
      const user = await _jwtApi.authenticate(
        new JwtAuthenticateRequest({...request, type: SsoType.Register}));
      JWTStorage.setTokensData(user);

      enqueueSuccess('Авторизовано!');

      login(user);
      navigate('/');
    } catch (e: any) {
      if (e.response.data.message.includes('already taken')) {
        enqueueError('Користувач з такою електронною адресою вже зареєстрований');
      }
    }
  };

  return <Accordion allowToggle>
    <AccordionItem
      bg={useColorModeValue('#F3EEFF', '#333439')}
      padding={'20px'}
      borderRadius={'12px'}
    >
      <Text as={'b'} color={useColorModeValue('#757575', 'whiteDark')}>
        Оберіть спосіб авторизації
      </Text>
      <Box margin={'10px'}/>
      <Box display={'flex'} gap={6} mt={'5px'}>
        <GoogleLoginButton onClick={async () => await firstStep(ProviderType.Google)}/>
        <TelegramLoginButton onClick={async () => await firstStep(ProviderType.Telegram)}/>
        <FacebookLoginButton onClick={async () => await firstStep(ProviderType.Facebook)}/>
      </Box>
      <AccordionButton ref={accordionButtonRef} display={'none'}/>
      <AccordionPanel paddingInlineStart={0} paddingInlineEnd={0}>
        <Box margin={'10px'}/>
        <Text as={'p'} fontWeight={700}>Уточніть інформацію</Text>
        <Box margin={'10px'}/>
        <TitleInput
          title={'Username*'}
          placeholder={'Ім\'я користувача'}
          value={request.username!}
          isInvalid={!!errors.username}
          errorMessage={errors.username?._errors?.join(', ')}
          onChange={(e) => {
            updateRequest('username', e.target.value);
            if (e.target.value !== null && e.target.value !== '')
              debounce(e.target.value);
          }}
          width={'100%'}
        />
        <TitleInput
          title={'Email*'}
          placeholder={'Електронна пошта'}
          value={request.email!}
          isInvalid={!!errors.email}
          errorMessage={[...new Set(errors.email?._errors)].join(', ')}
          onChange={(e) => updateRequest('email', e.target.value)}
          width={'100%'}
          isDisabled={!registrationStep.enableEmail}
        />
        <TitleInput
          title={'Firstname*'}
          placeholder={'Iм\'я'}
          value={request.firstName!}
          isInvalid={!!errors.firstName}
          errorMessage={errors.firstName?._errors?.join(', ')}
          onChange={(e) => updateRequest('firstName', e.target.value)}
          width={'100%'}
        />
        <TitleInput
          title={'Lastname*'}
          placeholder={'Прізвище'}
          value={request.lastName!}
          isInvalid={!!errors.lastName}
          errorMessage={errors.lastName?._errors?.join(', ')}
          onChange={(e) => updateRequest('lastName', e.target.value)}
          width={'100%'}
        />
        <TitleInput
          title={'Middlename'}
          placeholder={'По-батькові'}
          value={request.middleName ?? ''}
          isInvalid={!!errors.middleName}
          errorMessage={errors.middleName?._errors?.join(', ')}
          onChange={(e) => updateRequest('middleName', e.target.value === '' ? undefined : e.target.value)}
          width={'100%'}
        />
        <Button onClick={secondStep}>Зареєструватись</Button>
      </AccordionPanel>
    </AccordionItem>
  </Accordion>
};

export default Login;
