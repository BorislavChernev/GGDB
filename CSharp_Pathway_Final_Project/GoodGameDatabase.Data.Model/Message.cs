using GoodGameDatabase.Data.Model;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

public class Message
{
    [Key]
    public int Id { get; set; }

    [Required]
    public int DiscussionId { get; set; }

    [Required]
    [ForeignKey(nameof(DiscussionId))]
    public Discussion Discussion { get; set; }

    [Required]
    public Guid SenderId { get; set; }

    [Required]
    [ForeignKey(nameof(SenderId))]
    public ApplicationUser Sender { get; set; }

    [Required]
    [StringLength(1000, MinimumLength = 1)]
    public string Content { get; set; }

    [Required]
    public DateTime Timestamp { get; set; }
}