import React, {FC, useState} from 'react';
import cl from './UserLibrary.module.sass';
import RadioCardGroup, {RadioGroupConfig} from "../UI/RadioCardGroup";

interface IUserLibraryProps {
  items: ILibraryItem[],
  radioGroupConfig: RadioGroupConfig
}

export interface ILibraryItem {
  name: string,
  component: React.ReactNode
}

const UserLibrary: FC<IUserLibraryProps> = ({items, radioGroupConfig}) => {
  const [renderComponent, setRenderComponent] = useState<React.ReactNode>(items[0].component)

  const handleChange = (
    newAlignment: string
  ) => {
    setRenderComponent(items.filter(i => i.name === newAlignment)[0].component)
  };

  return (
    <div className={cl.libraryWrapper}>
      <div className={cl.contentButtons}>
        <RadioCardGroup
          config={radioGroupConfig}
          onChange={handleChange}
          options={items.map(i => i.name)}
        />
      </div>
      <div className={cl.actualComponent}>
        {renderComponent}
      </div>

    </div>
  );
};
export default UserLibrary;
