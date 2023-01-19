import React, {FC, useState} from 'react';
import classes from "./ArticleCreating.module.sass";
import {Avatar, Box, Text} from "@chakra-ui/react";
import FilledDiv from "../../UI/FilledDiv";
import Tag from "../One/Body/Tag";
import SearchContributor from "./SearchContributor";
import {IPrivateUserInfoResponse} from "../../../types/api/User/IUserInfoResponse";
import {createImageFromInitials} from "../../../utils/logoGenerator";
import ILocalization from "../../../types/ILocalization";

export type Contributor = { user: IPrivateUserInfoResponse, role: string };

interface IAddContributorsBlockProps {
  article: ILocalization,
  setArticle: (article: ILocalization) => void
}

const AddContributorsBlock: FC<IAddContributorsBlockProps> = ({article, setArticle}) => {
  const [contributors, setContributors] = useState<Contributor[]>([]);

  const setContributorsHandle = (contributors: Contributor[]) => {
    setContributors(contributors);
    setArticle({
      ...article, contributors: contributors.map(c => {
        return {userId: c.user.id, role: c.role}
      })
    })
  }

  const onTagClickHandle = (contributor: Contributor) => {
    setContributors(contributors.filter(({user, role}) => {
      if (user.userName === contributor.user.userName)
        if (role === contributor.role)
          return false;
      return true;
    }))
  }

  return (
    <FilledDiv className={classes.addContributor} mt={'20px'}>
      <Text as={'p'} mb={'10px'}>
        Співавтори
      </Text>
      <Box className={classes.option}>
        <SearchContributor contributors={contributors} setContributors={setContributorsHandle}/>
      </Box>
      {contributors
        ? <Box display={'flex'} gap={2} mt={2} flexWrap={'wrap'}>
          {contributors.map(c =>
            <Tag value={c.user.userName} onClick={() => onTagClickHandle(c)}>
              <Box display={'flex'} alignItems={'center'}>
                <Avatar
                  size='xs'
                  src={c.user.profilePhotoUrl ?? createImageFromInitials(500, c.user.userName)}
                  mr={2}
                />
                <Box display={'flex'} flexDirection={'column'} alignItems={'flex-start'}>
                  <Text as={'p'} fontSize={12}>{c.role}</Text>
                  <Text as={'b'} fontSize={14}>{c.user.userName}</Text>
                </Box>
              </Box>
            </Tag>)}
        </Box>
        : null
      }
    </FilledDiv>
  );
};

export default AddContributorsBlock;
