import {
  Box,
  Button,
  Modal,
  ModalContent,
  ModalOverlay,
  Text,
  Textarea,
  useColorModeValue
} from '@chakra-ui/react';
import React, { FC, useState } from 'react';
import TitleInput from "../UI/TitleInput/TitleInput";
import FilledDiv from "../UI/FilledDiv";
import { FeedbackSchema, FeedbackType } from "../../types/schemas/Feedback/FeedbackSchema";
import { z as u } from "zod/lib";
import { CreateArticleFormError } from "../../pages/Articles/Create/ArticleCreateSpace.Provider";
import { CreateArticleFormSchema } from "../../types/schemas/Article/CreateArticleFormSchema";
import useCustomSnackbar from "../../hooks/useCustomSnackbar";
import { _feedbacksApi } from "../../api";
import { FeedbackCreateRequest } from "../../api/_api";

export type FeedbackErrors = u.ZodFormattedError<{
  name: string,
  email: string,
  message: string,
}>;

interface IFeedbackModalProps {
  isOpen: boolean,
  onClose: () => void
}

const FeedbackModal: FC<IFeedbackModalProps> = ({isOpen, onClose}) => {
  const {enqueueSuccess, enqueueError} = useCustomSnackbar();
  const [feedbackState, setFeedbackState] = useState<FeedbackType>({
    name: '',
    email: '',
    message: ''
  });

  const [errors, setErrors] = useState<FeedbackErrors>({_errors: []});

  const sendFeedback = async () => {
    if (!await validateFeedbackForm()) return;

    const request = new FeedbackCreateRequest(feedbackState);
    await _feedbacksApi.create(request);

    enqueueSuccess('Дякуємо за відгук :)')
  }

  async function validateFeedbackForm() {
    const validationResult = FeedbackSchema.safeParse(feedbackState);

    if (!validationResult.success) {
      const errors = validationResult.error.format()
      setErrors(errors);
      errors._errors.forEach(e => enqueueError(e))
      return;
    }

    setErrors({_errors: []});

    return validationResult.success;
  }

  return <Modal isOpen={isOpen} onClose={onClose}>
    <ModalOverlay/>
    <ModalContent minW={'50%'} borderRadius={'12px'}>
      <FilledDiv display={'flex'} flexDirection={'column'}
                 bg={useColorModeValue('violetLight', '#1F2023')}>
        <Box display={'flex'} justifyContent={'space-between'} gap={'40px'}>
          <TitleInput title={'Ім\'я'}
                      isInvalid={!!errors.name}
                      errorMessage={errors.name?._errors.join(', ')}/>
          <TitleInput title={'Пошта'} isInvalid={!!errors.email}
                      errorMessage={errors.email?._errors.join(', ')}/>
        </Box>
        <Text as={'p'} mb={'10px'}>Відгук</Text>
        <Textarea _focusVisible={{}} rows={10}
                  resize={'none'} isInvalid={!!errors.message}

        />
        <Text as={'p'} display={'block'}
              width={'100%'} textAlign={'end'}
              mb={'15px'} mt={'5px'} fontSize={12}>
          Макс.: 700
        </Text>
        <Button>Відправити</Button>
      </FilledDiv>
    </ModalContent>
  </Modal>
};

export default FeedbackModal;