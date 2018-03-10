import { Reducer, Action } from "redux";
import { AppThunkAction } from './'
import { Dispatch } from "react-redux";


export interface LoginState {
    loggedin: boolean;
    email?: string;
}

export class LoginInputModel{
    email: string;
    password: string;

    
}



export const LOGIN_SUCCESS = 'LOGIN_SUCCESS';


export enum AuthenticateActionType {
  START_LOGIN = "START_LOGIN",
  LOGIN_SUCCESS = "LOGIN_SUCCESS",
  LOGIN_FAILED = "LOGIN_FAILED",
  START_LOGOUT = "START_LOGOUT",
  LOGOUT_SUCCESS = "LOGOUT_SUCCESS",
  LOGOUT_FAILED = "LOGOUT_FAILED"
}

interface StartLoginAction      { type: AuthenticateActionType.START_LOGIN }
interface LoginSuccessAction    { type: AuthenticateActionType.LOGIN_SUCCESS }
interface LoginFailedAction     { type: AuthenticateActionType.LOGIN_FAILED }
interface StartLogoutAction     { type: AuthenticateActionType.START_LOGOUT }
interface LogoutSuccessAction   { type: AuthenticateActionType.LOGOUT_SUCCESS}
interface LogoutFailedAction    { type: AuthenticateActionType.LOGOUT_FAILED }


// Declare a 'discriminated union' type. This guarantees that all references to 'type' properties contain one of the
// declared type strings (and not any other arbitrary string).
type KnownAction = | StartLoginAction | LoginSuccessAction | LoginFailedAction | StartLogoutAction | LogoutSuccessAction | LogoutFailedAction;

// -

export const actionCreators ={
    login: (uid: string, pwd: string) => async (dispatch: Dispatch<KnownAction>, getState: any) => {
        dispatch({type: 'START_LOGIN'});
        var data = {
            email: uid,
            password: pwd
        }

        let response = <Response> await fetch('/authorization/token', {
            method: 'POST',
            body: JSON.stringify(data),
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            }
        })
        .then(x => x.json());

        if(response.ok){
            let json = await response.json();
            let token = json.access_token;
            dispatch({type: AuthenticateActionType.LOGIN_SUCCESS});
            localStorage.setItem('token', token);
        }

    }
}


// ----------------
// REDUCER - For a given state and action, returns the new state. To support time travel, this must not mutate the old state.
export const reducer: Reducer<LoginState> = (state: LoginState, action: KnownAction) => {
    switch (action.type) {
        case AuthenticateActionType.START_LOGIN:
            return { loggedin: false };
        case AuthenticateActionType.LOGIN_SUCCESS:
            return { loggedin: true };
        case AuthenticateActionType.LOGIN_FAILED:
            return { loggedin: false };
        case AuthenticateActionType.START_LOGOUT:
            return { loggedin: true };
        case AuthenticateActionType.LOGOUT_SUCCESS:
            return { loggedin: false };
        case AuthenticateActionType.LOGOUT_FAILED:
            return { loggedin: true };

        default:
            // The following line guarantees that every action in the KnownAction union has been covered by a case above
            const exhaustiveCheck: never = action;
    }

    // For unrecognized actions (or in cases where actions have no effect), must return the existing state
    //  (or default initial state if none was supplied)
    return state || { loggedin: false };
};

