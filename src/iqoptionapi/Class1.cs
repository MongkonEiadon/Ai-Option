﻿using System;
using System.Collections;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using iqoption.core.Extensions;
using iqoptionapi.http;
using iqoptionapi.ws;

namespace iqoptionapi
{
    public class IqOptionApi  {
        private string _host { get; }

        public IqOptionWebClient WebClient { get; private set; }
        public IqOptionWebSocketClient WsClient { get; private set; }


        public IqOptionApi(string username, string password, string host = "iqoption.com") {
            _host = host;

            //set up client
            WebClient = new IqOptionWebClient(username, password);
        }

        public async Task<bool> ConnectAsync() {

            var result = await WebClient.LoginAsync();
            if (result.StatusCode == HttpStatusCode.OK) {
                WsClient = new IqOptionWebSocketClient(WebClient.SecuredToken, "iqoption.com");

                if (await WsClient.OpenWebSocketAsync()) {

                    Debug.WriteLine("Ws Ready!");
                }

                return true;
            }
            return false;
        }

        public async Task<IqResult<Profile>> GetProfileAsync() {
            var result =  await WebClient.GetProfileAsync();
            return result.Content.JsonAs<IqResult<Profile>>();
        }

    

    }
}
