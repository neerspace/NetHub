import SvgSelector from "../../UI/SvgSelector/SvgSelector";
import classes from "./ArticleCreating.module.sass"
import React, {FC, useState} from "react";
import Tag from "../One/Body/Tag";
import {regexTest} from "../../../utils/validators";
import {tagRegex} from "../../../utils/regex";
import {Box, Button, FormControl, FormErrorMessage, Input} from "@chakra-ui/react";
import {useArticleCreatingContext} from "../../../pages/Articles/Create/ArticleCreatingSpace.Provider";

interface IArticleTagsSettingsProps {
  addToAllTags: (tag: string) => void,
  deleteTag: (tag: string) => void,
}

const ArticleTagsSettings: FC<IArticleTagsSettingsProps> = ({addToAllTags, deleteTag}) => {

  const {article, errors, setErrors} = useArticleCreatingContext();

  const [middleTag, setMiddleTag] = useState<string>('');

  const addTag = async () => {
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
    addToAllTags(middleTag)
    setMiddleTag('');
  }

  return (
    <>
      <div>
        <FormControl isInvalid={!!errors.tags} className={classes.fixedTags}>
          <Box display={'flex'} width={'100%'} justifyContent={'space-between'}>
            <Input
              placeholder={'Теги'}
              value={middleTag}
              onChange={(e) => setMiddleTag(e.target.value)}
              width={'75%'}
            />
            <Button onClick={addTag}>
              <SvgSelector id={"AddIcon"}/>
            </Button>
          </Box>
          {!!errors.tags
            ? <FormErrorMessage>{errors.tags?._errors?.join(', ')}
            </FormErrorMessage>
            : null}
        </FormControl>
      </div>
      <div className={classes.addedTags}>
        {
          article.tags.length > 0 &&
          article.tags.map(tag =>
            <Tag key={tag} value={tag} onClick={deleteTag}>
              #{tag}
            </Tag>)
        }
      </div>
    </>
  );
}

export default ArticleTagsSettings;
