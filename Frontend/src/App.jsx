import { Route, Routes } from 'react-router-dom';

import Home from "./Pages/Home";
import About from "./Pages/About";
import Contact from "./Pages/Contact";
import Information from "./Pages/Information";
import NavBar from "./Components/NavBar";

import * as routes from './Routes';

import './App.css';

export default function App() {
  return (
    <>
      <NavBar />
      <div className='container'>
        <Routes>
          <Route exact path={routes.HOME_ROUTE} element={<Home />} />
          <Route exact path={routes.ABOUT_ROUTE} element={<About />} />
          <Route exact path={routes.CONTACT_ROUTE} element={<Contact />} />
          <Route exaxt path={routes.INFORMATION_ROUTE} element={<Information />}/>

        </Routes>
      </div>
    </>
  );
}
