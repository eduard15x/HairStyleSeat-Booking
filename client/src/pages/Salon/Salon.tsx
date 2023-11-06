import { Link } from "react-router-dom";

export const Salon = () => {
  return (
    <div className="w-full min-h-[calc(100vh-100px-50px)] text-gray-400 flex justify-center flex-col items-center text-2xl tablet:text-3xl laptop:text-5xl">
      <Link to="details" className="hover:text-gray-200 mb-10 laptop:mb-14">Details</Link>
      <Link to="reservation-list" className="hover:text-gray-200">Reservations</Link>
      <Link to="services" className="hover:text-gray-200 mt-10 laptop:mt-14">Services</Link>
    </div>
  );
};