﻿using CsvHelper;
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
        DataContext _dataContext;

        public CounterListService()
        {
        }
        public async Task GetObjectList(string path, int period, string connectionString)
        {
            //string path = $"Files/{file.FileName}";
            //Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            List<Counters> counters = new List<Counters>();
            List<List> countersList = new List<List>();
            List<Flow> countersFlow = new List<Flow>();
            //using (var fileStreem = new FileStream(path, FileMode.Create))
            //{
            //    await file.CopyToAsync(fileStreem);
            //}

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
                            VerificationInterval = int.TryParse(record.DateBegin, out var verificationIntervalResult) ? (verificationIntervalResult) : (null),
                            MarkId = int.TryParse(record.MarkID, out var markIDResult) ? (markIDResult) : (null),
                            Model = record.Model,
                        };
                        countersList.Add(counterListEnd);
                    }
                    //await _dataContext.Lists.AddRangeAsync(countersList);
                    //await _dataContext.SaveChangesAsync(); // Нельзя убрать, т.к. в дальнейшем ID из этой таблицы будет привязан к Account

                }
                var optionsBuilder = new DbContextOptionsBuilder<DataContext>();
                var options = optionsBuilder.UseSqlServer(connectionString).Options;
                using (DataContext dataContext = new DataContext(options))
                {
                    await dataContext.Lists.AddRangeAsync(countersList);
                    await dataContext.SaveChangesAsync();

                    foreach (var counter in counters)
                    {
                        var list = countersList.FirstOrDefault(u => u.SerialNumber == counter.SerialNumber);
                        //var account = dataContext.Owners.FirstOrDefault(u => u.AccountName == counter.AccountName);
                        //Console.WriteLine(account.AccountName);
                        if (list != null)
                        {
                            counter.CounterID = list.Id.ToString();
                            //counter.AccountID = account.AccountId.ToString();
                            //var list = countersList.FirstOrDefault(u => u.SerialNumber == counter.SerialNumber);
                            var account = dataContext.Owners.FirstOrDefault(u => u.AccountName == counter.AccountName);
                            if (account != null)
                            {
                                Console.WriteLine(account.AccountName);
                                //counter.CounterID = list.Id.ToString();
                                counter.AccountID = account.AccountId.ToString();
                                var counterAccount = new Account()
                                {
                                    AccountId = int.Parse(counter.AccountID),
                                    CounterId = int.Parse(counter.CounterID)
                                };
                                var counterFlowTillEnd = new Flow()
                                {
                                    CounterId = int.Parse(counter.CounterID),
                                    PeriodId = period,
                                    SubListId = int.Parse(counter.SublistID),
                                    PrevDate = DateTime.Parse(counter.PrevDate),
                                    PrevValue = decimal.Parse(counter.Value),
                                    Rate = decimal.Parse(counter.Rate),
                                    FlowTypeId = 0
                                };
                                var counterFlowEnd = new Flow()
                                {
                                    CounterId = int.Parse(counter.CounterID),
                                    PeriodId = period - 1,
                                    SubListId = int.Parse(counter.SublistID),
                                    PrevValue = decimal.Parse(counter.Value),
                                    Value = decimal.Parse(counter.Value),
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

                    //await _dataContext.Counters.AddRangeAsync(counters);
                    //await _dataContext.CountersLIst.AddRangeAsync(countersLIst)
                    await dataContext.SaveChangesAsync();
                }
            }
        }
    }
}