import React, { useState, useEffect } from 'react';
import { useParams, useNavigate, Link } from "react-router-dom";
import { fetchLaptop } from '../../services/laptopApi.js';
import styles from '../../../../styles/laptop/laptopDetail.module.css'

export default function LaptopDetail() {
    const { laptopId } = useParams();
    const [laptop, setLaptop] = useState(null);
    const [loading, setLoading] = useState(true);
    const [error, setError]   = useState(null);
    const navigate = useNavigate();

    useEffect(() => {
        fetchLaptop(laptopId)
            .then(data => setLaptop(data))
            .catch(err => setError(err.message))
            .finally(() => setLoading(false));
    }, [laptopId]);

    if (loading)   return <p>Loading details…</p>;
    if (error)     return <p>Error: {error}</p>;
    if (!laptop)   return <p>Not found</p>;

    return (
        <div className={styles.container}>
            <Link to={`/laptops`}>
            <button className={styles.backButton}>
                ← Back
            </button>
            </Link>
            <h1 className={styles.title}>{laptop.name}</h1>
            <table className={styles.detailTable}>
                <tbody>
                <tr><th>Price</th><td>${laptop.price.toFixed(2)}</td></tr>
                <tr><th>Brand</th><td>{laptop.brand}</td></tr>
                <tr><th>Stock</th><td>{laptop.stockQuantity}</td></tr>
                <tr>
                    <th>Created At</th>
                    <td>{new Date(laptop.createdAt).toLocaleString()}</td>
                </tr>
                <tr><th>User ID</th><td>{laptop.userId}</td></tr>
                </tbody>
            </table>
        </div>
    );
}
