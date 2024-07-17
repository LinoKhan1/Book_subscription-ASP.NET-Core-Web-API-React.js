import React from 'react'
import ReactDOM from 'react-dom/client'
import { BrowserRouter, Routes, Route } from "react-router-dom";

//import App from './App.jsx';
import Login from "./pages/Login.jsx";
import Register from "./pages/Register.jsx";
import Layout from "./components/Layout.jsx";
import BookCatalog from "./pages/Book.jsx";
import BookDetail from "./pages/BookDetail.jsx";
import Subscription from "./pages/Subscription.jsx";



ReactDOM.createRoot(document.getElementById('root')).render(
    <React.StrictMode>
        <BrowserRouter>
            <Routes>
                <Route path="/" element={<Layout />}>
                    <Route index element={<Login />} />
                    <Route path="/register" element={<Register/> }/>
                    <Route path="/book" element={<BookCatalog />} />
                    <Route path="/book/:id" element={<BookDetail />} />
                    <Route path="/subscription" element={<Subscription/>}/>
                </Route>
            </Routes>
        </BrowserRouter>
    </React.StrictMode>
);
