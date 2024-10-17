import React, { useState } from 'react';
import modal from '../styles/modal.module.css';

const AddBookModal = ({ onSave, onClose }) => {
    const [title, setTitle] = useState('');
    const [authorId, setAuthor] = useState('');
    const [genre, setGenre] = useState('');
    const [description, setDescription] = useState('');
    const [isbn, setISBN] = useState('');

    const handleSave = () => {
        let form = new FormData();
        form.append('ISBN', isbn);
        form.append('Title', title);
        form.append('Genre', genre);
        form.append('Description', description);
        form.append('AuthorId', authorId);
        form.append('Image', null);

        onSave(form);
    };

    return (
        <div className={modal['modal-overlay']}>
            <div className={modal['modal-content']}>
                <h2>Add New Book</h2>
                <label>ISBN:</label>
                <input type="text" value={isbn} onChange={(e) => setISBN(e.target.value)} />
                <label>Title:</label>
                <input type="text" value={title} onChange={(e) => setTitle(e.target.value)} />

                <label>Library author number:</label>
                <input type="text" value={authorId} onChange={(e) => setAuthor(e.target.value)} />

                <label>Genre:</label>
                <input type="text" value={genre} onChange={(e) => setGenre(e.target.value)} />

                <label>Description:</label>
                <textarea value={description} onChange={(e) => setDescription(e.target.value)} />

                <div className={modal['modal-actions']}>
                    <button onClick={handleSave}>Add Book</button>
                    <button onClick={onClose}>Cancel</button>
                </div>
            </div>
        </div>
    );
};

export default AddBookModal;
