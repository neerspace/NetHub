import React, {FC} from 'react';
import cl from '../ArticleCreate.module.sass';
import ArticleTags from "./ArticleTags";
import TitleInput from "../../../UI/TitleInput/TitleInput";
import ArticleImages from "./ArticleImages";
import {ArticleStorage} from "../../../../utils/localStorageProvider";
import FilledDiv from '../../../UI/FilledDiv';
import {Button, Skeleton, Text} from "@chakra-ui/react";
import {
  useArticleCreatingContext
} from "../../../../pages/Articles/Create/ArticleCreateSpace.Provider";
import ArticleLanguages from "../../Shared/ArticleLanguages";

interface IArticleSettingsProps {
  createArticle: () => Promise<void>,
}

const ArticleCreateInfo: FC<IArticleSettingsProps> = ({createArticle}) => {

  const {
    article,
    setArticle,
    languagesAccessor,
    articleSet,
    errors,
    isFirst
  } = useArticleCreatingContext();

  const handleSetLink = (event: React.ChangeEvent<HTMLInputElement>) => {
    setArticle({...article, originalArticleLink: event.target.value});
    ArticleStorage.setLink(event.target.value);
  }
  const handleSetTags = (tag: string) => {
    const allTags = [...article.tags, tag];
    setArticle({...article, tags: allTags});
    ArticleStorage.setTags(JSON.stringify(allTags));
  }
  const handleDeleteTag = (tag: string) => {
    //User cannot change tags if articleSet previously created
    if (!isFirst)
      return;

    const filteredTags = article.tags.filter(t => t !== tag);
    setArticle({...article, tags: filteredTags});
    ArticleStorage.setTags(JSON.stringify(filteredTags));
  }

  const handleSetLanguage = (code: string) => {
    setArticle({...article, language: code})
  }

  return (
    <div className={cl.articleSettings}>
      {
        !languagesAccessor.isSuccess || !languagesAccessor.isSuccess
          ? <Skeleton height={100} className={cl.infoBlock}/>
          : <ArticleLanguages
            disabled={isFirst}
            languages={
              languagesAccessor.data.map(l => {
                return {
                  code: l.code,
                  action: () => handleSetLanguage(l.code)
                }
              })
            }
            errorMessage={errors.language?._errors.join(', ')}
          />
      }

      {isFirst
        ? <FilledDiv className={cl.infoBlock}>
          <Text as={'p'} className={cl.title}>Теги по темам</Text>
          <ArticleTags
            isDisabled={!isFirst}
            addToAllTags={handleSetTags}
            deleteTag={handleDeleteTag}
          />
          <Text as={'p'} className={cl.specification}>*натисність на тег, для його
            видалення</Text>
        </FilledDiv>
        : !articleSet?.isSuccess ? <Skeleton height={100} className={cl.infoBlock}/>
          : <FilledDiv className={cl.infoBlock}>
            <Text as={'p'} className={cl.title}>Теги по темам</Text>
            <ArticleTags
              isDisabled={!isFirst}
              addToAllTags={handleSetTags}
              deleteTag={handleDeleteTag}
            />
            <Text as={'p'} className={cl.specification}>*натисність на тег, для його
              видалення</Text>
          </FilledDiv>
      }
      <FilledDiv className={cl.infoBlock}>
        <TitleInput
          isDisabled={!isFirst}
          isInvalid={!!errors.originalLink}
          errorMessage={errors.originalLink?._errors.join(', ')}
          value={isFirst ? article.originalArticleLink ?? undefined : articleSet?.data?.originalArticleLink ?? undefined}
          onChange={handleSetLink}
          title={"Посилання на оригінал "}
          placeholder={"Посилання на статтю"}
          width={"100%"}
        />
        <Text as={'p'} style={{marginTop: '-10px'}} className={cl.specification}>
          *якщо стаття переведена, вкажіть посилання на оригінал
        </Text>
      </FilledDiv>
      {articleSet?.data !== undefined && articleSet.data.imagesLinks.length > 0 &&
        <FilledDiv className={cl.infoBlock}>
          <Text as={'p'} className={cl.title}>Пропоновані зображення</Text>
          <ArticleImages/>
          <Text as={'p'} className={cl.specification}>
            *натисність, щоб скопіювати посилання на фото
          </Text>
        </FilledDiv>
      }
      <Button onClick={createArticle}>Зберегти статтю</Button>
      {/*{<pre>{JSON.stringify(errors, null, 2)}</pre>}*/}
      {/*{<pre>{JSON.stringify(article, null, 2)}</pre>}*/}
      {/*{<pre>{JSON.stringify(articleSet?.data?.tags, null, 2)}</pre>}*/}
    </div>
  );
};

export default ArticleCreateInfo;
