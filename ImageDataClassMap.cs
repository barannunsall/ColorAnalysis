using CsvHelper.Configuration;

namespace ColorAnalitycs
{
    public class ReadDataClassMap : ClassMap<ReadData>
    {
        public ReadDataClassMap()
        {
            Map(i => i.ImageUrl).Name("ImageUrl");
            Map(i => i.QuestionPartImageUrls).Name("QuestionPartImageUrls");
        }
    }

    public class WriteDataClassMap : ClassMap<WritedData>
    {
        public WriteDataClassMap()
        {
            Map(i => i.Id);
            Map(i => i.BookSectionId);
            Map(i => i.UserId);
            Map(i => i.QuestionNumber);
            Map(i => i.PageNumber);
            Map(i => i.ImageId);
            Map(i => i.AnswerImageId);
            Map(i => i.AnswerOption);
            Map(i => i.CreatedDate);
            Map(i => i.ModifiedDate);
            Map(i => i.RowStatus);
            Map(i => i.DifficultyLevel);
            Map(i => i.AnswerImagePath);
            Map(i => i.ImagePath);
            Map(i => i.AnswerPageNumber);
            Map(i => i.AnswerVideoPath);
            Map(i => i.Dimensions);
        }
    }
    public class ErrorDataClassMap : ClassMap<ErrorListData>
    {
        public ErrorDataClassMap()
        {
            Map(i => i.Id);
            Map(i => i.ImageId);
            Map(i => i.ImageUrl);
        }
    }
}