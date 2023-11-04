import { useState, useEffect } from 'react';
import Calendar from 'react-calendar';
import 'react-calendar/dist/Calendar.css';
import { MdClose } from 'react-icons/md';
import { useUserContext } from '../hooks/useUserContext';
import { useCustomerReservationHandling } from '../hooks/useCustomerReservationHandling';
import { hasZeroOrEmptyStringProperties, getFormaterDateUS } from '../utils/utils';
import { TimePicker } from './TimePicker';
import { IReservationModal, ISalonData, ISalonService, IReservationDetails } from '../shared/interfaces';
import { CalendarDate } from '../shared/types';
const SALON_DETAILS_URL_STRING = process.env.REACT_APP_SALON_DETAILS_URL;
const SALON_SERVICES_LIST_URL_STRING = process.env.REACT_APP_SALON_SERVICES_LIST_URL;
const CUSTOMER_MAKE_RESERVATION_URL = process.env.REACT_APP_CUSTOMER_MAKE_RESERVATION_URL;

export const ReservationModal = ({showReservationModal, handleReservationModal, salonId}: IReservationModal) => {
    const { userState } = useUserContext();
    const { createReservation, errorNewReservation, isCreatingReservation, successMessage } = useCustomerReservationHandling();
    // time picker
    const startTime = new Date(2023, 0, 1, 5, 0); // Start time: 09:00
    const endTime = new Date(2023, 0, 1, 23, 0); // End time: 17:00
    const interval = 60; // Interval in minutes

    const [reservationDetails, setReservationDetails] = useState<IReservationDetails>({
        userId: 0,
        salonId: 0,
        salonServiceId: 0,
        reservationDay: "",
        reservationHour: "",
    });

    const [selectedService, setSelectedService] = useState<string>("");
    const [salonData, setSalonData] = useState<ISalonData | null>(null);
    const [salonServicesData, setSalonServicesData] = useState<ISalonService[] | []>([]);
    const [isFetch, setIsFetch] = useState<boolean>(false);
    const [error, setError] = useState<any | null>(null);

    //calendar
    const [calendarDate, setCalendarDate] = useState<CalendarDate>(new Date());

    const handleTimeSelect = (selectedTime: any) => {
        // Handle the selected time
        console.log(`Selected time: ${selectedTime}`);
        setReservationDetails({...reservationDetails, reservationHour: selectedTime})
    };


    const getSalonDetails = async () => {
        setIsFetch(true);
        setError(null);

        try {
            const urls = [
                `${SALON_DETAILS_URL_STRING}${salonId}`,
                `${SALON_SERVICES_LIST_URL_STRING}${salonId}/services`
            ];

            setReservationDetails({...reservationDetails, userId: userState.userId, salonId: salonId});
            const responses = await Promise.all(urls.map((url) => fetch(url)));
            const data = await Promise.all(responses.map((response) => response.json()));

            setSalonData(data[0].value);
            setSalonServicesData(data[1].value);
            setIsFetch(false);
        } catch (error) {
            setIsFetch(false);
            setError(error);
        }
    };

    useEffect(() => {
        if (salonId !== 0) {
            getSalonDetails();
        }
    }, [salonId]);

  return (
    showReservationModal === true
    ? <div className="fixed top-0 left-0 right-0 z-50 p-4 w-full bg-[rgba(0,0,0,0.6)] overflow-x-hidden overflow-y-auto md:inset-0 h-[calc(100%-1rem)] max-h-full">
        <div className="w-full max-w-[550px] h-full max-h-[650px] bg-gray-200 py-2 px-4 top-1/2 left-1/2 -translate-x-1/2 -translate-y-1/2 relative border rounded-lg">
            <button disabled={isFetch} className={`absolute top-2.5 right-2.5 ${isFetch ? "" : "hover:cursor-pointer"}`} onClick={() => handleReservationModal()}>
                <MdClose className='text-black text-3xl active:scale-75' />
            </button>


            <h2 className='text-center text-xl font-semibold mb-4'>New Reservation</h2>
            {
                isFetch
                ? <div className='flex items-center justify-center m-auto h-full'>
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
                // : <p className='text-red-600 text-lg font-bold'>Error loading salon details.</p>
                : ( error === null && salonData !== null
                    ?
                    <>
                        <div className='flex'>
                            <div className='flex-1'>
                                <p className='mb-1'><span className='font-medium'>Salon Name:</span> {salonData.salonName}</p>
                                <p className='mb-1'><span className='font-medium'>City:</span> {salonData.salonCity}</p>
                                <p className='mb-1'><span className='font-medium'>Address:</span> {salonData.salonAddress}</p>
                                <p className='mb-1 capitalize'><span className='font-medium'>Work Days:</span> {salonData.workDays !== "" ? (salonData.workDays).split(',').map(dayString => dayString.slice(0,3)).join(', ') : "-" }</p>
                                <p className='mb-1'><span className='font-medium'>Work hours:</span> {salonData.startTimeHour} - {salonData.endTimeHour}</p>
                                <p><span className='font-medium'>Salon Reviews:</span> {salonData.salonReviews}</p>
                            </div>
                            <div className='flex-1 relative'>
                                <p className='mb-1'><span className='font-medium'>Hairstylist:</span> {salonData.userDetails.userName}</p>
                                <p className='mb-1'><span className='font-medium'>Phone Nr:</span> {salonData.userDetails.phoneNumber}</p>
                                <p className='mb-1'><span className='font-medium'>Email:</span> {salonData.userDetails.email}</p>


                                <select
                                    disabled={salonServicesData.length === 0}
                                    value={selectedService}
                                    onChange={(e) => {
                                        setReservationDetails({...reservationDetails, salonServiceId: Number(e.currentTarget.value)});
                                        setSelectedService(e.currentTarget.value);
                                    }} //e.currentTarget.value
                                    className={`absolute bottom-2 py-1 px-2 w-11/12 font-semibold text-xs
                                    ${salonServicesData.length === 0 ? "" : "hover:bg-gray-50 hover:outline-slate-700 hover:outline hover:outline-1"}`}
                                >
                                    <option value="">{salonServicesData.length === 0 ? "No services available for salon." : "Service type/Price/Duration"}</option>
                                    {salonServicesData.length > 0
                                        ? salonServicesData.map((item, index) => (
                                            <option key={index} value={item.serviceId}>
                                                {item.serviceName} - ${item.price} - {item.haircutDurationTime}
                                            </option>
                                        ))
                                        : null
                                    }
                                </select>
                            </div>
                        </div>
                        <div className='mt-6 max-h-[75%]'>
                            <div className='flex mb-2'>
                                <h3 className='flex-1 font-bold text-center'>Select date</h3>
                                <h3 className='flex-1 font-bold text-center'>Select hour</h3>
                            </div>
                            <div className='flex'>
                                <div className='flex-1 w-1/2'>
                                    <Calendar
                                        onChange={(e) => {
                                            setCalendarDate(e);
                                            setReservationDetails({...reservationDetails, reservationDay: getFormaterDateUS(calendarDate!.toString())});
                                        }}
                                        value={calendarDate} />
                                </div>
                                <div className='flex-1 w-1/2 pl-8'>
                                    <TimePicker startTime={startTime} endTime={endTime} interval={interval} onTimeSelect={handleTimeSelect} />
                                </div>
                            </div>
                        </div>

                        <button
                            disabled={hasZeroOrEmptyStringProperties(reservationDetails) || isCreatingReservation}
                            onClick={() => createReservation(reservationDetails, CUSTOMER_MAKE_RESERVATION_URL!)}
                            className={`bg-[#006edc] text-white flex m-auto py-2 px-4 mt-6
                            ${hasZeroOrEmptyStringProperties(reservationDetails) || isCreatingReservation ? "bg-[#006edc56]" : "active:scale-95" }`}
                        >
                            Create Reservation
                        </button>
                        {errorNewReservation !== null ? <p className='text-center text-red-600 font-bold'>{errorNewReservation}</p> : null}
                        {successMessage !== null ? <p className='text-center text-green-600 font-bold'>{successMessage}</p> : null}
                    </>
                    : <p>{error}</p>
                )
            }
        </div>
    </div>
    : null
  );
};