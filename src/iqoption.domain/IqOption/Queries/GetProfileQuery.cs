using EventFlow.Queries;

namespace iqoption.domain.IqOption.Queries {

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