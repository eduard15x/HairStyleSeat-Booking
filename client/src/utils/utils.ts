/*
  -check if any object has properties with value 0 (zero) or empty string ("")
*/
export const hasZeroOrEmptyStringProperties = (obj: any) => {
    for (const key in obj) {
        if (obj.hasOwnProperty(key)) {
            if (obj[key] === 0 || obj[key] === "") {
                console.log(key);
                return true; // Found a property that is 0 or an empty string
            }
        }
    }
    return false; // No properties with 0 or empty string found
};


/*
  -format date to local en-US from a string of default date
*/
export const getFormaterDateUS = (value: string): string => {
    const originalDate = new Date(value!.toString());
    const month = (originalDate.getMonth() + 1).toString().padStart(2, '0'); // Add 1 to month since it's 0-based
    const day = originalDate.getDate().toString().padStart(2, '0');
    const year = originalDate.getFullYear();
    // formatted date - Output: "11/18/2023"
    return `${month}/${day}/${year}`;
};