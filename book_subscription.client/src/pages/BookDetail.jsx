import { useEffect, useState } from 'react';
import { useParams } from 'react-router-dom';

function BookDetail() {
    const { BookId } = useParams();
    const [book, setBook] = useState(null);

    useEffect(() => {
        fetchBookData();
    }, [BookId]);

    async function fetchBookData() {
        try {
            const response = await fetch(`api/book/${bookId}`);
            const data = await response.json();
            console.log('Book data:', data); // Log the data received
            setBook(data);
        } catch (error) {
            console.error('Error fetching book data:', error);
        }
    }


    if (!book) {
        return <p>Loading...</p>;
    }

    return (
        <div>
            <h1>{book.Name}</h1>
            <p>{book.BookId}</p>
            <p>{book.Text}</p>
            <p>{book.PurchasePrice}</p>
        </div>
    );
}

export default BookDetail;
