import React from 'react';
import { BrowserRouter as Router, Switch, Route } from 'react-router-dom';

import { IRoute } from '../../types/route';
import { routes } from '../../pages/routes';


const App: React.FunctionComponent = () => {
    return(
        <Router>
            <Switch>
            {
                routes.map((route: IRoute, i:number) => (
                    <Route
                        key={i}
                        path={route.route}
                    >
                        {route.module}
                    </Route>
                ))
            }
            </Switch>
        </Router>
    );
};

export default App;