import NavBar from "./Components/NavBar";

import Home from "./Pages/Home";
import About from "./Pages/About";
import Contact from "./Pages/Contact";
import Login from "./Pages/Login";
import Register from "./Pages/Register";

import * as routes from "./routes";
import { Route, Routes } from "react-router-dom";

import "./App.css";

export default function App() {
  return (
    <>
      <NavBar />
      <div className="container">
        <Routes>
          <Route exact path={routes.HOME_ROUTE} element={<Home />} />
          <Route exact path={routes.ABOUT_ROUTE} element={<About />} />
          <Route exact path={routes.CONTACT_ROUTE} element={<Contact />} />
          <Route exact path={routes.LOGIN_ROUTE} element={<Login />} />
          <Route exact path={routes.REGISTER_ROUTE} element={<Register />} />
        </Routes>
      </div>
    </>
  );
}
