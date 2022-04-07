
import React from 'react';
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';

import './style.scss';

import { IRoute } from '../../types/route';
import { routes } from '../../pages/routes';


const App: React.FunctionComponent = () => {
    return (
        <div className='App'>
            <Router>
                <Routes>
                    {
                        routes.map((route: IRoute, i: number) => (
                            <Route
                                key={i}
                                path={route.route}
                                element={<route.module/>}
                            />
                        ))
                    }
                    <Route key="route-error-404" path="/*" element={<div/>} />
                    <Route key="route-error-404" path="404" element={<div/>} />
                </Routes>
            </Router>
        </div>
    );
};

export default App;