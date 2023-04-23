import * as React from "react";
import AppBar from "@mui/material/AppBar";
import Box from "@mui/material/Box";
import Toolbar from "@mui/material/Toolbar";
import Button from "@mui/material/Button";
import Style from "./NavBar.module.css";
import { Link } from "react-router-dom";
import * as routes from "../../routes.js";

const navItems = ["Home", "About", "Contact"];

const getRoute = (item) => {
  if (item === "Home") return "/";

  return "/" + item;
};

export default function NavBar() {
  return (
    <Box sx={{ flexGrow: 1 }}>
      <AppBar position="static">
        <Toolbar sx={{ justifyContent: "space-between" }}>
          <Box sx={{ display: "flex", alignItems: "center" }}>
            {navItems.map((item) => (
              <Link key={item} className={Style.Link} to={getRoute(item)}>
                <Box sx={{ display: { xs: "none", sm: "block" } }}>
                  <Button sx={{ color: "#fff" }}>{item}</Button>
                </Box>
              </Link>
            ))}
          </Box>
          <Link className={Style.Link} to={routes.LOGIN_ROUTE}>
            <Button sx={{ color: "#fff" }}>Login</Button>
          </Link>
        </Toolbar>
      </AppBar>
    </Box>
  );
}
