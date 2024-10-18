import React, { useState, useEffect } from 'react';
import { useLocation, useNavigate } from 'react-router-dom';
import api from '../services/api';
import itemPage from '../styles/itemPage.module.css';
import adminButtons from '../styles/adminButtons.module.css';
import EditBookModal from '../components/EditBookModal'; // Импорт модального окна
import DeleteModal from '../components/DeleteModal';

const BookPage = ({ isAuthenticated, isAdmin }) => {
	const [book, setBook] = useState(null);
	const [isLoading, setIsLoading] = useState(true);
	const [error, setError] = useState(null);
	const [isEditModalOpen, setIsEditModalOpen] = useState(false); // Для контроля модального окна
	const [isDeleteModalOpen, setIsDeleteModalOpen] = useState(false); // Для контроля модального окна
	const location = useLocation();
	const navigate = useNavigate();

	const { isbn } = location.state;  // Получаем isAdmin и ISBN из state

	// Получаем информацию о книге по ISBN
	useEffect(() => {
		api.get(`Books/GetBookByISBN?ISBN=${isbn}`)
			.then(response => {
				setBook(response.data.data);
				setIsLoading(false);
			})
			.catch(err => {
				setError('Error fetching book details');
				setIsLoading(false);
			});
	}, [isbn]);

	const handleEditBook = (updatedBook) => {
		api.put(`Books/EditBook`, updatedBook)
			.then(() => {
				alert('Book updated successfully');
				setBook(updatedBook);  // Обновляем данные книги на странице
				setIsEditModalOpen(false);  // Закрываем модальное окно
			})
			.catch(() => alert('Failed to update book'));
	};

	const handleDeleteBook = () => {
		api.delete(`Books/DeleteBook?ISBN=${isbn}`)
			.then(() => {
				setIsDeleteModalOpen(false);  // Закрываем модальное окно
				alert('Book deleted successfully');
				navigate('/books'); // Возвращаемся на список книг
			})
			.catch(() => alert('Failed to delete book'));
	};

	const handleTakeBook = (book) => {
		api.post("User/TakeBook", { bookISBN: book.isbn, authorId: book.author.id })
		.then(response => {
			alert(response.data.message);
		});
	};

	if (isLoading) {
		return <p>Loading book details...</p>;
	}

	if (error) {
		return <p>{error}</p>;
	}

	const checkAuth = () => {
		if (isAuthenticated)
			return <button onClick={() => handleTakeBook(book)}>Take Book</button>;
		return null;
	}

	return (
		<div className={itemPage['item-page']}>
			<div className={itemPage['item-info-container']}>
				<img src={'../' + book.pathToImage} alt={book.title} className={itemPage['item-image']} />
				<div className={itemPage['item-details']}>
					<h1>{book.title}</h1>
					<p><strong>Author:</strong> {book.author.name} {book.author.surname}</p>
					<p><strong>Library author number:</strong> {book.author.id}</p>
					<p><strong>Genre:</strong> {book.genre}</p>
				</div>
			</div>
			<div className={itemPage['item-description']}>
				<h2>Description</h2>
				<p>{book.description}</p>
			</div>

			{isAdmin ? (
				<div className={adminButtons['admin-actions']}>
					<button onClick={() => setIsEditModalOpen(true)}>Edit Book</button> {/* Открываем модальное окно */}
					<button onClick={() => setIsDeleteModalOpen(true)}>Delete Book</button>
				</div>
			) : (
				!book.isOccupied && checkAuth()
			)}

			{isEditModalOpen && (
				<EditBookModal
					book={book}
					onSave={handleEditBook}
					onClose={() => setIsEditModalOpen(false)}  // Закрытие модального окна
				/>
			)}

			{isDeleteModalOpen && (
				<DeleteModal
					onOk={handleDeleteBook}
					onClose={() => setIsDeleteModalOpen(false)}  // Закрытие модального окна
				/>
			)}
		</div>
	);
};

export default BookPage;
