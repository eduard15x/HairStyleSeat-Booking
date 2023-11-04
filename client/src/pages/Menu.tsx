import { useState } from "react";
import { Link } from "react-router-dom";
const GET_SALON_STATUS_URL_STRING = process.env.REACT_APP_GET_SALON_STATUS_URL;

export const Menu = () => {
  const [salonStatus, setSalonStatus] = useState<number>(0);

  const getSalonStatus = async () => {
    try {

      const response = await fetch(GET_SALON_STATUS_URL_STRING!, {
        method: "GET",
        headers: {"Content-Type": "application/json"},
        credentials: 'include',
      });
      const json = await response.json();

      if (json.statusCode >= 400) {
        setSalonStatus(0);
      } else {
        setSalonStatus(json.value);
      }

    } catch (error) {
      console.log(error);
      setSalonStatus(0);
    }
  };

  getSalonStatus();
  const userAdditionalInfo = salonStatus === 0 || salonStatus === 2 || salonStatus === 3
  ? {
    name: 'Become an affiliate',
    path: 'become-an-affiliate'
  } : {
    name: 'Your salon',
    path: 'salon'
  };

  return (
    <div className="w-full absolute top-2/4 left-2/4 -translate-x-2/4 -translate-y-2/4 text-gray-400 flex flex-col items-center text-2xl tablet:text-3xl laptop:text-5xl">
      <Link to="book-a-seat" className="hover:text-gray-200 mb-10 laptop:mb-14">Book a seat</Link>
      <Link to="reservation-list" className="hover:text-gray-200">Your reservations</Link>
      <Link to={userAdditionalInfo.path} className="hover:text-gray-200 mt-10 laptop:mt-14">{userAdditionalInfo.name}</Link>
    </div>
  );
};