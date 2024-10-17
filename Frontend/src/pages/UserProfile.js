import React, { useState, useEffect } from 'react';
import { useNavigate } from 'react-router-dom';
import api from '../services/api';
import itemsList from '../styles/itemsList.module.css';
import books from '../styles/books.module.css';
import buttons from '../styles/adminButtons.module.css';

const UserProfile = () => {
	const [userBooks, setUserBooks] = useState([]);
	const [isLoading, setIsLoading] = useState(true);
	const [error, setError] = useState(null);
	const [currentPage, setCurrentPage] = useState(1);
	const [totalPages, setTotalPages] = useState(1);
	const navigate = useNavigate();

	const booksPerPage = 5;

	useEffect(() => {
		api.get(`Books/GetUserBooks?Id=${localStorage.getItem('userId')}&PageNumber=${currentPage}&PageSize=${booksPerPage}`)
			.then(response => {
				setUserBooks(response.data.data);
				setTotalPages(response.data.totalPages);
				setIsLoading(false);
			})
			.catch(() => {
				setError('Error fetching user details');
				setIsLoading(false);
			});
	}, [currentPage]);

	if (isLoading) {
		return <p>Loading user details...</p>;
	}

	if (error) {
		return <p>{error}</p>;
	}

	const handlePageChange = (pageNumber) => {
		setCurrentPage(pageNumber);
	};

	const handleBookClick = (book) => {
		navigate(`/book/${book.isbn}`, { state: { isbn: book.isbn } });
	};

	const handleExtend = (bookId) => {
		// Логика для продления книги
		console.log(`Продлить книгу с ID: ${bookId}`);
		// Здесь можно выполнить запрос на сервер для продления книги
	};

	const handleReturn = (bookId) => {
		// Логика для возврата книги
		console.log(`Вернуть книгу с ID: ${bookId}`);
		// Здесь можно выполнить запрос на сервер для возврата книги
	};

	return (
		<div>
			<div style={{ textAlign: "center", marginBottom: "2rem" }}>
				<h2>List of books</h2>
			</div>

			<div className={itemsList.list}>
				{userBooks.map((userBook, index) => (
					<div key={index} className={itemsList.item}>
						<h2><label className={itemsList.title} onClick={() => handleBookClick(userBook)}>{userBook.book.title}</label></h2>
						<div className={itemsList.details}>
							<img src={'../' + userBook.book.pathToImage} alt={userBook.book.title} className={books.image} />
							<div className={itemsList.info}>
								<p><strong>ISBN:</strong> {userBook.book.isbn}</p>
								<p><strong>Genre:</strong> {userBook.book.genre}</p>
								<p><strong>Author:</strong> {userBook.book.author.name + " " + userBook.book.author.surname}</p>
								<p><strong>Issue date:</strong> {userBook.issueDate}</p>
								<p><strong>Refund date:</strong> {userBook.refundDate}</p>
							</div>

							{/* Блок с кнопками справа */}
							<div className={buttons['admin-actions']} style={{flexDirection: "column", marginLeft: "auto", alignSelf: "center"}}>
								<button onClick={() => handleExtend(userBook.book.id)}>Продлить</button>
								<button onClick={() => handleReturn(userBook.book.id)}>Вернуть</button>
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

		</div>
	);
};

export default UserProfile;
