import { createContext, useContext, useState } from "react";


/**
 * Creating the Application state context for the provider
 */
export const CountContext = createContext<{ count: number, setCount: React.Dispatch<React.SetStateAction<number>>} | undefined>(undefined);


export function CountContextProvider({ children }: any) {
  const [count, setCount] = useState(0);

  return (
    <CountContext.Provider value={{ count, setCount }}>
        {children}
    </CountContext.Provider>
  );
}

// custom hook
export function useCountContext() {
  const count = useContext(CountContext);

  if (!count) {
    console.log("count is 0");
    throw new Error("count is 0");
  }

  return count;
}