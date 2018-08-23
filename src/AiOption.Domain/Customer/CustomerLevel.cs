namespace AiOption.Domain.Customer
{
    public enum CustomerLevel  {

        None = 0,
        Baned = -100,
        Standard = 100,
        Silver = 200,
        Gold = 300,
        Platinum = 400,
        Vip = 500,

        /// <summary>
        /// Administrator who can manage all 
        /// </summary>
        Administrator = 99,

        /// <summary>
        /// For traders only, no need to buy position
        /// </summary>
        Traders = 9999
    }


}