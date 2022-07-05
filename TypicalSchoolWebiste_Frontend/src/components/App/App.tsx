
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
import Footer from '../Footer';


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
                </Routes>

                <Footer/>
            </Router>
        </div>
    );
};

export default App;