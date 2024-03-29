import React, { FC, useState } from 'react';
import { Avatar, Box, Button, Input, Select, useColorModeValue } from "@chakra-ui/react";
import { useDebounce } from "../../../../hooks/useDebounce";
import Selection from "../../../Shared/Selection";
import { createImageFromInitials } from "../../../../utils/logoGenerator";
import useValidator from "../../../../hooks/useValidator";
import { isNotNull, isNotNullOrWhiteSpace } from "../../../../utils/validators";
import useCustomSnackbar from "../../../../hooks/useCustomSnackbar";
import { Contributor } from "./ArticleContributors";
import ErrorMessage from "../../../UI/Snackbar/ErrorMessage";
import { _usersApi } from "../../../../api";
import { ArticleContributorRole, PrivateUserResult } from "../../../../api/_api";

interface IContributorErrors {
  username: boolean,
  role: boolean
}

interface ISearchContributorProps {
  contributors: Contributor[],
  setContributors: (contributors: Contributor[]) => void
}

export enum ContributorRole {
  Editor = "Editor",
  Copyrighter = "Copyrighter",
  Translator = "Translator",
}

const ArticleContributorsSearch: FC<ISearchContributorProps> = ({contributors, setContributors}) => {
  const debounceLogic = async (searchValue: string) => {
    const results = await _usersApi.usersFind(searchValue);
    setResults(results);
  };
  const debounce = useDebounce(debounceLogic, 1000);
  const lineColor = useColorModeValue('#D0D0D0', 'white');
  const boxBgColor = useColorModeValue('whiteLight', 'whiteDark');
  const contributorRoles = Object.keys(ContributorRole);


  const [results, setResults] = useState<PrivateUserResult[]>([]);
  const [currentContributor, setCurrentContributor] =
    useState<Contributor>({
      user: {} as PrivateUserResult,
      role: ArticleContributorRole.Editor
    })
  const {subscribeValidator, validateAll, errors} = useValidator<IContributorErrors>();
  const {enqueueError} = useCustomSnackbar();

  const onChangeHandle = async (e: React.ChangeEvent<HTMLInputElement>) => {
    const value = e.target.value.replace(/\s/g, '');

    if (e.target.value === '') {
      setResults([]);
      return;
    }

    if (value !== null && value !== '' && value.length > 0) {
      await debounce(value);
    }
  }

  const onTagClickHandle = (user: PrivateUserResult) => {
    setCurrentContributor({...currentContributor, user});
  }

  const onInputChangeHandle = (role: ArticleContributorRole) => {
    setCurrentContributor({...currentContributor, role})
  }

  const onButtonClick = async () => {
    const isSuccess = await validateContributor();

    if (!isSuccess) return;

    if (contributors.findIndex(({user, role}) =>
      user.userName === currentContributor.user.userName && role === currentContributor.role) === -1)
      setContributors([...contributors, currentContributor]);
  }

  const validateContributor = async () => {
    subscribeValidator({
      value: currentContributor.user,
      field: 'username',
      validators: [isNotNull],
      message: <ErrorMessage title={'Співавтор'} message={'Виберіть користувача'}/>
    });
    subscribeValidator({
      value: currentContributor.role,
      field: 'role',
      validators: [isNotNullOrWhiteSpace],
      message: <ErrorMessage title={'Співавтор'} message={'Виберіть роль'}/>
    });

    const {isSuccess, errors} = await validateAll();
    if (!isSuccess) errors.forEach(enqueueError)

    return isSuccess;
  }

  return (
    <Box
      width={'100%'}
      display={'flex'}
      justifyContent={'space-between'}
    >
      <Box
        bg={boxBgColor} borderRadius={12} width={'50%'} display={'flex'} flexDirection={'column'} alignItems={'center'}
      >
        <Input
          isInvalid={errors.username}
          width={'100%'}
          onChange={onChangeHandle}
        />
        {results.length > 0
          ? <>
            <hr style={{border: `1px solid ${lineColor}`, borderRadius: '12px', width: '95%'}}/>
            <Box display={'flex'} flexWrap={'wrap'} width={'100%'} p={2} gap={2}>
              {results.map(u =>
                <Selection
                  value={u.userName} onClick={() => onTagClickHandle(u)}
                  active={currentContributor.user.userName === u.userName}
                  onHover={false}
                >
                  <Box display={'flex'} alignItems={'center'}>
                    <Avatar
                      size='xs'
                      src={u.profilePhotoUrl ?? createImageFromInitials(500, u.userName)}
                      mr={2}
                    />
                    {u.userName}
                  </Box>
                </Selection>)}
            </Box>
          </>
          : null}
      </Box>
      <Select
        width={'20%'}
        onChange={(e) => {
          onInputChangeHandle(e.target.value as ArticleContributorRole)
        }}
      >
        {
          contributorRoles.map(r =>
            <option key={r} value={r}>{r}</option>
          )
        }
      </Select>
      <Button onClick={onButtonClick} width={'15%'}>Додати</Button>
    </Box>
  );
};

export default ArticleContributorsSearch;
