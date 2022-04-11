import Home from "./Home";
import { IRoute } from '../types/route';


export const routes: IRoute[] = [
    {
        route: '/',
        module: Home,
        roles: ['ROLE_USER', 'ROLE_ADMIN', 'NOT_LOGGED'],
    },

    
];