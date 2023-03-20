import React, {forwardRef, ForwardRefRenderFunction, useImperativeHandle, useRef} from 'react';
import cl from './ArticleCreate.module.sass';
import TitleInput from '../../UI/TitleInput/TitleInput';
import TinyInput from "./ArticleCreateTinyInput";
import {ArticleStorage} from "../../../utils/localStorageProvider";
import FilledDiv from "../../UI/FilledDiv";
import {Box} from "@chakra-ui/react";
import ArticleContributors from "./Info/ArticleContributors";
import {useArticleCreatingContext} from "../../../pages/Articles/Create/ArticleCreateSpace.Provider";

interface IMainArticleProps {
}

export interface IMainArticleHandle {
  getTinyRef: () => React.RefObject<TinyRef>
}

type TinyRef = React.ElementRef<typeof TinyInput>;

const ArticleCreate: ForwardRefRenderFunction<IMainArticleHandle, IMainArticleProps> =
  ({}, ref) => {

    const {article, setArticle, setArticleValue, errors} = useArticleCreatingContext();

    const tinyRef = useRef<TinyRef>(null)

    const handleUpdateTitle = (event: React.ChangeEvent<HTMLInputElement>) => {
      setArticleValue('title')(event.target.value);
      ArticleStorage.setTitle(event.target.value)
    }

    const handleUpdateDescription = (event: React.ChangeEvent<HTMLInputElement>) => {
      setArticleValue('description')(event.target.value);
      ArticleStorage.setDescription(event.target.value)
    }

    const handleUpdateHtml = (value: string) => {
      setArticle({...article, html: value})
      ArticleStorage.setHtml(value)
    }

    useImperativeHandle(ref, () => ({
      getTinyRef() {
        return tinyRef;
      }
    }), [tinyRef]);

    return (
      <Box className={cl.createArticle}>
        <FilledDiv className={cl.mainArticleParams}>
          <TitleInput
            isInvalid={!!errors.title}
            errorMessage={errors.title?._errors?.join(', ')}
            value={article.title}
            onChange={handleUpdateTitle}
            title={'Заголовок статті'}
            placeholder={'Заголовок вашої статті'}
            width={'100%'}
          />
          <TitleInput
            isInvalid={!!errors.description}
            errorMessage={errors.description?._errors?.join(', ')}
            value={article.description}
            onChange={handleUpdateDescription}
            title={'Короткий опис статті'}
            placeholder={'Короткий опис вашої статті'}
            width={'100%'}
          />
          <TinyInput
            isInvalid={!!errors.html}
            errorMessage={errors.html?._errors?.join(', ')}
            data={article.html}
            setData={handleUpdateHtml}
            editorTitle={'Тіло статті'}
            ref={tinyRef}
          />
        </FilledDiv>
        <ArticleContributors article={article} setArticle={setArticle}/>
      </Box>
    );
  };

export default forwardRef(ArticleCreate);
