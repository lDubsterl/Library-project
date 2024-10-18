import React, { useState, useEffect } from 'react';
import api from '../services/api.js';
import booksStyles from '../styles/books.module.css';
import itemsList from '../styles/itemsList.module.css';
import adminButtons from '../styles/adminButtons.module.css';
import { useNavigate } from 'react-router-dom';
import AddBookModal from '../components/AddBookModal.js';

const Books = ({ isAdmin }) => {
	const [books, setBooks] = useState([]);
	const [filteredBooks, setFilteredBooks] = useState([]);
	const [searchTerm, setSearchTerm] = useState('');
	const [genreFilter, setGenreFilter] = useState('');
	const [authorFilter, setAuthorFilter] = useState('');
	const [currentPage, setCurrentPage] = useState(1);
	const [totalPages, setTotalPages] = useState(1);
	const [showModal, setShowModal] = useState(false);

	const navigate = useNavigate();
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
		setCurrentPage(pageNumber);
	};

	const handleUploadImage = (e, book) => {
		let isbn = book.isbn;
		let image = e.target.files[0];
		let form = new FormData();
		form.append('ISBN', isbn);
		form.append('Image', image);
		api.post('Books/AddImageToBook', form)
			.then(() => navigate(0))
			.catch(() => console.log("Image upload error"));
	};

	const handleBookClick = (book) => {
		navigate(`/book/${book.isbn}`, { state: { isbn: book.isbn } });
	};

	const handleSaveBook = (newBook) => {
		api.post('Books/AddBook', newBook)
			.then(() => {
				setShowModal(false); // Закрываем модальное окно
				navigate(0); // Обновляем страницу для отображения новой книги
				alert("Book added successfully");
			})
			.catch(error => {
				let messages = '';
				if (error.response.data.Errors) {
					error.response.data.Errors.forEach(element => messages += element.Message + '\n');
				}
				else
					messages = 'Some fields are incorrect';
				alert('Failed to add book\n' + messages)
			});
	};

	return (
		<div className={itemsList.container}>
			<h1>Book search</h1>

			{isAdmin && <div className={adminButtons['admin-actions']}>
				<button onClick={() => setShowModal(true)}>Add a new book</button>
			</div>}

			{/* Строка поиска */}
			<div className={booksStyles['search-filter-container']}>
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
			<div className={itemsList.list}>
				{filteredBooks.map((book, index) => (
					<div key={index} className={itemsList.item}>
						<h2><label className={itemsList.title} onClick={() => handleBookClick(book)}>{book.title}</label></h2>
						<div className={itemsList.details}>
							{!isAdmin ? (<img src={book.pathToImage} alt={book.title} className={booksStyles.image} />)
								: (
									<label htmlFor={book.isbn}>
										<img src={book.pathToImage} alt={book.title} className={booksStyles.image} />
										<input id={book.isbn} hidden type="file" accept=".jpg,.png" onChange={(e) => handleUploadImage(e, book)} />
									</label>
								)
							}
							<div className={itemsList.info}>
								<p><strong>ISBN:</strong> {book.isbn}</p>
								<p><strong>Author:</strong> {book.author.name + ' ' + book.author.surname}</p>
								<p><strong>Genre:</strong> {book.genre}</p>
								{book.isOccupied && <p><strong>Out of stock</strong></p>}
							</div>
						</div>
					</div>
				))}
			</div>

			{/* Пагинация */}
			{totalPages > 1 && (
				<div className={itemsList.pagination}>
					{Array.from({ length: totalPages }, (_, i) => i + 1).map(pageNumber => (
						<button
							key={pageNumber}
							onClick={() => handlePageChange(pageNumber)}
							className={pageNumber === currentPage ? itemsList.active : ''}
						>
							{pageNumber}
						</button>
					))}
				</div>
			)}
			{showModal && <AddBookModal onSave={handleSaveBook} onClose={() => setShowModal(false)} />}
		</div>
	);
};

export default Books;
