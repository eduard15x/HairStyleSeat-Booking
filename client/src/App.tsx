import { BrowserRouter as Router, Routes, Route } from "react-router-dom";
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

function App() {
  return (
    <Router>
      <Routes>
        <Route path="/" element={<Homepage />}>
          <Route path="about" element={<About />} />
        </Route>
        <Route path="/menu" element={<Menu />}>
          <Route path="book-a-seat" element={<BookService />} />
          <Route path="reservation-list" element={<CustomerReservations />} />
          <Route path="become-an-affiliate" element={<SalonRegistration />} />
          {/* Below is user with role "affiliate" that is allowed to display salon details */}
          <Route path="salon" element={<Salon />}>
            <Route path="details" element={<SalonDetails />} />
            <Route path="reservation-list" element={<SalonReservations />} />
            <Route path="services" element={<SalonServices />} />
          </Route>
        </Route>
        <Route path="*" element={<Page404 />} />
      </Routes>
    </Router>
  );
}

export default App;
