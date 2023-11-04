import { useState, useEffect } from 'react';
import { useUserContext } from '../../hooks/useUserContext';
import { PaginationTableRow } from '../../components/PaginationTableRow';

import { FaLongArrowAltDown, FaLongArrowAltUp } from 'react-icons/fa';
import { AiOutlineSearch } from 'react-icons/ai';
import { ReservationModal } from '../../components/ReservationModal';

const SALON_LIST_URL_STRING = process.env.REACT_APP_SALON_LIST_URL;

// TODO - refactoring
const BookASeat = () => {

  interface Salons {
    id: number;
    salonCity: string;
    salonName: string;
    salonAddress: string;
    salonReviews: string;
  };

  const { userState } = useUserContext();
  const [salons, setSalons] = useState<Salons[]>([]);
  const [salonsNumber, setSalonsNumber] = useState<number>(0);
  const [isFetch, setIsFetch] = useState<boolean>(true);
  const defaultData = [{}, {}, {}, {}, {}];

  const [currentPage, setCurrentPage] = useState<number>(1);
  const [searchString, setSearchString] = useState<string>("");
  const [searchStringBtn, setSearchStringBtn] = useState<string>(searchString);
  const [selectedCities, setSelectedCities] = useState<string[]>([]);
  const [filterModalShow, setFilterModalShow] = useState<boolean>(false);

  //reservation modal
  const [showReservationModal, setShowReservationModal] = useState<boolean>(false);


  const pageSize = 8;
  // Render checkboxes for each city
  const cityOptions = ["Bucuresti", "Sibiu", "Brasov", "Iasi", "Cluj", "Craiova", "Timisoara", "Constanta", "Arad", "Chisinau", "Alba-Iulia", "Valcea", "Olt", "Buzau"];

  const handlePageChange = (newPage: number) => {
    setCurrentPage(newPage);
  };

  const handleSearchChange = (searchString: string) => {
    setSearchStringBtn(searchString);
    setCurrentPage(1); // Reset to the first page when searching
  };

  const handleFilterChange = (city: string) => {
    // Check if the city is already selected
    if (selectedCities.includes(city)) {
      setSelectedCities(selectedCities.filter((c) => c !== city)); // Deselect the city
    } else {
      setSelectedCities([...selectedCities, city]); // Select the city
    }
  };

  const handleFilterModal = () => {
    if (filterModalShow === true) {
      setFilterModalShow(false);
      getSalons();
    } else {
      setFilterModalShow(true);
    }
  };


  // reservation modal
  const [salonId, setSalonId] = useState<number>(0);
  const handleReservationModal = () => setShowReservationModal(showReservationModal ? false : true);


  const getSalons = async () => {
    setIsFetch(true);

    try {
      const response = await fetch(`
      ${SALON_LIST_URL_STRING}?page=${currentPage}&pageSize=${pageSize}&search=${encodeURIComponent(searchString.trim())}&selectedCities=${selectedCities.join(',')}
      `, {
        method: 'GET',
      });

      const json = await response.json();
      console.log(json);
      setSalons(json.value.salons);
      setSalonsNumber(json.value.totalSalons);
      setIsFetch(false);

    } catch (error) {
      console.log(error);
      setIsFetch(false);
    }
  };

  useEffect(() => {
    getSalons();
  }, [currentPage, searchStringBtn])

  console.log(salonId)

  return (
    <div className='min-h-[calc(100vh-100px-50px)]'>
      <h1 className="text-gray-200 text-2xl tablet:text-2xl text-center pb-10 mt-6">
        Salon List
      </h1>
      <div className="overflow-x-auto">
        <table className="w-full max-w-[1280px] mx-auto text-xs laptop:text-sm desktop:text-base text-left border rounded-xl border-t-0 border-gray-400">
          {/* Table option - Search / Sort / Filters */}
          <thead className='text-xs laptop:text-sm desktop:text-base text-gray-300 uppercase bg-gray-800 border border-gray-400'>
              <tr  className="flex">
                {/* <td className="px-1 tablet:px-2 laptop:px-3 py-5 font-bold">{index + 1}</td> */}
                <th className="pl-3 flex flex-1 items-center justify-center border-r border-gray-600">
                  <AiOutlineSearch onClick={() => handleSearchChange(searchString)} className='text-2xl relative z-50 hover:cursor-pointer hover:text-gray-400' />
                  <input
                    type="text"
                    className="bg-transparent font-normal w-full h-full bg-clip-padding pl-2.5 py-1.5 text-neutral-100 placeholder:hover:text-neutral-300 placeholder:focus:text-neutral-300 focus:outline-none"
                    placeholder="Search salon's name"
                    onChange={(e) => setSearchString(e.target.value)}
                    onKeyDown={(e) => {
                      if (e.key === 'Enter') {
                        // Handle Enter key press here
                        handleSearchChange(searchString); // Call your search function or API request
                      }
                    }}
                  />
                </th>
                <th className="pr-3 py-1.5 flex-1 text-end relative border-l border-gray-600">
                  <p className='flex items-center justify-center font-normal'>
                    Select city
                  {
                    !filterModalShow
                    ? <FaLongArrowAltDown className='text-lg ml-2 hover:cursor-pointer hover:text-gray-400' onClick={handleFilterModal}/>
                    : <FaLongArrowAltUp className='text-lg ml-2 hover:cursor-pointer hover:text-gray-400' onClick={handleFilterModal}/>
                  }
                  </p>
                  {filterModalShow ?
                    <div className='flex flex-col mt-2 left-1/2 -translate-x-1/2 items-start p-2 bg-gray-500 w-1/2 absolute max-h-[200px] overflow-y-auto'>
                      {cityOptions.map((item, index) => (
                        <label key={index} htmlFor={`option${index}`} className='mb-1.5 text-gray-200'>
                          <input
                            type="checkbox"
                            id={`option${index}`}
                            name={`option${index}`}
                            value={item}
                            className='mr-1'
                            defaultChecked={selectedCities.includes(item)}
                            onClick={() => handleFilterChange(item)} />
                          {item}
                        </label>
                      ))}
                    </div>
                    : ""
                  }
                </th>
              </tr>
          </thead>

          <thead className='text-xs laptop:text-sm text-gray-300 uppercase bg-[#1b1b1b] border border-gray-400'>
            <tr className='w-full flex flex-row'>
              {/* <th scope="col" className='px-1 py-5'></th> */}
              <th scope="col" className="px-2.5 tablet:px-4 laptop:px-7 py-2 flex-1 text-center">Salon Name</th>
              <th scope="col" className="px-2.5 tablet:px-4 laptop:px-7 py-2 flex-1 text-center">City</th>
              <th scope="col" className="px-2.5 tablet:px-4 laptop:px-7 py-2 flex-1 text-center">Address</th>
              <th scope="col" className="px-2.5 tablet:px-4 laptop:px-7 py-2 flex-1 text-center">Reviews</th>
            </tr>
          </thead>

          {
            salons.length > 0
            ?
            <tbody className='max-h-[640px] flex flex-col overflow-y-auto'>
              {salons.map((item, index) => (
                <tr
                  key={index}
                  data-salon-id={item.id}
                  className="w-full flex flex-row text-gray-300 bg-[#252525] border-b border-[#575757] hover:bg-[#3d3d3d] hover:cursor-pointer"
                  onClick={() => {
                    handleReservationModal();
                    setSalonId(item.id);
                  }}
                >
                  {/* <td className="px-1 tablet:px-2 laptop:px-3 py-5 font-bold">{index + 1}</td> */}
                  <td className="px-2.5 tablet:px-4 laptop:px-7 py-5 flex-1 text-center">{item.salonName}</td>
                  <td className="px-2.5 tablet:px-4 laptop:px-7 py-5 flex-1 text-center">{item.salonCity}</td>
                  <td className="px-2.5 tablet:px-4 laptop:px-7 py-5 flex-1 text-center">{item.salonAddress}</td>
                  <td className="px-2.5 tablet:px-4 laptop:px-7 py-5 flex-1 text-center">{item.salonReviews}</td>
                </tr>
              ))}
                <PaginationTableRow currentPage={currentPage} handlePageChange={handlePageChange} pageSize={pageSize} totalCount={salonsNumber} />
            </tbody>
            :
            <tbody className='max-h-[640px] flex flex-col overflow-y-auto'>
            {defaultData.map((item, index) => (
              <tr key={index} className="w-full flex flex-row text-gray-300 text-lg bg-[#252525]">
                <td className=" flex-1"></td>
                <td className="px-12 py-3 flex-2">
                  {
                    index === 2
                    ? (isFetch ? <svg aria-hidden="true" className="w-8 h-8 mr-2 text-gray-100 animate-spin fill-blue-500 text-base" viewBox="0 0 100 101" fill="none" xmlns="http://www.w3.org/2000/svg"><path d="M100 50.5908C100 78.2051 77.6142 100.591 50 100.591C22.3858 100.591 0 78.2051 0 50.5908C0 22.9766 22.3858 0.59082 50 0.59082C77.6142 0.59082 100 22.9766 100 50.5908ZM9.08144 50.5908C9.08144 73.1895 27.4013 91.5094 50 91.5094C72.5987 91.5094 90.9186 73.1895 90.9186 50.5908C90.9186 27.9921 72.5987 9.67226 50 9.67226C27.4013 9.67226 9.08144 27.9921 9.08144 50.5908Z" fill="currentColor"/><path d="M93.9676 39.0409C96.393 38.4038 97.8624 35.9116 97.0079 33.5539C95.2932 28.8227 92.871 24.3692 89.8167 20.348C85.8452 15.1192 80.8826 10.7238 75.2124 7.41289C69.5422 4.10194 63.2754 1.94025 56.7698 1.05124C51.7666 0.367541 46.6976 0.446843 41.7345 1.27873C39.2613 1.69328 37.813 4.19778 38.4501 6.62326C39.0873 9.04874 41.5694 10.4717 44.0505 10.1071C47.8511 9.54855 51.7191 9.52689 55.5402 10.0491C60.8642 10.7766 65.9928 12.5457 70.6331 15.2552C75.2735 17.9648 79.3347 21.5619 82.5849 25.841C84.9175 28.9121 86.7997 32.2913 88.1811 35.8758C89.083 38.2158 91.5421 39.6781 93.9676 39.0409Z" fill="currentFill"/></svg> : "No salons found")
                    : ""
                  }
                </td>
                <td className=" flex-1"></td>
              </tr>
            ))}
            </tbody>
          }
        </table>
      </div>
      <ReservationModal showReservationModal={showReservationModal} handleReservationModal={handleReservationModal} salonId={salonId} />
    </div>
  )
}

export default BookASeat;