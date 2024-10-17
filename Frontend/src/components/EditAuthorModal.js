import React, { useState } from 'react';
import modal from '../styles/modal.module.css'; // Добавим стили для модального окна

const EditBookModal = ({ author, onSave, onClose }) => {
  const [name, setName] = useState(author.name);
  const [surname, setSurname] = useState(author.surname);
  const [birthDate, setBirthDate] = useState(author.birthDate);
  const [country, setCountry] = useState(author.country);

  const handleSave = () => {
    let date = new Date(birthDate).toISOString().split('T')[0];
    const updatedAuthor = {
      id: author.id,
      name,
      surname,
      birthDate: date,
      country
    };

    onSave(updatedAuthor);
  };

  return (
    <div className={modal['modal-overlay']}>
      <div className={modal['modal-content']}>
        <h2>Edit Author</h2>
        <label>Name:</label>
        <input type="text" value={name} onChange={(e) => setName(e.target.value)} />
        
        <label>Surname:</label>
        <input type="text" value={surname} onChange={(e) => setSurname(e.target.value)} />
        
        <label>Birth date:</label>
        <input type="date" value={birthDate} onChange={(e) => setBirthDate(e.target.value)} />
        
        <label>Country:</label>
        <input type="text" value={country} onChange={(e) => setCountry(e.target.value)} />
        
        <div className={modal['modal-actions']}>
          <button onClick={handleSave}>Save</button>
          <button onClick={onClose}>Cancel</button>
        </div>
      </div>
    </div>
  );
};

export default EditBookModal;
