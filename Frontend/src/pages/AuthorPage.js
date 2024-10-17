import React, { useState, useEffect } from 'react';
import { useLocation, useNavigate } from 'react-router-dom';
import api from '../services/api';

import itemPage from '../styles/itemPage.module.css';
import adminButtons from '../styles/adminButtons.module.css';
import itemsList from '../styles/itemsList.module.css';
import books from '../styles/books.module.css';

import EditAuthorModal from '../components/EditAuthorModal.js'; // Импорт модального окна
import DeleteModal from '../components/DeleteModal.js';

const AuthorPage = ({ isAdmin }) => {
	const [author, setAuthor] = useState(null);
	const [isLoading, setIsLoading] = useState(true);
	const [isBookLoading, setIsBookLoading] = useState(true);
	const [error, setError] = useState(null);
	const [isEditModalOpen, setIsEditModalOpen] = useState(false); // Для контроля модального окна
	const [isDeleteModalOpen, setIsDeleteModalOpen] = useState(false); // Для контроля модального окна
	const [currentPage, setCurrentPage] = useState(1);
	const [totalPages, setTotalPages] = useState(1);
	const navigate = useNavigate();
	const location = useLocation();

	const { id } = location.state;
	const booksPerPage = 5;

	useEffect(() => {
		api.get(`Authors/GetAuthorById?Id=${id}`)
			.then(response => {
				setAuthor(response.data.data);
				setIsLoading(false);
			})
			.catch(() => {
				setError('Error fetching author details');
				setIsLoading(false);
			});
	}, [id]);

	useEffect(() => {
		if (author) {
			setIsBookLoading(true);
			api.get(`Authors/GetAllBooksByAuthor?PageNumber=${currentPage}&PageSize=${booksPerPage}&AuthorId=${author.id}`)
				.then(response => {
					author.books = response.data.data;
					setTotalPages(response.data.totalPages);
					setIsBookLoading(false);
				})
				.catch(err => {
					setError('Error fetching author books');
					setIsBookLoading(false);
				});
		}
	}, [author, currentPage]);

	const handleEditAuthor = (updatedAuthor) => {
		api.put(`Authors/EditAuthor`, updatedAuthor)
			.then(() => {
				alert('Author updated successfully');
				setAuthor(updatedAuthor);
				setIsEditModalOpen(false);
			})
			.catch((error) => {
				let messages = '';
				error.response.data.Errors.forEach(element => {
					messages += element.Message + '\n';
				});
				alert('Failed to update author\n' + messages);
			});
	};

	const handleDeleteAuthor = () => {
		api.delete(`Authors/DeleteAuthor?Id=${id}`)
			.then(() => {
				setIsDeleteModalOpen(false);
				alert('Author deleted successfully');
				navigate('/authors');
			})
			.catch(() => alert('Failed to delete author'));
	};

	if (isLoading) {
		return <p>Loading author details...</p>;
	}

	if (isBookLoading) {
		return null;
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

	return (
		<div className={itemPage['item-page']}>
			<div className={itemPage['item-container']}>
				<div className={itemPage['item-details']} style={{ marginLeft: 0 }}>
					<h1>{author.name} {author.surname}</h1>
					<p><strong>Library author number:</strong> {author.id}</p>
					<p><strong>Birth date (yyyy-mm-dd):</strong> {author.birthDate}</p>
					<p><strong>Country:</strong> {author.country}</p>
				</div>
			</div>

			{isAdmin && (
				<div className={adminButtons['admin-actions']}>
					<button onClick={() => setIsEditModalOpen(true)}>Edit Author</button> {/* Открываем модальное окно */}
					<button onClick={() => setIsDeleteModalOpen(true)}>Delete Author</button>
				</div>
			)}

			<div style={{ textAlign: "center", marginBottom: "2rem" }}>
				<h2>List of author books</h2>
			</div>

			<div className={itemsList.list}>
				{author.books.map((book, index) => (
					<div key={index} className={itemsList.item}>
						<h2><label className={itemsList.title} onClick={() => handleBookClick(book)}>{book.title}</label></h2>
						<div className={itemsList.details}>
							<img src={'../' + book.pathToImage} alt={book.title} className={books.image} />
							<div className={itemsList.info}>
								<p><strong>ISBN:</strong> {book.isbn}</p>
								<p><strong>Genre:</strong> {book.genre}</p>
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

			{isEditModalOpen && (
				<EditAuthorModal
					author={author}
					onSave={handleEditAuthor}
					onClose={() => setIsEditModalOpen(false)}  // Закрытие модального окна
				/>
			)}

			{isDeleteModalOpen && (
				<DeleteModal
					onOk={handleDeleteAuthor}
					onClose={() => setIsDeleteModalOpen(false)}  // Закрытие модального окна
				/>
			)}
		</div>
	);
};

export default AuthorPage;
