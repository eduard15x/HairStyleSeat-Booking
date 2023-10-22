const Login = () => {
  return (
    <>
      <h1 className="text-gray-200 text-4xl text-center pb-8">
        Login
      </h1>
      <form action="#" method="POST" className="flex flex-col mx-auto w-[500px] py-16 border-2 border-gray-600">
        {/* User Email */}
        <label htmlFor="Email" className="mb-16 flex">
          <input
            required
            type="email"
            name="Email"
            id="Email"
            placeholder="Email"
            className="w-3/5 mx-auto pb-2.5 text-xl text-center text-gray-300 bg-transparent outline-none border-b-2 border-b-gray-600  focus-visible:border-b-gray-300"/>
        </label>
        {/* Password */}
        <label htmlFor="Password" className="mb-16 flex">
          <input
            required
            type="password"
            name="Password"
            id="Password"
            placeholder="Password"
            className="w-3/5 mx-auto pb-2.5 text-xl text-center text-gray-300 bg-transparent outline-none border-b-2 border-b-gray-600  focus-visible:border-b-gray-300"/>
        </label>
        {/* Submit button */}
        <button
          type="submit"
          className="w-40 mx-auto py-2 text-gray-200 border-2 border-gray-300 transition duration-300 ease-in-out hover:scale-105 hover:text-gray-200 hover:border-gray-200 active:scale-110 active:text-gray-50 active:border-gray-50">
          Login
        </button>
    </form>
    </>
  )
}

export default Login;