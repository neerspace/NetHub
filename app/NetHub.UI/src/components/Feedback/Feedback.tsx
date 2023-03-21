import React from 'react';
import { Button, Text, useDisclosure } from '@chakra-ui/react';
import FilledDiv from "../UI/FilledDiv";
import FeedbackModal from "./FeedbackModal";

const Feedback = () => {
  const { isOpen, onOpen, onClose } = useDisclosure()

  return (
    <FilledDiv>
      <Text as={'p'} mb={'15px'}>
        Ми дуже вам вдячні за те що ви використовуєте наш ресурс! Будь ласка допоможіть нам зробити його ще краще, залиште свій відгук натиснувши кнопку нижче.
      </Text>
      <Button w={'100%'} onClick={onOpen}>Залишити відгук</Button>
      <FeedbackModal isOpen={isOpen} onClose={onClose}/>
    </FilledDiv>
  );
};

export default Feedback;