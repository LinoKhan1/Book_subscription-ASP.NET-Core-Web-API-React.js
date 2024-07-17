import React, { useEffect, useState } from 'react';

function Subscription() {
    const [subscriptions, setSubscriptions] = useState([]);
    const [loading, setLoading] = useState(true);

    useEffect(() => {
        fetchSubscriptions();
    }, []);

    async function fetchSubscriptions() {
        try {
            const token = localStorage.getItem('token');
            const response = await fetch('/api/subscription', {
                headers: {
                    Authorization: `Bearer ${token}`,
                },
            });
            if (response.ok) {
                const data = await response.json();
                setSubscriptions(data);
            } else {
                console.error('Failed to fetch subscriptions');
            }
        } catch (error) {
            console.error('Failed to fetch subscriptions', error);
        } finally {
            setLoading(false);
        }
    }

    if (loading) {
        return <p>Loading subscriptions...</p>;
    }

    if (subscriptions.length === 0) {
        return <p>No subscriptions found.</p>;
    }

    return (
        <div>
            <h2>Subscriptions</h2>
            <ul>
                {subscriptions.map((subscription) => (
                    <li key={subscription.SubscriptionId}>
                        <strong>Book:</strong> {subscription.book.name} <br />
                        <strong>Subscription Date:</strong> {new Date(subscription.SubscriptionDate).toLocaleDateString()}
                    </li>
                ))}
            </ul>
        </div>
    );
}

export default Subscription;
