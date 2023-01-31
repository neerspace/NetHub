import React, {FC} from 'react';
import {Route, RouteProps} from "react-router-dom";
import AuthorizedHoc from "../hocs/AuthorizedHoc";


const AuthorizedRoute: FC<RouteProps> = (props) => {

  const {children} = props;

  return (
    <Route>
      {/*<AuthorizedHoc>*/}
      {/**/}
      {/*</AuthorizedHoc>*/}
    </Route>

  );
};

export default AuthorizedRoute;