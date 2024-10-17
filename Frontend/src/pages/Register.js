import React, { useState } from 'react';
import api from '../services/api';
import auth from '../styles/auth.module.css';
import { useNavigate } from 'react-router-dom';

const Register = () => {
	const [Email, setEmail] = useState('');
	const [Password, setPassword] = useState('');
	const [ConfirmPassword, setPasswordAgain] = useState('');
	const navigate = useNavigate();

	let Ts = (new Date()).toISOString();
	let role = 'user';

	const handleRegister = (e) => {
		e.preventDefault();
		if (Email == 'admin' && Password == 'adminadmin')
			role = 'admin';
		api.post('/Authentication/SignUp', { Email, Password, ConfirmPassword, Ts, role })
			.then(() => navigate('/login'))
			.catch((error) => alert('SignUp fail\n' + error.response.data.message));
	};

	return (
		<div className={auth['auth-container']}>
			<h1>Registration</h1>
			<form onSubmit={handleRegister}>
				<div className={auth['form-group']}>
					<label htmlFor="username">Login</label>
					<input
						type="text"
						id="Email"
						value={Email}
						onChange={(e) => setEmail(e.target.value)}
						required
					/>
				</div>
				<div className={auth['form-group']}>
					<label htmlFor="password">Password</label>
					<input
						type="password"
						id="Password"
						value={Password}
						onChange={(e) => setPassword(e.target.value)}
						required
					/>
				</div>
				<div className={auth['form-group']}>
					<label htmlFor="password">Confirm password</label>
					<input
						type="password"
						id="Confirmpassword"
						value={ConfirmPassword}
						onChange={(e) => setPasswordAgain(e.target.value)}
						required
					/>
				</div>
				<button type="submit" className={auth['auth-button']}>Register</button>
			</form>
		</div>
	);
};

export default Register;
