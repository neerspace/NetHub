import React, {FC} from 'react';
import SvgSelector from "../UI/SvgSelector/SvgSelector";
import {Text, useColorModeValue} from "@chakra-ui/react";
import FilledDiv from "../UI/FilledDiv";
import {useDnD} from "../../hooks/useDnD";

interface IProfileImageDropProps {
  onDrop: (e: React.DragEvent<HTMLSpanElement>) => Promise<void>,
}

const ProfileImageDrop: FC<IProfileImageDropProps> = ({onDrop}) => {

  const {dragState, handleDragStart, handleDrop, handleDragLeave} = useDnD(onDrop);
  const dndBg = useColorModeValue('whiteLight', 'whiteDark');

  return (
    <FilledDiv
      onDragStart={handleDragStart}
      onDragLeave={handleDragLeave}
      onDragOver={handleDragStart}
      onDrop={handleDrop}
      background={dragState ? 'lightgray' : dndBg} padding={'33px 76px'}
    >
      <Text
        as={'p'}
        color={'#838383'}
        fontWeight={700}
      >
        {dragState ? 'Відпустіть для відправки' : 'Перетягніть фото сюди'}
      </Text>
      <SvgSelector id={'Dnd'}/>
    </FilledDiv>
  );
};

export default ProfileImageDrop;
