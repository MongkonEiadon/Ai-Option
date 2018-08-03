using EventFlow.Commands;
using EventFlow.Queries;
using iqoption.core;

namespace iqoption.domain.IqOption.Command {

    public class GetProfileResult {
        public Profile ProfileResult { get; }

        public GetProfileResult(Profile profile) {
            ProfileResult = profile;
        }

    }
    public class GetProfileQuery : IQuery<GetProfileResult> {

        public string Ssid { get; }

        public GetProfileQuery(string ssid) {
            Ssid = ssid;
        }
    }
}