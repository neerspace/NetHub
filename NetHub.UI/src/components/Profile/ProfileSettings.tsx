import React, {useEffect, useRef, useState} from 'react';
import TitleInput from "../UI/TitleInput/TitleInput";
import SvgSelector from "../UI/SvgSelector/SvgSelector";
import cl from './Profile.module.sass'
import {
  Accordion,
  AccordionButton,
  AccordionItem,
  AccordionPanel,
  Button,
  Text,
  useColorModeValue
} from "@chakra-ui/react";
import FilledDiv from "../UI/FilledDiv";
import {z as u} from "zod";
import {useProfileContext} from "../../pages/Profile/ProfileSpace.Provider";
import AnimateHeight from "react-animate-height";
import {useProfileUpdateFunctions} from "../../pages/Profile/ProfileSpace.functions";


const ProfileSettings = () => {
  const accordionButtonRef = useRef<HTMLButtonElement>(null);
  const [isSettingsExpanded, setIsSettingsExpanded] = useState<boolean>(false);
  const [errors, setErrors] = useState<u.ZodFormattedError<
    {
      middleName?: string | undefined,
      description?: string | undefined,
      username: string,
      email: string,
      firstName: string,
      lastName: string
    }
  >>({_errors: []});
  const {changeRequest, changes} = useProfileContext();

  const handleSettingsButton = () => {
    setIsSettingsExpanded(!isSettingsExpanded)
    accordionButtonRef?.current!.click();
  }

  const {
    handleUpdateUsername,
    handleUpdateProfileInfo,
    updateProfile
  } = useProfileUpdateFunctions(errors, setErrors, handleSettingsButton);


  return (
    <>
      <Accordion allowToggle>
        <AccordionItem border={0}>
          <FilledDiv>
            <div className={cl.settingsTitle}>
              <div>
                <div>
                  <SvgSelector id={'Settings'}/>
                </div>
                <Text fontWeight={700} fontSize={20} as={'p'}>Уточніть інформацію</Text>
              </div>
              <AccordionButton
                ref={accordionButtonRef}
                width={'fit-content'}
                bg={useColorModeValue('#896DC8', '#835ADF')}
                borderRadius={'12px'}
                minW={'120px'}
                minH={'40px'}
                justifyContent={'center'}
                onClick={handleSettingsButton}
                _hover={{bg: '#BBAFEA'}}
              >
                <Text
                  as={'b'}
                  color={useColorModeValue('#FFFFFF', '#EFEFEF')}
                  fontWeight={'semibold'}
                  fontSize={14}
                >
                  Змінити
                </Text>
              </AccordionButton>
            </div>
          </FilledDiv>
          <AccordionPanel paddingInlineStart={0} paddingInlineEnd={0}>
            <FilledDiv
              width={'100%'}
            >
              <TitleInput
                title={'Username*'} placeholder={'Ім\'я користувача'} value={changeRequest.username}
                isInvalid={!!errors.username}
                errorMessage={errors.username?._errors?.join(', ')}
                onChange={handleUpdateUsername}
                width={'100%'}
              />
              <TitleInput
                title={'Email'} placeholder={'Електронна пошта'} defaultValue={changeRequest.email}
                isInvalid={!!errors.email}
                errorMessage={errors.email?._errors?.join(', ')}
                width={'100%'}
              />
              <TitleInput
                title={'Firstname*'} placeholder={'Iм\'я'} value={changeRequest.firstName}
                isInvalid={!!errors.firstName}
                errorMessage={errors.firstName?._errors?.join(', ')}
                onChange={(e) => handleUpdateProfileInfo({...changeRequest, firstName: e.target.value})}
                width={'100%'}
              />
              <TitleInput
                title={'Lastname*'} placeholder={'Прізвище'} value={changeRequest.lastName}
                isInvalid={!!errors.lastName}
                errorMessage={errors.lastName?._errors?.join(', ')}
                onChange={(e) => handleUpdateProfileInfo({...changeRequest, lastName: e.target.value})}
                width={'100%'}
              />
              <TitleInput
                title={'Middlename'} placeholder={'По-батькові'} value={changeRequest.middleName ?? ''}
                isInvalid={!!errors.middleName}
                errorMessage={errors.middleName?._errors?.join(', ')}
                onChange={(e) => handleUpdateProfileInfo({...changeRequest, middleName: e.target.value})}
                width={'100%'}
              />
              <TitleInput
                title={'Description'} placeholder={'Опис'} value={changeRequest.description ?? ''}
                isInvalid={!!errors.description}
                errorMessage={errors.description?._errors?.join(', ')}
                onChange={(e) => handleUpdateProfileInfo({...changeRequest, description: e.target.value})}
                width={'100%'}
              />
            </FilledDiv>
          </AccordionPanel>
        </AccordionItem>
      </Accordion>
      <AnimateHeight
        height={changes.length > 0 ? 'auto' : 0}
        duration={700}
      >
        <div className={cl.saveChangesBlock}>
          <Text as={'p'}>
            Ви внесли зміни до профілю, збережіть їх
          </Text>
          <Button onClick={updateProfile} padding={'15px 95px'}>Зберегти</Button>
        </div>
      </AnimateHeight>
    </>
  );
};

export default ProfileSettings;
