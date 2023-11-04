import { Link } from "react-router-dom";

export const Homepage = () => {
  return (
    <div className="min-h-[calc(100vh-100px)]  text-gray-400 flex flex-col items-center justify-center text-2xl tablet:text-3xl laptop:text-5xl">
      <Link to="login" className="hover:text-gray-200 mb-10 laptop:mb-14">Login</Link>
      <Link to="register" className="hover:text-gray-200">Register</Link>
      <Link to="about" className="hover:text-gray-200 mt-10 laptop:mt-14">About</Link>
    </div>
  );
};