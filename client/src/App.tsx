import { BrowserRouter as Router, Routes, Route, Outlet, Navigate } from "react-router-dom";
import Page404 from "./components/Page404";
import Homepage from "./pages/Homepage";
import Menu from "./pages/Menu";
import BookService from "./pages/Customer/BookASeat";
import CustomerReservations from "./pages/Customer/CustomerReservations";
import SalonRegistration from "./pages/Salon/SalonRegistration";
import Salon from "./pages/Salon/Salon";
import SalonServices from "./pages/Salon/SalonServices";
import SalonReservations from "./pages/Salon/SalonReservations";
import SalonDetails from "./pages/Salon/SalonDetails";
import About from "./pages/About";
import Login from "./components/Authentication/Login";
import Register from "./components/Authentication/Register";
import Header from "./components/Header";
import Footer from "./components/Footer";

import { useUserContext } from './hooks/useUserContext';

function App() {
  const { userState } = useUserContext();

  const MenuLayout = () => {
    return (
      <div>
        <Header />
        <Outlet />
        <Footer />
      </div>
    );
  };

  const SalonLayout = () => {
    return (
      <div>
        <Outlet />
      </div>
    );
  };

  return (
      <Router>
        <Routes>
          {/*//TODO protected routes -> https://www.robinwieruch.de/react-router-private-routes/ */}

          {userState.userId === 0
          ?
          <>
            <Route path="/" element={<Homepage />} />
            <Route path="login" element={<Login />} />
            <Route path="register" element={<Register />} />
            <Route path="about" element={<About />} />
          </>
          :
          <>
            <Route path="/" element={<Navigate to="/menu" />} />
            <Route path="login" element={<Navigate to="/menu" />} />
            <Route path="register" element={<Navigate to="/menu" />} />
            <Route path="about" element={<Navigate to="/menu" />} />
          </>
          }

          {userState.userId !== 0
          ?
          <Route path="/menu" element={<MenuLayout />}>
            <Route index element={<Menu />} />
            <Route path="book-a-seat" element={<BookService />} />
            <Route path="reservation-list" element={<CustomerReservations />} />
            <Route path="become-an-affiliate" element={<SalonRegistration />} />
            {/* Below is user with role "affiliate" that is allowed to display salon details */}
            <Route path="salon" element={<SalonLayout />}>
              <Route index element={<Salon />} />
              <Route path="details" element={<SalonDetails />} />
              <Route path="reservation-list" element={<SalonReservations />} />
              <Route path="services" element={<SalonServices />} />
            </Route>
          </Route>
          :
          <Route path="/menu" element={<Navigate to="/" />}>
            <Route index element={<Navigate to="/login" />} />
            <Route path="book-a-seat" element={<Navigate to="/login" />} />
            <Route path="reservation-list" element={<Navigate to="/login" />} />
            <Route path="become-an-affiliate" element={<Navigate to="/login" />} />
            {/* Below is user with role "affiliate" that is allowed to display salon details */}
            <Route path="salon" element={<Navigate to="/login" />}>
              <Route index element={<Navigate to="/login" />} />
              <Route path="details" element={<Navigate to="/login" />} />
              <Route path="reservation-list" element={<Navigate to="/login" />} />
              <Route path="services" element={<Navigate to="/" />} />
            </Route>
          </Route>
          }
          <Route path="*" element={<Page404 />} />
        </Routes>
      </Router>
  );
}

export default App;
