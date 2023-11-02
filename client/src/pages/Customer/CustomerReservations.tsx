import { useState, useEffect } from 'react';
import { useUserContext } from '../../hooks/useUserContext';

const CustomerReservations = () => {
  interface CustomerReservation {
    haircutDurationTime: string;
    price: number;
    reservationDay: string;
    reservationHour: string;
    reservationId: number;
    salonCity: string;
    salonName: string;
    salonServiceId: number;
    serviceName: string;
  };

  const { userState } = useUserContext();
  const [reservations, setReservations] = useState<CustomerReservation[]>([]);
  const [isFetch, setIsFetch] = useState<boolean>(true);
  const defaultData = [{}, {}, {}, {}, {}];

  const getUserReservations = async () => {
    setIsFetch(true);

    try {
      const response = await fetch('https://localhost:44315/api/reservation/reservations-list?customerId=' + userState.userId, {
        method: 'GET',
        credentials: 'include'
      });

      const json = await response.json();
      setReservations(json.value);
      setIsFetch(false);

    } catch (error) {
      console.log(error);
      setIsFetch(false);
    };
  };

  useEffect(() => {
    getUserReservations();
  }, [])

  return (
    <div>
      <h1 className="text-gray-200 text-base desktop:text-2xl text-center pb-8 mt-4">
        Reservations
      </h1>
      <div className="overflow-x-auto">
      <table className="w-full max-w-[1280px] mx-auto text-xs laptop:text-sm desktop:text-base text-left border rounded-xl border-t-0 border-gray-400">
        <thead className='text-xs laptop:text-sm desktop:text-base text-gray-300 uppercase  bg-[#1b1b1b] dark:bg-gray-700 border border-gray-400'>
          <tr className='w-full flex flex-row'>
            <th scope="col" className='px-1 py-5'></th>
            <th scope="col" className="px-2.5 tablet:px-4 laptop:px-7 py-6 flex-1">Salon Name</th>
            <th scope="col" className="px-2.5 tablet:px-4 laptop:px-7 py-6 flex-1">Service Name</th>
            <th scope="col" className="px-2.5 tablet:px-4 laptop:px-7 py-6 flex-1">Price</th>
            <th scope="col" className="px-2.5 tablet:px-4 laptop:px-7 py-6 flex-1">Duration</th>
            <th scope="col" className="px-2.5 tablet:px-4 laptop:px-7 py-6 flex-1">Reserv. Day</th>
            <th scope="col" className="px-2.5 tablet:px-4 laptop:px-7 py-6 flex-1">Reserv. Hour</th>
            <th scope="col" className="px-2.5 tablet:px-4 laptop:px-7 py-6 flex-1">Status</th>
          </tr>
        </thead>
        {
          reservations.length > 0
          ?
          <tbody className='max-h-[640px] flex flex-col overflow-y-auto'>
          {reservations.map((item, index) => (
            <tr key={index} data-salon-id={item.reservationId} className="w-full flex flex-row text-gray-300 bg-[#252525] border-b border-[#575757] dark:bg-gray-800 dark:border-gray-700 hover:bg-[#3d3d3d] hover:cursor-pointer">
              <td className="px-1 tablet:px-2 laptop:px-3 py-5 font-bold">{index + 1}</td>
              <td className="px-2.5 tablet:px-4 laptop:px-7 py-5 flex-1">{item.salonName}</td>
              <td className="px-2.5 tablet:px-4 laptop:px-7 py-5 flex-1">{item.serviceName}</td>
              <td className="px-2.5 tablet:px-4 laptop:px-7 py-5 flex-1">${item.price}</td>
              <td className="px-2.5 tablet:px-4 laptop:px-7 py-5 flex-1">{item.haircutDurationTime}</td>
              <td className="px-2.5 tablet:px-4 laptop:px-7 py-5 flex-1">{item.reservationDay}</td>
              <td className="px-2.5 tablet:px-4 laptop:px-7 py-5 flex-1">{item.reservationHour}</td>
              <td className={`px-2.5 tablet:px-4 laptop:px-7 py-5 flex-1 font-bold ${item.reservationId === 1? "text-green-500" : "text-red-500"}`}>{item.reservationId === 1 ? 'Completed' : 'Waiting'}</td>
            </tr>
          ))}
          </tbody>
          :
          <tbody className='max-h-[640px] flex flex-col overflow-y-auto'>
          {defaultData.map((item, index) => (
            <tr key={index} className="w-full flex flex-row text-gray-300 text-lg bg-[#252525] dark:bg-gray-800 dark:border-gray-700">
              <td className=" flex-1"></td>
              <td className=" flex-1"></td>
              <td className=" flex-1"></td>
              <td className="px-12 py-3 flex-2">
                {
                  index === 2
                  ? (isFetch ? <svg aria-hidden="true" className="w-8 h-8 mr-2 text-gray-100 animate-spin dark:text-gray-600 fill-blue-500 text-base" viewBox="0 0 100 101" fill="none" xmlns="http://www.w3.org/2000/svg"><path d="M100 50.5908C100 78.2051 77.6142 100.591 50 100.591C22.3858 100.591 0 78.2051 0 50.5908C0 22.9766 22.3858 0.59082 50 0.59082C77.6142 0.59082 100 22.9766 100 50.5908ZM9.08144 50.5908C9.08144 73.1895 27.4013 91.5094 50 91.5094C72.5987 91.5094 90.9186 73.1895 90.9186 50.5908C90.9186 27.9921 72.5987 9.67226 50 9.67226C27.4013 9.67226 9.08144 27.9921 9.08144 50.5908Z" fill="currentColor"/><path d="M93.9676 39.0409C96.393 38.4038 97.8624 35.9116 97.0079 33.5539C95.2932 28.8227 92.871 24.3692 89.8167 20.348C85.8452 15.1192 80.8826 10.7238 75.2124 7.41289C69.5422 4.10194 63.2754 1.94025 56.7698 1.05124C51.7666 0.367541 46.6976 0.446843 41.7345 1.27873C39.2613 1.69328 37.813 4.19778 38.4501 6.62326C39.0873 9.04874 41.5694 10.4717 44.0505 10.1071C47.8511 9.54855 51.7191 9.52689 55.5402 10.0491C60.8642 10.7766 65.9928 12.5457 70.6331 15.2552C75.2735 17.9648 79.3347 21.5619 82.5849 25.841C84.9175 28.9121 86.7997 32.2913 88.1811 35.8758C89.083 38.2158 91.5421 39.6781 93.9676 39.0409Z" fill="currentFill"/></svg> : "No reservations found")
                  : ""
                }
              </td>
              <td className="flex-1"></td>
              <td className=" flex-1"></td>
              <td className=" flex-1"></td>
            </tr>
          ))}
          </tbody>
        }
      </table>
      </div>
    </div>
  )
}

export default CustomerReservations;