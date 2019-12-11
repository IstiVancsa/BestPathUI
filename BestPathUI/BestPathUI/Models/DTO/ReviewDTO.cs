namespace Models.DTO
{
    public class ReviewDTO : BaseModel
    {
        public string ReviewComment { get; set; }
        public int Stars { get; set; }
    }
}
