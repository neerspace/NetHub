import React, {FC, useEffect, useRef} from 'react';
import {useColorMode, useColorModeValue} from "@chakra-ui/react";

const CommentsWidget: FC<{ display: boolean, deps: any[] }> = ({display, deps}) => {
  const ref = useRef<HTMLDivElement>(null)
  const {colorMode} = useColorMode();
  const mainColor = useColorModeValue('896DC8', '835ADF');

  useEffect(() => {
    const script = document.createElement("script");
    script.src = "https://comments.app/js/widget.js?3";
    script.setAttribute('data-comments-app-website', 'jEdGOHK1');
    script.setAttribute('data-limit', '5');
    script.setAttribute('data-color', mainColor);
    script.setAttribute('data-colorful', '1');
    if (colorMode === 'dark') {
      script.setAttribute('data-dark', '1');
    }
    script.async = true;
    ref.current?.appendChild(script)

    return () => {
      while (ref.current?.firstChild) {
        ref.current?.removeChild(ref.current?.lastChild!);
      }
    }
  }, [...deps, colorMode])

  return (
    <div ref={ref} style={{display: display ? 'flex' : 'none', marginTop: '10px'}}></div>
  );
};

export default CommentsWidget;
