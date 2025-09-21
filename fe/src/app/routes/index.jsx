import App from "../../App";
import AuthLayout from "../layouts/AuthLayout";
import DefaultLayout from "../layouts/DefaultLayout";
import { authRoutes } from "./authRoutes";
import Laptops from "../../features/auth/pages/laptops/index.jsx";
import LaptopDetail from "../../features/auth/pages/laptops/detailLaptop.jsx";
import CreateLaptop from "../../features/auth/pages/laptops/createLaptop.jsx";
import EditLaptop from "../../features/auth/pages/laptops/editLaptop.jsx";

export const routes = [
    { element: <AuthLayout />, children: authRoutes },
    { element: <DefaultLayout />, children: [
        { path: "/", element: <App /> },
        { path: "/laptops", element: <Laptops/> },
        { path: "/laptops/:laptopId", element: <LaptopDetail/> },
        { path: "/laptops/create", element: <CreateLaptop/> },
        { path: "/laptops/:laptopId/edit", element: <EditLaptop />},
        ]
    },
];
