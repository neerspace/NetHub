import React, { FC, useState } from 'react';
import FilledDiv from "../../UI/FilledDiv";
import cl from "./Article.module.sass";
import { Box, Button, Text, useColorModeValue } from "@chakra-ui/react";
import SvgSelector from "../../UI/SvgSelector/SvgSelector";
import { useParams } from "react-router-dom";

type Language = { code: string, action: () => void }

interface IArticleLocalizationsProps {
  languages: Language[],
  disabled?: boolean,
  errorMessage?: string
}

const ArticleLanguages: FC<IArticleLocalizationsProps> =
  ({languages, disabled, errorMessage}) => {
    const {code} = useParams();

    const [selectedLanguage, setSelectedLanguage] = useState<string | null>(code ?? null);

    const whiteTextColor = useColorModeValue('whiteLight', 'whiteDark');
    const buttonBg = useColorModeValue('#896DC8', '#835ADF');
    const selectedButtonBg = useColorModeValue('red', 'green');
    const errorColor = useColorModeValue('error', 'errorDark');

    const onClickHandle = (language: Language) => {
      if (disabled) return;
      setSelectedLanguage(language.code);
      language.action();
    }

    const buttonColorCondition = (code: string) => {
      return code === selectedLanguage
        ? selectedButtonBg
        : buttonBg
    }

    return (
      <FilledDiv className={cl.infoBlock}>
        <p className={cl.infoBlockTitle}>Переклади:</p>
        <div className={cl.translates}>
          {languages.map(language =>
            <Button
              isDisabled={disabled}
              onClick={() => onClickHandle(language)}
              key={language.code}
              borderRadius={'10px'}
              padding={'5px 16px'}
              width={'fit-content'}
              backgroundColor={buttonColorCondition(language.code)}
            >
              <Text as={'p'} mr={2} color={whiteTextColor}>
                {language.code.toUpperCase()}
              </Text>
              <SvgSelector id={language.code}/>
            </Button>
          )}
        </div>
        {
          errorMessage
            ? <Box mt={'0.5rem'} color={errorColor} fontSize={'0.875rem'}>{errorMessage}</Box>
            : null
        }
      </FilledDiv>
    );
  };

export default ArticleLanguages;