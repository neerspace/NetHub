import React from 'react';
import Login from "../../components/Auth/Login";
import { Text } from "@chakra-ui/react";
import Dynamic, { IPage } from "../../components/Layout/Dynamic";


const AuthSpace: IPage = () => {
  const config = {Left: {showSidebar: false}}

  const titles = {
    Center: <Text
      mb={2}
      fontWeight={700}
      as={'h2'}
    >
      Вітаємо на NetHub!
    </Text>
  }

  return <Dynamic Config={config} Titles={titles}>
    <Login/>
  </Dynamic>
};

AuthSpace.Provider = React.Fragment;

export default AuthSpace;