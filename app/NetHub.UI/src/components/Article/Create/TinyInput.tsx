import React, {
  forwardRef,
  ForwardRefRenderFunction,
  useCallback,
  useImperativeHandle,
  useRef
} from 'react';
import { Editor } from '@tinymce/tinymce-react';
import { Editor as TinyEditor } from 'tinymce';
import classes from './ArticleCreating.module.sass';
import { articlesApi } from "../../../api/api";
import { tinyConfig } from "../../../utils/constants";
import ILocalization from "../../../types/ILocalization";
import { Box, FormControl, useColorModeValue } from "@chakra-ui/react";
import { _articlesApi, _myArticlesApi } from "../../../api";
import { ArticleCreateRequest, IArticleCreateRequest } from "../../../api/_api";

interface ITinyInputProps {
  data: string;
  setData: (value: string) => void;
  editorTitle: string;
  isInvalid: boolean,
  errorMessage?: string
}

interface ITinyInputHandle {
  saveImages: (article: ILocalization, id?: string) => Promise<string>;
}

const TinyInput: ForwardRefRenderFunction<ITinyInputHandle, ITinyInputProps> =
  ({data, setData, editorTitle, isInvalid, errorMessage}, ref) => {
    const editorRef = useRef<TinyEditor | null>(null);

    const saveImageCallback = useCallback(async (article: ILocalization, id?: string) => {

      if (!id) {
        const request = ArticleCreateRequest.fromJS({
          name: article.title,
          tags: article.tags,
          originalArticleLink: article.originalLink!
        });

        id = (await _articlesApi.create(request)).id.toString();
      }

      sessionStorage.setItem('articleId', id!);
      await editorRef.current!.uploadImages();
      sessionStorage.removeItem('articleId');

      const newData = editorRef.current!.getContent();
      setData(newData);

      return id;
    }, []);

    useImperativeHandle(ref, () => ({
      async saveImages(article: ILocalization, id?: string) {
        return await saveImageCallback(article, id)
      }
    }), [saveImageCallback]);


    const saveImages = async (blobInfo: any) => {
      const id = sessionStorage.getItem('articleId');
      if (!id) return;
      const {location} = await _articlesApi.uploadImage(+id, {data: blobInfo.blob(), fileName: 'File'});

      return location;
    };

    const errorColor = useColorModeValue('error', 'errorDark');

    return (
      <div className={classes.tinyInput}>
        <p>{editorTitle}</p>
        <FormControl width={'100%'} isInvalid={isInvalid}>
          <div className={isInvalid ? classes.wrong : ''}>
            <Editor
              onInit={(evt, editor) => editorRef.current = editor}
              value={data}
              onEditorChange={(newValue, _) => {
                setData(newValue);
                // localStorage.setItem('articleBody', data)
              }}
              apiKey={tinyConfig.key}
              init={{
                height: 500,
                menubar: true,
                plugins: tinyConfig.plugins,
                toolbar: tinyConfig.toolbar,
                content_style:
                  'body { font-family:Helvetica,Arial,sans-serif; font-size:14px }',
                images_upload_handler: saveImages,
                automatic_uploads: false,
              }}
            />
          </div>
          {isInvalid && !!errorMessage
            ? <Box mt={'0.5rem'} color={errorColor} fontSize={'0.875rem'}>{errorMessage}</Box>
            : null
          }
        </FormControl>
      </div>
    );
  };

export default forwardRef(TinyInput);
