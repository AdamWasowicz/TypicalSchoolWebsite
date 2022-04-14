import { IRoute } from '../types/route';

//Routes
import Home from "./Home";
import ReduxTest from './ReduxTest';


export const routes: IRoute[] = [
    {
        route: '/',
        module: Home,
        roles: ['ROLE_USER', 'ROLE_ADMIN', 'NOT_LOGGED'],
    },

    {
        route: '/reduxTest',
        module: ReduxTest,
        roles: ['ROLE_USER', 'ROLE_ADMIN', 'NOT_LOGGED'],
    },

    
];