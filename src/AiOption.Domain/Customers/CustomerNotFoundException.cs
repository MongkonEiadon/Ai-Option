namespace AiOption.Domain.Customers {

    public class CustomerNotFoundException : DomainException {

        public CustomerNotFoundException(string businessMessage) : base(businessMessage) {
        }

    }

}