namespace MedicineClient.Models
{

    public class Medicine
    {
        public int MedicineId { get; set; }

        public int PharmacyId { get; set; }


        public string MedicineName { get; set; }


        public string BrandName { get; set; }

        public int StatusId { get; set; }

        public int UserId { get; set; }
        public Medicine()
        { }

        public Medicine(int medicineid, int pharamacyid, string medicinename, string brandname, int statusid, int userid)
        {
            this.MedicineId=medicineid;
            this.MedicineName=medicinename;
            this.BrandName=brandname;
            this.StatusId=statusid;
            this.UserId=userid;
            this.PharmacyId=pharamacyid;
        }
    }
}
