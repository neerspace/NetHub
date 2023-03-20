import React, {FC, useEffect, useState} from 'react';
import cl from "./Profile.module.sass";
import SvgSelector from "../UI/SvgSelector/SvgSelector";
import {createImageFromInitials} from "../../utils/logoGenerator";
import {Image} from "@chakra-ui/react";
import { useAppStore } from "../../store/store";
import {useDnD} from "../../hooks/useDnD";

interface IDashboardImageProps {
  openModal: () => void,
  handleDrop: (e: React.DragEvent<HTMLSpanElement>) => Promise<void>,
}

const ProfileDashboardImage: FC<IDashboardImageProps> = ({openModal, handleDrop: onDrop}) => {

  const user = useAppStore(state => state.user);
  const {dragState, handleDragStart, handleDrop, handleDragLeave} = useDnD(onDrop);
  const getImage = () => user.profilePhotoUrl ?? createImageFromInitials(500, user.username);
  const [image, setImage] = useState<string>(getImage());

  useEffect(() => {
    setImage(getImage())
  }, [user.profilePhotoUrl])

  const handleImageError = () => {
    setImage(createImageFromInitials(500, user.username));
  }

  return (
    <div
      onDragStart={handleDragStart}
      onDragLeave={handleDragLeave}
      onDragOver={handleDragStart}
      onDrop={handleDrop}
      className={`${cl.dashboardMainImage} + ${dragState ? cl.darkFilter : ''}`} onClick={openModal}
    >
      <Image
        src={image}
        minH={120}
        minW={120}
        maxW={120}
        onError={handleImageError}
        alt={'damaged'}
      />
      <SvgSelector id={'DriveFileRenameOutlineIcon'}/>
    </div>
  );
};

export default ProfileDashboardImage;
