
namespace Company.Day02.PL.Healpers
{
    public static class DocumentSettings
    {
        //1-Upload
        public static string UploadFile(IFormFile file , string folderName)
        {
            //string folderPath = "E:\\Route\\MVC\\Day 3 MVC\\Company.Day02\\Company.Day02.PL\\wwwroot\\files\\" + folderName;
            //var folderPath = Directory.GetCurrentDirectory() + "\\wwwroot\\files\\" + folderName;
            var folderPath = Path.Combine(Directory.GetCurrentDirectory() , @"wwwroot\files" , folderName);



            var fileName = $"{Guid.NewGuid()} {file.FileName}";

            var filePath = Path.Combine(folderPath, fileName);

            using var fileStream = new FileStream(filePath , FileMode.Create);

            file.CopyTo(fileStream);

            return fileName;
        }

        //2-Delete
        public static void DeleteFile(string fileName , string folderName)
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory() , @"wwwroot\files" , folderName , fileName);
            if(File.Exists(filePath) )
            {
                File.Delete(filePath);
            }
        }
    }
}
