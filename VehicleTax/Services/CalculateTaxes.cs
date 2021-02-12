using VehicleTax.Domain;

namespace VehicleTax.Services
{

    public static class CalculateTaxes
    {
        public static double CalculateDuty(this VehicleTaxDto tax, double CIF) =>
            CIF * (tax.ImportDuty + tax.Nhil + tax.GetfundLevy + tax.AuLevy + tax.EcowasLevy
                    + tax.EximLevy + tax.ExamLevy + tax.ProcessingFee + tax.SpecialImportLevy)
                    + tax.CalculateVat(CIF);

        //VAT is on the duty inclusive value (CIF + Duty + NHIL + GETFUND LEVY)
        public static double CalculateVat(this VehicleTaxDto tax, double CIF) =>
            (CIF + CIF * tax.ImportDuty + CIF * tax.Nhil + CIF * tax.GetfundLevy) * tax.Vat;
            
        
    }
}
