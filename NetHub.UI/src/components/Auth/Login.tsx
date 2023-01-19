import React, {useRef, useState} from 'react';
import GoogleAuthButton from "./Buttons/GoogleAuthButton";
import TelegramAuthButton from "./Buttons/TelegramAuthButton";
import TitleInput from "../UI/TitleInput/TitleInput";
import {userApi} from "../../api/api";
import {useNavigate} from "react-router-dom";
import FacebookAuthButton from "./Buttons/FacebookAuthButton";
import LoginService from "../../utils/LoginService";
import {ProviderType} from "../../types/ProviderType";
import {useDebounce} from "../../hooks/useDebounce";
import useCustomSnackbar from "../../hooks/useCustomSnackbar";
import {usernameDebounce} from '../../utils/debounceHelper';
import {
  Accordion,
  AccordionButton,
  AccordionItem,
  AccordionPanel,
  Box,
  Button,
  Text,
  useColorModeValue
} from "@chakra-ui/react";
import {useAppStore} from "../../store/config";
import {z as u} from "zod";
import {SsoRequest, SsoRequestSchema} from "../../types/schemas/Sso/SsoSchema";

interface ISecondStep {
  isExpanded: boolean,
  enableEmail: boolean
}

const Login = () => {
  const [registrationStep, setRegistrationStep] = useState<ISecondStep>({isExpanded: false, enableEmail: false});
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
    type: "register" | "login",
    providerKey: string
  }
  >>({_errors: []});

  const debounceLogic = async (username: string | null) => await usernameDebounce(username, setErrors, errors);
  const debounce = useDebounce(debounceLogic, 1000);

  const validate = async () => {
    const validationResult = await SsoRequestSchema.safeParseAsync(request);

    if (!validationResult.success) {
      const errors = validationResult.error.format()
      setErrors(errors);
      return;
    }

    setErrors({_errors: []});

    return validationResult.success;
  }

  const updateRequest = (key: string, value: string | undefined) => {
    setRequest((prev: SsoRequest) => {
      return {...prev, [key]: value}
    })
  }

  const firstStep = async (provider: ProviderType) => {
    let isExpanded = registrationStep.isExpanded;

    if (isExpanded) {
      accordionButtonRef.current?.click();
      isExpanded = !isExpanded;
    }

    setRegistrationStep({isExpanded: false, enableEmail: false});

    const providerRequest = await LoginService.ProviderHandle(provider);
    setRequest(providerRequest);

    const {isProviderRegistered} = await userApi.checkIfExists(providerRequest.providerKey, provider);

    if (!isProviderRegistered) {
      if (!isExpanded) {
        accordionButtonRef.current?.click();
        isExpanded = true;
      }
      setRegistrationStep({isExpanded, enableEmail: !providerRequest.email});
      // accordionButtonRef.current?.click();
      return;
    }
    enqueueSnackBar('Завантаження...')
    const user = await userApi.authenticate({...providerRequest, type: 'login'});
    enqueueSuccess('Авторизовано!')

    login(user);
    navigate('/');
  }

  const secondStep = async () => {
    if (!await validate())
      return;
    try {
      enqueueSnackBar('Завантаження...')
      const user = await userApi.authenticate({...request, type: 'register'});
      enqueueSuccess('Авторизовано!')

      login(user);
      navigate('/');
    } catch (e: any) {
      if (e.response.data.message.includes('already taken')) {
        enqueueError('Користувач з такою електронною адресою вже зареєстрований');
      }
    }
  }

  return (
    <>
      <Accordion allowToggle>
        <AccordionItem bg={useColorModeValue('#F3EEFF', '#333439')} padding={'20px'} borderRadius={'12px'}>
          <Text as={'b'} color={useColorModeValue('#757575', 'whiteDark')}>
            Оберіть спосіб авторизації
          </Text>
          <Box margin={'10px'}/>
          <Box display={'flex'} gap={6} mt={'5px'}>
            <GoogleAuthButton onClick={async () => await firstStep(ProviderType.GOOGLE)}/>
            <TelegramAuthButton onClick={async () => await firstStep(ProviderType.TELEGRAM)}/>
            <FacebookAuthButton onClick={async () => await firstStep(ProviderType.FACEBOOK)}/>
          </Box>
          <AccordionButton ref={accordionButtonRef} display={'none'}/>
          <AccordionPanel paddingInlineStart={0} paddingInlineEnd={0}>
            <Box margin={'10px'}/>
            <Text as={'p'} fontWeight={700}>Уточніть інформацію</Text>
            <Box margin={'10px'}/>
            <TitleInput
              title={'Username*'} placeholder={'Ім\'я користувача'} value={request.username!}
              isInvalid={!!errors.username}
              errorMessage={errors.username?._errors?.join(', ')}
              onChange={(e) => {
                updateRequest('username', e.target.value);
                if (e.target.value !== null && e.target.value !== '')
                  debounce(e.target.value)
              }}
              width={'100%'}
            />
            <TitleInput
              title={'Email*'} placeholder={'Електронна пошта'} value={request.email!}
              isInvalid={!!errors.email}
              errorMessage={[...new Set(errors.email?._errors)].join(', ')}
              onChange={(e) => updateRequest('email', e.target.value)}
              width={'100%'}
              isDisabled={!registrationStep.enableEmail}
            />
            <TitleInput
              title={'Firstname*'} placeholder={'Iм\'я'} value={request.firstName!}
              isInvalid={!!errors.firstName}
              errorMessage={errors.firstName?._errors?.join(', ')}
              onChange={(e) => updateRequest('firstName', e.target.value)}
              width={'100%'}
            />
            <TitleInput
              title={'Lastname*'} placeholder={'Прізвище'} value={request.lastName!}
              isInvalid={!!errors.lastName}
              errorMessage={errors.lastName?._errors?.join(', ')}
              onChange={(e) => updateRequest('lastName', e.target.value)}
              width={'100%'}
            />
            <TitleInput
              title={'Middlename'} placeholder={'По-батькові'} value={request.middleName}
              isInvalid={!!errors.middleName}
              errorMessage={errors.middleName?._errors?.join(', ')}
              onChange={(e) => updateRequest('middleName', e.target.value === '' ? undefined : e.target.value)}
              width={'100%'}
            />
            <Button onClick={secondStep}>Зареєструватись</Button>
          </AccordionPanel>
        </AccordionItem>
      </Accordion>
    </>
  );
};

interface ILoginErrors {
  username: boolean,
  email: boolean,
  firstName: boolean,
  lastName: boolean,
  middleName: boolean,
}

export default Login;
