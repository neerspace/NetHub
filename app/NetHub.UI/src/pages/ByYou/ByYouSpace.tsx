import React from 'react';
import Layout, {Page} from "../../components/Layout/Layout";
import {ILibraryItem} from "../../components/Library/UserLibrary";
import SavedArticles from "../../components/Article/Saved/SavedArticles";

const ByYouSpace: Page = () => {
  const items: ILibraryItem[] = [
    {
      name: 'Статті',
      component: <SavedArticles/>
    },
  ]

  return (
    <Layout>
      {/*<UserLibrary items={items} title={<p>Hello World!</p>}>*/}
    <></>
      {/*</UserLibrary>*/}
    </Layout>
  );
};

ByYouSpace.Provider = React.Fragment;

export default ByYouSpace;
