import { useContext } from "react";
import { UserContext } from "../context/UserContext";

export const useUserContext = () => {
    const userContext = useContext(UserContext);

    if (!userContext) {
        throw new Error("useUserContext must be used within a UserProvider");
    }

    return userContext;
};