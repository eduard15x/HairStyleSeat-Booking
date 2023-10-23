import { Link } from 'react-router-dom';

const Register = () => {
  return (
    <>
      <h1 className="text-gray-200 text-2xl tablet:text-4xl text-center pb-10 mt-6">
        Create account
      </h1>
      <form action="#" method="POST" className="flex flex-col mx-auto py-10 tablet:w-[480px] tablet:border-2 tablet:border-gray-600">
        <label htmlFor="Username" className="mb-12 flex">
          <input
            required
            type="text"
            name="Username"
            id="Username"
            placeholder="Full Name"
            className="w-10/12 tablet:w-8/12 mx-auto pb-2.5 text-md tablet:text-xl placeholder:text-center text-gray-300 bg-transparent outline-none border-b-2 border-b-gray-600  focus-visible:border-b-gray-300"/>
        </label>
        <label htmlFor="Email" className="mb-12 flex">
          <input
            required
            type="email"
            name="Email"
            id="Email"
            placeholder="Email"
            className="w-10/12 tablet:w-8/12 mx-auto pb-2.5 text-md tablet:text-xl placeholder:text-center text-gray-300 bg-transparent outline-none border-b-2 border-b-gray-600  focus-visible:border-b-gray-300"/>
        </label>
        <label htmlFor="Password" className="mb-12 flex">
          <input
            required
            type="password"
            name="Password"
            id="Password"
            placeholder="Password"
            className="w-10/12 tablet:w-8/12 mx-auto pb-2.5 text-md tablet:text-xl placeholder:text-center text-gray-300 bg-transparent outline-none border-b-2 border-b-gray-600  focus-visible:border-b-gray-300"/>
        </label>
        <label htmlFor="ConfirmPassword" className="mb-12 flex">
          <input
            required
            type="password"
            name="ConfirmPassword"
            id="ConfirmPassword"
            placeholder="Confirm Password"
            className="w-10/12 tablet:w-8/12 mx-auto pb-2.5 text-md tablet:text-xl placeholder:text-center text-gray-300 bg-transparent outline-none border-b-2 border-b-gray-600  focus-visible:border-b-gray-300"/>
        </label>
        <label htmlFor="City" className="mb-12 flex">
          <input
            required
            type="text"
            name="City"
            id="City"
            placeholder="City"
            className="w-10/12 tablet:w-8/12 mx-auto pb-2.5 text-md tablet:text-xl placeholder:text-center text-gray-300 bg-transparent outline-none border-b-2 border-b-gray-600  focus-visible:border-b-gray-300"/>
        </label>
        <label htmlFor="PhoneNumber" className="mb-12 flex">
          <input
            required
            type="tel"
            name="PhoneNumber"
            id="PhoneNumber"
            placeholder="PhoneNumber"
            className="w-10/12 tablet:w-8/12 mx-auto pb-2.5 text-md tablet:text-xl placeholder:text-center text-gray-300 bg-transparent outline-none border-b-2 border-b-gray-600  focus-visible:border-b-gray-300"/>
        </label>

        <button
          type="submit"
          className="w-5/12 tablet:w-4/12 mx-auto py-2 text-gray-200 border-2 border-gray-300 transition duration-300 ease-in-out hover:scale-105 hover:text-gray-200 hover:border-gray-200 active:scale-110 active:text-gray-50 active:border-gray-50">
          Create account
        </button>

        <div className='flex flex-col mx-auto mt-8 text-gray-400 text-md tablet:text-xl text-center'>
          <p>Already have an account?</p>
          <Link to="/login" className='w-6/12 mx-auto hover:text-gray-200'>Sign in</Link>
        </div>
      </form>
    </>
  )
}

export default Register;