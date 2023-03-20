import React, { FC, useState } from 'react';
import cl from "../ArticleCreate.module.sass";
import { Avatar, Box, Text } from "@chakra-ui/react";
import FilledDiv from "../../../UI/FilledDiv";
import Selection from "../../../Shared/Selection";
import ArticleContributorsSearch from "./ArticleContributorsSearch";
import { createImageFromInitials } from "../../../../utils/logoGenerator";
import {
  IArticleCreateExtendedRequest
} from "../../../../pages/Articles/Create/ArticleCreateSpace.Provider";
import { ArticleContributorRole, PrivateUserResult } from "../../../../api/_api";

export type Contributor = { user: PrivateUserResult, role: ArticleContributorRole };

interface IAddContributorsBlockProps {
  article: IArticleCreateExtendedRequest,
  setArticle: (article: IArticleCreateExtendedRequest) => void
}

const ArticleContributors: FC<IAddContributorsBlockProps> = ({article, setArticle}) => {
  const [contributors, setContributors] = useState<Contributor[]>([]);

  const setContributorsHandle = (contributors: Contributor[]) => {
    setContributors(contributors);
    setArticle({
      ...article, contributors: contributors.map(c => {
        return {userId: c.user.id, role: c.role, userName: c.user.userName, profilePhotoUrl: c.user.profilePhotoUrl}
      })
    })
  }

  const onTagClickHandle = (contributor: Contributor) => {
    const newContributors = contributors.filter(({user, role}) => {
      if (user.userName === contributor.user.userName)
        if (role === contributor.role)
          return false;
      return true;
    })

    setContributors(newContributors);
    setArticle({
      ...article, contributors: newContributors.map(c => {
        return {userId: c.user.id, role: c.role, userName: c.user.userName, profilePhotoUrl: c.user.profilePhotoUrl}
      })
    })
  }

  return (
    <FilledDiv className={cl.addContributor} mt={'20px'}>
      <Text as={'p'} mb={'10px'}>
        Співавтори
      </Text>
      <Box className={cl.option}>
        <ArticleContributorsSearch contributors={contributors} setContributors={setContributorsHandle}/>
      </Box>
      {contributors
        ? <Box display={'flex'} gap={2} mt={2} flexWrap={'wrap'}>
          {contributors.map(c =>
            <Selection value={c.user.userName} onClick={() => onTagClickHandle(c)}>
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
            </Selection>)}
        </Box>
        : null
      }
    </FilledDiv>
  );
};

export default ArticleContributors;
