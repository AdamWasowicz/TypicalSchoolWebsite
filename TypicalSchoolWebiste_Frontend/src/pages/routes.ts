import { IRoute } from '../types/route';

//Routes
import Home from "./Home";
import Error404 from './Error404';

import ReduxTest from './ReduxTest';


export const routes: IRoute[] = [
    {
        route: '/',
        module: Home
    },

    {
        route: '/reduxTest',
        module: ReduxTest
    },

    {
        route: '/*',
        module: Error404
    },

    {
        route: '404',
        module: Error404
    }
];