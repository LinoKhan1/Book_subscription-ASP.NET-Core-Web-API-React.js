import React, { useEffect, useState } from 'react';
import { useParams, Link } from 'react-router-dom';

function BookDetail() {
    const { id } = useParams();
    const [book, setBook] = useState(null);
    const [loading, setLoading] = useState(true);
    const [subscribed, setSubscribed] = useState(false); // Track subscription status

    useEffect(() => {
        fetchBookDetail();
    }, [id]);

    async function fetchBookDetail() {
        try {
            const token = localStorage.getItem('token');
            const response = await fetch(`/api/book/${id}`, {
                headers: {
                    Authorization: `Bearer ${token}`,
                },
            });
            if (response.ok) {
                const data = await response.json();
                setBook(data);
            } else {
                setBook(null);
            }
        } catch (error) {
            console.error("Failed to fetch book detail", error);
            setBook(null);
        } finally {
            setLoading(false);
        }
    }

    async function handleSubscribe() {
        try {
            const token = localStorage.getItem('token');
            const response = await fetch(`/api/subscription`, {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: `Bearer ${token}`,
                },
                body: JSON.stringify({ bookId: id }),
            });
            if (response.ok) {
                // Optionally update UI to reflect subscription status
                setSubscribed(true);
            } else {
                console.error('Failed to subscribe');
            }
        } catch (error) {
            console.error('Failed to subscribe', error);
        }
    }

    if (loading) {
        return <p>Loading...</p>;
    }

    if (!book) {
        return <p>Book not found</p>;
    }

    return (
        <div>
            <h2>Book Detail</h2>
            <p><strong>BookId:</strong> {book.bookId}</p>
            <p><strong>Name:</strong> {book.name}</p>
            <p><strong>Text:</strong> {book.text}</p>
            <p><strong>Purchase Price:</strong> {book.purchasePrice}</p>
            
            {/* Display Subscribe button if not already subscribed */}
            {!subscribed && (
                <button onClick={handleSubscribe}>Subscribe</button>
            )}

            <Link to="/book">Back to Book Catalog</Link>
        </div>
    );
}

export default BookDetail;
