import React, {FC, useState} from 'react';
import SvgSelector from "../UI/SvgSelector/SvgSelector";
import {Text, useColorModeValue} from "@chakra-ui/react";
import FilledDiv from "../UI/FilledDiv";

interface IProfileImageDropProps {
  onDrop: (e: React.DragEvent<HTMLSpanElement>) => Promise<void>,
}

const ProfileImageDrop: FC<IProfileImageDropProps> = ({onDrop}) => {

  const [drag, setDrag] = useState<boolean>(false);
  const dndBg = useColorModeValue('whiteLight', 'whiteDark');

  const handleDrop = async (e: React.DragEvent<HTMLSpanElement>) => {
    await onDrop(e);
    setDrag(false);
  }


  const handleDragStart = (e: React.DragEvent<HTMLSpanElement>) => {
    e.preventDefault();
    setDrag(true);
  }

  const handleDragLeave = (e: React.DragEvent<HTMLSpanElement>) => {
    e.preventDefault();
    setDrag(false);
  }


  return (
    <FilledDiv
      onDragStart={handleDragStart}
      onDragLeave={handleDragLeave}
      onDragOver={handleDragStart}
      onDrop={handleDrop}
      background={drag ? 'lightgray' : dndBg} padding={'33px 76px'}
    >
      <Text
        as={'p'}
        color={'#838383'}
        fontWeight={700}
      >
        {drag ? 'Відпустіть для відправки' : 'Перетягніть фото сюди'}
      </Text>
      <SvgSelector id={'Dnd'}/>
    </FilledDiv>
  );
};

export default ProfileImageDrop;
