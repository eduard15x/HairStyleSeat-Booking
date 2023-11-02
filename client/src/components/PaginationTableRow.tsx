import { useState } from "react";

interface PaginationProps {
    currentPage: number;
    pageSize: number;
    totalCount: number;
    handlePageChange: (newPage: number) => void;
};

export const PaginationTableRow = ({currentPage, pageSize, totalCount, handlePageChange} : PaginationProps) => {
    const [startListCount, setStartListCount] = useState<number>(1);
    const [endListCount, setEndListCount] = useState<number>(pageSize);
    const calculateRemainingItems = totalCount % pageSize;
    const prevBtnDisabled = startListCount <= 1 && endListCount === pageSize;
    const nextBtnDisabled = endListCount === totalCount;

    const prevPage = () => {
        if (startListCount === 1) {
            return;
        }
        setStartListCount(startListCount - pageSize);

        if (endListCount === totalCount) {
            setEndListCount(endListCount - calculateRemainingItems);
        } else {
            setEndListCount(endListCount - pageSize);
        }
        handlePageChange(currentPage - 1);
    };

    const nextPage = () => {
        if (endListCount === totalCount) {
            return;
        }
        setStartListCount(startListCount + pageSize);

        if (totalCount - (endListCount + calculateRemainingItems) === 0 ) {
            setEndListCount(totalCount);
        } else {
            setEndListCount(endListCount + pageSize);
        }
        handlePageChange(currentPage + 1);
    };

  return (
    <tr className="flex items-center justify-between border-t border-gray-200 px-4 py-3 sm:px-6">
        <td>
            <p className="text-sm text-gray-300">
                Showing
                <span className="font-bold"> {startListCount}  </span>
                to
                <span className="font-bold"> {endListCount}  </span>
                of
                <span className="font-bold"> {totalCount}  </span>
                results
            </p>
        </td>

        <td>
            <button
                disabled={prevBtnDisabled}
                className={`px-5 py-2 font-medium text-white bg-gray-800 rounded-l ${prevBtnDisabled ? "text-gray-600 bg-gray-800" : "hover:bg-gray-900"}`}
                onClick={prevPage}
            >
                Prev
            </button>
            <button
                disabled={nextBtnDisabled}
                className={`px-5 py-2 font-medium text-white bg-gray-800 border-l border-gray-700 rounded-r ${nextBtnDisabled ? "text-gray-600 bg-gray-900" : "hover:bg-gray-900"}`}
                onClick={nextPage}
            >
                Next
            </button>
        </td>
    </tr>
  );
};