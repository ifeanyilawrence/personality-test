interface QuestionState {
    currentIndex: number;
    isLoading: boolean;
    isFirstIndex: boolean;
    isLastIndex: boolean;
    questions: QuestionObject[] | null;
    result: string;
};

let questionState: QuestionState = {
    currentIndex: -1,
    isLoading: false,
    isFirstIndex: false,
    isLastIndex: false,
    questions: null,
    result: ""
};

type QuestionObject = {
    id: number;
    questionNumber: number;
    text: string;
    options: [
        {
            id: string;
            text: string;
        }
    ],
    chosenOption: string;
};

type GetQuestionResponse = {
    id: number;
    question: string;
    answers: [
        {
            id: string;
            text: string;
        }
    ],
    chosenOption: string;
};

export { questionState };
export type { QuestionState, QuestionObject, GetQuestionResponse };
