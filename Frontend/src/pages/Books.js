import React, { useState, useEffect } from 'react';
import api from '../services/api.js';
import '../styles/books.css';

const Books = ({ isAdmin }) => {
  const [books, setBooks] = useState([]);
  const [filteredBooks, setFilteredBooks] = useState([]);
  const [searchTerm, setSearchTerm] = useState('');
  const [genreFilter, setGenreFilter] = useState('');
  const [authorFilter, setAuthorFilter] = useState('');
  const [currentPage, setCurrentPage] = useState(1);
  const [totalPages, setTotalPages] = useState(1);

  const booksPerPage = 5;

  useEffect(() => {
    api.get(`Books/GetAllBooks?PageNumber=${currentPage}&PageSize=${booksPerPage}`)
      .then(response => {
        setBooks(response.data.data);
        setTotalPages(response.data.totalPages);
      })
      .catch(error => console.error('Error during books loading occured', error));
  }, [currentPage]);

  // Фильтрация по названию, жанру и автору
  useEffect(() => {
    if (books.length == 0) return;

    let filtered = books.filter(book =>
      book.title.toLowerCase().includes(searchTerm.toLowerCase()) &&
      (genreFilter ? book.genre.toLowerCase().includes(genreFilter.toLowerCase()) : true) &&
      (authorFilter ? book.author.toLowerCase().includes(authorFilter.toLowerCase()) : true)
    );

    let adminImageUrl = 'https://static.vecteezy.com/system/resources/previews/022/891/082/non_2x/plus-icon-isolated-free-png.png';
    let userImageUrl = 'https://www.pngall.com/wp-content/uploads/2/Question-Mark-PNG.png'

    filtered.forEach(book => {
      if (book.pathToImage === '')
        if (isAdmin)
          book.pathToImage = adminImageUrl;
        else
          book.pathToImage = userImageUrl;
    })

    setFilteredBooks(filtered);
  }, [searchTerm, genreFilter, authorFilter, books, isAdmin]);

  const handlePageChange = (pageNumber) => {
    getBooksPage();
    setCurrentPage(pageNumber);
  };

  return (
    <div className="books-container">
      <h1>Book search</h1>

      {/* Строка поиска */}
      <div className="search-filter-container">
        <input
          type="text"
          placeholder="Search by name"
          value={searchTerm}
          onChange={(e) => setSearchTerm(e.target.value)}
        />
        <input
          type="text"
          placeholder="Filter by genre"
          value={genreFilter}
          onChange={(e) => setGenreFilter(e.target.value)}
        />
        <input
          type="text"
          placeholder="Filter by author"
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
              {!isAdmin ? (<img src={book.pathToImage} alt={book.title} className="book-image" />)
                : (<button>
                  <img src={book.pathToImage} alt={book.title} className="book-image" />
                </button>)}
              <div className="book-info">
                <p><strong>ISBN:</strong> {book.isbn}</p>
                <p><strong>Author:</strong> {book.author.name + ' ' + book.author.surname}</p>
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
