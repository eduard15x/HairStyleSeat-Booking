import { useState } from "react";
import { useNavigate } from "react-router-dom";

export const useRegister = () => {
  interface IUserRegisterCredentials {
    UserName: string;
    Email: string;
    Password: string;
    ConfirmPassword: string;
    City: string;
    PhoneNumber: string;
  };

  const [error, setError] = useState<string | null>(null);
  const [successMessage, setSuccessMessage] = useState<string | null>(null);
  const [isLoading, setIsLoading] = useState<boolean>(false);
  const navigateTo = useNavigate();

  const register = async (userRegister: IUserRegisterCredentials, registerUrlEndpoint: string) => {
    setIsLoading(true);
    setError(null);
    setSuccessMessage(null);

    const response = await fetch(registerUrlEndpoint, {
      method: "POST",
      headers: {"Content-Type": "application/json"},
      body: JSON.stringify(userRegister)
    });
    const json = await response.json();

    if (!response.ok) {
      setIsLoading(false);
      setError(json.value);
    } else {
      if (json.statusCode >= 400) {
          setIsLoading(false);
          setError(json.value);
      } else {
        setIsLoading(true);
        setSuccessMessage("Accout created. \n You will be redirected...");
        setTimeout(() => {
          navigateTo("/login");
        }, 3000);
      }
    }
  };

  return { register, isLoading, error, successMessage };
};