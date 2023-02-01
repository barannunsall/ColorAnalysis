using CsvHelper.Configuration.Attributes;

namespace ColorAnalitycs
{
    public class ReadData
    {
        public string ImageUrl { get; set; }
        public string? QuestionPartImageUrls { get; set; }
    }

    public class WritedData
    {        
        public string Id { get; set; }
        
        public string BookSectionId { get; set; }
        
        public string UserId { get; set; }
        
        public string QuestionNumber { get; set; }
        
        public string PageNumber { get; set; }
        
        public string ImageId { get; set; }
        
        public string AnswerImageId { get; set; }
        
        public string AnswerOption { get; set; }
        
        public string CreatedDate { get; set; }
        
        public string ModifiedDate { get; set; }
        
        public string RowStatus { get; set; }
        
        public string DifficultyLevel { get; set; }
        
        public string AnswerImagePath { get; set; }
        
        public string ImagePath { get; set; }
        
        public string AnswerPageNumber { get; set; }
        
        public string AnswerVideoPath { get; set; }
        
        public string Dimensions { get; set; }
        
        public string IsNew { get; set; }
        public string ColorAnalyzeJSON { get; set; }
    }

    public class ErrorListData
    {
        public string Id { get; set; }

        public string ImageId { get; set; }
        public string ImageUrl { get; set; }
    }
}
