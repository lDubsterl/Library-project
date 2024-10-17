import React, { useState } from 'react';
import modal from '../styles/modal.module.css'; // Добавим стили для модального окна

const DeleteModal = ({ onOk, onClose }) => {

  return (
    <div className={modal['modal-overlay']}>
      <div className={modal['modal-delete']}>
        <h2>Are you sure about that?</h2>
    
        <div className={modal['modal-actions']} style={{justifyContent: "center"}}>
          <button onClick={() => onOk()}>Proceed</button>
          <button onClick={() => onClose()}>Cancel</button>
        </div>
      </div>
    </div>
  );
};

export default DeleteModal;
