import React from "react";
import { QuestionState } from "./types";

type ContextType = {
    store: QuestionState;
    getQuestions: () => void;
    setIndex: (index: number) => void;
    submitAnswers: () => void;
    setOption: (id: number | undefined, optionId: string) => void;
} | null;


export const QuestionContext = React.createContext<ContextType>(null);
