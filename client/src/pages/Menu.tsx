import { useEffect, useState } from "react";
import { Link } from "react-router-dom";
const GET_SALON_DETAILS_FOR_USER_URL_STRING = process.env.REACT_APP_GET_SALON_DETAILS_FOR_USER_URL;

export const Menu = () => {
  const [salonStatus, setSalonStatus] = useState<number>(0);

  const getSalonStatus = async () => {
    try {

      const response = await fetch(GET_SALON_DETAILS_FOR_USER_URL_STRING!, {
        method: "GET",
        headers: {"Content-Type": "application/json"},
        credentials: 'include',
      });
      const json = await response.json();
      console.log(json)

      if (json.statusCode >= 400) {
        setSalonStatus(0);
      } else {
        setSalonStatus(json.value.salonStatus);
      }

    } catch (error) {
      console.log(error);
      setSalonStatus(0);
    }
  };

  const userAdditionalInfo = salonStatus === 0 || salonStatus === 2 || salonStatus === 3
  ? {
    name: 'Become an affiliate',
    path: 'become-an-affiliate'
  } : {
    name: 'Your salon',
    path: 'salon'
  };

  useEffect(() => {
    getSalonStatus();
  }, [salonStatus]);

  return (
    <div className="w-full min-h-[calc(100vh-100px-50px)] text-gray-400 flex justify-center flex-col items-center text-2xl tablet:text-3xl laptop:text-5xl">
      <Link to="book-a-seat" className="hover:text-gray-200 mb-10 laptop:mb-14">Book a seat</Link>
      <Link to="reservation-list" className="hover:text-gray-200">Your reservations</Link>
      <Link to={userAdditionalInfo.path} className="hover:text-gray-200 mt-10 laptop:mt-14">{userAdditionalInfo.name}</Link>
    </div>
  );
};