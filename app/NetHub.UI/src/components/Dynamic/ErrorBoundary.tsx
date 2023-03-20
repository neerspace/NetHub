import React, {Component, PropsWithChildren, ReactNode} from 'react';
import ErrorBlock from "../UI/Error/ErrorBlock";

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
