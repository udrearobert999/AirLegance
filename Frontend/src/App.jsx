import { Route, Routes } from 'react-router-dom';

import Home from './Pages/Home';
import Profile from './Pages/Profile';
import About from './Pages/About';
import Contact from './Pages/Contact';
import Information from './Pages/Information';

import NavBar from './Components/NavBar';

import * as routes from './Routes';

import './App.css';
import PersistLogin from './Components/PersistLogin';
import RequireAuth from './Components/RequireAuth';

export default function App() {
  return (
    <>
      <NavBar />
      <div className='container'>
        <Routes>
          <Route element={<PersistLogin />}>
            <Route exact path={routes.HOME_ROUTE} element={<Home />} />
            <Route exact path={routes.ABOUT_ROUTE} element={<About />} />
            <Route exact path={routes.CONTACT_ROUTE} element={<Contact />} />
            <Route
              exaxt
              path={routes.INFORMATION_ROUTE}
              element={<Information />}
            />

            <Route element={<RequireAuth />}>
              <Route exact path={routes.PROFILE_ROUTE} element={<Profile />} />
            </Route>
          </Route>
        </Routes>
      </div>
    </>
  );
}
