using Kennel.Data.Users;
using Kennel.Models.Data.Medication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KennelData.Data;
using System.Web.Http;
using System.Data.Entity;

namespace Kennel.Service.Data
{
    public class MedicationService
    {
        //private user field
        private readonly Guid _userId;

        //private context
        private ApplicationDbContext _context = new ApplicationDbContext();

        //service constructor
        public MedicationService(Guid userId)
        {
            _userId = userId;
        }

        //Create new
        public async Task<bool> CreateMedication(MedicationCreate model)
        {
            Medication medication =
                new Medication()
                {
                    Name = model.Name,
                    Dose = model.Dose,
                    Instructions = model.Instructions,
                    MorningMeal = model.MorningMeal,
                    EveningMeal = model.EveningMeal
                };

            _context.Medications.Add(medication);
            return await _context.SaveChangesAsync() == 1;
        }

        //Get by id
        public async Task<MedicationDetails> GetMedicationById([FromUri] int id)
        {
            var query =
                await
                _context
                .Medications
                .Where(q => q.MedicationId == id)
                .Select(
                    q =>
                    new MedicationDetails()
                    {
                        Name = q.Name,
                        Dose = q.Dose,
                        Instructions = q.Instructions,
                        MorningMeal = q.MorningMeal,
                        EveningMeal = q.EveningMeal
                    }).ToListAsync();
            return query[0];
        }

        //Get by id
        public async Task<MedicationEdit> GetMedicationByIdEditable([FromUri] int id)
        {
            var query =
                await
                _context
                .Medications
                .Where(q => q.MedicationId == id)
                .Select(
                    q =>
                    new MedicationEdit()
                    {
                        Name = q.Name,
                        Dose = q.Dose,
                        Instructions = q.Instructions,
                        MorningMeal = q.MorningMeal,
                        EveningMeal = q.EveningMeal
                    }).ToListAsync();
            return query[0];
        }

        //Update by id
        public async Task<bool> UpdateMedication([FromUri] int id, [FromBody] MedicationEdit model)
        {
            Medication medication =
                _context
                .Medications
                .Single(a => a.MedicationId == id);
            medication.Name = model.Name;
            medication.Dose = model.Dose;
            medication.Instructions = model.Instructions;
            medication.MorningMeal = model.MorningMeal;
            medication.EveningMeal = model.EveningMeal;

            return await _context.SaveChangesAsync() == 1;
        }

        public async Task<bool> DeleteMedication(int id)
        {
            var entity =
                _context
                .Medications
                .Single(e => e.MedicationId == id);

            _context.Medications.Remove(entity);

            return await _context.SaveChangesAsync() == 1;
        }
    }
}
