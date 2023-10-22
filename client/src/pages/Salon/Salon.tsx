import { Outlet, Link } from "react-router-dom";

const Salon = () => {
  return (
    <div>
      <Link to="details">Details</Link>
      <Link to="reservation-list">Reservations</Link>
      <Link to="services">Services</Link>
      <Outlet />
    </div>
  )
}

export default Salon;