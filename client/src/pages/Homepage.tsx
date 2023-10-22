import { Outlet, Link } from "react-router-dom";

const Homepage = () => {
  return (
    <div>
      <>
        <Link to="login">Login</Link>
        <Link to="register">Register</Link>
      </>
      <Link to="about">About</Link>
      <Outlet />
    </div>
  )
}

export default Homepage;