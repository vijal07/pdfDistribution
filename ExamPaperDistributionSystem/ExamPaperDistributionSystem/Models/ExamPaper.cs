using System;

namespace ExamPaperDistributionSystem.Models
{
    public class ExamPaper
    {
        public int PaperId { get; set; }
        public string Title { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public string UploadedBy { get; set; }
        public DateTime UploadedAt { get; set; }
        public byte[] Encrypted { get; set; }
    }
}
