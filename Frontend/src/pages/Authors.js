import React, { useState, useEffect } from 'react';
import api from '../services/api.js';
import itemsList from '../styles/itemsList.module.css';
import adminButtons from '../styles/adminButtons.module.css';
import { useNavigate } from 'react-router-dom';
import AddAuthorModal from '../components/AddAuthorModal.js';

const Authors = ({isAdmin}) => {
	const [authors, setAuthors] = useState([]);
	const [currentPage, setCurrentPage] = useState(1);
	const [totalPages, setTotalPages] = useState(1);
	const [showModal, setShowModal] = useState(false);

	const navigate = useNavigate();
	const authorsPerPage = 5;

	useEffect(() => {
		api.get(`Authors/GetAllAuthors?PageNumber=${currentPage}&PageSize=${authorsPerPage}`)
			.then(response => {
				setAuthors(response.data.data);
				setTotalPages(response.data.totalPages);
			})
			.catch(error => console.error('Error during authors loading occured', error));
	}, [currentPage]);

	const handleSaveAuthor = (newAuthor) => {
		api.post('Authors/AddAuthor', newAuthor)
			.then(() => {
				setShowModal(false); // Закрываем модальное окно
				navigate(0); // Обновляем страницу для отображения новой книги
				alert("Author added successfully");
			})
			.catch((error) => {
				let messages = '';
				error.response.data.Errors.forEach(element => {
					messages += element.Message + '\n';
				});
				alert('Failed to add author\n' + messages);
			});
	};

	const handleAuthorClick = (author) => {
		navigate(`/author/${author.id}`, { state: { id: author.id } });
	};

	const handlePageChange = (pageNumber) => {
		setCurrentPage(pageNumber);
	};

	return (
		<div className={itemsList.container}>
			{isAdmin && <div className={adminButtons['admin-actions']}>
				<button onClick={() => setShowModal(true)}>Add a new author</button>
			</div>}
			<h1>Authors</h1>
			<div className={itemsList.list}>
				{authors.map((author, index) => (
					<div key={index} className={itemsList.item}>
						<h2><label className={itemsList.title} onClick={() => handleAuthorClick(author)}>{author.name + " " + author.surname}</label></h2>
						<div className={itemsList.details}>
							<div className={itemsList.info}>
								<p><strong>Birth date (yyyy-mm-dd):</strong> {author.birthDate}</p>
								<p><strong>Country:</strong> {author.country}</p>
								<p><strong>Library author number:</strong> {author.id}</p>
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
							className={pageNumber === currentPage ? 'active' : ''}
						>
							{pageNumber}
						</button>
					))}
				</div>
			)}

			{showModal && <AddAuthorModal onSave={handleSaveAuthor} onClose={() => setShowModal(false)} />}
		</div>
	);
};

export default Authors;
