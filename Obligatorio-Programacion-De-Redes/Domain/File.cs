using System;

namespace Domain
{
    public class File
    {
        public string Name { get; set; }
        public double Size { get; set; }
        public Theme Theme { get; set; }
        public DateTime UploadDate { get; set; }
    }
}