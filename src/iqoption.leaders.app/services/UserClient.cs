using System;
using System.Collections.Generic;
using iqoption.domain.Users;
using iqoptionapi;

namespace iqoption.leaders.app.services {
    public class UserClient<T> : IEquatable<User>
        where T : User, new() {
        public T User { get; set; }
        public IIqOptionApi Client { get; set; }

        public bool Equals(User other) {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            if (other.GetType() != this.GetType()) return false;
            return EqualityComparer<string>.Default.Equals(this.User.UserId, other.UserId);
        }

        public override int GetHashCode() {
            unchecked {
                return (EqualityComparer<T>.Default.GetHashCode(User) * 397) ^ (Client != null ? Client.GetHashCode() : 0);
            }
        }

        public UserClient(string email, string password) { 
            User = new T() {Email = email, Password = password};
            Client = new IqOptionApi(email, password);

            this.TradersBalanceIds = new List<long>();


        }
        

        public List<long> TradersBalanceIds { get; set; }
        
    }


}