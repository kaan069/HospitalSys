using Hastane_Proj.Data;
using Hastane_Proj.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Hastane_Proj.Controllers
{
    public class PatientController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PatientController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Hastaları listeleyen sayfa
        public IActionResult Index()
        {
            var patients = _context.Patients.ToList(); // Tüm hastaları getir
            return View(patients);
        }

        // Yeni hasta ekleme sayfası
        public IActionResult Create()
        {
            return View();
        }

        // Yeni hasta ekleme işlemi (POST)
        [HttpPost]
        public IActionResult Create(Patient patient)
        {
            // Aynı TC Kimlik Numarasına sahip hasta var mı?
            var olanPatient = _context.Patients.FirstOrDefault(p => p.TcNo == patient.TcNo);

            if (olanPatient != null)
            {
                // Eğer hasta zaten varsa ModelState'e hata ekle
                ModelState.AddModelError("TcNo", "Bu TC Kimlik Numarası Zaten Kayıtlıdır.");
            }
            if (ModelState.IsValid)
            {
                _context.Patients.Add(patient); // Yeni hastayı ekle
                _context.SaveChanges();
                return RedirectToAction("Index"); // Listeye geri dön
            }
            return View(patient);
        }
        // Hasta düzenleme sayfası
        public IActionResult Edit(int id)
        {
            var patient = _context.Patients.Find(id);
            if (patient == null)
            {
                return NotFound();
            }
            return View(patient);
        }

        // Hasta güncelleme işlemi (POST)
        [HttpPost]
        public IActionResult Edit(int id, Patient updatedPatient)
        {
            if (ModelState.IsValid)
            {
                var patient = _context.Patients.Find(id);
                if (patient == null)
                {
                    return NotFound();
                }

                patient.TcNo = updatedPatient.TcNo;
                patient.FirstName = updatedPatient.FirstName;
                patient.LastName = updatedPatient.LastName;
                patient.Phone = updatedPatient.Phone;
                

                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(updatedPatient);
        }
        public IActionResult Delete(int id)
        {
            var patient = _context.Patients.Find(id);
            if (patient == null)
            {
                return NotFound();
            }

            _context.Patients.Remove(patient);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        // T.C Kimlik Numarası ile hasta arama
        public IActionResult Search(string tcNo)
        {
            if (string.IsNullOrEmpty(tcNo))
            {
                return View();
            }

            var patient = _context.Patients.FirstOrDefault(p => p.TcNo == tcNo);
            if (patient == null)
            {
                ViewBag.Message = "Hasta bulunamadı.";
                return View();
            }

            return View(patient);
        }
    }
}
