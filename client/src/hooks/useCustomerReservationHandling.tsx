import { useState } from "react";
import { useNavigate } from "react-router-dom";
import { IReservationDetails } from '../shared/interfaces';

export const useCustomerReservationHandling = () => {
    const [errorNewReservation, setErrorNewReservation] = useState<string | null>(null);
    const [successMessage, setSuccessMessage] = useState<string | null>(null);
    const [isCreatingReservation, setIsCreatingReservation] = useState<boolean>(false);
    const navigateTo = useNavigate();

    const createReservation = async (reservationDetails: IReservationDetails, newReservationEndpoint: string) => {
        setIsCreatingReservation(true);
        setErrorNewReservation(null);
        setSuccessMessage(null);

        try {
            const response = await fetch(newReservationEndpoint, {
                method: "POST",
                headers: {"Content-Type": "application/json"},
                credentials: 'include',
                body: JSON.stringify(reservationDetails)
            });

            const json = await response.json();

            if (json.statusCode >= 400) {
                setIsCreatingReservation(false);
                setErrorNewReservation(json.value);
            } else {
                setIsCreatingReservation(true);
                setSuccessMessage("Reservation created. You will be redirected...");
                // setTimeout(() => {
                //     navigateTo("/login");
                // }, 3000);
            }

        } catch (error) {
            console.log(error);
            setIsCreatingReservation(false);
            setErrorNewReservation("Server/Fetching error.");
        }
    };

    return { createReservation, isCreatingReservation, errorNewReservation, successMessage };
};