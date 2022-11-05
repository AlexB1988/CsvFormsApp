using CsvHelper;
using CsvFormsApp.Domain.Entities;
using System.Globalization;
using CsvFormsApp.Domain;
using System.Text;
using CsvHelper.Configuration;
using Microsoft.EntityFrameworkCore;

namespace CsvFormsApp.Services
{
    public class CounterListService : IObjectService
    {
        public CounterListService()
        {
        }
        public async Task GetObjectList(string path, int period, string connectionString,bool currentFlow)
        {
            var optionsBuilder = new DbContextOptionsBuilder<DataContext>();
            var options = optionsBuilder.UseSqlServer(connectionString).Options;

            List<Counters> counters = new List<Counters>();
            List<List> countersList = new List<List>();
            //List<Flow> countersFlow = new List<Flow>();
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            using (StreamReader sr = new StreamReader(path, Encoding.GetEncoding("windows-1251")))
            {
                var csvConfig = new CsvConfiguration(CultureInfo.GetCultureInfo("ru-RU"))
                {
                    HasHeaderRecord = true,
                    HeaderValidated = null,
                    MissingFieldFound = null,
                    Delimiter = ";"
                };

                using (var csvReader = new CsvReader(sr, csvConfig))
                {
                    var records = csvReader.GetRecords<Counters>();
                    foreach (var record in records)
                    {
                        counters.Add(record);
                        var counterListEnd = new List()
                        {
                            DateBegin = DateTime.Parse(record.DateBegin),
                            SubListId = int.Parse(record.SublistID),
                            Number = short.Parse(record.Number),
                            Rate = decimal.Parse(record.Rate),
                            UnitId = int.TryParse(record.UnitID, out var unitIDResult) ? (unitIDResult) : (null),
                            SerialNumber = record.SerialNumber,
                            InstallationDate = DateTime.TryParse(record.InstallationDate, out var installationDateResult) ? (installationDateResult) : (null),
                            VerificationDate = DateTime.TryParse(record.VerificationDate, out var verificationDateResult) ? (verificationDateResult) : (null),
                            StampNumber = record.StampNumber,
                            AntiMagnetStampNumber = record.AntiMagnetStampNumber,
                            VerificationInterval = int.TryParse(record.VerificationInterval, out var verificationIntervalResult) ? (verificationIntervalResult) : (null),
                            MarkId = int.TryParse(record.MarkID, out var markIDResult) ? (markIDResult) : (null),
                            Model = record.Model,
                        };
                        countersList.Add(counterListEnd);
                    }
                    //using (DataContext dataContext = new DataContext(options))
                    //{
                    //    await dataContext.Lists.AddRangeAsync(countersList);
                    //    await dataContext.SaveChangesAsync();
                    //}
                }
            }

            using (DataContext dataContext = new DataContext(options))
            {
                await dataContext.Lists.AddRangeAsync(countersList);
                await dataContext.SaveChangesAsync();
                foreach (var counter in counters)
                {
                    var list = countersList.FirstOrDefault(u => u.SerialNumber == counter.SerialNumber);
                    if (list != null)
                    {
                        counter.CounterID = list.Id.ToString();
                        var account = dataContext.Owners.FirstOrDefault(u => u.AccountName == counter.AccountName);
                        if (account == null)
                        {
                            account = dataContext.Owners.FirstOrDefault(u => u.FlatBookId.ToString() == counter.FlatBookID &&
                                                                         u.FlatName == counter.FlatName &&
                                                                         u.HouseId.ToString()==counter.HouseID);
                        }
                        if (account != null)
                        {
                            Console.WriteLine(account.AccountName);
                            counter.AccountID = account.AccountId.ToString();
                            var counterAccount = new Account()
                            {
                                AccountId = int.Parse(counter.AccountID),
                                CounterId = int.Parse(counter.CounterID)
                            };
                            Flow counterFlowTillEnd;
                            if (currentFlow is not true)
                            {
                                counterFlowTillEnd = new Flow()
                                {
                                    CounterId = int.Parse(counter.CounterID),
                                    PeriodId = period,
                                    SubListId = int.Parse(counter.SublistID),
                                    PrevDate = DateTime.Parse(counter.PrevDate),
                                    PrevValue = decimal.Parse(counter.PrevValue),
                                    Rate = decimal.Parse(counter.Rate),
                                    FlowTypeId = 0
                                };
                            }
                            else
                            {
                                counterFlowTillEnd = new Flow()
                                {
                                    CounterId = int.Parse(counter.CounterID),
                                    PeriodId = period,
                                    SubListId = int.Parse(counter.SublistID),
                                    PrevDate = DateTime.Parse(counter.PrevDate),
                                    PrevValue = decimal.Parse(counter.PrevValue),
                                    Date = DateTime.Parse(counter.Date),
                                    Value = decimal.Parse(counter.Value),
                                    Rate = decimal.Parse(counter.Rate),
                                    FlowTypeId = 1
                                };
                            }
                            var counterFlowEnd = new Flow()
                            {
                                CounterId = int.Parse(counter.CounterID),
                                PeriodId = period - 1,
                                SubListId = int.Parse(counter.SublistID),
                                PrevValue = decimal.Parse(counter.PrevValue),
                                Value = decimal.Parse(counter.PrevValue),
                                PrevDate = DateTime.Parse(counter.PrevDate),
                                Rate = decimal.Parse(counter.Rate),
                                FlowTypeId = 5
                            };
                            await dataContext.Accounts.AddAsync(counterAccount);
                            await dataContext.Flows.AddAsync(counterFlowTillEnd);
                            await dataContext.Flows.AddAsync(counterFlowEnd);
                        }
                    }
                }
                await dataContext.SaveChangesAsync();
                counters.Clear();
                countersList.Clear();
            }
        }
    }
}
