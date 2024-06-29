using System;
using System.IO;
using System.Web.Mvc;
using ExamPaperDistributionSystem.Models;
using ExamPaperDistributionSystem.Services;
using System.Web;
using System.Security.Cryptography;
using System.Text;

namespace ExamPaperDistributionSystem.Controllers
{
    public class PaperController : Controller
    {
        private readonly ExamPaperService _examPaperService;

        public PaperController()
        {
            _examPaperService = new ExamPaperService();
        }

        // GET: Paper/Upload
        public ActionResult Upload()
        {
            // Check if user is authorized to upload papers (e.g., Admin or Examiner role)
            if (Session["User"] == null || !IsAuthorizedToUpload((User)Session["User"]))
            {
                return RedirectToAction("Login", "Account");
            }

            return View();
        }

        // POST: Paper/Upload
        [HttpPost]
        public ActionResult Upload(HttpPostedFileBase paper, string title)
        {
            // Check if user is authorized to upload papers (e.g., Admin or Examiner role)
            if (Session["User"] == null || !IsAuthorizedToUpload((User)Session["User"]))
            {
                return RedirectToAction("Login", "Account");
            }

            try
            {
                if (paper != null && paper.ContentLength > 0)
                {
                    // Save the uploaded paper to the server
                    var fileName = Path.GetFileName(paper.FileName);
                    var path = Path.Combine(Server.MapPath("~/App_Data/Papers"), fileName);
                    paper.SaveAs(path);

                    // Encrypt the paper (dummy method for demonstration)
                    byte[] encryptedBytes = EncryptPaper(System.IO.File.ReadAllBytes(path));
                    System.IO.File.WriteAllBytes(path, encryptedBytes);

                    // Save paper metadata to database
                    ExamPaper examPaper = new ExamPaper
                    {
                        FileName = title,
                        FilePath = path,
                        UploadedBy = ((User)Session["User"]).Username,
                        UploadedAt = DateTime.Now
                    };
                    _examPaperService.UploadExamPaper(examPaper);

                    ViewBag.Message = "Paper uploaded successfully.";
                }
                else
                {
                    ViewBag.Message = "No file uploaded.";
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = $"Error: {ex.Message}";
            }

            return View();
        }

        // GET: Paper/Download/{id}
        public ActionResult Download(int id)
        {
            // Check if user is authorized to download papers (e.g., Invigilator or Student role)
            if (Session["User"] == null || !IsAuthorizedToDownload((User)Session["User"]))
            {
                return RedirectToAction("Login", "Account");
            }

            // Fetch paper details from database by ID
            ExamPaper examPaper = _examPaperService.GetExamPaperById(id);
            if (examPaper == null)
            {
                return HttpNotFound();
            }

            // Decrypt the paper (dummy method for demonstration)
            byte[] decryptedBytes = DecryptPaper(System.IO.File.ReadAllBytes(examPaper.FilePath));
            string fileName = Path.GetFileName(examPaper.FilePath);
            string mimeType = MimeMapping.GetMimeMapping(fileName);

            // Return the paper as a file download
            return File(decryptedBytes, mimeType, fileName);
        }

        // Dummy encryption method (replace with actual encryption logic)
        private byte[] EncryptPaper(byte[] fileBytes)
        {
            // Replace with actual encryption logic
            // Example using dummy encryption
            byte[] encryptedBytes = fileBytes;
            return encryptedBytes;
        }

        // Dummy decryption method (replace with actual decryption logic)
        private byte[] DecryptPaper(byte[] fileBytes)
        {
            // Replace with actual decryption logic
            // Example using dummy decryption
            byte[] decryptedBytes = fileBytes;
            return decryptedBytes;
        }

        // Check if user is authorized to upload papers (Admin or Examiner role)
        private bool IsAuthorizedToUpload(User user)
        {
            // Replace with actual role-based authorization logic
            return user.Role.Name == "Admin" || user.Role.Name == "Examiner";
        }

        // Check if user is authorized to download papers (Invigilator or Student role)
        private bool IsAuthorizedToDownload(User user)
        {
            // Replace with actual role-based authorization logic
            return user.Role.Name == "Invigilator" || user.Role.Name == "Student";
        }
    }
}
