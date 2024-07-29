import React from "react";
import { useTheme } from "../Theme/ThemeContext";
import { NavLink } from "react-router-dom";
import { FaMoon, FaSun } from "react-icons/fa"; // Importing icons from react-icons

let logoDark = require("../../Assets/Images/logo-favicon-white.png");
let logoLight = require("../../Assets/Images/logo-favicon-color.png");

function Header() {
    const { theme, toggleTheme } = useTheme();

    return (
        <div>
            <nav className={`navbar navbar-expand-lg ${theme === "light" ? "bg-light" : "bg-dark"}`}>
                <div className="container-fluid">
                    <NavLink className="nav-link" aria-current="page" to="/">
                        <img
                            src={theme === "light" ? logoLight : logoDark}
                            alt="Logo"
                            style={{ height: "40px" }}
                        />
                    </NavLink>

                    <button className="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbarSupportedContent" aria-controls="navbarSupportedContent" aria-expanded="false" aria-label="Toggle navigation">
                        <span className="navbar-toggler-icon"></span>
                    </button>
                    <div className="collapse navbar-collapse" id="navbarSupportedContent">
                        <ul className="navbar-nav me-auto mb-2 mb-lg-0">
                            <li className="nav-item">
                                <NavLink className="nav-link" aria-current="page" to="/">Home</NavLink>
                            </li>
                            <li className="nav-item">
                                <NavLink className="nav-link" aria-current="page" to="/shoppingCart">
                                    <i className="bi bi-cart"></i>
                                </NavLink>
                            </li>
                            <li className="nav-item dropdown">
                                <a className="nav-link dropdown-toggle" href="#" role="button" data-bs-toggle="dropdown" aria-expanded="false">
                                    Admin Panel
                                </a>
                                <ul className="dropdown-menu">
                                    <li><a className="dropdown-item" href="#">Action</a></li>
                                    <li><a className="dropdown-item" href="#">Another action</a></li>
                                    <li><a className="dropdown-item" href="#">Something else here</a></li>
                                </ul>
                            </li>
                        </ul>
                        <div onClick={toggleTheme} style={{ cursor: "pointer", color: theme === "light" ? "#000" : "#fff" }}>
                            {theme === "light" ? <FaMoon size={24} /> : <FaSun size={24} />}
                        </div>
                    </div>
                </div>
            </nav>
        </div>
    );
}

export default Header;
