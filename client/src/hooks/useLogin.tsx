import { useState } from "react";
import Cookie from 'js-cookie';
import { useUserContext } from "./useUserContext";



import { IUserCredentials } from "../shared/interfaces";




export const useLogin = () => {

    const { userState, dispatch } = useUserContext();
    const [error, setError] = useState<string | null>(null);
    const [isLoading, setIsLoading] = useState<boolean>(false);

    const login = async (user: IUserCredentials, loginUrl: string) => {
        console.log(loginUrl)
        setIsLoading(true);
        setError(null);

        const response = await fetch(loginUrl, {
            method: "POST",
            headers: {"Content-Type": "application/json"},
            credentials: 'include',
            body: JSON.stringify(user)
        });
        const json = await response.json();

        console.log(response)
        console.log(json)

        if (!response.ok) {
            setIsLoading(false);
            setError(json.value);
        } else {
            if (json.statusCode >= 400) {
                setError(json.value);
                setIsLoading(false);
            } else {
                const expireTime30Minutes = new Date(new Date().getTime() + 30 * 60 * 1000);
                const cookieString = json.value.userId + "-" + json.value.email + "-" + json.value.role;

                Cookie.set("user", cookieString, {expires: expireTime30Minutes, path: "/", secure: true});
                dispatch({ type: "LOGIN", userId: json.value.userId, email: json.value.email, role: json.value.role });
                setIsLoading(false);
            }
        }
    };

    return { login, isLoading, error, userState };
};