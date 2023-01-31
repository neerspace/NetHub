import React from 'react';
import SvgSelector from "../../UI/SvgSelector/SvgSelector";
import cl from './ArticleInfo.module.sass'
import {createImageFromInitials} from "../../../utils/logoGenerator";
import {getArticleContributors} from "../../../pages/Articles/One/ArticleSpace.functions";
import {useNavigate} from "react-router-dom";
import {useQuery} from "react-query";
import ContributorsSkeleton from "./ContributorsSkeleton";
import FilledDiv from '../../UI/FilledDiv';
import {Button, Link, Skeleton, Text, useColorModeValue} from "@chakra-ui/react";
import {useArticleContext} from "../../../pages/Articles/One/ArticleSpace.Provider";

const ArticleInfo = () => {
  const {articleAccessor, localizationAccessor} = useArticleContext();

  const navigate = useNavigate();
  const whiteTextColor = useColorModeValue('whiteLight', 'whiteDark');

  const contributors = useQuery(['contributors', localizationAccessor.data!.articleId, localizationAccessor.data!.languageCode],
    () => getArticleContributors(localizationAccessor.data!.contributors));

  const getDomain = (link: string) => {
    const url = new URL(link);
    const domain = url.hostname.replace('www.', '');
    return domain.charAt(0).toUpperCase() + domain.slice(1);
  }

  const divBg = useColorModeValue('purpleLight', 'purpleDark');

  return (
      <div className={cl.articleInfo}>
        {
          !localizationAccessor.isSuccess || !articleAccessor.isSuccess ? <Skeleton height={100} className={cl.infoBlock}/> :
            <FilledDiv className={cl.infoBlock}>
              <p className={cl.infoBlockTitle}>Переклади:</p>
              <div className={cl.translates}>
                {articleAccessor.data.localizations?.map(localization =>
                  <Button
                    onClick={() => navigate(`/article/${localization.articleId}/${localization.languageCode}`)}
                    key={localization.languageCode}
                    borderRadius={'10px'}
                    padding={'5px 16px'}
                    width={'fit-content'}
                  >
                    <Text as={'p'} mr={2} color={whiteTextColor}>
                      {localization.languageCode.toUpperCase()}
                    </Text>
                    <SvgSelector id={localization.languageCode}/>
                  </Button>
                )}
              </div>
            </FilledDiv>
        }

        {
          !localizationAccessor.isSuccess ? <Skeleton height={100} className={cl.infoBlock}/> :
            <FilledDiv className={cl.infoBlock}>
              <Text as={'p'} className={cl.infoBlockTitle}>Автори:</Text>
              <div className={cl.contributors}>
                {contributors.isLoading ? <ContributorsSkeleton/> : contributors.data!.map(contributor =>
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
                    <img src={contributor.profilePhotoUrl ?? createImageFromInitials(25, contributor.userName)} alt={'damaged'}/>
                  </Button>
                )}
              </div>
            </FilledDiv>
        }

        {
          !articleAccessor.isSuccess ? <Skeleton height={100} className={cl.infoBlock}/> :
            articleAccessor.data.originalArticleLink &&
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
                  href={articleAccessor.data.originalArticleLink}
                  target={'_blank'}
                >
                  {getDomain(articleAccessor.data.originalArticleLink)}
                </Link>
              </Button>
            </FilledDiv>
        }
      </div>
  );
};

export default ArticleInfo;
