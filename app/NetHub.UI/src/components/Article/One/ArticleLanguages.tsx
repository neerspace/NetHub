import React, { FC, useState } from 'react';
import FilledDiv from "../../UI/FilledDiv";
import cl from "./ArticleInfo.module.sass";
import { Button, Text, useColorModeValue } from "@chakra-ui/react";
import SvgSelector from "../../UI/SvgSelector/SvgSelector";
import { useParams } from "react-router-dom";

type Language = { code: string, action: () => void }

interface IArticleLocalizationsProps {
  languages: Language[],
  disabled?: boolean
}

const ArticleLanguages: FC<IArticleLocalizationsProps> =
  ({languages, disabled}) => {
    const {code} = useParams();

    const [selectedLanguage, setSelectedLanguage] = useState<string | null>(code ?? null);

    const whiteTextColor = useColorModeValue('whiteLight', 'whiteDark');
    const buttonBg = useColorModeValue('#896DC8', '#835ADF');
    const selectedButtonBg = useColorModeValue('red', 'green');
    const disabledButtonBg = useColorModeValue('#1F2023', '#1F2023');


    const onClickHandle = (language: Language) => {
      if (disabled) return;
      setSelectedLanguage(language.code);
      language.action();
    }

    const buttonColorCondition = (code: string) => {
      if (disabled) return disabledButtonBg;

      return code === selectedLanguage
        ? selectedButtonBg
        : buttonBg
    }

    const buttonHoverColorCondition = () => {
      if (disabled) return disabledButtonBg;

      return '#BBAFEA';
    }

    return (
      <FilledDiv className={cl.infoBlock}>
        <p className={cl.infoBlockTitle}>Переклади:</p>
        <div className={cl.translates}>
          {languages.map(language =>
            <Button
              onClick={() => onClickHandle(language)}
              key={language.code}
              borderRadius={'10px'}
              padding={'5px 16px'}
              width={'fit-content'}
              backgroundColor={buttonColorCondition(language.code)}
              _hover={
                {
                  bg: buttonHoverColorCondition()
                }
              }
            >
              <Text as={'p'} mr={2} color={whiteTextColor}>
                {language.code.toUpperCase()}
              </Text>
              <SvgSelector id={language.code}/>
            </Button>
          )}
        </div>
      </FilledDiv>
    );
  };

export default ArticleLanguages;