using iqoption.core.data;
using iqoption.data.Model;

namespace iqoption.data.Services {
    public interface IPersonService {
    }

    public class PersonService : IPersonService {
        public PersonService(IRepository<PersonDto, string> personRepository) {
        }
    }
}