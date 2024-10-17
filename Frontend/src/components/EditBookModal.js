import React, { useState } from 'react';
import modal from '../styles/modal.module.css'; // Добавим стили для модального окна

const EditBookModal = ({ book, onSave, onClose }) => {
  const [title, setTitle] = useState(book.title);
  const [authorId, setAuthor] = useState(book.author.id);
  const [genre, setGenre] = useState(book.genre);
  const [description, setDescription] = useState(book.description);

  const handleSave = () => {
    const updatedBook = {
      ...book,
      title,
      authorId,
      genre,
      description,
    };

    onSave(updatedBook);
  };

  return (
    <div className={modal['modal-overlay']}>
      <div className={modal['modal-content']}>
        <h2>Edit Book</h2>
        <label>Title:</label>
        <input type="text" value={title} onChange={(e) => setTitle(e.target.value)} />
        
        <label>Library author number:</label>
        <input type="text" value={authorId} onChange={(e) => setAuthor(e.target.value)} />
        
        <label>Genre:</label>
        <input type="text" value={genre} onChange={(e) => setGenre(e.target.value)} />
        
        <label>Description:</label>
        <textarea value={description} onChange={(e) => setDescription(e.target.value)} />
        
        <div className={modal['modal-actions']}>
          <button onClick={handleSave}>Save</button>
          <button onClick={onClose}>Cancel</button>
        </div>
      </div>
    </div>
  );
};

export default EditBookModal;
