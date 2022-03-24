import React from 'react';
import ReactDOM from 'react-dom';

interface Props {
  children: React.ReactNode;
  show: boolean;
}

export const ReusableModal = ({ children, show }: Props) => {
  const [headerChild, bodyChild] = React.Children.toArray(children);

  return ReactDOM.createPortal(
    <>
      {show && (
        <>
          <div className="modal-backdrop fade show" />
          <div className="modal modal-static show d-block">
            <div className="modal-dialog modal-dialog-scrollable">
              <div className="modal-content">
                <div className="modal-header">{headerChild}</div>
                <div className="modal-body">{bodyChild}</div>
              </div>
            </div>
          </div>
        </>
      )}
    </>,
    document.body,
  );
};
