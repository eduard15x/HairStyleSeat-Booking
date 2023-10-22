import { Outlet, Link } from "react-router-dom";

const Menu = () => {
  return (
    <div className="">
      <Link to="book-a-seat">Book a seat</Link>
      <Link to="reservation-list">Your reservations</Link>
      <Link to="become-an-affiliate">Become an affiliate</Link>
      <Outlet />
    </div>
  )
}

export default Menu;