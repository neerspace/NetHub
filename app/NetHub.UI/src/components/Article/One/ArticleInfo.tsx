import React from 'react';
import cl from './Article.module.sass'
import { createImageFromInitials } from "../../../utils/logoGenerator";
import { getArticleContributors } from "../../../pages/Articles/One/ArticleSpace.functions";
import { useNavigate } from "react-router-dom";
import { useQuery } from "react-query";
import ArticleContributorsSkeleton from "./ArticleContributorsSkeleton";
import FilledDiv from '../../UI/FilledDiv';
import { Button, Link, Skeleton, Text, useColorModeValue } from "@chakra-ui/react";
import { useArticleContext } from "../../../pages/Articles/One/ArticleSpace.Provider";
import { QueryClientKeysHelper } from "../../../utils/QueryClientKeysHelper";
import ArticleLanguages from "../Shared/ArticleLanguages";

const ArticleInfo = () => {
  const {articleSetAccessor, articleAccessor} = useArticleContext();

  const navigate = useNavigate();
  const whiteTextColor = useColorModeValue('whiteLight', 'whiteDark');


  const contributors = useQuery(QueryClientKeysHelper.Contributors(articleAccessor.data!.articleSetId, articleAccessor.data!.languageCode),
    () => getArticleContributors(articleAccessor.data!.contributors));

  const getDomain = (link: string) => {
    const url = new URL(link);
    const domain = url.hostname.replace('www.', '');
    return domain.charAt(0).toUpperCase() + domain.slice(1);
  }

  const divBg = useColorModeValue('purpleLight', 'purpleDark');

  return (
    <div className={cl.articleInfo}>
      {
        !articleAccessor.isSuccess || !articleSetAccessor.isSuccess
          ? <Skeleton height={100} className={cl.infoBlock}/>
          : <ArticleLanguages languages={
            articleSetAccessor.data.articles!.map(a => {
              return {
                code: a.languageCode,
                action: () => navigate(`/article/${a.articleSetId}/${a.languageCode}`)
              }
            })
          }/>
      }
      {
        !articleAccessor.isSuccess ? <Skeleton height={100} className={cl.infoBlock}/> :
          <FilledDiv className={cl.infoBlock}>
            <Text as={'p'} className={cl.infoBlockTitle}>Автори:</Text>
            <div className={cl.contributors}>
              {contributors.isLoading ?
                <ArticleContributorsSkeleton/> : contributors.data!.map(contributor =>
                  <Button
                    key={contributor.id + contributor.role}
                    className={cl.contributor}
                    width={'fit-content'}
                    bg={divBg}
                    borderRadius={'10px'}
                    padding={'6px 15px'}
                    color={whiteTextColor}
                    onClick={() => navigate('/profile/' + contributor.userName)}
                  >
                    <div className={cl.role}>
                      <Text as={'p'} color={whiteTextColor}>{contributor.role}</Text>
                      <Text as={'p'} color={whiteTextColor}>{contributor.userName}</Text>
                    </div>
                    <img
                      src={contributor.profilePhotoUrl ?? createImageFromInitials(25, contributor.userName)}
                      alt={'damaged'}
                      onError={e => {
                        e.currentTarget.src = createImageFromInitials(25, contributor.userName)
                      }}
                    />
                  </Button>
                )}
            </div>
          </FilledDiv>
      }

      {
        !articleSetAccessor.isSuccess ? <Skeleton height={100} className={cl.infoBlock}/> :
          articleSetAccessor.data.originalArticleLink &&
          <FilledDiv className={cl.infoBlock}>
            <Text as={'p'} className={cl.infoBlockTitle}>Перейти до оригіналу:</Text>
            <Button
              background={'#896DC8'}
              borderRadius={'10px'}
              padding={'5px 16px'}
              className={cl.originalLink}
              width={'fit-content'}
              bg={divBg}
            >
              <Link
                color={whiteTextColor}
                href={articleSetAccessor.data.originalArticleLink}
                target={'_blank'}
              >
                {getDomain(articleSetAccessor.data.originalArticleLink)}
              </Link>
            </Button>
          </FilledDiv>
      }
    </div>
  );
};

export default ArticleInfo;
