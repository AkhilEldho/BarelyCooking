import React, { useEffect, useState } from 'react';
import "bootstrap/dist/css/bootstrap.css"
import "bootstrap/dist/js/bootstrap.js"
import "bootstrap-icons/font/bootstrap-icons.css"
import { Footer, Header } from '../Components/Layout';
import { ThemeProvider } from "../Components/Theme/ThemeContext";
import { Home, NotFound, CakeDetails } from '../Pages';
import { Route, Routes } from 'react-router-dom';

function App() {
  return (
    <div>
      <ThemeProvider>
        <Header/>
        <div className="pb-5">
          <Routes>
            <Route path="/" element={<Home/>} />
            <Route path="/cake/:cakeId" element={<CakeDetails/>} />
            <Route path="*" element={<NotFound/>} />
          </Routes>
        </div>
        <Footer/>
      </ThemeProvider>
    </div>
  );
}

export default App;
