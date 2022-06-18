import React from 'react';

interface ButtonProps {
    onClick: (e: any) => void;
    btnText: string;
    disabled?: boolean;
}

export const Button: React.FC<ButtonProps> = ({ btnText, onClick, disabled }) => {
  return (
    <div className={disabled ? `div-disabled` : ``} >
        <a onClick={onClick} className="btn btn-primary" >
            {btnText}
        </a>
    </div>
  )
}
