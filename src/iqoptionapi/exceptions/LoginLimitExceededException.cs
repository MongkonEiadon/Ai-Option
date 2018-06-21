using System;
using System.Collections.Generic;
using System.Text;

namespace iqoptionapi.exceptions
{
    public class LoginLimitExceededException : Exception
    {
        public LoginLimitExceededException(int ttl) :
            base($"Login limit exceeded, you can re-login in next {ttl} secs.") {

        }
    }

    public class LoginFailedException : Exception {

        public LoginFailedException(string emailAddress, object content) :
            base($"Login With {emailAddress} not passed!, Response is : {content}")
        {

        }
    }
}
