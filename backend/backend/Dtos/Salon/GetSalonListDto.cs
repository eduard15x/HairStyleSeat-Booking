namespace backend.Dtos.Salon
{
    public class GetSalonListDto
    {
        public List<Models.Salon> Salons { get; set; }
        public int TotalSalons { get; set; }
    }
}
