import React, { useReducer, useEffect } from "react";
import { useNavigate } from "react-router-dom";

import { QuestionContext } from "./context";
import { GetQuestionResponse, QuestionObject, questionState, QuestionState } from "./types";

const actions = {
    GET_QUESTIONS: "GET_QUESTIONS",
    GET_QUESTIONS_SUCCESS: "GET_QUESTIONS_SUCCESS",
    SUBMIT_ANSWERS: "SUBMIT_ANSWERS",
    SUBMIT_ANSWERS_SUCCESS: "SUBMIT_ANSWERS_SUCCESS",
    SET_INDEX: "SET_INDEX",
    SET_CHOSEN_OPTION: "SET_CHOSEN_OPTION",
    UPDATE_STORE_FROM_LOCAL_STORAGE: "UPDATE_STORE_FROM_LOCAL_STORAGE",
};

const reducer = (state: QuestionState, action: { type: string, payload: any }) => {
    let newState;

    switch (action.type) {
        case actions.GET_QUESTIONS:
            return {
                ...state,
                isLoading: true,
                isFirstIndex: false,
                isLastIndex: false,
                currentIndex: -1,
                questions: null,
                result: ""
            };

        case actions.GET_QUESTIONS_SUCCESS: {
            const questions = action.payload.questions.map((question: GetQuestionResponse, i: number) => {
                return {
                    id: question.id,
                    questionNumber: i + 1,
                    text: question.question,
                    options: question.answers,
                    chosenOption: ""
                }
            });

            newState = {
                ...state,
                questions: questions,
                isLoading: false,
                isFirstIndex: true,
                isLastIndex: false,
                currentIndex: 1,
                result: ""
            };

            localStorage.setItem("store", JSON.stringify(newState));

            return newState;
        }

        case actions.SUBMIT_ANSWERS:
            return {
                ...state,
                isLoading: true,
            };

        case actions.SUBMIT_ANSWERS_SUCCESS: {
            newState = {
                ...state,
                questions: null,
                isLoading: false,
                isFirstIndex: false,
                isLastIndex: false,
                currentIndex: -1,
                result: action.payload
            };

            localStorage.setItem("store", JSON.stringify(newState));

            return newState;
        }

        case actions.SET_INDEX:
            newState = {
                ...state,
                currentIndex: action.payload,
                isFirstIndex: action.payload === 1,
                isLastIndex: action.payload === state.questions?.length,
            };

            localStorage.setItem("store", JSON.stringify(newState));

            return newState;

        case actions.SET_CHOSEN_OPTION:
            let questions = state.questions?.map((question: QuestionObject) => {
                if (question.id === action.payload.id) {
                    question.chosenOption = action.payload.optionId;
                }
                return question;
            });

            newState = {
                ...state,
                questions
            };

            localStorage.setItem("store", JSON.stringify(newState));

            return newState;

        case actions.UPDATE_STORE_FROM_LOCAL_STORAGE:
            return {
                ...state,
                isLoading: false,
                questions: action.payload.questions,
                isFirstIndex: true,
                isLastIndex: false,
                currentIndex: 1,
                result: ""
            };

        default:
            return state;
    }
};

interface ProviderProps {
    children: React.ReactNode;
}

export const Provider: React.FC<ProviderProps> = ({ children }) => {
    const [state, dispatch] = useReducer(reducer, questionState);
    let navigate = useNavigate();

    let url = `${process.env.REACT_APP_API_URL}/personality`;

    useEffect(() => {
        const localStorageStore = localStorage.getItem("store");
        if (localStorageStore) {
            let passedData = JSON.parse(localStorageStore);

            if (passedData && passedData.questions) {
                dispatch({
                    type: actions.UPDATE_STORE_FROM_LOCAL_STORAGE,
                    payload: passedData
                });

                navigate("/question");
            } else {
                const response = fetch(url);
                response.then(res => res.json()).then(data => {
                    dispatch({
                        type: actions.GET_QUESTIONS_SUCCESS,
                        payload: data
                    });

                    navigate("/question");
                }).catch(err => {
                    console.log(err);
                });
            }
        } else {
            const response = fetch(url);
            response.then(res => res.json()).then(data => {
                dispatch({
                    type: actions.GET_QUESTIONS_SUCCESS,
                    payload: data
                });

                navigate("/question");
            }).catch(err => {
                console.log(err);
            });
        }
    }, []);

    const value = {
        store: state,

        getQuestions: async () => {
            dispatch({ type: actions.GET_QUESTIONS, payload: null });

            const response = await fetch(url);
            const questions = await response.json();

            dispatch({ type: actions.GET_QUESTIONS_SUCCESS, payload: questions });

            navigate("/question");
        },

        setIndex: (index: number) => {
            dispatch({ type: actions.SET_INDEX, payload: index });
        },

        setOption: (id: number | undefined, optionId: string) => {
            dispatch({ type: actions.SET_CHOSEN_OPTION, payload: { id, optionId } });
        },

        submitAnswers: async () => {
            dispatch({ type: actions.SUBMIT_ANSWERS, payload: null });

            const response = await fetch(url, {
                method: "POST",
                headers: {
                    "Content-Type": "application/json"
                },
                body: JSON.stringify(state.questions.map((question: QuestionObject) => {
                    return {
                        questionId: question.id,
                        ChosenAnswerId: question.chosenOption
                    }
                }))
            });

            const result = await response.json();

            dispatch({ type: actions.SUBMIT_ANSWERS_SUCCESS, payload: result.personality });

            navigate("/result");
        }
    };

    return (
        <QuestionContext.Provider value={value}>
            {children}
        </QuestionContext.Provider>
    );
};