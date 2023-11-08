import { BsArrowLeft } from 'react-icons/bs';
import { useLocation, useNavigate  } from 'react-router-dom';

export const BackPageButton = () => {
    const location = useLocation();
    const navigate = useNavigate();
    
    const pathName = location.pathname;
    const handleGoBack = () => {
        navigate(-1); // This function navigates back to the previous page.
    };

    return (
        pathName !== "/menu"
        ?
        <button className='text-xl border border-gray-300 rounded-[50%] p-1 hover:border-white hover:text-white active:scale-90' title='Navigate back.' onClick={handleGoBack}>
            <BsArrowLeft className='text-gray-300 hover:text-white' />
        </button>
        : <span></span>
    );
};