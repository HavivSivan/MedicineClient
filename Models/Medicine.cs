

namespace MedicineClient.Models
{

    public class Medicine
    {
        public int MedicineId { get; set; }

        public Pharmacy Pharmacy { get; set; }


        public string MedicineName { get; set; }


        public string BrandName { get; set; }

        public MedicineStatus Status { get; set; }

        public bool NeedsPrescription { get; set; }

        public AppUser user { get; set; }
        public Medicine()
        { }

        public Medicine(int medicineid, Pharmacy pharamacy, string medicinename, string brandname, MedicineStatus status, AppUser user, bool needsPrescription)
        {
            this.MedicineId=medicineid;
            this.MedicineName=medicinename;
            this.BrandName=brandname;
            this.Status=status;
            this.user=user;
            this.Pharmacy=pharamacy;
            this.NeedsPrescription=needsPrescription;
        }
    }
}
