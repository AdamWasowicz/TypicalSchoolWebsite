export interface IRoute {
    route: string;
    module: React.FunctionComponent;
    type?: 'add' | 'edit',
    roles: ('ROLE_USER' | 'ROLE_ADMIN' | 'NOT_LOGGED')[];
}