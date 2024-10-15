import React, { useState } from 'react';
import { Link } from 'react-router-dom';
import '../styles/styles.css';

const Navbar = ({ isAuthenticated, onLogout }) => {
    const [dropdownOpen, setDropdownOpen] = useState(false);
  
    const toggleDropdown = () => {
      setDropdownOpen(!dropdownOpen);
    };
  
    return (
      <nav className="navbar">
        <ul>
          <li><Link to="/">Home</Link></li>
          <li><Link to="/books">Books</Link></li>
          <li className="dropdown">
            <img
              src="https://digital.spmi.ru/article/images/blank.png"
              alt="User Avatar"
              className="avatar"
              onClick={toggleDropdown}
            />
            {dropdownOpen && (
              <ul className="dropdown-menu">
                {isAuthenticated ? (
                  <>
                    <li><Link to="/profile">Profile</Link></li>
                    <li><button onClick={onLogout}>Logout</button></li>
                  </>
                ) : (
                  <>
                    <li><Link to="/login">Login</Link></li>
                    <li><Link to="/register">Register</Link></li>
                  </>
                )}
              </ul>
            )}
          </li>
        </ul>
      </nav>
    );
  };

  export default Navbar;