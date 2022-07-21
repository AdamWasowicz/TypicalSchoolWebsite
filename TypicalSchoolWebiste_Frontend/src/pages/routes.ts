import { IRoute } from "../assets/Interfaces/IRoute";

//Routes
import Home from "./Home";
import Error404 from './Error404';
import ReduxTest from './ReduxTest';
import NewsPage from "./NewsPage";


export const routes: IRoute[] = [
    {
        route: '/',
        module: Home,
        subRoutes: []
    },

    {
        route: 'reduxTest',
        module: ReduxTest,
        subRoutes: []
    },

    {
        route: 'news/:newsName',
        module: NewsPage,
        subRoutes: []
    },

    {
        route: '*',
        module: Error404,
        subRoutes: []
    },

    {
        route: '404',
        module: Error404,
        subRoutes: []
    },
];