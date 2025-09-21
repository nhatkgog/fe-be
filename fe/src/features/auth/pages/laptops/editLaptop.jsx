import React, { useState, useEffect } from 'react';
import { Link, useNavigate, useParams } from "react-router-dom";
import { fetchLaptop, updateLaptop } from '../../services/laptopApi';
import styles from '../../../../styles/laptop/laptopForm.module.css';

export default function EditLaptop() {
    const { laptopId } = useParams();
    const [form, setForm] = useState({
        name: '', brand: '', price: '', stockQuantity: '',
        createdAt: '', userId: '',
    });
    const [loading, setLoading] = useState(true);
    const [saving, setSaving] = useState(false);
    const [error, setError] = useState(null);
    const navigate = useNavigate();

    // 1. Load existing data
    useEffect(() => {
        fetchLaptop(laptopId)
            .then(l => setForm({
                name: l.name,
                brand: l.brand,
                price: l.price,
                stockQuantity: l.stockQuantity,
                createdAt: l.createdAt.slice(0,16), // for datetime-local
                userId: l.userId,
            }))
            .catch(err => setError(err.message))
            .finally(() => setLoading(false));
    }, [laptopId]);

    function handleChange(e) {
        const { name, value } = e.target;
        setForm(f => ({ ...f, [name]: value }));
    }

    async function handleSubmit(e) {
        e.preventDefault();
        setSaving(true);
        setError(null);

        try {
            const payload = {
                ...form,
                price: parseFloat(form.price),
                stockQuantity: parseInt(form.stockQuantity, 10),
                userId: parseInt(form.userId, 10),
            };
            await updateLaptop(laptopId, payload);
            navigate(`/laptops/${laptopId}`);
        } catch (err) {
            setError(err.message);
        } finally {
            setSaving(false);
        }
    }

    if (loading) return <p>Loading…</p>;
    if (error)   return <p>Error: {error}</p>;

    return (
        <div className={styles.container}>
            <h1 className={styles.title}>Edit Laptop</h1>
            <Link to={`/laptops`}>
                <button type="submit" disabled={loading}>
                    {loading ? 'Backing...' : 'Back to List'}
                </button>
            </Link>
            <form onSubmit={handleSubmit} className={styles.form}>
                {/* Reuse the same fields as CreateLaptop */}
                <label>
                    Name
                    <input name="name" value={form.name} onChange={handleChange} required />
                </label>

                <label>
                    Brand
                    <input name="brand" value={form.brand} onChange={handleChange} required />
                </label>

                <label>
                    Price
                    <input
                        type="number" step="0.01" name="price"
                        value={form.price} onChange={handleChange} required
                    />
                </label>

                <label>
                    Stock Quantity
                    <input
                        type="number" name="stockQuantity"
                        value={form.stockQuantity} onChange={handleChange} required
                    />
                </label>

                <label>
                    Created At
                    <input
                        type="datetime-local" name="createdAt"
                        value={form.createdAt} onChange={handleChange} required
                    />
                </label>

                <label>
                    User ID
                    <input
                        type="number" name="userId"
                        value={form.userId} onChange={handleChange} required
                    />
                </label>

                <button type="submit" disabled={saving}>
                    {saving ? 'Saving…' : 'Save Changes'}
                </button>

                {error && <p className={styles.error}>Error: {error}</p>}
            </form>
        </div>
    );
}
