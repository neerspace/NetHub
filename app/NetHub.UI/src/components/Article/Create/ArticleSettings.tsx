import React, {FC} from 'react';
import classes from './ArticleCreating.module.sass';
import ArticleTagsSettings from "./ArticleTagsSettings";
import TitleInput from "../../UI/TitleInput/TitleInput";
import ArticleImagesSettings from "./ArticleImagesSettings";
import {ArticleStorage} from "../../../utils/localStorageProvider";
import FilledDiv from '../../UI/FilledDiv';
import {Button, Text} from "@chakra-ui/react";
import {useArticleCreatingContext} from "../../../pages/Articles/Create/ArticleCreatingSpace.Provider";

interface IArticleSettingsProps {
  createArticle: () => Promise<void>,
}

const ArticleSettings: FC<IArticleSettingsProps> = ({createArticle}) => {

  const {article, setArticle, images, errors} = useArticleCreatingContext();

  const handleSetLink = (event: React.ChangeEvent<HTMLInputElement>) => {
    setArticle({...article, originalLink: event.target.value});
    ArticleStorage.setLink(event.target.value);
  }
  const handleSetTags = (tag: string) => {
    const allTags = [...article.tags, tag];
    setArticle({...article, tags: allTags});
    ArticleStorage.setTags(JSON.stringify(allTags));
  }
  const handleDeleteTag = (tag: string) => {
    const filteredTags = article.tags.filter(t => t !== tag);
    setArticle({...article, tags: filteredTags});
    ArticleStorage.setTags(JSON.stringify(filteredTags));
  }

  return (
    <div className={classes.articleSettings}>
      <FilledDiv>
        <Text as={'p'} className={classes.title}>Теги по темам</Text>
        <ArticleTagsSettings
          addToAllTags={handleSetTags}
          deleteTag={handleDeleteTag}
        />
        <Text as={'p'} className={classes.specification}>*натисність на тег, для його видалення</Text>
      </FilledDiv>
      <FilledDiv className={classes.settingsItem}>
        <TitleInput
          isInvalid={!!errors.originalLink}
          errorMessage={errors.originalLink?._errors.join(', ')}
          value={article.originalLink}
          onChange={handleSetLink}
          title={"Посилання на оригінал "}
          placeholder={"Посилання на статтю"}
          width={"100%"}
        />
        <Text as={'p'} style={{marginTop: '-10px'}} className={classes.specification}>*якщо стаття переведена, вкажіть
          посилання на
          оригінал</Text>
      </FilledDiv>
      {images?.data !== undefined && images.data.length > 0 &&
       <FilledDiv className={classes.settingsItem}>
         <Text as={'p'} className={classes.title}>Пропоновані зображення</Text>
         <ArticleImagesSettings/>
         <Text as={'p'} className={classes.specification}>*натисність, щоб скопіювати посилання на фото</Text>
       </FilledDiv>
      }
      <Button onClick={createArticle}>Зберегти статтю</Button>
      {/*{<pre>{JSON.stringify(errors, null, 2)}</pre>}*/}
      {/*{<pre>{JSON.stringify(article, null, 2)}</pre>}*/}
    </div>
  );
};

export default ArticleSettings;
