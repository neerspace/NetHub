import React, {FC} from 'react';
import cl from './Profile.module.sass'
import useValidator from "../../hooks/useValidator";
import {isNotNullOrWhiteSpace, regexTest} from "../../utils/validators";
import {imageLinkRegex} from "../../utils/regex";
import useCustomSnackbar from "../../hooks/useCustomSnackbar";
import ProfileImageDrop from "./ProfileImageDrop";
import {
  Box,
  Button,
  Input,
  Modal,
  ModalBody,
  ModalContent,
  ModalHeader,
  ModalOverlay,
  Text,
  useColorModeValue
} from "@chakra-ui/react";

interface ISetImageModalProps {
  isModalOpened: boolean,
  closeModal: () => void,
  imageLink: string,
  setImageLink: (value: string) => void,
  onClick: () => void,
  handleDrop: (e: React.DragEvent<HTMLSpanElement>) => Promise<void>
}

const SetImageModal: FC<ISetImageModalProps> = ({
                                                  isModalOpened,
                                                  closeModal,
                                                  imageLink,
                                                  setImageLink,
                                                  onClick,
                                                  handleDrop
                                                }) => {

  const {subscribeValidator, validateAll, errors} = useValidator<{ link: boolean }>();
  const {enqueueError} = useCustomSnackbar();

  const handleOnClick = async () => {
    subscribeValidator({
      value: imageLink,
      field: 'link',
      validators: [regexTest(imageLinkRegex), isNotNullOrWhiteSpace],
      message: 'Не правильне посилання'
    })

    const {isSuccess, errors} = await validateAll();

    if (!isSuccess) {
      errors.forEach(e => enqueueError(e))
      return
    }

    onClick();
    closeModal();
  }

  return (
    <Modal
      isOpen={isModalOpened}
      onClose={closeModal}
    >
      <ModalOverlay/>
      <ModalContent bg={useColorModeValue('violetLight', '#1F2023')} minW={'45%'} paddingBottom={3} top={150}>
        <ModalHeader><Text as={'h6'}>Редагування фотографії профілю</Text></ModalHeader>
        <ModalBody className={cl.setImageModalBody}>
          <Box className={cl.leftModalBlock}>
            <Text as={'p'}>Додати посилання на фото</Text>
            <Input
              isInvalid={errors.link} width={'100%'} placeholder={'Посилання'} value={imageLink}
              onChange={(e) => setImageLink(e.target.value)}
            />
            <Button onClick={handleOnClick} width={'100%'}>Зберегти</Button>
          </Box>
          <Box
            width={1} height={'15vh'}
            margin={'0 30px'}
            borderRadius={10}
            bg={'#FFFFFF'}
          />
          <Box className={cl.rightModalBlock}>
            <ProfileImageDrop onDrop={handleDrop}/>
          </Box>
        </ModalBody>
      </ModalContent>
    </Modal>
  );
};

export default SetImageModal;
