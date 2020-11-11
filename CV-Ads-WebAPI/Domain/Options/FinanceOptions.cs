namespace CV_Ads_WebAPI.Domain.Options
{
    public class FinanceOptions
    {
        public const string SectionName = "FinanceOptions";

        public int PricePerViewForCustomer { get; set; }
        public int PricePerViewForPartner { get; set; }
    }
}
