const Footer = () => {
    return (
        <footer className="text-sm text-gray-500 flex justify-center absolute bottom-0 w-full pb-4">
            &copy; 2023 Eduard
        </footer>
    )
}

export default Footer;


// import { useState } from 'react';

// const Footer: React.FC = () => {
//     interface Salon {
//         salonName: string;
//         // Add other properties as needed
//     }

//     const [info, setInfo] = useState<Salon[]>([]);

//     const getData = async () => {
//         const response = await fetch("https://localhost:44315/api/salons/list");
//         const movies = await response.json();
//         console.log(movies.value);
//         setInfo(movies.value);
//     }

//     return (
//         <footer className="text-sm text-gray-500 flex justify-center absolute bottom-0 w-full pb-4">
//             <ul>
//                 {info.map((item, index) => (
//                     <li key={index}> {item.salonName}</li>
//                 ))}
//             </ul>
//             <button onClick={getData}>xx</button>
//         </footer>
//     )
// }

// export default Footer;