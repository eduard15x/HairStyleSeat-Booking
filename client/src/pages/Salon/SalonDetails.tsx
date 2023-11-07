import { useState, useEffect } from 'react';
import { useUserContext } from '../../hooks/useUserContext';
import { ISalonData, ISalonUpdateData, IUserUpdateDate } from '../../shared/interfaces';
import { MdClose } from 'react-icons/md';
import { hasZeroOrEmptyStringProperties } from '../../utils/utils';
const GET_SALON_DETAILS_FOR_USER_URL_STRING = process.env.REACT_APP_GET_SALON_DETAILS_FOR_USER_URL;
const UPDATE_USER_DATA_URL_STRING = process.env.REACT_APP_UPDATE_USER_DATA_URL;
const UPDATE_SALON_DATA_URL_STRING = process.env.REACT_APP_UPDATE_SALON_DATA_URL;
const WEEK_DAYS = ["monday", "tuesday", "wednesday", "thursday", "friday", "saturday", "sunday"];
const WORK_HOURS = ["06:00", "07:00", "08:00", "09:00", "10:00", "11:00", "12:00", "13:00", "14:00", "15:00", "16:00", "17:00", "18:00", "19:00", "20:00", "21:00", "22:00"];

export const SalonDetails = () => {
  const { userState } = useUserContext();
  const [isLoading, setIsLoading] = useState<boolean>(true);
  const [isFetching, setIsFetching] = useState<boolean>(false);
  const [error, setError] = useState<any>(null);
  const [errorUpdate, setErrorUpdate] = useState<any>(null);
  const [salonData, setSalonData] = useState<ISalonData | null>(null);
  const [updateSalonData, setUpdateSalonData] = useState<ISalonUpdateData | null>(null);
  const [updateUserData, setUpdateUserData] = useState<IUserUpdateDate | null>(null);
  const [selectedWeekDays, setSelectedWeekDays] = useState<string[]>([]);
  const [showUpdateSalonModal, setShowUpdateSalonModal] = useState<boolean>(false);
  const [showUpdateUserData, setShowUpdateUserData] = useState<boolean>(false);

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

      if (json.statusCode >= 400) {
        setSalonData(null);
        setError("Could not fetch.");
      } else {
        setSalonData(json.value);
      }
      setIsLoading(false);

    } catch(error) {
      console.error("Error: " + error);
      setIsLoading(false);
      setError(error);
      setSalonData(null);
    }
  };

  const showUpdateForm = (e: React.MouseEvent<HTMLButtonElement, MouseEvent>) => {
    if (e.currentTarget.dataset.update === "salonDetails") {
      setShowUpdateSalonModal(true);
      setUpdateSalonData({
        id: salonData!.salonId,
        salonName: salonData!.salonName,
        salonCity: salonData!.salonCity,
        salonAddress: salonData!.salonAddress,
        userId: userState.userId,
        workDays: salonData!.workDays,
        startTimeHour: salonData!.startTimeHour,
        endTimeHour: salonData!.endTimeHour,
      });
      setSelectedWeekDays(salonData!.workDays.split(','));

    } else if (e.currentTarget.dataset.update === "userDetails") {
      setShowUpdateUserData(true);
      setUpdateUserData({
        id: userState.userId,
        userName: salonData!.userDetails.userName,
        city: salonData!.salonCity,
        phoneNumber: salonData!.userDetails.phoneNumber,
      });
    }
  };

  const hideUpdateForm = (e : React.MouseEvent<HTMLButtonElement, MouseEvent>) => {
    e.preventDefault();
    setShowUpdateSalonModal(false);
    setShowUpdateUserData(false);
  }

  const updateFormFields = (e: React.ChangeEvent<HTMLInputElement> | React.ChangeEvent<HTMLSelectElement>, key: string) => {
    if (showUpdateSalonModal) {
      setUpdateSalonData({...updateSalonData!, [key]: e.target.value});
    } else if (showUpdateUserData) {
      setUpdateUserData({...updateUserData!, [key]: e.target.value});
    }
  };

  const handleSelectedWeekDay = (day: string) => {
    setSelectedWeekDays((prevSelectedWeekDays) => {
      const updatedSelectedWeekDays = prevSelectedWeekDays.includes(day)
        ? prevSelectedWeekDays.filter((d) => d !== day)
        : [...prevSelectedWeekDays, day];

        setUpdateSalonData({
        ...updateSalonData!,
        workDays: updatedSelectedWeekDays.join(",").trim(),
      });

      return updatedSelectedWeekDays;
    });
  };

  const handleUpdateSubmit = (e: React.FormEvent<HTMLFormElement>) => {
    e.preventDefault();

    if (e.currentTarget.dataset.updateName === "salonData") {
      if (hasZeroOrEmptyStringProperties(updateSalonData))
        return;
      updateData(UPDATE_SALON_DATA_URL_STRING!, updateSalonData!, "salonData");
    } else if (e.currentTarget.dataset.updateName === "userData") {
      if (hasZeroOrEmptyStringProperties(updateUserData))
        return;
      updateData(UPDATE_USER_DATA_URL_STRING!, updateUserData!, "userData");
    }
  };

  //UTILY
  const updateData = async (urlEndpoint: string, obj: ISalonUpdateData | IUserUpdateDate, objToUpdate: string) => {
    setIsFetching(true);
    try {
      const response = await fetch(urlEndpoint, {
        method: "PUT",
        headers: {"Content-Type": "application/json"},
        credentials: "include",
        body: JSON.stringify(obj)
      });
      const json = await response.json();

      if (response.status >= 400) {
        setErrorUpdate(json.value);
      } else {
        if (objToUpdate === "salonData") {
          setSalonData(json.value);
          setShowUpdateSalonModal(false);
        } else {
          setSalonData({ ...salonData!, userDetails: { ...json.value, email: userState.userEmail }});
          setShowUpdateUserData(false);
        }
      }

    } catch(error) {
      console.error('Error updating data:', error);
      setErrorUpdate('Error updating data');
    } finally {
      setIsFetching(false);
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
        <p className='capitalize w-3/5'>{salonData?.workDays.replace(/,/g, ', ')}</p>
      </div>
      <div className='flex flex-row mb-1'>
        <p className='w-2/5 font-semibold'>Salon Workhours</p>
        <p className='w-3/5'>{salonData?.startTimeHour} - {salonData?.endTimeHour}</p>
      </div>
      <div className='flex flex-row'>
        <p className='w-2/5 font-semibold'>Salon Reviews</p>
        <p className='w-3/5'>{salonData?.salonReviews}</p>
      </div>
      <button onClick={showUpdateForm} data-update="salonDetails" className='mt-5 py-1.5 border border-gray-400 hover:brightness-75 active:scale-95 w-1/2'>Update Salon Details</button>
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

      <button onClick={showUpdateForm} data-update="userDetails" className='mt-5 py-1.5 border border-gray-400 hover:brightness-75 active:scale-95 w-1/2'>Update Contact Details</button>
    </div>

    {showUpdateSalonModal
    ?
    <div className="fixed top-0 left-0 right-0 z-50 p-4 w-full bg-[rgba(0,0,0,0.8)] overflow-x-hidden overflow-y-auto md:inset-0 h-[calc(100%-1rem)] max-h-full">
      <form className="w-full max-w-[550px] bg-gray-200 py-2 px-4 top-1/2 left-1/2 -translate-x-1/2 -translate-y-1/2 relative border rounded-lg" onSubmit={handleUpdateSubmit} data-update-name={"salonData"}>
        <button className={`absolute top-2 right-2 ${true ? "" : "hover:cursor-pointer"}`} onClick={hideUpdateForm} >
            <MdClose className='text-black hover:text-gray-700 text-3xl active:scale-90' />
        </button>
        <label htmlFor="salonName" className="block text-base font-medium text-gray-800 mt-4">Salon Name</label>
        <input defaultValue={salonData?.salonName} onChange={(e) => updateFormFields(e, "salonName")} id="salonName" name="salonName" type="text" autoComplete="salonName" className="px-2 my-3 block w-full rounded-md border-0 py-1.5 text-gray-900 shadow-sm ring-1 ring-inset ring-gray-300 placeholder:text-gray-400 focus:ring-2 focus:ring-inset focus:ring-indigo-600 sm:text-sm sm:leading-6" />

        <label htmlFor="salonCity" className="block text-base font-medium text-gray-800">Salon City</label>
        <input defaultValue={salonData?.salonCity} onChange={(e) => updateFormFields(e, "salonCity")}  id="salonCity" name="salonCity" type="text" autoComplete="salonCity" className="px-2 my-3 block w-full rounded-md border-0 py-1.5 text-gray-900 shadow-sm ring-1 ring-inset ring-gray-300 placeholder:text-gray-400 focus:ring-2 focus:ring-inset focus:ring-indigo-600 sm:text-sm sm:leading-6" />

        <label htmlFor="salonAddress" className="block text-base font-medium text-gray-800">Salon Address</label>
        <input defaultValue={salonData?.salonAddress} onChange={(e) => updateFormFields(e, "salonAddress")}  id="salonAddress" name="salonAddress" type="text" autoComplete="salonAddress" className="px-2 my-3 block w-full rounded-md border-0 py-1.5 text-gray-900 shadow-sm ring-1 ring-inset ring-gray-300 placeholder:text-gray-400 focus:ring-2 focus:ring-inset focus:ring-indigo-600 sm:text-sm sm:leading-6" />

        <label htmlFor="salonWorkdays" className=" block text-base font-medium text-gray-800">Salon Workdays</label>
        <ul className='flex flex-wrap my-3'>
          {
            WEEK_DAYS.map((day) => (
              <li key={day} className='w-1/3 pl-6'>
                <input defaultChecked={salonData?.workDays.includes(day)} type='checkbox' id={day} name={day} onChange={() => handleSelectedWeekDay(day)} />
                <label htmlFor={day} className='capitalize text-gray-800 ml-2'>{day}</label>
              </li>
            ))
          }
        </ul>

        <label htmlFor="salonWorkHours" className="block text-base font-medium text-gray-800">Salon Workhours</label>
        <div className='flex justify-start my-3'>
          <span className='text-black mx-2'>From</span>
          <select required className='text-black py-0.5 px-2' onChange={(e) => updateFormFields(e, "startTimeHour")} >
            <option defaultValue={salonData?.startTimeHour}>{salonData?.startTimeHour}</option>
            {
              WORK_HOURS.map((hour) => (
                <option key={hour} value={hour}>{hour}</option>
              ))
            }
          </select>
          <span className='text-black mx-2'>to</span>
          <select required className='text-black py-1 px-2' onChange={(e) => updateFormFields(e, "endTimeHour")} >
            <option defaultValue={salonData?.endTimeHour}>{salonData?.endTimeHour}</option>
            {
              WORK_HOURS.map((hour) => (
                <option key={hour} value={hour}>{hour}</option>
              ))
            }
          </select>
        </div>

        <button
          disabled={hasZeroOrEmptyStringProperties(updateSalonData) || isFetching}
          title={hasZeroOrEmptyStringProperties(updateSalonData) ? "Please complete all fields" : ""}
          type='submit'
          className={`px-10 py-1 mt-8 mb-4 bg-gray-50 border  rounded-md flex mx-auto  ${hasZeroOrEmptyStringProperties(updateSalonData) ? "text-gray-700 brightness-90 border-gray-400" : "text-gray-900 border-black hover:bg-gray-400 hover:border-transparent hover:text-white active:scale-95"}`}
        >
          Save
        </button>
        {errorUpdate !== null ? <p className='font-semibold text-red-600 text-center'>{errorUpdate}</p> : null}
      </form>
    </div>
    : null}

    {showUpdateUserData
    ?
    <div className="fixed top-0 left-0 right-0 z-50 p-4 w-full bg-[rgba(0,0,0,0.8)] overflow-x-hidden overflow-y-auto md:inset-0 h-[calc(100%-1rem)] max-h-full">
      <form className="w-full max-w-[550px] bg-gray-200 py-2 px-4 top-1/2 left-1/2 -translate-x-1/2 -translate-y-1/2 relative border rounded-lg" onSubmit={handleUpdateSubmit} data-update-name={"userData"} >
        <button  className={`absolute top-2 right-2 ${true ? "" : "hover:cursor-pointer"}`} onClick={hideUpdateForm} >
            <MdClose className='text-black hover:text-gray-700 text-3xl active:scale-90' />
        </button>
        <label htmlFor="userName" className="block text-base font-medium text-gray-800 mt-4">Owner Name</label>
        <input defaultValue={salonData?.userDetails.userName} onChange={(e) => updateFormFields(e, "userName")} id="userName" name="userName" type="text" autoComplete="userName" className="px-2 my-3 block w-full rounded-md border-0 py-1.5 text-gray-900 shadow-sm ring-1 ring-inset ring-gray-300 placeholder:text-gray-400 focus:ring-2 focus:ring-inset focus:ring-indigo-600 sm:text-sm sm:leading-6" />

        <label htmlFor="phoneNumber" className="block text-base font-medium text-gray-800">Phone Number</label>
        <input defaultValue={salonData?.userDetails.phoneNumber} onChange={(e) => updateFormFields(e, "phoneNumber")}  id="phoneNumber" name="phoneNumber" type="text" autoComplete="phoneNumber" className="px-2 my-3 block w-full rounded-md border-0 py-1.5 text-gray-900 shadow-sm ring-1 ring-inset ring-gray-300 placeholder:text-gray-400 focus:ring-2 focus:ring-inset focus:ring-indigo-600 sm:text-sm sm:leading-6" />

        <button
          disabled={hasZeroOrEmptyStringProperties(updateUserData) || isFetching}
          title={hasZeroOrEmptyStringProperties(updateUserData) ? "Please complete all fields" : ""}
          type='submit'
          className={`px-10 py-1 mt-8 mb-4 bg-gray-50 border  rounded-md flex mx-auto  ${hasZeroOrEmptyStringProperties(updateUserData) ? "text-gray-700 brightness-90 border-gray-400" : "text-gray-900 border-black hover:bg-gray-400 hover:border-transparent hover:text-white active:scale-95"}`}
        >
          Save
        </button>
        {errorUpdate !== null ? <p className='font-semibold text-red-600 text-center'>{errorUpdate}</p> : null}
      </form>
    </div>
    : null}
  </div>

  return (
    <div className='w-full min-h-[calc(100vh-100px-50px)] text-gray-200'>
      <h1 className='text-gray-200 text-2xl tablet:text-2xl text-center pb-12 mt-4'>Salon Details</h1>

      {
        isLoading
        ?
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
        : renderEl
      }
    </div>
  );
};