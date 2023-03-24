import { CssBaseline } from "@mui/material";
import { ThemeProvider } from "@mui/material";
import { Route, BrowserRouter as Router, Routes } from "react-router-dom";
import SignInSide from "./pages/Signin";
import Style from "./styleConstants";
import NotFound from "./pages/NotFound";
import Home from "./pages/Home";

export default function App() {
  return (
    <ThemeProvider theme={Style.theme}>
      <CssBaseline />
      <Router>
        <Routes>
          <Route path="/" element={<Home />} />
          <Route path="/signin" element={<SignInSide />} />
          <Route path="*" exact={true} element={<NotFound />} />
        </Routes>
      </Router>
    </ThemeProvider>
  );
}
