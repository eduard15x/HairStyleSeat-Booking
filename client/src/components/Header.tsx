import { Link } from 'react-router-dom';
import { TbLogout2 } from 'react-icons/tb';

const Header = () => {
  return (
    <header className="text-gray-400 pt-3 pr-5 laptop:pt-4 laptop:pr-6 flex justify-end items-center">
        <Link to="my-account" className='text-md laptop:text-lg mr-4 hover:text-gray-200' title='Your account'>youremail@gmail.com</Link>
        <button className='text-lg laptop:text-xl hover:text-gray-200' title='Logout'><TbLogout2 /></button>
    </header>
  )
}

export default Header