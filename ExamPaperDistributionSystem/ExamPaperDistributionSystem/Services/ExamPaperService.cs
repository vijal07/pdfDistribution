using System;
using System.Collections.Generic;
using ExamPaperDistributionSystem.Models;
using ExamPaperDistributionSystem.Repositories;

namespace ExamPaperDistributionSystem.Services
{
    public class ExamPaperService
    {
        private readonly ExamPaperRepository _examPaperRepository;

        public ExamPaperService()
        {
            _examPaperRepository = new ExamPaperRepository();
        }

        public List<ExamPaper> GetAllExamPapers()
        {
            return _examPaperRepository.GetAllExamPapers();
        }

        public ExamPaper GetExamPaperById(int id)
        {
            // return _examPaperRepository.GetExamPaperById(id);
            return new ExamPaper();
        }

        public void UploadExamPaper(ExamPaper examPaper)
        {
            // Perform any validation or business logic here
            _examPaperRepository.UploadExamPaper(examPaper);
        }
        
        public void DeleteExamPaper(int id)
        {
           // _examPaperRepository.DeleteExamPaper(id);
        }

        public void UpdateExamPaper(ExamPaper examPaper)
        {
            // Perform any validation or business logic here
          //  _examPaperRepository.UpdateExamPaper(examPaper);
        }
    }
}
