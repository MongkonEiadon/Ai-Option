using System.Collections.Generic;

namespace iqoption.domain.Users {
    public class Follower : User {
        public List<Host> FollowingHosts { get; set; }
    }
}