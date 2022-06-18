import React from 'react';

import self from "../../../assets/self.svg";
import { Button } from '../../components/button';
import { QuestionContext } from '../../store/context';

export const Home: React.FC = () => {
    const ctx = React.useContext(QuestionContext);

    const handleStartTest = (e: any) => {
        e.preventDefault();

        ctx?.getQuestions();
    }

    return (
        <div className="landing">
            <div className="landing__intro">
                <div className="landing__intro--title">
                    <h1 className="heading-primary">Personality Test</h1>
                </div>
                <div className="landing__intro--btn">
                    {
                        ctx?.store.isLoading ? (
                            <p>Loading...</p>
                        ) : (
                            <Button
                                btnText="Start Test"
                                onClick={handleStartTest}
                            />
                        )
                    }

                </div>
            </div>
            <div className="landing__img">
                <img className="landing__img--svg" src={self} alt="" />
            </div>
        </div>
    )
}
