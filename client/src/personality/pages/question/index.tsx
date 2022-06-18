import React from 'react';
import questionImg from "../../../assets/question.svg";
import { Button } from '../../components/button';
import { QuestionContext } from '../../store/context';

export const Question: React.FC = () => {
    const ctx = React.useContext(QuestionContext);

    const question = ctx?.store.questions?.find(question => question.questionNumber === ctx?.store.currentIndex);
    const isFirstIndex = ctx?.store.isFirstIndex;
    const isLastIndex = ctx?.store.isLastIndex;
    const isLoading = ctx?.store.isLoading;
    const isActiveOption = (optionId: string) => question?.options.find(x => x.id === optionId)?.id === question?.chosenOption;

    const handleOptionClick = (e: any, optionId: string) => {
        e.preventDefault();
        ctx?.setOption(question?.id, optionId);
    }

    const handleNextClick = (e: any) => {
        e.preventDefault();

        let selectedAnOption = question?.options.find(x => x.id === question?.chosenOption);
        if (selectedAnOption)
            ctx?.setIndex(ctx?.store.currentIndex + 1);
    }

    const handlePreviousClick = (e: any) => {
        e.preventDefault();
        ctx?.setIndex(ctx?.store.currentIndex - 1);
    }

    const handleSubmitClick = (e: any) => {
        e.preventDefault();

        let selectedAnOption = question?.options.find(x => x.id === question?.chosenOption);
        if (selectedAnOption)
            ctx?.submitAnswers();
    }

    let hasSelectedAnOption = question?.options.find(x => x.id === question?.chosenOption) !== undefined;

    return (
        <div className="question">
            <div className="question__description">
                <img src={questionImg} alt="" className="question__description--img" />
                <div className="question__description--body">
                    <h2 className="question__text">Question {question?.questionNumber}.</h2>
                    <h2 className="question__text">{question?.text}</h2>
                </div>
            </div>
            <div className="question__answer--container">
                <div className="question__answers">
                    {question?.options.map((option, i) => (
                        <div
                            className={isActiveOption(option.id) ? `question__answers--item question-active` : `question__answers--item`}
                            key={option.id}
                            onClick={(e) => handleOptionClick(e, option.id)}
                        >
                            <span className="question__answers--item-num">{option.id}.</span>
                            <span className="question__answers--item-desc">{option.text}</span>
                        </div>
                    ))}
                </div>

                <div className="question__nav">
                    {
                        !isFirstIndex && (<Button btnText="Previous" onClick={handlePreviousClick} />)
                    
                    }
                    {
                        isLoading ? (
                            <p>Loading...</p>
                        ) : isLastIndex ? 
                            hasSelectedAnOption ? (
                                <Button
                                    btnText="Submit"
                                    onClick={handleSubmitClick}
                                    disabled={false}
                                />
                            ) : (
                                <Button
                                    btnText="Submit"
                                    onClick={handleSubmitClick}
                                    disabled={true}
                                />
                            )
                         :  hasSelectedAnOption ? (
                            <Button
                                btnText="Next"
                                onClick={handleNextClick}
                                disabled={false}
                            />
                        ) : (
                            <Button
                                btnText="Next"
                                onClick={handleNextClick}
                                disabled={true}
                            />
                        )
                    }
                </div>
            </div>
        </div>
    )
};
