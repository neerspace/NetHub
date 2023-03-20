import SvgSelector from "../../../UI/SvgSelector/SvgSelector";
import cl from "../ArticleCreate.module.sass"
import React, {FC, useState} from "react";
import Selection from "../../../Shared/Selection";
import {regexTest} from "../../../../utils/validators";
import {tagRegex} from "../../../../utils/regex";
import {Box, Button, FormControl, FormErrorMessage, Input} from "@chakra-ui/react";
import {
  useArticleCreatingContext
} from "../../../../pages/Articles/Create/ArticleCreateSpace.Provider";

interface IArticleTagsSettingsProps {
  addToAllTags: (tag: string) => void,
  deleteTag: (tag: string) => void,
  isDisabled?: boolean
}

const ArticleTags: FC<IArticleTagsSettingsProps> = ({
                                                              addToAllTags,
                                                              deleteTag,
                                                              isDisabled
                                                            }) => {

  const {article, articleSet, errors, setErrors, isFirst} = useArticleCreatingContext();

  const [middleTag, setMiddleTag] = useState<string>('');

  const addTag = async () => {
    if (isDisabled)
      return;

    if (article.tags.includes(middleTag) || middleTag === '') return;
    setErrors({...errors, tags: undefined});
    const isSuccess = regexTest(tagRegex)(middleTag);
    if (!isSuccess) {

      const newErrors = new Set(errors.tags?._errors ? [...errors.tags._errors] : []);
      newErrors.add('Неправильний тег');
      setErrors({
        ...errors,
        tags: {_errors: [...newErrors]}
      });
      return;
    }
    addToAllTags(middleTag.toLowerCase())
    setMiddleTag('');
  }

  return (
    <>
      <div>
        <FormControl isInvalid={!!errors.tags} className={cl.fixedTags}>
          <Box display={'flex'} width={'100%'} justifyContent={'space-between'}>
            <Input
              placeholder={'Теги'}
              value={middleTag}
              onChange={(e) => setMiddleTag(e.target.value)}
              width={'75%'}
              isDisabled={isDisabled}
            />
            <Button onClick={addTag} isDisabled={isDisabled}>
              <SvgSelector id={"AddIcon"}/>
            </Button>
          </Box>
          {!!errors.tags
            ? <FormErrorMessage>{errors.tags?._errors?.join(', ')}
            </FormErrorMessage>
            : null}
        </FormControl>
      </div>
      <div className={cl.addedTags}>
        {
          isFirst
            ? article.tags.map(tag =>
              <Selection key={tag} value={tag} onClick={deleteTag} isDisabled={isDisabled}>
                #{tag}
              </Selection>)
            : articleSet?.data?.tags.map(tag =>
              <Selection key={tag} value={tag} onClick={deleteTag} isDisabled={isDisabled}>
                #{tag}
              </Selection>
            )
        }
      </div>
    </>
  );
}

export default ArticleTags;
