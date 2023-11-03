import { useState } from "react";

export const TimePicker = ({ startTime, endTime, interval, onTimeSelect }: any) => {
    const [selectedButton, setSelectedButton] = useState<number>(0)

    const generateTimeOptions = () => {
        const timeOptions = [];
        let currentTime = new Date(startTime);

        while (currentTime < endTime) {
            const hours = currentTime.getHours();
            const minutes = currentTime.getMinutes();
            const timeString = `${hours.toString().padStart(2, '0')}:${minutes.toString().padStart(2, '0')}`;
            timeOptions.push(timeString);
            currentTime.setMinutes(currentTime.getMinutes() + interval);
        }

        return timeOptions;
    };

    const timeOptions = generateTimeOptions();

    return (
        <div className="flex flex-wrap justify-start">
            {timeOptions.map((timeOption, index) => (
            <button
                key={index}
                data-button-index={index + 1}
                onClick={(e) => {
                    onTimeSelect(timeOption);
                    setSelectedButton(Number(e.currentTarget.dataset.buttonIndex));
                }}
                className={`border border-gray-500 m-1 px-2 py-0.5 max-w-full hover:bg-[#00dc00] focus:bg-[#006edc] hover:text-white active:scale-90 ${index + 1 === selectedButton ? "bg-[#006edc] text-white" : ""}`}
            >
                {timeOption}
            </button>
            ))}
        </div>
    );
};