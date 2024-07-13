using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend_api.Dtos.Comment
{
    public class CommentDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public DateTime CreatedOn { get; set; } = DateTime.Now;

        public string CreatedBy { get; set; } = string.Empty;
        // Foreign Key Issue Will Occur;
        // First, Insert Violation => StockId 999 not in the Stock table
        // Second, Delete Violation => Delete StockId 1的时候 因为 comment references the StockId 1.
        // Third, Update Violation => UpdateSomething Not in the Stock table
        // Run every stock in SQL will be very slow and complex
        public int? StockId { get; set; }
    }
}