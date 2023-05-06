namespace BM7_Backend.Dto;

public class NewTransactionDto
{
    public float amount { get; set; }
    public String title { get; set; }
    public bool status { get; set; }
    public Guid categoryId { get; set; }
}