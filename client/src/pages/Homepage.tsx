import { Outlet, Link } from "react-router-dom";

const Homepage = () => {
  return (
    <div>
      Homepage
      <Link to="about">About</Link>
      <Outlet />
    </div>
  )
}

export default Homepage;