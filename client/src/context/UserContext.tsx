import { createContext, useReducer, useEffect, useState } from "react";
import Cookie from 'js-cookie';

interface User {
    userId: number;
    userEmail: string | undefined;
    userRole: string | undefined
};

// The dummy user object used for this example
// Try to set this from localStorage
const initialUserState: User ={
    userId: 0,
    userEmail: undefined,
    userRole: undefined
}

type UserAction = {
    type: 'LOGIN' | 'LOGOUT';
    userId: number;
    email: string;
    role: string;
  };

const userReducer = (state: User, action: UserAction): User => {
    switch(action.type) {
        case 'LOGIN':
            return {
                userId: action.userId,
                userEmail: action.email,
                userRole: action.role,
            };
        case 'LOGOUT':
            return {
                userId: 0,
                userEmail: "",
                userRole: "",
            };
        default:
            return state;
    }
}
/**
 * Creating the Application state context for the provider
 */
export const UserContext = createContext<{ userState: User; dispatch: React.Dispatch<UserAction> }>({
    userState: initialUserState,
    dispatch: () => undefined,
});

export const UserContextProvider = ({ children }: any) => {
    const [userState, dispatch] = useReducer(userReducer, initialUserState);
    const [isLoading, setIsLoading] = useState(true);

    useEffect(() => {
        const cookie = Cookie.get("user");
        if (cookie || cookie !== undefined) {
            const userPropertiesArr = cookie.split("-");
            dispatch({
                type: "LOGIN",
                userId: Number(userPropertiesArr[0]),
                email: userPropertiesArr[1],
                role: userPropertiesArr[2]
            });
        }
        setIsLoading(false); // Done loading user context
    }, []);

    if (isLoading) {
        return <div>Loading...</div>;
    }

    return (
    <UserContext.Provider value={{ userState, dispatch }}>
        {children}
    </UserContext.Provider>
    );
};




export const ExampleContext = createContext<string | undefined>(undefined);