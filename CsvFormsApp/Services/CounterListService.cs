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
        public async Task GetObjectList(string path, int period, string connectionString,bool currentFlow
                                        ,int sublistID,int unitID,decimal rate)
        {
            try
            {
                var optionsBuilder = new DbContextOptionsBuilder<DataContext>();
                var options = optionsBuilder.UseSqlServer(connectionString).Options;
                int counterCount = 0;
                int accountCount = 0;
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
                        using (DataContext dataContext = new DataContext(options))
                        {
                            foreach (var record in records)
                            {
                                var tempMark = dataContext.Marks.FirstOrDefault(u => u.Name == record.Mark);
                                if (tempMark == null)
                                {
                                    var tempLastMark = new Mark()
                                    {
                                        Name =record.Mark
                                    };
                                    await dataContext.Marks.AddAsync(tempLastMark);
                                    await dataContext.SaveChangesAsync();
                                    tempMark = dataContext.Marks.FirstOrDefault(u => u.Name == record.Mark);
                                }
                                record.MarkID = tempMark.Id.ToString();
                                counters.Add(record);
                            }
                        }
                    }
                }
                using (DataContext dataContext = new DataContext(options))
                {
                    foreach (var record in counters)
                    {
                        //counters.Add(record);
                        //var tempMark = dataContext.Marks.FirstOrDefaultAsync(u => u.Name == record.Mark);
                        //record.MarkID = tempMark.Id.ToString();
                        var counterListEnd = new List()
                        {
                            DateBegin = DateTime.Parse(record.DateBegin),
                            SubListId = sublistID,
                            Number = short.Parse(record.Number),
                            Rate = rate,
                            UnitId = unitID,
                            SerialNumber = record.SerialNumber,
                            InstallationDate = DateTime.TryParse(record.InstallationDate, out var installationDateResult) ? (installationDateResult) : (null),
                            VerificationDate = DateTime.TryParse(record.VerificationDate, out var verificationDateResult) ? (verificationDateResult) : (null),
                            StampNumber = record.StampNumber,
                            AntiMagnetStampNumber = record.AntiMagnetStampNumber,
                            VerificationInterval = int.TryParse(record.VerificationInterval, out var verificationIntervalResult) ? (verificationIntervalResult) : (null),
                            MarkId=int.Parse(record.MarkID),
                            //MarkId = int.TryParse(record.MarkID, out var markIDResult) ? (markIDResult) : (null),
                            Model = record.Model,
                        };
                        countersList.Add(counterListEnd);
                        counterCount++;
                    }

                    await dataContext.Lists.AddRangeAsync(countersList);
                    await dataContext.SaveChangesAsync();
                }



                using (DataContext dataContext = new DataContext(options))
                {
                    //await dataContext.Lists.AddRangeAsync(countersList);
                    //await dataContext.SaveChangesAsync();
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
                                                                             u.HouseId.ToString() == counter.HouseID);
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
                                        SubListId = sublistID,
                                        PrevDate = DateTime.Parse(counter.PrevDate),
                                        PrevValue = decimal.Parse(counter.PrevValue),
                                        Rate = rate,
                                        FlowTypeId = 0
                                    };
                                }
                                else
                                {
                                    counterFlowTillEnd = new Flow()
                                    {
                                        CounterId = int.Parse(counter.CounterID),
                                        PeriodId = period,
                                        SubListId = sublistID,
                                        PrevDate = DateTime.Parse(counter.PrevDate),
                                        PrevValue = decimal.Parse(counter.PrevValue),
                                        Date = DateTime.Parse(counter.Date),
                                        Value = decimal.Parse(counter.Value),
                                        Rate = rate,
                                        FlowTypeId = 1
                                    };
                                }
                                var counterFlowEnd = new Flow()
                                {
                                    CounterId = int.Parse(counter.CounterID),
                                    PeriodId = period - 1,
                                    SubListId = sublistID,
                                    PrevValue = decimal.Parse(counter.PrevValue),
                                    Value = decimal.Parse(counter.PrevValue),
                                    PrevDate = DateTime.Parse(counter.PrevDate),
                                    Date = DateTime.Parse(counter.PrevDate),
                                    Rate = rate,
                                    FlowTypeId = 5
                                };
                                accountCount++;
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

                MessageBox.Show(
                    $"Успешно загружено {counterCount} ИПУ!\n" +
                    $"Из них закреплено за л/с {accountCount}.",
                    "Уведомление",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information,
                    MessageBoxDefaultButton.Button1,
                    MessageBoxOptions.DefaultDesktopOnly) ;
        }
            catch (Exception ex)
            {
                
                MessageBox.Show(ex.Message,
                    ex.GetType().Name,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error,
                    MessageBoxDefaultButton.Button1,
                    MessageBoxOptions.DefaultDesktopOnly);
            }
}
    }
}
