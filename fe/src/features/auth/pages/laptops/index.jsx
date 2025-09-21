import React, { useState, useEffect } from 'react';
import { deleteLaptop, fetchLaptops } from "../../services/laptopApi.js";
import styles from '../../../../styles/laptop/laptop.module.css'
import { Link } from "react-router-dom";

export default function Laptops() {
    const [items, setItems] = useState([]);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(null);

    useEffect(() => {
        fetchLaptops()
            .then(setItems)
            .catch(err => setError(err.message))
            .finally(() => setLoading(false));
    }, []);

    const handleDelete = async (id) => {
        if (!window.confirm('Delete this laptop?')) return;
        try {
            await deleteLaptop(id);
            setItems(items.filter(l => l.laptopId !== id));
        } catch (err) {
            setError(err.message);
        }
    };

    if (loading) return <p>Loading laptops…</p>;
    if (error) return <p>Error: {error}</p>;
    if (!items.length) return <p>No laptops found.</p>;

    return (
        <div className={styles.tableContainer}>
            <h1 className={styles.heading}>Laptops</h1>

            <div className={styles.controls}>
                <Link to="/laptops/create" className={styles.createButton}>
                    + Create Laptop
                </Link>
            </div>

            <table className={styles.laptops}>
                <thead>
                <tr>
                    <th>Name</th>
                    <th>Price</th>
                    <th>Brand</th>
                    <th>Stock</th>
                    <th>Created At</th>
                    <th>User ID</th>
                    <th>Actions</th>
                </tr>
                </thead>

                <tbody>
                {items.map(p => (
                    <tr key={p.laptopId}>
                        <td>
                            <Link to={`/laptops/${p.laptopId}`} className={styles.nameLink}>
                                {p.name}
                            </Link>
                        </td>
                        <td>${p.price.toFixed(2)}</td>
                        <td>{p.brand}</td>
                        <td>{p.stockQuantity}</td>
                        <td>{new Date(p.createdAt).toLocaleString()}</td>
                        <td>{p.userId}</td>
                        <td>
                            <Link
                                to={`/laptops/${p.laptopId}/edit`}
                                className={styles.editButton}
                            >
                                Edit
                            </Link>
                            <button
                                onClick={() => handleDelete(p.laptopId)}
                                className={styles.deleteButton}
                            >
                                Delete
                            </button>
                        </td>
                    </tr>
                ))}
                </tbody>
            </table>
        </div>
    );
}