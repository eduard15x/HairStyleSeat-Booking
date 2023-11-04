import { Link } from "react-router-dom";

export const Menu = () => {

  const user = "X";

  return (
    <div className="w-full absolute top-2/4 left-2/4 -translate-x-2/4 -translate-y-2/4 text-gray-400 flex flex-col items-center text-2xl tablet:text-3xl laptop:text-5xl">
      <Link to="book-a-seat" className="hover:text-gray-200 mb-10 laptop:mb-14">Book a seat</Link>
      <Link to="reservation-list" className="hover:text-gray-200">Your reservations</Link>
      {
        user !== "X"
        ? <Link to="become-an-affiliate" className="hover:text-gray-200 mt-10 laptop:mt-14">Become an affiliate</Link>
        : <Link to="salon" className="hover:text-gray-200 mt-10 laptop:mt-14">Your salon</Link>
      }
    </div>
  );
};