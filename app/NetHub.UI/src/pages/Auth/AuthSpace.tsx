import React from 'react';
import Login from "../../components/Auth/Login";
import {Box, Text} from "@chakra-ui/react";
import Layout, {Page} from "../../components/Layout/Layout";


const AuthSpace: Page = () => {
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

  return <Layout Config={config} Titles={titles}>
    <Login/>
  </Layout>
};

AuthSpace.Provider = React.Fragment;

export default AuthSpace;