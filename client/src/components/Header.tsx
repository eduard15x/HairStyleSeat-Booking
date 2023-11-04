import { Link } from 'react-router-dom';
import { TbLogout2 } from 'react-icons/tb';
import { useLogout } from '../hooks/useLogout';

export const Header: React.FC = () => {
  const { logout, isLoading, userState } = useLogout();
  console.log(userState);

  return (
    <header className="text-gray-400 pr-5 h-[50px] laptop:pr-6 flex justify-end items-center">
        <Link to="my-account" className='text-md laptop:text-lg mr-4 hover:text-gray-200' title='Your account'>{userState.userEmail}</Link>
        <button className={`text-lg laptop:text-xl ${isLoading ? 'disabled text-gray-500' : 'hover:text-gray-200'}`} title='Logout' onClick={() => logout()}><TbLogout2 /></button>
    </header>
  );
};