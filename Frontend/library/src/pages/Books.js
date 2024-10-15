import React, { useState, useEffect } from 'react';
import api from '../services/api.js';
import '../styles/books.css';

const Books = () => {
  const [books, setBooks] = useState([]);
  const [filteredBooks, setFilteredBooks] = useState([]);
  const [searchTerm, setSearchTerm] = useState('');
  const [genreFilter, setGenreFilter] = useState('');
  const [authorFilter, setAuthorFilter] = useState('');
  const [currentPage, setCurrentPage] = useState(1);
  const [totalPages, setTotalPages] = useState(1);

  const booksPerPage = 5;

  const getBooksPage = () => {
    api.get(`Books/GetAllBooks?PageNumber=${currentPage}&PageSize=${booksPerPage}`)
      .then(response => {
        setBooks(response.data.data);
        setFilteredBooks(response.data.data);
        setTotalPages(response.data.totalPages);
      })
      .catch(error => console.error('Error during books loading occured', error));
  }

  useEffect(() => {
    // Получаем список книг с API (замените URL на ваш)
    getBooksPage();
  }, []);

  // Фильтрация по названию, жанру и автору
  useEffect(() => {
    const filtered = books.filter(book =>
      book.title.toLowerCase().includes(searchTerm.toLowerCase()) &&
      (genreFilter ? book.genre.toLowerCase().includes(genreFilter.toLowerCase()) : true) &&
      (authorFilter ? book.author.toLowerCase().includes(authorFilter.toLowerCase()) : true)
    );
    setFilteredBooks(filtered);
  }, [searchTerm, genreFilter, authorFilter, books]);

  const handlePageChange = (pageNumber) => {
    getBooksPage();
    setCurrentPage(pageNumber);
  };

  return (
    <div className="books-container">
      <h1>Список книг</h1>

      {/* Строка поиска */}
      <div className="search-filter-container">
        <input
          type="text"
          placeholder="Поиск по названию"
          value={searchTerm}
          onChange={(e) => setSearchTerm(e.target.value)}
        />
        <input
          type="text"
          placeholder="Фильтр по жанру"
          value={genreFilter}
          onChange={(e) => setGenreFilter(e.target.value)}
        />
        <input
          type="text"
          placeholder="Фильтр по автору"
          value={authorFilter}
          onChange={(e) => setAuthorFilter(e.target.value)}
        />
      </div>

      {/* Список книг */}
      <div className="books-list">
        {filteredBooks.map((book, index) => (
          <div key={index} className="book-item">
            <h2 className="book-title">{book.title}</h2>
            <div className="book-details">
              <img src={book.imageUrl} alt={book.title} className="book-image" />
              <div className="book-info">
                <p><strong>ISBN:</strong> {book.isbn}</p>
                <p><strong>Author:</strong> {book.author}</p>
                <p><strong>Genre:</strong> {book.genre}</p>
                <p><strong>Description:</strong> {book.description}</p>
              </div>
            </div>
          </div>
        ))}
      </div>

      {/* Пагинация */}
      {totalPages > 1 && (
      <div className="pagination">
        {Array.from({ length: totalPages }, (_, i) => i + 1).map(pageNumber => (
          <button
            key={pageNumber}
            onClick={() => handlePageChange(pageNumber)}
            className={pageNumber === currentPage ? 'active' : ''}
          >
            {pageNumber}
          </button>
        ))}
      </div>
      )}
    </div>
  );
};

export default Books;
