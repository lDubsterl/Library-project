import React, { useState } from 'react';
import modal from '../styles/modal.module.css';

const AddAuthorModal = ({ onSave, onClose }) => {
    const [name, setName] = useState('');
    const [surname, setSurname] = useState('');
    const [birthDate, setBirthDate] = useState('');
    const [country, setCountry] = useState('');

    const handleSave = () => {
        let author = {
            name,
            surname,
            birthDate: new Date(birthDate).toISOString().split('T')[0],
            country
        };

        onSave(author);
    };

    return (
        <div className={modal['modal-overlay']}>
            <div className={modal['modal-content']}>
                <h2>Add New Author</h2>
                <label>Name:</label>
                <input type="text" value={name} onChange={(e) => setName(e.target.value)} />

                <label>Surname:</label>
                <input type="text" value={surname} onChange={(e) => setSurname(e.target.value)} />

                <label>Birth date:</label>
                <input type="date" value={birthDate} onChange={(e) => setBirthDate(e.target.value)} />

                <label>Country:</label>
                <input type="text" value={country} onChange={(e) => setCountry(e.target.value)} />

                <div className={modal['modal-actions']}>
                    <button onClick={handleSave}>Add Author</button>
                    <button onClick={onClose}>Cancel</button>
                </div>
            </div>
        </div>
    );
};

export default AddAuthorModal;
