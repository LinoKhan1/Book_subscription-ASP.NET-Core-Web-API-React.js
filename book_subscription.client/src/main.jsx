import React from 'react'
import ReactDOM from 'react-dom/client'
import { BrowserRouter, Routes, Route } from "react-router-dom";

//import App from './App.jsx';
import Authentication from "./pages/Authentication.jsx";
import Layout from "./components/Layout.jsx";
import BookCatalog from "./pages/Book.jsx";
import BookDetail from "./pages/BookDetail.jsx";
import Subscription from "./pages/Subscription.jsx";



ReactDOM.createRoot(document.getElementById('root')).render(
    <React.StrictMode>
        <BrowserRouter>
            <Routes>
                <Route path="/" element={<Layout />}>
                    <Route index element={<Authentication />} />
                    <Route path="/books" element={<BookCatalog />} />
                    <Route path="/book/:BookId" element={<BookDetail />} />
                    <Route path="/subscription" element={<Subscription/>}/>
                </Route>
            </Routes>
        </BrowserRouter>
    </React.StrictMode>
);
