namespace AiOption.Domain.Customer {

    public class CustomerNotFoundException : DomainException {

        public CustomerNotFoundException(string businessMessage) : base(businessMessage) {
        }

    }

}