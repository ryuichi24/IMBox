import React from 'react';

interface Props {
  label?: string;
  type?: string;
  placeholder?: string;
  className?: string;
  formText?: string;
  id?: string;
  multiLine?: boolean;
  maxRows?: number;
  value?: string | number | readonly string[];
  onChange: React.ChangeEventHandler<HTMLInputElement | HTMLTextAreaElement>;
}

export const InputField = ({
  id,
  className,
  type,
  placeholder,
  formText,
  label,
  multiLine = false,
  maxRows,
  onChange,
  value,
}: Props) => {
  if (multiLine)
    return (
      <>
        {label && (
          <label htmlFor={id} className="form-label">
            {label}
          </label>
        )}
        <textarea
          className={`form-control ${className}`}
          id={id}
          placeholder={placeholder}
          onChange={onChange}
          value={value}
          rows={maxRows}
        ></textarea>
        {formText && <div className="form-text">{formText}</div>}
      </>
    );

  return (
    <>
      {label && (
        <label htmlFor={id} className="form-label">
          {label}
        </label>
      )}
      <input
        className={`form-control ${className}`}
        placeholder={placeholder}
        id={id}
        type={type}
        value={value}
        onChange={onChange}
      />
      {formText && <div className="form-text">{formText}</div>}
    </>
  );
};
