import React, { useState, useEffect } from 'react';
import { Routes, Route, useNavigate } from 'react-router-dom';
import Navbar from './components/Navbar';
import Home from './pages/Home';
import Books from './pages/Books';
import BookPage from './pages/BookPage';
import Authors from './pages/Authors';
import AuthorPage from './pages/AuthorPage';
import UserProfile from './pages/UserProfile';
import Login from './pages/Login';
import Register from './pages/Register';
import api from './services/api';
import app from './app.module.css';

const App = () => {
  const [isAuthenticated, setIsAuthenticated] = useState(null);
  const [isAdmin, setIsAdmin] = useState(false);
  const navigate = useNavigate();

  useEffect(() => {
    api.get('/Authentication/Verify')
  .then(response => {
    setIsAuthenticated(true);
    setIsAdmin(response.data);
  })
  .catch(() => setIsAuthenticated(false));
  }, []);

  const handleLogout = () => {
    api.post('/Authentication/LogOut')
      .then(() => {
        localStorage.removeItem('userId');
        localStorage.removeItem('accessToken');
        localStorage.removeItem('refreshToken');
        setIsAuthenticated(false);
        navigate(0);
      })
      .catch();
  };

  const handleLogin = (isAdmin) => {

    setIsAuthenticated(true);
    setIsAdmin(isAdmin);
  };

  if (isAuthenticated == null)
    return;

  return (
    <div className={app.app}>
      <Navbar isAuthenticated={isAuthenticated} onLogout={handleLogout} />
      <Routes>
        <Route path="/" element={<Home />} />
        <Route path="/login" element={<Login onLogin={handleLogin} />} />
        <Route path="/books" element={<Books isAdmin={isAdmin} />} />
        <Route path="/book/:isbn" element={<BookPage isAuthenticated={isAuthenticated} isAdmin={isAdmin}/>} />
        <Route path="/authors" element={<Authors isAdmin={isAdmin}/>} />
        <Route path="/author/:id" element={<AuthorPage isAdmin={isAdmin}/>} />
        <Route path="/profile/:id" element={isAuthenticated ? <UserProfile /> : <Home />} />
        <Route path="/register" element={<Register />} />
      </Routes>
    </div>
  );
};
export default App;
