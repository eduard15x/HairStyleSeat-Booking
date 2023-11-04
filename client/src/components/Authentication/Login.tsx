import { useState } from 'react';
import { Link } from 'react-router-dom';
import { useLogin } from '../../hooks/useLogin';
const LOGIN_URL_STRING = process.env.REACT_APP_LOGIN_URL;

export const Login: React.FC = () => {
  const { login, isLoading, error} = useLogin();
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');

  const submitLoginForm = async (e: React.FormEvent<HTMLFormElement> ) => {
    e.preventDefault();

    console.log(LOGIN_URL_STRING)
    login({ email: email, password: password }, LOGIN_URL_STRING!);
  };

  return (
    <div className='min-h-[calc(100vh-100px)]'>
      <h1 className="text-gray-200 text-2xl tablet:text-4xl text-center pb-10 mt-6">
        Login
      </h1>
      <form
        onSubmit={submitLoginForm}
        action="#"
        method="POST"
        className="flex flex-col mx-auto py-10 tablet:w-[480px] tablet:border-2 tablet:border-gray-600"
      >
        {/* User Email */}
        <label htmlFor="Email" className="mb-16 tablet:mb-12 flex">
          <input
            onChange={e => setEmail(e.target.value)}
            required
            type="email"
            name="Email"
            id="Email"
            placeholder="Email"
            autoComplete="email"
            className="w-10/12 tablet:w-8/12 mx-auto pb-2.5 text-md tablet:text-xl placeholder:text-center text-gray-300 bg-transparent outline-none border-b-2 border-b-gray-600  focus-visible:border-b-gray-300"
          />
        </label>
        {/* Password */}
        <label htmlFor="Password" className="mb-16 tablet:mb-12 flex">
          <input
            onChange={e => setPassword(e.target.value)}
            required
            type="password"
            name="Password"
            id="Password"
            placeholder="Password"
            autoComplete="current-password"
            className="w-10/12 tablet:w-8/12 mx-auto pb-2.5 text-md tablet:text-xl placeholder:text-center text-gray-300 bg-transparent outline-none border-b-2 border-b-gray-600  focus-visible:border-b-gray-300"
          />
        </label>
        {/* Error handling */}
        {error && <p className='text-red-500 text-center  text-xl -mt-4 pb-6'>{error}</p>}
        {/* Submit button */}
        <button
          disabled={isLoading}
          type="submit"
          className={`
            w-5/12 tablet:w-4/12 mx-auto py-2 text-gray-200 border-2 border-gray-300
            ${ isLoading ? "text-gray-500 border-gray-500" : "transition duration-300 ease-in-out hover:scale-105 hover:text-gray-200 hover:border-gray-200 active:scale-110 active:text-gray-50 active:border-gray-50"}
          `}
        >
          Login
        </button>
        <div className='flex flex-col mx-auto mt-8 text-gray-400 text-md tablet:text-xl text-center'>
          <p>Don't have an account?</p>
          <Link to="/register" className='w-8/12 mx-auto hover:text-gray-200'>Register now</Link>
        </div>
      </form>
    </div>
  );
};