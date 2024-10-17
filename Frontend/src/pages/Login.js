import React, { useState } from 'react';
import api from '../services/api.js';
import auth from '../styles/auth.module.css'; // Импортируем стили для страницы
import { useNavigate } from 'react-router-dom';

const Login = ({ onLogin }) => {
	const [username, setUsername] = useState('');
	const [password, setPassword] = useState('');
	const navigate = useNavigate();

	const handleLogin = async (e) => {
		e.preventDefault();
		const parseJwt = (token) => {
			var base64Url = token.split('.')[1];
			var base64 = base64Url.replace(/-/g, '+').replace(/_/g, '/');
			var jsonPayload = decodeURIComponent(window.atob(base64).split('').map(function (c) {
				return '%' + ('00' + c.charCodeAt(0).toString(16)).slice(-2);
			}).join(''));

			return JSON.parse(jsonPayload);
		}
		api.post('/Authentication/LogIn', { Email: username, password }).then(response => {
			localStorage.setItem('accessToken', response.data.data.accessToken);
			localStorage.setItem('refreshToken', response.data.data.refreshToken);
			localStorage.setItem('userId', parseJwt(localStorage.getItem('accessToken')).nameid);
			let isAdmin = parseJwt(localStorage.getItem('accessToken')).role == 'admin';
			onLogin(isAdmin);
			navigate('/');
		})
			.catch((error) => alert('Login fail\n' + error.response.data.message));
	};

	return (
		<div className={auth['auth-container']}>
			<h1>Login</h1>
			<form onSubmit={handleLogin}>
				<div className={auth['form-group']}>
					<label htmlFor="username">Login</label>
					<input
						type="text"
						id="username"
						value={username}
						onChange={(e) => setUsername(e.target.value)}
						required
					/>
				</div>
				<div className={auth['form-group']}>
					<label htmlFor="password">Password</label>
					<input
						type="password"
						id="password"
						value={password}
						onChange={(e) => setPassword(e.target.value)}
						required
					/>
				</div>
				<button type="submit" className={auth['auth-button']}>Log in</button>
			</form>
		</div>
	);
};

export default Login;
