﻿using RestSharp;

namespace iqoptionapi.http {
    public abstract class IqOptionRequest : RestRequest {
        protected IqOptionRequest(string action, Method method = Method.GET) : base(action, method){
            this.AddHeader("Accept", "application/json");
        }

    }
}