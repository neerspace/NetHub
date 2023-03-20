import React, {useState} from "react";

export const useDnD = (dropAction: (e: React.DragEvent<HTMLSpanElement>) => Promise<void>) => {
  const [dragState, setDragState] = useState<boolean>(false);

  const handleDrop = async (e: React.DragEvent<HTMLSpanElement>) => {
    await dropAction(e);
    setDragState(false);
  }


  const handleDragStart = (e: React.DragEvent<HTMLSpanElement>) => {
    e.preventDefault();
    setDragState(true);
  }

  const handleDragLeave = (e: React.DragEvent<HTMLSpanElement>) => {
    e.preventDefault();
    setDragState(false);
  }

  return{
    dragState,
    setDragState,
    handleDragStart,
    handleDrop,
    handleDragLeave,
  }
}