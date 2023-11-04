export interface IUserCredentials {
    email: string;
    password: string;
};



export interface IUserRegisterCredentials {
    UserName: string;
    Email: string;
    Password: string;
    ConfirmPassword: string;
    City: string;
    PhoneNumber: string;
};




export interface IUser {
    userId: number;
    userEmail: string | undefined;
    userRole: string | undefined
};



export interface IUserAction {
    type: 'LOGIN' | 'LOGOUT';
    userId: number;
    email: string;
    role: string;
};




// reservationModal
export interface IReservationModal {
    showReservationModal: boolean;
    handleReservationModal: () => void;
    salonId: number;
};

export interface ISalonData {
    endTimeHour: string;
    salonAddress: string;
    salonCity: string;
    salonName: string;
    salonReviews: number;
    salonStatus: number;
    startTimeHour: string;
    userDetails: {
        email: string;
        phoneNumber: string;
        userName: string;
    },
    workDays: string;
};

export interface ISalonService {
    serviceId: number;
    haircutDurationTime: string;
    price: number;
    serviceName: string;
};

// reservation details
export interface IReservationDetails {
    userId: number;
    salonId: number;
    salonServiceId: number;
    reservationDay: string;
    reservationHour: string;
};





export interface IPaginationProps {
    currentPage: number;
    pageSize: number;
    totalCount: number;
    handlePageChange: (newPage: number) => void;
};








export interface IProps {
    text: string;
    isDisplayed: boolean;
    isCanceled: boolean;
    handleConfirm: () => void;
};




//
export interface ISalonRegistration {
    salonName: string;
    salonCity: string;
    salonAddress: string;
    userId: number;
    workDays: string;
    startTimeHour: string;
    endTimeHour: string;
};