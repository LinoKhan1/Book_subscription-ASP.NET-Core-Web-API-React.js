import { Outlet, Link } from "react-router-dom";

const Layout = () => {
    return (
        <>
            <nav>
                <ul>
                    <li>
                        <Link to="/">Login</Link>
                    </li>
                    <li>
                    <Link to="/register">Register</Link>

                    </li>
                    <li>
                    <Link to="/book">Books</Link>
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