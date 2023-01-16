using CsvHelper;
using CsvFormsApp.Domain.Entities;
using System.Globalization;
using CsvFormsApp.Domain;
using System.Text;
using CsvHelper.Configuration;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace CsvFormsApp.Services
{
    public class CounterListService : IObjectService
    {
        public CounterListService()
        {
        }
        public async Task GetObjectList(string path, int period, string connectionString, bool isCurrentValues, 
                                        int sublistID, int unitID, decimal rate)
        {
            try
            {
                var optionsBuilder = new DbContextOptionsBuilder<DataContext>();
                var options = optionsBuilder.UseSqlServer(connectionString).Options;
                int counterCount = 0;
                int accountCount = 0;
                //List<Counters> counters = new List<Counters>();
                List<Counter> countersList = new List<Counter>();
                //List<Flow> countersFlow = new List<Flow>();
                Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

                using var context = new DataContext(options);

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


                        var index = context.Marks
                            .Where(x => x.Name != null)
                            .ToList()
                            .GroupBy(x => x.Name)
                            .ToDictionary(x => x.Key, x => x.FirstOrDefault());

                        var periodDateEnd = await context.PeriodList.FirstOrDefaultAsync(u => u.Id == period - 1);

                        foreach (var record in records)
                        {
                            index.TryGetValue(record.Mark, out var tempMark);

                            if (tempMark == null)
                            {
                                tempMark = new Mark()
                                {
                                    Name = record.Mark
                                };
                                index.Add(tempMark.Name,tempMark);
                            }

                            var counter = new Counter()
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
                                FactorySealDate=DateTime.TryParse(record.FactorySealDate, out var factorySealedDateResult)?(factorySealedDateResult):(null),
                                VerificationInterval = int.TryParse(record.VerificationInterval, out var verificationIntervalResult) ? (verificationIntervalResult) : (null),
                                Mark = tempMark,
                                Model = record.Model,
                            };

                            if (counter is not null)
                            {
                                counterCount++;
                            }
                            await context.Lists.AddAsync(counter);
                            //Debug.Write(counter.DateBegin);
                            var account = context.Owners.FirstOrDefault(u => u.AccountName == record.AccountName);
                            if (account == null)
                            {
                                account = context.Owners.FirstOrDefault(u => u.FlatBookId.ToString() == record.FlatBookID &&
                                                                             u.FlatName == record.FlatName &&
                                                                             u.HouseId.ToString() == record.HouseID);
                            }

                            if (account == null)
                            {
                                continue;
                            }

                            var counterAccount = new CounterAccount()
                            {
                                AccountId = account.AccountId,
                                Counter = counter
                            };
                            accountCount++;


                            await context.AddAsync(counterAccount);

                            if (record.PrevValue.Contains("."))
                            {
                                record.PrevValue=record.PrevValue.Replace(".",",");
                            }

                            if (record.Value is not null && record.Value.Contains("."))
                            {
                                record.Value = record.Value.Replace(".", ",");
                            }


                            Flow counterFlow;
                            if (isCurrentValues)
                            {

                                counterFlow = new Flow()
                                {
                                    Counter = counter,
                                    PeriodId = period,
                                    SubListId = sublistID,
                                    PrevDate = periodDateEnd.DateEnd,
                                    PrevValue = decimal.Parse(record.PrevValue),
                                    Date = DateTime.Parse(record.Date),
                                    Value = decimal.Parse(record.Value),
                                    Rate = rate,
                                    FlowTypeId = 1
                                };
                            }
                            else
                            {

                                counterFlow = new Flow()
                                {
                                    Counter = counter,
                                    PeriodId = period,
                                    SubListId = sublistID,
                                    PrevDate = periodDateEnd.DateEnd,
                                    PrevValue = decimal.Parse(record.PrevValue),
                                    Rate = rate,
                                    FlowTypeId = 0
                                };


                            }

                            var counterFlowEnd = new Flow()
                            {
                                Counter = counter,
                                PeriodId = period - 1,
                                SubListId = sublistID,
                                PrevValue = decimal.Parse(record.PrevValue),
                                Value = decimal.Parse(record.PrevValue),
                                PrevDate = periodDateEnd.DateEnd,
                                Date = periodDateEnd.DateEnd,
                                Rate = rate,
                                FlowTypeId = 5
                            };


                            await context.Flows.AddAsync(counterFlow);
                            await context.Flows.AddAsync(counterFlowEnd);
                        }

                        await context.SaveChangesAsync();
                    }
                }
                MessageBox.Show(
                    $"Успешно загружено {counterCount} ИПУ!\n" +
                    $"Из них закреплено за л/с {accountCount}.",
                    "Уведомление",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information,
                    MessageBoxDefaultButton.Button1,
                    MessageBoxOptions.DefaultDesktopOnly);

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
