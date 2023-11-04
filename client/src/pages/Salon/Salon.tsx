import { Outlet, Link } from "react-router-dom";

export const Salon = () => {
  return (
    <div>
      <Link to="details">Details</Link>
      <Link to="reservation-list">Reservations</Link>
      <Link to="services">Services</Link>
      <Outlet />
    </div>
  );
};