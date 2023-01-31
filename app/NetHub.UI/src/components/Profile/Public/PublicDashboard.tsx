import React, {useState} from 'react';
import FilledDiv from "../../UI/FilledDiv";
import cl from "../Profile.module.sass";
import {getTimeFrom} from "../../../utils/timeHelper";
import {Box, Image, Skeleton, Text, useColorModeValue} from "@chakra-ui/react";
import SvgSelector from "../../UI/SvgSelector/SvgSelector";
import {createImageFromInitials} from "../../../utils/logoGenerator";
import millify from "millify";
import {useProfileContext} from "../../../pages/Profile/ProfileSpace.Provider";

const PublicDashboard = () => {
  const {userAccessor, dashboardAccessor} = useProfileContext();
  const user = userAccessor.data!;
  const dashboard = dashboardAccessor.data!;

  const getImage = () => user.profilePhotoUrl ?? createImageFromInitials(500, user.userName);
  const [image, setImage] = useState<string>(getImage());
  const handleImageError = () => {
    setImage(createImageFromInitials(500, user.userName));
  }
  const articlesCount = () => millify(dashboard!.articlesCount);
  const articlesViews = () => millify(dashboard!.articlesViews);

  const descriptionBg = useColorModeValue('whiteLight', 'whiteDark');

  return (
    <FilledDiv display={'flex'} flexDirection={'column'} mb={'20px'}>
      <Box
        className={cl.dashboardWrapper}
        justifyContent={dashboard!.articlesCount === 0 ? '' : 'space-between'}
      >
        <div className={cl.dashboardMainImage}>
          {
            image
              ? <Image
                src={image}
                minH={120}
                minW={120}
                maxW={120}
                onError={handleImageError}
                alt={'damaged'}
              />
              : <Skeleton minH={115} minW={115}/>
          }
          <SvgSelector id={'DriveFileRenameOutlineIcon'}/>
        </div>
        <div className={cl.dashboardInfoWrapper}>
          <div className={cl.dateBlock}>
            <span>{getTimeFrom(user.registered)}</span> разом з NetHub
          </div>
          {dashboard!.articlesCount === 0 ?
            <div className={cl.dashboardEmpty}>
              <Text as={'p'}>
                {user.userName} ще не було опублікувано жодної статті.
              </Text>
            </div>
            :
            <div className={cl.filledDashboard}>
              <div style={{width: articlesViews().length > 4 ? '50%' : '40%'}} className={cl.dashboardInfoBlock}>
                <Text as={'p'}>
                  Опубліковано:
                </Text>
                <div>
                  <Text as={'p'}>{articlesCount()}</Text>
                  <Text as={'p'}>статтей</Text>
                </div>
              </div>

              <div style={{width: articlesViews().length > 5 ? '50%' : '40%'}} className={cl.dashboardInfoBlock}>
                <Text as={'p'}>
                  Статті зібрали:
                </Text>
                <div>
                  <Text as={'p'}>{articlesViews()}</Text>
                  <Text as={'p'}>переглядів</Text>
                </div>
              </div>
            </div>
          }
        </div>
      </Box>

      {
        user.description ?
          <FilledDiv bg={descriptionBg}>
            <Text as={'p'} color={'black'}>{user.description}</Text>
          </FilledDiv>
          : null
      }

    </FilledDiv>
  );
};

export default PublicDashboard;
