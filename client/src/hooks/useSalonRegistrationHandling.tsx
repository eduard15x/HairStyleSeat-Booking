import { useState } from "react";
import { useNavigate } from "react-router-dom";
import { ISalonRegistration } from '../shared/interfaces';

export const useSalonRegistrationHandling = () => {
    const [errorNewReservation, setErrorNewReservation] = useState<string | null>(null);
    const [successMessage, setSuccessMessage] = useState<string | null>(null);
    const [isSendingRequest, setIsSendingRequest] = useState<boolean>(false);
    const navigateTo = useNavigate();

    const registerSalon = async (salonRegistrationDetails: ISalonRegistration, registerSalonEndpoint: string) => {
        setIsSendingRequest(true);
        setErrorNewReservation(null);
        setSuccessMessage(null);

        try {
            const response = await fetch(registerSalonEndpoint, {
                method: "POST",
                headers: {"Content-Type": "application/json"},
                credentials: 'include',
                body: JSON.stringify(salonRegistrationDetails)
            });

            const json = await response.json();

            if (json.statusCode >= 400) {
                setIsSendingRequest(false);
                setErrorNewReservation(json.value);
            } else {
                setIsSendingRequest(true);
                setSuccessMessage("Reservation created. You will be redirected...");
                // setTimeout(() => {
                //     navigateTo("/login");
                // }, 3000);
            }

        } catch (error) {
            console.log(error);
            setIsSendingRequest(false);
            setErrorNewReservation("Server/Fetching error.");
        }
    };

    return { registerSalon, isSendingRequest, errorNewReservation, successMessage };
};