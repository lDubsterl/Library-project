import React, { useState } from 'react';
import { Link } from 'react-router-dom';
import styles from '../styles/styles.module.css';

const Navbar = ({ isAuthenticated, onLogout }) => {
  const [dropdownOpen, setDropdownOpen] = useState(false);

  const toggleDropdown = () => {
    setDropdownOpen(!dropdownOpen);
  };

  return (
    <nav className={styles.navbar}>
      <ul>
        <li><Link to="/">Home</Link></li>
        <li><Link to="/books">Books</Link></li>
        <li><Link to="/authors">Authors</Link></li>
        <li className={styles.dropdown}>
          <img
            src="https://digital.spmi.ru/article/images/blank.png"
            alt="User Avatar"
            className={styles.avatar}
            onClick={toggleDropdown}
          />
          {dropdownOpen && (
            <ul className={styles['dropdown-menu']}>
              {isAuthenticated ? (
                <>
                  <li><Link to={`/profile/${localStorage.getItem('userId')}`}>Profile</Link></li>
                  <li><a onClick={onLogout}>Logout</a></li>
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