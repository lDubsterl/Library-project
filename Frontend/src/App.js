import React, { useState, useEffect } from 'react';
import { Routes, Route } from 'react-router-dom';
import Navbar from './components/Navbar';
import Home from './pages/Home';
import Books from './pages/Books';
import Profile from './pages/Profile';
import Login from './pages/Login';
import Register from './pages/Register';
import api from './services/api';

const App = () => {
  const [isAuthenticated, setIsAuthenticated] = useState(false);
  const [isAdmin, setIsAdmin] = useState(false);
  useEffect(() => {
    const verify = async () => {
      try {
        await api.get('/Authentication/Verify');
        setIsAuthenticated(true);
      }
      catch (error) { };
    }
    verify();
  }, []);

  const handleLogout = async () => {
    try {
      await api.post('/Authentication/LogOut');
      localStorage.removeItem('userId');
      localStorage.removeItem('accessToken');
      localStorage.removeItem('refreshToken');
      setIsAuthenticated(false);
    }
    catch (error) { }
  };

  const handleLogin = (isAdmin) => {
    setIsAuthenticated(true);
    setIsAdmin(isAdmin);
  };

  return (
    <div className="app">
      <Navbar isAuthenticated={isAuthenticated} onLogout={handleLogout} />
      <Routes>
        <Route path="/" element={<Home />} />
        <Route path="/books" element={<Books isAdmin={isAdmin}/>} />
        <Route path="/profile" element={isAuthenticated ? <Profile /> : <Home />} />
        <Route path="/login" element={<Login onLogin={handleLogin} />} />
        <Route path="/register" element={<Register />} />
      </Routes>
    </div>
  );
};
export default App;
