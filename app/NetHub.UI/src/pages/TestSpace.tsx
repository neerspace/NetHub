import React, {useState} from 'react';
import {Box, Button, Input} from "@chakra-ui/react";
import Currency from "../components/Currency/Currency";
import {z as u} from "zod";
import useCustomSnackbar from "../hooks/useCustomSnackbar";
import {formatZodErrors} from "../utils/zodHelper";
import Dynamic, { IPage } from "../components/Dynamic/Dynamic";

const formSchema = u.object({
  username: u.string().min(5, 'Username Min - 5'),
  email: u.string().min(3, 'Email min - 3')
})

type user = u.infer<typeof formSchema>;

const TestSpace: IPage = () => {

  const {enqueueError} = useCustomSnackbar('error');

  const [state, setState] = useState<user>({} as user);
  const [errors, setErrors] = useState<{
    field: string,
    message: string
  }[]>()
  const handleValidate = () => {
    console.log('vvv')
    const validationResult = formSchema.safeParse(state);

    if (!validationResult.success) {
      // console.log('sss', validationResult.success)
      // const errors =
      const result = formatZodErrors(validationResult.error);
      console.log(result)
      setErrors(result);
      return;
      // for (const error of errors._errors) {
      //   enqueueError(error)
      // }
    }

    setErrors([])
  }


  return <Dynamic>
    <Box display={'flex'} flexDirection={'column'}>
      <Input
        value={state.username}
        onChange={(e) => setState(prev => {
          return {...prev, username: e.target.value}
        })}
      />
      <Input
        value={state.email}
        onChange={(e) => setState(prev => {
          return {...prev, email: e.target.value}
        })}
      />
      <Button onClick={handleValidate} isDisabled={true}>Validate</Button>
      {
        JSON.stringify(state)
      }
      {
        JSON.stringify(errors)
      }
    </Box>
    <Currency/>
  </Dynamic>
}

TestSpace.Provider = React.Fragment;


export default TestSpace;
