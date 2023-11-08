
import { useState, useEffect } from "react";
import { useUserContext } from "../../hooks/useUserContext";
import { BackPageButton } from "../../components/BackPageButton";
import { PaginationTableRow } from "../../components/PaginationTableRow";
const GET_SALON_RESERVATION_LIST_URL_STRING = process.env.REACT_APP_SALON_RESERVATION_LIST_URL;

const strings = "?salonAffiliateId=2"

export const SalonReservations = () => {
  const { userState } = useUserContext();

  const [isLoading, setIsLoading] = useState<boolean>(true);
  const [error, setError] = useState<any>(null);

  
  const [reservations, setReservations] = useState<any[]>([]);
  const defaultData = [{}, {}, {}, {}, {}];

  
  const [currentPage, setCurrentPage] = useState<number>(1);
  const [salonsNumber, setSalonsNumber] = useState<number>(0);
  const pageSize = 10;
  const handlePageChange = (newPage: number) => {
    setCurrentPage(newPage);
  };


  const getSalonReservations = async () => {
    setError(null);
    setIsLoading(true);

    try {
      const response = await fetch(GET_SALON_RESERVATION_LIST_URL_STRING + `?salonAffiliateId=${userState.userId}&page=${currentPage}&pageSize=${pageSize}`, {
        method: "GET",
        headers: {"Content-Type": "application/json"},
        credentials: "include"
      });
      const json = await response.json();

      console.log(response);
      console.log(json);
      
      if (json.statusCode >= 400) {
        setError(json.value);
        console.error(json.value);
      } else {
        setReservations(json.value.reservations);
        setSalonsNumber(json.value.totalReservations);
      }

    } catch (error) {
      console.error(error);
    } finally {
      setIsLoading(false);
    }
  };

  useEffect(() => {
    getSalonReservations();
  }, [currentPage]);


  // component
  const loadingAnimation =
  <div className='flex items-center justify-center m-auto h-full'>
    <svg
        aria-hidden="true"
        className="w-8 h-8 text-gray-100 animate-spin dark:text-gray-600 fill-blue-500 "
        viewBox="0 0 100 101"
        fill="none"
        xmlns="http://www.w3.org/2000/svg">
        <path
            d="M100 50.5908C100 78.2051 77.6142 100.591 50 100.591C22.3858 100.591 0 78.2051 0 50.5908C0 22.9766 22.3858 0.59082 50 0.59082C77.6142 0.59082 100 22.9766 100 50.5908ZM9.08144 50.5908C9.08144 73.1895 27.4013 91.5094 50 91.5094C72.5987 91.5094 90.9186 73.1895 90.9186 50.5908C90.9186 27.9921 72.5987 9.67226 50 9.67226C27.4013 9.67226 9.08144 27.9921 9.08144 50.5908Z"
            fill="currentColor"/>
        <path
            d="M93.9676 39.0409C96.393 38.4038 97.8624 35.9116 97.0079 33.5539C95.2932 28.8227 92.871 24.3692 89.8167 20.348C85.8452 15.1192 80.8826 10.7238 75.2124 7.41289C69.5422 4.10194 63.2754 1.94025 56.7698 1.05124C51.7666 0.367541 46.6976 0.446843 41.7345 1.27873C39.2613 1.69328 37.813 4.19778 38.4501 6.62326C39.0873 9.04874 41.5694 10.4717 44.0505 10.1071C47.8511 9.54855 51.7191 9.52689 55.5402 10.0491C60.8642 10.7766 65.9928 12.5457 70.6331 15.2552C75.2735 17.9648 79.3347 21.5619 82.5849 25.841C84.9175 28.9121 86.7997 32.2913 88.1811 35.8758C89.083 38.2158 91.5421 39.6781 93.9676 39.0409Z"
            fill="currentFill"/>
    </svg>
  </div>



  const table =
  <div className="overflow-x-auto">
    <table className="w-full max-w-[1280px] mx-auto text-xs laptop:text-sm desktop:text-base text-left border rounded-xl border-t-0 border-gray-400">
      <thead className='text-xs laptop:text-sm desktop:text-base text-gray-300 uppercase  bg-[#1b1b1b] dark:bg-gray-700 border border-gray-400'>
        <tr className='w-full flex flex-row'>
          <th scope="col" className="px-2.5 tablet:px-4 py-5 flex-1  text-center">Reservation Id</th>
          <th scope="col" className="px-2.5 tablet:px-4 py-5 flex-1  text-center">Customer Id</th>
          <th scope="col" className="px-2.5 tablet:px-4 py-5 flex-1  text-center">Service Id</th>
          <th scope="col" className="px-2.5 tablet:px-4 py-5 flex-1  text-center">Service Name</th>
          <th scope="col" className="px-2.5 tablet:px-4 py-5 flex-1  text-center">Service Price</th>
          <th scope="col" className="px-2.5 tablet:px-4 py-5 flex-1  ">Duration</th>
          <th scope="col" className="px-2.5 tablet:px-4 py-5 flex-1  ">Reserv. Day</th>
          <th scope="col" className="px-2.5 tablet:px-4 py-5 flex-1  text-center">Reserv. Hour</th>
          <th scope="col" className="px-2.5 tablet:px-4 py-5 flex-1">Status</th>
        </tr>
      </thead>
      {
        reservations.length > 0
        ?
        <tbody className='max-h-[640px] flex flex-col overflow-y-auto'>
        {reservations.map((item, index) => (
          <tr key={index} data-salon-id={item.reservationId} className="w-full flex flex-row text-gray-300 bg-[#252525] border-b border-[#575757] dark:bg-gray-800 dark:border-gray-700 hover:bg-[#3d3d3d] hover:cursor-pointer">
            <td className="px-2.5 tablet:px-4 py-3 flex-1  text-center">{item.reservationId}</td>
            <td className="px-2.5 tablet:px-4 py-3 flex-1  text-center">{item.userId}</td>
            <td className="px-2.5 tablet:px-4 py-3 flex-1  text-center">{item.salonServiceId}</td>
            <td className="px-2.5 tablet:px-4 py-3 flex-1">{item.serviceName}</td>
            <td className="px-2.5 tablet:px-4 py-3 flex-1  text-center">${item.price}</td>
            <td className="px-2.5 tablet:px-4 py-3 flex-1">{Number(item.haircutDurationTime) / 60} minutes</td>
            <td className="px-2.5 tablet:px-4 py-3 flex-1">{item.reservationDay}</td>
            <td className="px-2.5 tablet:px-4 py-3 flex-1  text-center">{item.reservationHour}</td>
            <td className={`px-2.5 tablet:px-4 py-3 flex-1 font-semibold ${item.isReservationCompleted ? "text-green-500" : "text-red-500"}`}>{item.isReservationCompleted ? 'Completed' : 'Pending'}</td>
          </tr>
        ))}
        {reservations.length > 0 ? <PaginationTableRow currentPage={currentPage} handlePageChange={handlePageChange} pageSize={pageSize} totalCount={salonsNumber} /> : null}
        </tbody>
        :
        <tbody className='max-h-[640px] flex flex-col overflow-y-auto'>
        {defaultData.map((item, index) => (
          <tr key={index} className="w-full flex flex-row text-gray-300 text-lg bg-[#252525] dark:bg-gray-800 dark:border-gray-700">
            <td className=" flex-1"></td>
            <td className="px-12 py-3 flex-2">
              {
                index === 2
                ? (isLoading ? <svg aria-hidden="true" className="w-8 h-8 mr-2 text-gray-100 animate-spin dark:text-gray-600 fill-blue-500 text-base" viewBox="0 0 100 101" fill="none" xmlns="http://www.w3.org/2000/svg"><path d="M100 50.5908C100 78.2051 77.6142 100.591 50 100.591C22.3858 100.591 0 78.2051 0 50.5908C0 22.9766 22.3858 0.59082 50 0.59082C77.6142 0.59082 100 22.9766 100 50.5908ZM9.08144 50.5908C9.08144 73.1895 27.4013 91.5094 50 91.5094C72.5987 91.5094 90.9186 73.1895 90.9186 50.5908C90.9186 27.9921 72.5987 9.67226 50 9.67226C27.4013 9.67226 9.08144 27.9921 9.08144 50.5908Z" fill="currentColor"/><path d="M93.9676 39.0409C96.393 38.4038 97.8624 35.9116 97.0079 33.5539C95.2932 28.8227 92.871 24.3692 89.8167 20.348C85.8452 15.1192 80.8826 10.7238 75.2124 7.41289C69.5422 4.10194 63.2754 1.94025 56.7698 1.05124C51.7666 0.367541 46.6976 0.446843 41.7345 1.27873C39.2613 1.69328 37.813 4.19778 38.4501 6.62326C39.0873 9.04874 41.5694 10.4717 44.0505 10.1071C47.8511 9.54855 51.7191 9.52689 55.5402 10.0491C60.8642 10.7766 65.9928 12.5457 70.6331 15.2552C75.2735 17.9648 79.3347 21.5619 82.5849 25.841C84.9175 28.9121 86.7997 32.2913 88.1811 35.8758C89.083 38.2158 91.5421 39.6781 93.9676 39.0409Z" fill="currentFill"/></svg> : "No reservations found")
                : (error === null ? null : <p className="text-center text-red-500 font-semibold">Request error. Please retry.</p>)
              }
            </td>
            <td className="flex-1"></td>
          </tr>
        ))}
        </tbody>
      }
    </table>
  </div>

  return (
    <div className='w-full min-h-[calc(100vh-100px-50px)] text-gray-200'>
      <h1 className='text-gray-200 text-2xl tablet:text-2xl text-center pb-12 mt-4'>Salon Reservations</h1>
      {
        table
      }
    </div>
  );
};