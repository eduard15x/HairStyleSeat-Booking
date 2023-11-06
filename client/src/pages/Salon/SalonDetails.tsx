import { useState, useEffect } from 'react';
import { ISalonData } from '../../shared/interfaces';
const GET_SALON_DETAILS_FOR_USER_URL_STRING = process.env.REACT_APP_GET_SALON_DETAILS_FOR_USER_URL;

export const SalonDetails = () => {
  const [isLoading, setIsLoading] = useState<boolean>(true);
  const [error, setError] = useState<any>(null);
  const [salonData, setSalonData] = useState<ISalonData | null>(null);

  const getSalonData = async () => {
    setIsLoading(true);
    setError(null);

    try {
      const response = await fetch(GET_SALON_DETAILS_FOR_USER_URL_STRING!, {
        method: "GET",
        headers: {"Content-Type": "application/json"},
        credentials: 'include',
      });

      const json = await response.json();
      console.log(json);

      if (json.statusCode >= 400) {
        setSalonData(null);
        setError("Could not fetch.");
      } else {
        setSalonData(json.value);
      }
      setIsLoading(false);

    } catch(error) {
      setIsLoading(false);
      setError(error);
      setSalonData(null);
    }
  };

  useEffect(() => {
    getSalonData();
  }, []);


  const renderEl = error !== null
  ? <p>{error}x</p>
  :
  <div className='flex mx-auto px-2 text-lg w-full max-w-[1080px]'>
    <div className='flex flex-col w-1/2 mx-5 px-2'>
      <h2 className='text-center mb-4 pb-1.5 font-bold border-b border-gray-500'>Salon Details</h2>
      <div className='flex flex-row mb-1'>
        <p className='w-2/5 font-semibold'>Salon Name</p>
        <p className='w-3/5'>{salonData?.salonName}</p>
      </div>
      <div className='flex flex-row mb-1'>
        <p className='w-2/5 font-semibold'>Salon City</p>
        <p className='w-3/5'>{salonData?.salonCity}</p>
      </div>
      <div className='flex flex-row mb-1'>
        <p className='w-2/5 font-semibold'>Salon Address</p>
        <p className='w-3/5'>{salonData?.salonAddress}</p>
      </div>
      <div className='flex flex-row mb-1'>
        <p className='w-2/5 font-semibold'>Salon Workdays</p>
        <p className='capitalize w-3/5'>{salonData?.workDays.replace(',', ', ')}</p>
      </div>
      <div className='flex flex-row mb-1'>
        <p className='w-2/5 font-semibold'>Salon Workhours</p>
        <p className='w-3/5'>{salonData?.startTimeHour} - {salonData?.endTimeHour}</p>
      </div>
      <div className='flex flex-row'>
        <p className='w-2/5 font-semibold'>Salon Reviews</p>
        <p className='w-3/5'>{salonData?.salonReviews}</p>
      </div>
      <button className='mt-5 py-1.5 border border-gray-400 hover:brightness-75 active:scale-95 w-1/2'>Update Salon Details</button>
    </div>

    <div className='flex flex-col w-1/2 mx-5 px-2'>
      <h2 className='text-center mb-4 pb-1.5 font-bold border-b border-gray-500'>Owner Details</h2>
      <div className='flex flex-row mb-1'>
        <p className='w-2/5 font-semibold'>Owner Name</p>
        <p className='w-3/5'>{salonData?.userDetails.userName}</p>
      </div>
      <div className='flex flex-row mb-1'>
        <p className='w-2/5 font-semibold'>Contact Phone</p>
        <p className='w-3/5'>{salonData?.userDetails.phoneNumber}</p>
      </div>
      <div className='flex flex-row'>
        <p className='w-2/5 font-semibold'>Contact Mail</p>
        <p className='w-3/5'>{salonData?.userDetails.email}</p>
      </div>

      <button className='mt-5 py-1.5 border border-gray-400 hover:brightness-75 active:scale-95 w-1/2'>Update Contact Details</button>
    </div>
  </div>

  return (
    <div className='w-full min-h-[calc(100vh-100px-50px)] text-gray-200'>
      <h1 className='text-gray-200 text-2xl tablet:text-2xl text-center pb-12 mt-4'>Salon Details</h1>

      {
        isLoading
        ? <p>Is Loading...</p>
        : renderEl
      }
    </div>
  );
};