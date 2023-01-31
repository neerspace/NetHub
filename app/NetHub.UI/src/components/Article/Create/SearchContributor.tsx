import React, {FC, useState} from 'react';
import {Avatar, Box, Button, Input, Select, useColorModeValue} from "@chakra-ui/react";
import {useDebounce} from "../../../hooks/useDebounce";
import {IPrivateUserInfoResponse} from "../../../types/api/User/IUserInfoResponse";
import {searchApi} from "../../../api/api";
import Tag from "../One/Body/Tag";
import {createImageFromInitials} from "../../../utils/logoGenerator";
import useValidator from "../../../hooks/useValidator";
import {isNotNull, isNotNullOrWhiteSpace} from "../../../utils/validators";
import useCustomSnackbar from "../../../hooks/useCustomSnackbar";
import {Contributor} from "./AddContributorsBlock";
import ErrorMessage from "../../UI/Snackbar/ErrorMessage";

interface IContributorErrors {
  username: boolean,
  role: boolean
}

interface ISearchContributorProps {
  contributors: Contributor[],
  setContributors: (contributors: Contributor[]) => void
}

const SearchContributor: FC<ISearchContributorProps> = ({contributors, setContributors}) => {
  const debounceLogic = async (searchValue: string) => {
    const results = await searchApi.searchUsersByUsername(searchValue);
    setResults(results);
  };
  const debounce = useDebounce(debounceLogic, 1000);
  const lineColor = useColorModeValue('#D0D0D0', 'white');
  const boxBgColor = useColorModeValue('whiteLight', 'whiteDark');
  const contributorRoles = ['Editor', 'Translator', 'Copyrighter']


  const [results, setResults] = useState<IPrivateUserInfoResponse[]>([]);
  const [currentContributor, setCurrentContributor] =
    useState<Contributor>({
      user: {} as IPrivateUserInfoResponse,
      role: contributorRoles[0]
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

  const onTagClickHandle = (user: IPrivateUserInfoResponse) => {
    setCurrentContributor({...currentContributor, user});
  }

  const onInputChangeHandle = (role: string) => {
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
                <Tag
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
                </Tag>)}
            </Box>
          </>
          : null}

      </Box>
      <Select
        width={'20%'}
        onChange={(e) => {
          onInputChangeHandle(e.target.value)
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

export default SearchContributor;
