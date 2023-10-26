import { useState } from 'react';
import Cookie from 'js-cookie';
import { useUserContext } from "./useUserContext";

export const useLogout = () => {
  const { userState, dispatch } = useUserContext();
  const [isLoading, setIsLoading] = useState<boolean>(false);

  const logout = () => {
    setIsLoading(true);
    Cookie.remove("user");
    Cookie.remove("token");
    dispatch({ type: "LOGOUT", userId: 0, email: "", role: "" });
    setIsLoading(false);
    console.log(userState);
  };

  return {logout, isLoading, userState}
};