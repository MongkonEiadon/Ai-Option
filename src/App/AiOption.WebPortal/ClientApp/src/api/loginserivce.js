

export const userService = {
    Login
};

function Login(username, password){
    const request = {
        method: 'POST',
        headers: { 'Content-type': 'application/json' },
        body: Json.stringify({username, password})
    };

    return fetch('api/customer/login', request)
        .then(
            (success) =>{},
            (error) =>{}
        )
        .then(user => {

        });
}