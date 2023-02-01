using CsvHelper;
using CsvHelper.Configuration;
using System.Drawing;
using System.Globalization;
using System.Net;
using System.Text.Json;

namespace ColorAnalitycs
{
    public class Helper
    {
        int number = 1;
        public void CsvRead()
        {
            List<ErrorListData> errorListDatas = new();
            try
            {
                var csvConfig = new CsvConfiguration(CultureInfo.InvariantCulture)
                {
                    IgnoreBlankLines = false,
                };
                var csvWriterConfig = new CsvConfiguration(CultureInfo.InvariantCulture)
                {
                    HasHeaderRecord = false,
                };
                string json;
                using (var reader = new StreamReader("../../../allquestions.csv"))
                //using (var reader = new StreamReader("../../../new.csv"))
                using (var csvReader = new CsvReader(reader, csvConfig))
                using (var writerReader = new StreamReader("../../../booksectioncrops.csv"))
                using (var csvWriterReader = new CsvReader(writerReader, csvWriterConfig))
                {
                    csvWriterReader.Context.RegisterClassMap<WriteDataClassMap>();
                    var records = csvReader.GetRecords<ReadData>().Skip(1).ToList();
                    var bookSection = csvWriterReader.GetRecords<WritedData>().Skip(1).ToList();
                    for (int i = 0; i < records.Count(); i++)
                    {
                        if (!string.IsNullOrEmpty(records[i].QuestionPartImageUrls) || !string.IsNullOrWhiteSpace(records[i].QuestionPartImageUrls))
                        {
                            Bitmap questionPartImageUrls = DownloadImage(records[i]!.QuestionPartImageUrls!);
                            if (records[i].QuestionPartImageUrls != null)
                            {
                                bool isQuestionColorful = IsColorful(questionPartImageUrls);
                                string Qjson = ConvertJson(questionPartImageUrls, bookSection[i].ColorAnalyzeJSON);
                                if (isQuestionColorful)
                                {
                                    int colorCount = GetColors(questionPartImageUrls).Distinct().Count();
                                    if (colorCount > 8400)
                                    {
                                        bookSection[i].ColorAnalyzeJSON = Qjson;
                                        bookSection[i].IsNew = "true";
                                        Console.WriteLine($"{number++}. - Yeni soru url: {records[i].QuestionPartImageUrls}");
                                    }
                                    else
                                    {
                                        bookSection[i].IsNew = "false";
                                        Console.WriteLine($"{number++}. - Eski soru url: {records[i].QuestionPartImageUrls}");
                                    }
                                }
                                else
                                {
                                    bookSection[i].IsNew = "false";
                                    Console.WriteLine($"{number++}. - Eski soru url: {records[i].QuestionPartImageUrls}");
                                }
                            }
                        }
                        bookSection[i].Id = bookSection[i].Id;
                        bookSection[i].ImageId = bookSection[i].ImageId;
                        bookSection[i].BookSectionId = bookSection[i].BookSectionId;
                        bookSection[i].UserId = bookSection[i].UserId;
                        bookSection[i].QuestionNumber = bookSection[i].QuestionNumber;
                        bookSection[i].PageNumber = bookSection[i].PageNumber;
                        bookSection[i].AnswerImageId = bookSection[i].AnswerImageId;
                        bookSection[i].AnswerOption = bookSection[i].AnswerOption;
                        bookSection[i].CreatedDate = bookSection[i].CreatedDate;
                        bookSection[i].ModifiedDate = bookSection[i].ModifiedDate;
                        bookSection[i].RowStatus = bookSection[i].RowStatus;
                        bookSection[i].DifficultyLevel = bookSection[i].DifficultyLevel;
                        bookSection[i].AnswerImagePath = bookSection[i].AnswerImagePath;
                        bookSection[i].ImagePath = bookSection[i].ImagePath;
                        bookSection[i].AnswerPageNumber = bookSection[i].AnswerPageNumber;
                        bookSection[i].AnswerVideoPath = bookSection[i].AnswerVideoPath;
                        bookSection[i].Dimensions = bookSection[i].Dimensions;
                        try
                        {
                            Bitmap image = DownloadImage(records[i].ImageUrl);
                            if(image != null)
                            {
                                bool isColorful = IsColorful(image);
                                json = ConvertJson(image, bookSection[i].ColorAnalyzeJSON);
                                bookSection[i].ColorAnalyzeJSON = json;
                                if (isColorful)
                                {
                                    int colorCount = GetColors(image).Distinct().Count();
                                    if (colorCount / 2 > 4500)
                                    {
                                        bookSection[i].IsNew = "true";
                                        Console.WriteLine($"{number++}. - Yeni soru url: " + records[i].ImageUrl);
                                    }
                                    else
                                    {
                                        bookSection[i].IsNew = "false";
                                        Console.WriteLine($"{number++}. - Eski soru url: " + records[i].ImageUrl);
                                    }
                                }
                                else
                                {
                                    bookSection[i].IsNew = "false";
                                    Console.WriteLine($"{number++}. - Eski soru url: " + records[i].ImageUrl);
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"{number++}. - Error: " + ex.Message);

                            ErrorListData errorListData = new();
                            errorListData.Id = bookSection[i].Id;
                            errorListData.ImageId = bookSection[i].ImageId;
                            errorListData.ImageUrl = records[i].ImageUrl;
                            errorListDatas.Add(errorListData);
                            continue;
                        }
                    }
                    reader.Close();
                    WriteCsv(bookSection);
                    if (errorListDatas != null)
                        WriteErrorCsv(errorListDatas);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{number++}. - CsvReader Error: " + ex.Message);
            }

        }

        public void WriteErrorCsv(List<ErrorListData> ErrorListData)
        {
            try
            {
                using (var steam = File.Open("../../../NewErrorImageURL.csv", FileMode.Create))
                using (var writer = new StreamWriter(steam))
                using (var csvWriter = new CsvWriter(writer, CultureInfo.InvariantCulture))
                {
                    csvWriter.WriteHeader<ErrorListData>();
                    csvWriter.NextRecord();
                    csvWriter.WriteRecords(ErrorListData);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{number++}. - WriterErrorCsv Error: {ex.Message}");
            }
        }

        public void WriteCsv(List<WritedData> writedDatas)
        {
            try
            {
                using (var steam = File.Open("../../../BookSectionIsNew.csv", FileMode.Create))
                using (var writer = new StreamWriter(steam))
                using (var csvWriter = new CsvWriter(writer, CultureInfo.InvariantCulture))
                {
                    csvWriter.WriteHeader<WritedData>();
                    csvWriter.NextRecord();
                    csvWriter.WriteRecords(writedDatas);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{number++}. - WriteCsv Error: " + ex.Message);
            }
        }


        HashSet<Color> GetColors(Bitmap image)
        {
            try
            {
                if (image != null)
                {
                    var colors = new HashSet<Color>();
                    for (int x = 0; x < image.Width; x++)
                    {
                        for (int y = 0; y < image.Height; y++)
                        {
                            var color = image.GetPixel(x, y);
                            if (!colors.Contains(color))
                            {
                                colors.Add(color);
                            }
                        }
                    }
                    return colors;
                }
                else
                {
                    Console.WriteLine($"{number++}. - GetColors image not found.");
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{number++}. - GetColors Error: {ex.Message}");
                return null;
            }
        }


        Bitmap DownloadImage(string url)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);

                request.UseDefaultCredentials = true;
                request!.Proxy!.Credentials = CredentialCache.DefaultCredentials;

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream stream = response.GetResponseStream();
                Bitmap image = new Bitmap(stream);

                stream.Close();
                response.Close();

                return image;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{number++}. - Error: " + ex.Message);
                return null;
            }
        }

        bool IsColorful(Bitmap image)
        {
            try
            {
                int colorCount = 0;
                int greyScale = 0;
                double totalSaturation = 0;
                if (image != null)
                {
                    for (int x = 0; x < image.Width; x++)
                    {
                        for (int y = 0; y < image.Height; y++)
                        {
                            Color pixelColor = image.GetPixel(x, y);
                            var hue = pixelColor.GetSaturation();
                            var light = pixelColor.GetBrightness();
                            if (hue <= 0.06 && light <= 30)
                            {
                                greyScale++;
                            }
                            else
                            {
                                colorCount++;
                                totalSaturation += pixelColor.GetSaturation();
                            }
                        }
                    }


                    int totalPixels = image.Width * image.Height;
                    double colorPercentage = (double)colorCount / totalPixels;
                    double averageSaturation = totalSaturation / colorCount;

                    return colorPercentage > 0.155 && averageSaturation > 0.155;
                }
                else
                    Console.WriteLine($"{number}. - IsColorFul image not found.");
                return false;
                }
            catch (Exception ex)
            {
                Console.WriteLine($"{number++}. - IsColorful Error: {ex.Message}");
                return false;
            }
           
        }

        string ConvertJson(Bitmap image, string json)
        {
            try
            {
                int colorCount = 0;
                int greyScale = 0;
                double totalSaturation = 0;
                if (image != null)
                {
                    for (int x = 0; x < image.Width; x++)
                    {
                        for (int y = 0; y < image.Height; y++)
                        {
                            Color pixelColor = image.GetPixel(x, y);
                            var hue = pixelColor.GetSaturation();
                            var light = pixelColor.GetBrightness();
                            if (hue <= 0.06 && light <= 30)
                            {
                                greyScale++;
                            }
                            else
                            {
                                colorCount++;
                                totalSaturation += pixelColor.GetSaturation();
                            }
                        }
                    };

                    int totalPixels = image.Width * image.Height;
                    double colorPercentage = (double)colorCount / totalPixels;
                    double averageSaturation = totalSaturation / colorCount;

                    json = $"Color Percentage: {Math.Round(colorPercentage, 2)}, Average Saturation: {Math.Round(averageSaturation, 2)}, Grey Scale Count: {greyScale}, Color Count: {colorCount}, Total Pixel: {totalPixels}";
                    json = JsonSerializer.Serialize(json);
                    return json;
                }
                else
                    return $"{number++}. - Convert Json Image Not Found";
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{number++}. - Convert Json Error: {ex.Message}");
                return "JsonError";
            }
           
        }
    }
}
