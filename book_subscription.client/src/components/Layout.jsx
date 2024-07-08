import { Outlet, Link } from "react-router-dom";

const Layout = () => {
    return (
        <>
            <nav>
                <ul>
                    <li>
                        <Link to="/">Authentication</Link>
                    </li>
                    <li>
                    <Link to="/books">Books</Link>
                    </li>
                    <li>
                    <Link to="/subscription">Subscriptions</Link>
                    </li>
                </ul>
           
            </nav>
            <Outlet/>

        </>
    )
}
export default Layout;