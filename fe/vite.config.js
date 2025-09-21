import { defineConfig } from "vite";
import react from "@vitejs/plugin-react";

// https://vite.dev/config/
export default defineConfig({
    plugins: [react()],
    server: {
        proxy: {
            // Any "/api" request in your React code will be forwarded:
            '/api': {
                target: 'http://localhost:5105',
                changeOrigin: true,
                secure: false,       // if your backend is HTTP
                rewrite: path => path.replace(/^\/api/, '/api')
            }
        }
    }
});
