import { useState } from 'react';
import { useUserContext } from '../../hooks/useUserContext';
import { useSalonRegistrationHandling } from '../../hooks/useSalonRegistrationHandling';
import { ISalonRegistration } from '../../shared/interfaces';
import { hasZeroOrEmptyStringProperties } from '../../utils/utils';

const REGISTER_NEW_SALON_URL_STRING = process.env.REACT_APP_REGISTER_NEW_SALON_URL;
const WEEK_DAYS = ["monday", "tuesday", "wednesday", "thursday", "friday", "saturday", "sunday"];
const WORK_HOURS = ["06:00", "07:00", "08:00", "09:00", "10:00", "11:00", "12:00", "13:00", "14:00", "15:00", "16:00", "17:00", "18:00", "19:00", "20:00", "21:00", "22:00"];

export const SalonRegistration = () => {
  const { userState } = useUserContext();
  const { registerSalon, isSendingRequest, successMessage, errorNewReservation } = useSalonRegistrationHandling();

  const [acceptCondition, setAcceptCondition] = useState<boolean>(false);
  const [selectedWeekDays, setSelectedWeekDays] = useState<string[]>([]);
  const [salonRegistrationDetails, setSalonRegistrationDetails] = useState<ISalonRegistration>({
    salonName: "",
    salonCity: "",
    salonAddress: "",
    userId: userState.userId,
    workDays: "",
    startTimeHour: "",
    endTimeHour: "",
  });

  const handleCondition = () => setAcceptCondition(!acceptCondition);

  const handleSubmitForm = (e: React.FormEvent<HTMLFormElement>)=> {
    e.preventDefault();
    registerSalon(salonRegistrationDetails, REGISTER_NEW_SALON_URL_STRING!)
  };

  const updateSalonRegisterDetails = (e: React.ChangeEvent<HTMLInputElement> | React.ChangeEvent<HTMLSelectElement>, key: string) => {
    setSalonRegistrationDetails({...salonRegistrationDetails, [key]: e.target.value});
  };

  const handleSelectedWeekDay = (day: string) => {
    setSelectedWeekDays((prevSelectedWeekDays) => {
      const updatedSelectedWeekDays = prevSelectedWeekDays.includes(day)
        ? prevSelectedWeekDays.filter((d) => d !== day)
        : [...prevSelectedWeekDays, day];

      setSalonRegistrationDetails({
        ...salonRegistrationDetails,
        workDays: updatedSelectedWeekDays.join(",").trim(),
      });

      return updatedSelectedWeekDays;
    });
  };

  return (
    <div className='text-gray-100 min-h-[calc(100vh-100px-50px)]'>
      <h1 className='text-center text-2xl '>Salon Registration</h1>
      <p className='text-center text-sm italic'>~ Become an affiliate ~</p>
      <ul className='w-3/5 m-auto mt-8'>
        <li className='mb-1.5'>- Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.</li>
        <li className='mb-1.5'>- Interdum velit euismod in pellentesque massa. Quis risus sed vulputate odio ut. Fermentum et sollicitudin ac orci. Ultricies integer quis auctor elit.</li>
        <li className='mb-1.5'>- Sed turpis tincidunt id aliquet risus feugiat in ante metus. Egestas fringilla phasellus faucibus scelerisque eleifend donec pretium.</li>
        <li className='mb-1.5'>
        - Ultrices neque ornare aenean euismod elementum nisi. Diam volutpat commodo sed egestas egestas fringilla phasellus faucibus.
          Vestibulum mattis ullamcorper velit sed ullamcorper morbi tincidunt.
        </li>

        <li className='mb-1.5'>
        - Viverra adipiscing at in tellus integer. Mauris ultrices eros in cursus. Eleifend quam adipiscing vitae proin sagittis.
          Gravida dictum fusce ut placerat orci nulla.
        </li>
        <li className='mb-1.5'>- Nulla facilisi etiam dignissim diam quis enim lobortis scelerisque fermentum. Fermentum odio eu feugiat pretium nibh. Aliquet bibendum enim facilisis gravida.</li>
        <li className='mb-1.5'>- Vel turpis nunc eget lorem dolor sed viverra. Lacinia at quis risus sed. Lectus vestibulum mattis ullamcorper velit sed ullamcorper morbi tincidunt.</li>
        <li className='mb-1.5'>
        - Neque laoreet suspendisse interdum consectetur. At auctor urna nunc id cursus metus aliquam eleifend mi. Semper auctor neque vitae tempus.
          Proin libero nunc consequat interdum varius sit amet mattis vulputate.
        </li>
      </ul>

        <form onSubmit={handleSubmitForm} className='flex flex-col mx-auto my-8 py-5 tablet:w-[480px] tablet:border-2 tablet:border-gray-600'>
          <h2 className='text-center text-lg'>Registration Form</h2>

          <div className='flex justify-center mt-3 mb-5'>
            <input required type='checkbox' id='accept-conditions' name='accept-conditions' className='mr-2 accent-red-600 hover:cursor-pointer' defaultChecked={acceptCondition} onChange={handleCondition} />
            <label htmlFor='accept-conditions'>To continue you must accept terms and conditions.</label>
          </div>

          <label htmlFor="SalonName" className="mb-8 flex">
            <input
              disabled={!acceptCondition}
              onChange={(e) => updateSalonRegisterDetails(e, "salonName")}
              required
              type="text"
              name="SalonName"
              id="SalonName"
              placeholder="SalonName"
              className="w-10/12 tablet:w-8/12 mx-auto pb-2 text-md tablet:text-base placeholder:text-center text-gray-300 bg-transparent outline-none border-b-2 border-b-gray-600  focus-visible:border-b-gray-300"/>
          </label>
          <label htmlFor="SalonCity" className="mb-8 flex">
            <input
              disabled={!acceptCondition}
              onChange={(e) => updateSalonRegisterDetails(e, "salonCity")}
              required
              type="text"
              name="SalonCity"
              id="SalonCity"
              placeholder="SalonCity"
              className="w-10/12 tablet:w-8/12 mx-auto pb-2 text-md tablet:text-base placeholder:text-center text-gray-300 bg-transparent outline-none border-b-2 border-b-gray-600  focus-visible:border-b-gray-300"/>
          </label>
          <label htmlFor="SalonAddress" className="mb-6 flex">
            <input
              disabled={!acceptCondition}
              onChange={(e) => updateSalonRegisterDetails(e, "salonAddress")}
              required
              type="text"
              name="SalonAddress"
              id="SalonAddress"
              placeholder="SalonAddress"
              className="w-10/12 tablet:w-8/12 mx-auto pb-2 text-md tablet:text-base placeholder:text-center text-gray-300 bg-transparent outline-none border-b-2 border-b-gray-600  focus-visible:border-b-gray-300"/>
          </label>


          {
            acceptCondition
            ?
            <>
              <p className='text-center'>Select work days</p>
              <ul className='flex flex-wrap my-3'>
                {
                  WEEK_DAYS.map((day) => (
                    <li key={day} className='w-1/3 pl-6'>
                      <input type='checkbox' id={day} name={day} onChange={() => handleSelectedWeekDay(day)} />
                      <label htmlFor={day} className='capitalize'>{day}</label>
                    </li>
                  ))
                }
              </ul>
            </>
            : null
          }

          {
            acceptCondition
            ?
            <>
              <p className='text-center'>Select work hours</p>
              <div className='flex justify-around my-3'>
                <select required className='text-black py-0.5 px-2' onChange={(e) => updateSalonRegisterDetails(e, "startTimeHour")}>
                  <option value="">Start hour</option>
                  {
                    WORK_HOURS.map((hour) => (
                      <option key={hour} value={hour}>{hour}</option>
                    ))
                  }
                </select>
                <select required className='text-black py-1 px-2' onChange={(e) => updateSalonRegisterDetails(e, "endTimeHour")}>
                  <option value="">End hour</option>
                  {
                    WORK_HOURS.map((hour) => (
                      <option key={hour} value={hour}>{hour}</option>
                    ))
                  }
                </select>
              </div>
            </>
            : null
          }

          <button disabled={!acceptCondition || isSendingRequest || hasZeroOrEmptyStringProperties(salonRegistrationDetails)} type='submit'
            className={`
              w-4/12 tablet:w-3/12 mx-auto my-3 py-1 text-gray-200 border-2 border-gray-300
              ${ !acceptCondition || isSendingRequest || hasZeroOrEmptyStringProperties(salonRegistrationDetails) ? "text-gray-500 border-gray-500" : "transition duration-300 ease-in-out hover:scale-105 hover:text-gray-200 hover:border-gray-200 active:scale-110 active:text-gray-50 active:border-gray-50"}
            `}
          >
            Submit
          </button>

          {errorNewReservation !== null ? <p className='text-center text-red-600 font-bold'>{errorNewReservation}</p> : null}
          {successMessage !== null ? <p className='text-center text-green-600 font-bold'>{successMessage}</p> : null}
        </form>
    </div>
  );
};