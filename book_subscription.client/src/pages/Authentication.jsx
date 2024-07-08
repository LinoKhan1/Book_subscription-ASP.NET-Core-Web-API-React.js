import React, { useState } from 'react';

function Authentication() {
    const [loginUser, setLoginUser] = useState({
        email: '',
        password: ''
    });

    const [registerUser, setRegisterUser] = useState({
        userName: '',
        email: '',
        password: ''
    });

    const [registerSuccess, setRegisterSuccess] = useState(false);
    const [showRegisterForm, setShowRegisterForm] = useState(false);
    const [errorMessage, setErrorMessage] = useState('');

    const handleLoginSubmit = async (e) => {
        e.preventDefault();

        try {
            const response = await fetch('api/auth/login', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify(loginUser)
            });

            if (!response.ok) {
                const errorData = await response.json();
                throw new Error(errorData.message);
            }

            const data = await response.json();
            localStorage.setItem('token', data.token); // Store token in localStorage or secure storage

            // Redirect to Book page or set authenticated state
            console.log('Logged in:', data);
        } catch (error) {
            setErrorMessage(error.message);
        }
    };

    const handleRegisterSubmit = async (e) => {
        e.preventDefault();

        try {
            const response = await fetch('api/auth/register', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify(registerUser)
            });

            if (!response.ok) {
                const errorData = await response.json();
                throw new Error(errorData.message);
            }

            setRegisterSuccess(true);
            setErrorMessage('');
        } catch (error) {
            setErrorMessage(error.message);
        }
    };

    const handleInputChange = (e, formType) => {
        const { name, value } = e.target;
        if (formType === 'login') {
            setLoginUser({ ...loginUser, [name]: value });
        } else {
            setRegisterUser({ ...registerUser, [name]: value });
        }
    };

    const toggleForm = () => {
        setShowRegisterForm(!showRegisterForm);
        setErrorMessage('');
    };

    return (
        <div>
            <h2>{showRegisterForm ? 'Register' : 'Login'}</h2>
            {showRegisterForm ? (
                <form onSubmit={handleRegisterSubmit}>
                    <div>
                        <label>Username:</label>
                        <input
                            type="text"
                            name="userName"
                            value={registerUser.userName}
                            onChange={(e) => handleInputChange(e, 'register')}
                            required
                        />
                    </div>
                    <div>
                        <label>Email:</label>
                        <input
                            type="email"
                            name="email"
                            value={registerUser.email}
                            onChange={(e) => handleInputChange(e, 'register')}
                            required
                        />
                    </div>
                    <div>
                        <label>Password:</label>
                        <input
                            type="password"
                            name="password"
                            value={registerUser.password}
                            onChange={(e) => handleInputChange(e, 'register')}
                            required
                        />
                    </div>
                    <button type="submit">Register</button>
                </form>
            ) : (
                <form onSubmit={handleLoginSubmit}>
                    <div>
                        <label>Email:</label>
                        <input
                            type="email"
                            name="email"
                            value={loginUser.email}
                            onChange={(e) => handleInputChange(e, 'login')}
                            required
                        />
                    </div>
                    <div>
                        <label>Password:</label>
                        <input
                            type="password"
                            name="password"
                            value={loginUser.password}
                            onChange={(e) => handleInputChange(e, 'login')}
                            required
                        />
                    </div>
                    <button type="submit">Login</button>
                </form>
            )}
            <p>
                {showRegisterForm
                    ? "Already have an account? "
                    : "Don't have an account? "}
                <button onClick={toggleForm}>
                    {showRegisterForm ? 'Login' : 'Register'}
                </button>
            </p>
            {errorMessage && <p style={{ color: 'red' }}>{errorMessage}</p>}
            {registerSuccess && (
                <p style={{ color: 'green' }}>Successfully registered! Now you can login.</p>
            )}
        </div>
    );
}

export default Authentication;
