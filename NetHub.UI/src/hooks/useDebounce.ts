import {useCallback, useRef} from "react";

export function useDebounce(callback: Function, delay: number) {
  const timer = useRef<NodeJS.Timeout>();

  return useCallback((...args: any) => {
    if (timer.current)
      clearTimeout(timer.current)

    timer.current = setTimeout(() => {
      return callback(...args);
    }, delay)
  }, [callback, delay]);
}
