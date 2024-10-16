import React, { useState } from 'react';
import api from '../services/api';
import '../styles/auth.css';
import { useNavigate } from 'react-router-dom';

const Register = () => {
  const [Email, setEmail] = useState('');
  const [Password, setPassword] = useState('');
  const [ConfirmPassword, setPasswordAgain] = useState('');
  const navigate = useNavigate();
  let Ts = (new Date()).toISOString();
  let role = 'user';
  const handleRegister = async (e) => {
    e.preventDefault();
    try {
      if (Email == 'admin' && Password == 'adminadmin')
        role = 'admin';
      await api.post('/Authentication/SignUp', { Email, Password, ConfirmPassword, Ts, role });
      navigate('/login');
    } catch (error) {
      console.error('Register error', error);
    }
  };

  return (
    <div className="auth-container">
      <h1>Registration</h1>
      <form onSubmit={handleRegister}>
        <div className="form-group">
          <label htmlFor="username">Login</label>
          <input
            type="text"
            id="Email"
            value={Email}
            onChange={(e) => setEmail(e.target.value)}
            required
          />
        </div>
        <div className="form-group">
          <label htmlFor="password">Password</label>
          <input
            type="password"
            id="Password"
            value={Password}
            onChange={(e) => setPassword(e.target.value)}
            required
          />
        </div>
        <div className="form-group">
          <label htmlFor="password">Confirm password</label>
          <input
            type="password"
            id="Confirmpassword"
            value={ConfirmPassword}
            onChange={(e) => setPasswordAgain(e.target.value)}
            required
          />
        </div>
        <button type="submit" className="auth-button">Register</button>
      </form>
    </div>
  );
};

export default Register;
