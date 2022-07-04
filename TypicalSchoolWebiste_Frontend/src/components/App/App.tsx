
import React from 'react';
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';

//Style
import './style.scss';

//Components
import Error404 from '../../pages/Error404';
import NavBar from '../NavBar/NavBar';

//Interfaces
import { IRoute } from '../../types/route';

//Other
import { routes } from '../../pages/routes';
import navBarDestinations from '../../constants/NavBarDestinations';


const App: React.FunctionComponent = () => {
    return (
        <div className='App'>
            <Router>
                <NavBar elements={navBarDestinations}/>
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
                    <Route key="route-error-404" path="/*" element={<Error404/>} />
                    <Route key="route-error-404" path="404" element={<Error404/>} />
                </Routes>
            </Router>
        </div>
    );
};

export default App;