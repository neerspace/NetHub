import React, {FC, PropsWithChildren, ReactElement} from 'react';
import Body from "./Body";

export interface ISectionConfig {
  showError?: boolean
}

export interface ISideBarConfig extends ISectionConfig {
  showSidebar?: boolean
}
export interface ILayoutProps extends PropsWithChildren {
  children: [ReactElement, ReactElement, ReactElement] | [ReactElement, ReactElement] | ReactElement,
  Config?: { Left?: ISideBarConfig, Center?: ISectionConfig, Right?: ISectionConfig}
  Titles?: { Left?: ReactElement, Center?: ReactElement, Right?: ReactElement }
}

const Dynamic: FC<ILayoutProps> =
  ({children, Config, Titles}) => {
    let left;
    let center;
    let right;

    if (Array.isArray(children)) {
      left = children.length === 3 ? children[0] : undefined;
      center = children.length === 3 ? children[1] : children[0];
      right = children.length === 3 ? children[2] : children.length === 2 ? children[1] : undefined;
    } else {
      center = children;
    }

    return (
        <Body
          Left={left}
          Center={center}
          Right={right}
          Titles={Titles}
          Config={Config}
        />
    );
  };

export type IPage = FC & { Provider: FC<PropsWithChildren> }

export default Dynamic;
