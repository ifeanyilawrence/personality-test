import React from 'react';
import { useNavigate } from "react-router-dom";

import { Button } from '../../components/button';
import { QuestionContext } from '../../store/context';

export const Result: React.FC = () => {
    const ctx = React.useContext(QuestionContext);
    const navigate = useNavigate();

    const personality = ctx?.store.result;

    const handleRetakeTest = (e: any) => {
        e.preventDefault();

        navigate("/");
    }

  return (
    <div className="result">
        <h4 className="heading-secondary">Result:</h4>
        <h4 className="heading-primary">{personality}</h4>
        <Button
            btnText="Retake Test"
            onClick={handleRetakeTest}
        />
    </div>
  )
}
