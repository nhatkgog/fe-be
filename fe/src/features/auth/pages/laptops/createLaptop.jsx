import React, { useState } from 'react';
import { Link, useNavigate } from "react-router-dom";
import { createLaptop } from '../../services/laptopApi.js';
import styles from '../../../../styles/laptop/laptopForm.module.css';

export default function CreateLaptop() {
    const [form, setForm] = useState({
        name: '',
        brand: '',
        price: '',
        stockQuantity: '',
        createdAt: '',
        userId: '',
    });
    const [loading, setLoading] = useState(false);
    const [error, setError] = useState(null);
    const navigate = useNavigate();

    function handleChange(e) {
        const { name, value } = e.target;
        setForm(f => ({ ...f, [name]: value }));
    }

    async function handleSubmit(e) {
        e.preventDefault();
        setLoading(true);
        setError(null);

        try {
            // coerce numeric fields
            const payload = {
                ...form,
                price: parseFloat(form.price),
                stockQuantity: parseInt(form.stockQuantity, 10),
                userId: parseInt(form.userId, 10),
            };
            const newLaptop = await createLaptop(payload);
            // navigate to detail page after creation
            navigate(`/laptops/${newLaptop.laptopId}`);
        } catch (err) {
            setError(err.message);
        } finally {
            setLoading(false);
        }
    }

    return (
        <div className={styles.container}>
            <h1 className={styles.title}>Create Laptop</h1>
            <Link to={`/laptops`}>
                <button type="submit" disabled={loading}>
                    {loading ? 'Backing...' : 'Back to List'}
                </button>
            </Link>
            <form onSubmit={handleSubmit} className={styles.form}>
                <label>
                    Name
                    <input
                        type="text"
                        name="name"
                        value={form.name}
                        onChange={handleChange}
                        required
                    />
                </label>

                <label>
                    Brand
                    <input
                        type="text"
                        name="brand"
                        value={form.brand}
                        onChange={handleChange}
                        required
                    />
                </label>

                <label>
                    Price
                    <input
                        type="number"
                        step="0.01"
                        name="price"
                        value={form.price}
                        onChange={handleChange}
                        required
                    />
                </label>

                <label>
                    Stock Quantity
                    <input
                        type="number"
                        name="stockQuantity"
                        value={form.stockQuantity}
                        onChange={handleChange}
                        required
                    />
                </label>

                <label>
                    Created At
                    <input
                        type="datetime-local"
                        name="createdAt"
                        value={form.createdAt}
                        onChange={handleChange}
                        required
                    />
                </label>

                <label>
                    User ID
                    <input
                        type="number"
                        name="userId"
                        value={form.userId}
                        onChange={handleChange}
                        required
                    />
                </label>

                <button type="submit" disabled={loading}>
                    {loading ? 'Creating…' : 'Create'}
                </button>

                {error && <p className={styles.error}>Error: {error}</p>}
            </form>
        </div>
    );
}
