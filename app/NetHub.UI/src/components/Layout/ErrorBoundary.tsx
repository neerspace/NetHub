import React, {Component, PropsWithChildren, ReactNode} from 'react';
import ErrorBlock from "./ErrorBlock";

interface Props extends PropsWithChildren {
  children?: ReactNode;
  show?: boolean,
  main?: boolean
}

interface State {
  isError: boolean;
  error?: Error
}

class ErrorBoundary extends Component<Props, State> {
  public state: State = {
    isError: false,
  };

  private isTest: boolean = import.meta.env.REACT_APP_IS_DEVELOPMENT === 'false'

  public static getDerivedStateFromError(_: Error): State {
    // Update state so the next render will show the fallback UI.
    return {isError: true, error: _};
  }

  render() {

    if (this.state.isError) {
      if (this.props.main){
        //redirect to problems page
      }
      return this.props.show ? <ErrorBlock>{this.state.error?.message}</ErrorBlock> : <ErrorBlock/>;
    }


    return this.props.children;
  }
}

export default ErrorBoundary;
