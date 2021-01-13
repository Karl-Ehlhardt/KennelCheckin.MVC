using Kennel.Data.Users;
using Kennel.Models.Data.Kennel.KennelDashboardItems;
using Kennel.Models.Data.Kennel.MealsAndMedications;
using Kennel.Models.Joining_Data.DogVisit;
using Kennel.Models.Kennel.KennelDashboardItems;
using Kennel.Service.Shared;
using KennelData.BillingData;
using KennelData.Data;
using KennelData.JoiningData;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace Kennel.Service.Kennel
{
    public class KennelService
    {
        //private user field
        private readonly Guid _userId;

        //private context
        private ApplicationDbContext _context = new ApplicationDbContext();

        //service constructor
        public KennelService(Guid userId)
        {
            _userId = userId;
        }

        public async Task<bool> CheckOwnerExists()
        {
            Owner owner =
                await
                _context
                .Owners
                .SingleOrDefaultAsync(a => a.ApplicationUserId == _userId.ToString());

            return owner == null;
        }

        // kennel Dashboard Display
        public async Task<KennelDashboardView> DisplayKennelDashboardView()
        {
            //On Site Dogs
            List<DogVisit> onSiteDogVisitList =
                await
                _context
                .DogVisits
                .Where(q => q.OnSite == true)
                .ToListAsync();

            List<KennelDashboardDogListItem> onSiteDogs = new List<KennelDashboardDogListItem>();

            foreach (DogVisit dogVisit in onSiteDogVisitList)
            {
                DogInfo dogInfo =
                await
                _context
                .DogInfos
                .SingleAsync(q => q.DogInfoId == dogVisit.DogInfoId);

                DogBasic dogBasic =
                await
                _context
                .DogBasics
                .SingleAsync(q => q.DogBasicId == dogInfo.DogBasicId);

                Special special = new Special();

                if (dogInfo.SpecialId > 0)
                {
                    special =
                    await
                    _context
                    .Specials
                    .SingleAsync(q => q.SpecialId == dogInfo.SpecialId);
                }


                onSiteDogs.Add(new KennelDashboardDogListItem(dogInfo.DogInfoId, dogVisit.DogVisitId, dogBasic.DogName, special.Instructions, special.Allergies, dogVisit.Notes));
            }

            //DateTime thisDay = DateTime.Today;
            //Incoming Dogs

            List<DogVisit> incomingDogVisitList =
                await
                _context
                .DogVisits
                .Where(q => q.OnSite == false && q.DropOffTime == DateTime.Today)
                .ToListAsync();

            List<KennelDashboardDogListItem> incomingDogs = new List<KennelDashboardDogListItem>();

            foreach (DogVisit dogVisit in incomingDogVisitList)
            {
                DogInfo dogInfo =
                await
                _context
                .DogInfos
                .SingleAsync(q => q.DogInfoId == dogVisit.DogInfoId);

                DogBasic dogBasic =
                await
                _context
                .DogBasics
                .SingleAsync(q => q.DogBasicId == dogInfo.DogBasicId);

                Special special = new Special();

                if (dogInfo.SpecialId > 0)
                {
                special =
                await
                _context
                .Specials
                .SingleAsync(q => q.SpecialId == dogInfo.SpecialId);
                }

                incomingDogs.Add(new KennelDashboardDogListItem(dogInfo.DogInfoId, dogVisit.DogVisitId, dogBasic.DogName, special.Instructions, special.Allergies, dogVisit.Notes));
            }

            //Outgoing Dogs
            List<DogVisit> outgoingDogVisitList =
                await
                _context
                .DogVisits
                .Where(q => q.OnSite == true && q.PickUpTime <= DateTime.Today)
                .ToListAsync();

            List<KennelDashboardDogListItem> outgoingDogs = new List<KennelDashboardDogListItem>();

            foreach (DogVisit dogVisit in outgoingDogVisitList)
            {
                DogInfo dogInfo =
                await
                _context
                .DogInfos
                .SingleAsync(q => q.DogInfoId == dogVisit.DogInfoId);

                DogBasic dogBasic =
                await
                _context
                .DogBasics
                .SingleAsync(q => q.DogBasicId == dogInfo.DogBasicId);

                Special special = new Special();

                if (dogInfo.SpecialId > 0)
                {
                    special =
                    await
                    _context
                    .Specials
                    .SingleAsync(q => q.SpecialId == dogInfo.SpecialId);
                }

                outgoingDogs.Add(new KennelDashboardDogListItem(dogInfo.DogInfoId, dogVisit.DogVisitId, dogBasic.DogName, special.Instructions, special.Allergies, dogVisit.Notes));
            }



            KennelDashboardView model = new KennelDashboardView(onSiteDogs.AsEnumerable(), incomingDogs.AsEnumerable(), outgoingDogs.AsEnumerable());

            return model;
        }

        public async Task<KennelDogDetails> KennelDogDetailsByDogVisitId(int id)
        {
            DogVisit dogVisit =
                await
                _context
                .DogVisits
                .SingleAsync(q => q.DogVisitId == id);

            DogInfo dogInfo =
                await
                _context
                .DogInfos
                .SingleAsync(q => q.DogInfoId == dogVisit.DogInfoId);

            DogBasic dogBasic =
                await
                _context
                .DogBasics
                .SingleAsync(q => q.DogBasicId == dogInfo.DogBasicId);

            Owner owner =
                await
                _context
                .Owners
                .SingleAsync(q => q.OwnerId == dogInfo.OwnerId);

            Food food =
                await
                _context
                .Foods
                .SingleOrDefaultAsync(q => q.FoodId == dogInfo.FoodId);

            Special special =
                await
                _context
                .Specials
                .SingleOrDefaultAsync(q => q.SpecialId == dogInfo.SpecialId);

            Vet vet =
                await
                _context
                .Vets
                .SingleOrDefaultAsync(q => q.VetId == dogInfo.VetId);

            List<MedicationToDogInfo> medicationToDogInfoList =
                await
                _context
                .MedicationToDogInfos
                .Where(q => q.DogInfoId == dogInfo.DogInfoId).ToListAsync();

            List<Medication> medicationList = new List<Medication>();

            if (medicationToDogInfoList.Count() > 0)
            {
                foreach (MedicationToDogInfo item in medicationToDogInfoList)
                {
                    Medication medication =
                    await
                    _context
                    .Medications
                    .SingleOrDefaultAsync(q => q.MedicationId == item.MedicationId);
                    medicationList.Add(medication);
                }
            }

            KennelDogDetails model = new KennelDogDetails(dogInfo, dogVisit, dogBasic, owner, food, special, vet, medicationList.AsEnumerable());

            return model;
        }

        public async Task<DogBasic> GetDogBasicByDogVisitId(int id)
        {
            DogVisit dogVisit =
                await
                _context
                .DogVisits
                .SingleAsync(q => q.DogVisitId == id);

            DogInfo dogInfo =
                await
                _context
                .DogInfos
                .SingleAsync(q => q.DogInfoId == dogVisit.DogInfoId);

            DogBasic dogBasic =
                await
                _context
                .DogBasics
                .SingleAsync(q => q.DogBasicId == dogInfo.DogBasicId);

            return dogBasic;
        }

        public async Task<bool> CheckInDogVisitById(int id)
        {
            DogVisit dogVisit =
                await
                _context
                .DogVisits
                .SingleAsync(q => q.DogVisitId == id);
            dogVisit.OnSite = true;
            dogVisit.CheckInTime = DateTimeOffset.Now;

            return await _context.SaveChangesAsync() == 1;
        }

        public async Task<bool> CheckOutDogVisitById(int id)
        {
            DogVisit dogVisit =
                await
                _context
                .DogVisits
                .SingleAsync(q => q.DogVisitId == id);

            DogVisitComplete dogVisitComplete = new DogVisitComplete();
            dogVisitComplete.OwnerId = await GetOwnerIdByVisitId(id);
            dogVisitComplete.DogName = await GetDogName(id);
            dogVisitComplete.CheckInTime = dogVisit.CheckInTime;
            dogVisitComplete.CheckOutTime = DateTimeOffset.Now;
            dogVisitComplete.TotalHoursOnSite = Convert.ToInt32(Math.Ceiling((DateTimeOffset.Now - dogVisit.CheckInTime).TotalHours));

            _context.DogVisits.Remove(dogVisit);
            _context.DogVisitCompletes.Add(dogVisitComplete);


            return await _context.SaveChangesAsync() ==2;
        }

        public async Task<bool> ResetDogVisitById(int id)
        {
            DogVisit dogVisit =
                await
                _context
                .DogVisits
                .SingleOrDefaultAsync(q => q.DogVisitId == id);

            if(dogVisit == null)
            {
                return false;
            }

            dogVisit.OnSite = false;
            dogVisit.CheckInTime = new DateTime();

            return await _context.SaveChangesAsync() == 1;
        }

        public async Task<List<MealsAndMeds>> AllMorningMealsAndMeds()
        {
            //On Site Dogs
            List<DogVisit> morningMealDogVisitList =
                await
                _context
                .DogVisits
                .Where(q => q.OnSite == true)
                .ToListAsync();

            List<MealsAndMeds> morningMealsAndMedsList = new List<MealsAndMeds>();

            foreach (DogVisit dogVisit in morningMealDogVisitList)
            {
                DogInfo dogInfo =
                await
                _context
                .DogInfos
                .SingleAsync(q => q.DogInfoId == dogVisit.DogInfoId);

                DogBasic dogBasic =
                await
                _context
                .DogBasics
                .SingleAsync(q => q.DogBasicId == dogInfo.DogBasicId);

                Food morningFood = new Food();

                if (_context.Foods.Any(q => q.FoodId == dogInfo.FoodId && q.MorningMeal == true))//Only feed dogs with no Food in the evening
                {
                    morningFood =
                    await
                    _context
                    .Foods
                    .SingleAsync(q => q.FoodId == dogInfo.FoodId);
                }

                List<MedicationToDogInfo> medicationToDogInfoList =
                    await
                    _context
                    .MedicationToDogInfos
                    .Where(q => q.DogInfoId == dogInfo.DogInfoId).ToListAsync();

                List<Medication> medicationList = new List<Medication>();

                //if (medicationToDogInfoList.Count() > 0)
                //{
                    foreach (MedicationToDogInfo item in medicationToDogInfoList)
                    {
                        if (_context.Medications.Any(q => q.MedicationId == item.MedicationId && q.MorningMeal == true))
                        {
                        Medication medication =
                        await
                        _context
                        .Medications
                        .SingleAsync(q => q.MedicationId == item.MedicationId);
                        medicationList.Add(medication);
                        }
                    }
                //}
                if (medicationToDogInfoList.Count() > 0 || morningFood.FoodId > 0)
                {
                morningMealsAndMedsList.Add(new MealsAndMeds(dogInfo, dogVisit, dogBasic, morningFood, medicationList.AsEnumerable()));
                }
            }

            return morningMealsAndMedsList;
        }

        public async Task<List<MealsAndMeds>> AllEveningMealsAndMeds()
        {
            //On Site Dogs
            List<DogVisit> eveningMealDogVisitList =
                await
                _context
                .DogVisits
                .Where(q => q.OnSite == true)
                .ToListAsync();

            List<MealsAndMeds> eveningMealsAndMedsList = new List<MealsAndMeds>();

            foreach (DogVisit dogVisit in eveningMealDogVisitList)
            {
                DogInfo dogInfo =
                await
                _context
                .DogInfos
                .SingleAsync(q => q.DogInfoId == dogVisit.DogInfoId);

                DogBasic dogBasic =
                await
                _context
                .DogBasics
                .SingleAsync(q => q.DogBasicId == dogInfo.DogBasicId);

                Food eveingFood = new Food();

                if (_context.Foods.Any(q => q.FoodId == dogInfo.FoodId && q.EveningMeal == true))//Only feed dogs with no Food in the evening
                {
                    eveingFood =
                    await
                    _context
                    .Foods
                    .SingleAsync(q => q.FoodId == dogInfo.FoodId);
                }
                else if(0 == dogInfo.FoodId)
                {
                    eveingFood.FoodId = 500;
                    eveingFood.Name = "Generic";
                    eveingFood.AmountPerMeal = await FoodForDogByWeight(dogBasic.Weight);//equation to determine food
                }

                List<MedicationToDogInfo> medicationToDogInfoList =
                    await
                    _context
                    .MedicationToDogInfos
                    .Where(q => q.DogInfoId == dogInfo.DogInfoId).ToListAsync();

                List<Medication> medicationList = new List<Medication>();

                //if (medicationToDogInfoList.Count() > 0)
                //{
                foreach (MedicationToDogInfo item in medicationToDogInfoList)
                {
                    if (_context.Medications.Any(q => q.MedicationId == item.MedicationId && q.EveningMeal == true))
                    {
                        Medication medication =
                        await
                        _context
                        .Medications
                        .SingleAsync(q => q.MedicationId == item.MedicationId);
                        medicationList.Add(medication);
                    }
                }
                //}
                if (medicationToDogInfoList.Count() > 0 || eveingFood.FoodId > 0)
                {
                    eveningMealsAndMedsList.Add(new MealsAndMeds(dogInfo, dogVisit, dogBasic, eveingFood, medicationList.AsEnumerable()));
                }
            }

            return eveningMealsAndMedsList;
        }



        //=====HELPER======//
        public async Task<double> FoodForDogByWeight(double weight)
        {
            double result;
            if (weight<=5)
            {
                return .5;
            }
            else if (weight <= 26)
            {
                return (weight/12);
            }
            else if (weight <= 60)
            {
                return weight/18;
            }
            else if (weight <= 85)
            {
                return weight/22.5;
            }
            else if (weight <= 100)
            {
                return weight/23.5;
            }
            else
            {
                return 4.25 + ((weight-100)*.025);
            }
        }

        //Helper the get dog name
        public async Task<string> GetDogName(int dogVisitid)
        {
            DogVisit dogVisit =
                await
                _context
                .DogVisits
                .SingleAsync(a => a.DogVisitId == dogVisitid);

            DogInfo dogInfo =
                await
                _context
                .DogInfos
                .SingleAsync(a => a.DogInfoId == dogVisit.DogInfoId);

            DogBasic dogBasic =
                await
                _context
                .DogBasics
                .SingleAsync(q => q.DogBasicId == dogInfo.DogBasicId);

            return dogBasic.DogName;
        }

        //Helper the get owner of the dog
        public async Task<int> GetOwnerIdByVisitId(int dogVisitid)
        {
            DogVisit dogVisit =
                await
                _context
                .DogVisits
                .SingleAsync(a => a.DogVisitId == dogVisitid);

            DogInfo dogInfo =
                await
                _context
                .DogInfos
                .SingleAsync(a => a.DogInfoId == dogVisit.DogInfoId);

            Owner owner =
                await
                _context
                .Owners
                .SingleAsync(q => q.OwnerId == dogInfo.OwnerId);

            return owner.OwnerId;
        }

    }

}
