import {  useCountContext } from '../context/countContext';
// import { useUserContext } from "../context/userContext";


const Comp1 = () => {
  const { count, setCount } = useCountContext();
  // const { userState, dispatch } = useUserContext();


  console.log("update count on Comp1");
  // console.log(userState);


  return (
    <div className='text-blue-50'>
      <p>{count}</p>
      <button className='text-blue-50 mb-10' onClick={() => setCount(count + 1)}>Increment</button>


      {/* <button className='text-blue-50 mb-10' onClick={() => handleLogin()}>LOGIN</button>
      <button className='text-blue-50 mb-10' onClick={() => handleLogout()}>LOGOUT</button> */}

    </div>
  )
}

export default Comp1