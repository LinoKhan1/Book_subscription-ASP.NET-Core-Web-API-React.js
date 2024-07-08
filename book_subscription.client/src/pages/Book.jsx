import { useEffect, useState } from 'react';
import { Link } from 'react-router-dom';

function BookCatalog() {
    const [books, setBooks] = useState([]);

    useEffect(() => {
        fetchBookData();
    }, []);

    const contents = books.length === 0
        ? <p><em>Loading... Please refresh once the ASP.NET backend has started. See <a href="https://aka.ms/jspsintegrationreact">https://aka.ms/jspsintegrationreact</a> for more details.</em></p>
        : <table className="table table-striped" aria-labelledby="tableLabel">
            <thead>
                <tr>
                    <th>BookId</th>
                    <th>Name</th>
                    <th>Text</th>
                    <th>Purchase Price</th>
                </tr>
            </thead>
            <tbody>
                {books.map(book =>
                    <tr key={book.bookId}>
                        <td>{book.bookId}</td>
                        <td>{book.name}</td>
                        <td>{book.text}</td>
                        <td>{book.purchasePrice}</td>
                        <td>
                            <Link to={`/book/${book.bookId}`}>View Detail</Link>
                        </td>
                    </tr>
                )}
            </tbody>
        </table>;

    return (
        <div>
            <h1 id="tableLabel">Books</h1>
            <p>This component demonstrates fetching data from the server.</p>
            {contents}
        </div>
    );

    async function fetchBookData() {
        const response = await fetch('api/book');
        const data = await response.json();
        setBooks(data);
    }
}

export default BookCatalog;
