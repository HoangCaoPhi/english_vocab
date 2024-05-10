import { createBrowserRouter, Navigate } from "react-router-dom";
import SignUp from "../views/SignUp";
import SignIn from "../views/SignIn";
import Home from "../views/Home";
import NotFoundComponent from "../views/NotFoundComponent";

const router = createBrowserRouter([
    {
        path: "/home",
        element: <Home></Home>,
        children: [

        ]
    },
    {
        path: "/sign-up",
        element: <SignUp></SignUp>
    },
    {
        path: "/sign-in",
        element: <SignIn></SignIn>
    },
    {
        path: "/",
        element: <Navigate to="/home" />
    },
    {
        path: "*",
        element: <NotFoundComponent></NotFoundComponent>
    }
]);


export default router