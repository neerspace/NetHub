import React, { FC } from 'react';
import { Route, Routes } from 'react-router-dom';
import { paths } from "../routes/paths";
import NotFoundSpace from "../pages/NotFoundSpace";
import ErrorBoundary from "./Layout/ErrorBoundary";
import AuthorizedHoc from "../hocs/AuthorizedHoc";
import Header from './Layout/Header/Header';
import Footer from "./Layout/Footer/Footer";
import Flag from "./Layout/Flag";
import { Box } from "@chakra-ui/react";

const AppRouter: FC = () => {
  return (
    <ErrorBoundary main={true}>
        <Flag/>
        <Header/>
        <Routes>
          {paths.map(({path, Component, requireAuthorization}) => {

            return <Route
              key={path} path={path} element={
              <AuthorizedHoc requireAuthorization={requireAuthorization}>
                {Component}
              </AuthorizedHoc>}
            />
          })}
          <Route
            path={'*'} element={
            <NotFoundSpace/>
          }
          />
        </Routes>
        <Footer/>
    </ErrorBoundary>
  );
};

export default AppRouter;
