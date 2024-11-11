using CsvHelper;
using CsvFormsApp.Domain.Entities;
using System.Globalization;
using System.Text;
using CsvHelper.Configuration;
using Microsoft.EntityFrameworkCore;

namespace CsvFormsApp.Services;

public class CounterListService : IObjectService
{
    public CounterListService()
    {
    }
    public async Task<string> GetObjectList(string path, int period, string connectionString, bool isCurrentValues, 
                                    int sublistID, int unitID, decimal rate)
    {
        var optionsBuilder = new DbContextOptionsBuilder<DataContext>();

        var options = optionsBuilder.UseSqlServer(connectionString).Options;

        int counterCount = 0;
        int accountCount = 0;

        List<Counter> countersList = new List<Counter>();
        
        Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

		var csvConfig = new CsvConfiguration(CultureInfo.GetCultureInfo("ru-RU"))
		{
			HasHeaderRecord = true,
			HeaderValidated = null,
			MissingFieldFound = null,
			Delimiter = ";"
		};

		using var context = new DataContext(options);
        using (StreamReader sr = new StreamReader(path, Encoding.GetEncoding("windows-1251")))
        using (var csvReader = new CsvReader(sr, csvConfig))
        {
            var records = csvReader.GetRecords<Counters>();

            var index = context?.Marks?
                .Where(x => !string.IsNullOrEmpty(x.Name))
                .ToList()
                .GroupBy(x => x.Name)
                .ToDictionary(x => x.Key ?? string.Empty, x => x.FirstOrDefault())
                    ?? new Dictionary<string, Mark?>();

		    if (context is null)
		        throw new Exception("DbContext is not recognized");

		    var periodDateEnd = await context.PeriodList.FirstOrDefaultAsync(u => u.Id == period)
                ?? throw new ArgumentException("Не найден период");

            Mark tempMark = default!;

            foreach (var record in records)
            {
                if (record.Number == null
                    || !short.TryParse(record.Number, out var number))
                    throw new ArgumentException("Некорректное значение номера счетчика");

                if (!string.IsNullOrEmpty(record.Mark))
                {
                    index.TryGetValue(record.Mark, out tempMark!);

                    if (tempMark is null)
                    {
                        tempMark = new Mark()
                        {
                            Name = record.Mark
                        };
                        index.Add(tempMark.Name, tempMark);
                    }
                }

                var counter = new Counter()
                {
                    DateBegin = DateTime.TryParse(record.DateBegin, out var dateBeginResult) ? (dateBeginResult) : (periodDateEnd!.DateBegin),
                    SubListId = sublistID,
                    Number = number,
                    CounterType = 1,
                    Rate = rate,
                    UnitId = unitID,
                    SerialNumber = record.SerialNumber,
                    InstallationDate = DateTime.TryParse(record.InstallationDate, out var installationDateResult) ? (installationDateResult) : (null),
                    VerificationDate = DateTime.TryParse(record.VerificationDate, out var verificationDateResult) ? (verificationDateResult) : (null),
                    StampNumber = record.StampNumber,
                    AntiMagnetStampNumber = record.AntiMagnetStampNumber,
                    FactorySealDate = DateTime.TryParse(record.FactorySealDate, out var factorySealedDateResult) ? (factorySealedDateResult) : (null),
                    VerificationInterval = int.TryParse(record.VerificationInterval, out var verificationIntervalResult) ? (verificationIntervalResult) : (null),
                    Mark = tempMark,
                    Model = record.Model,
                };

                if (counter is not null)
                {
                    counterCount++;

                    await context.Lists.AddAsync(counter);

                    var account = context.Owners
                        .FirstOrDefault(u => u.AccountName == record.AccountName);

                    if (account == null)
                    {
                        account = context.Owners
                            .FirstOrDefault(u => u.FlatBookId.ToString() == record.FlatBookID
                                && u.FlatName == record.FlatName
                                && u.HouseId.ToString() == record.HouseID
                                && u.IsClosed == 1);
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

                    record.PrevValue = record?.PrevValue?.Replace(".", ",");

                    record!.Value = record?.Value?.Replace(".", ",");

                    if (!decimal.TryParse(record!.PrevValue!, out var prevValue))
                        throw new ArgumentException("Некорректное значение предыдущих показаний");

                    Flow counterFlow;
                    if (isCurrentValues)
                    {
		    		    if (!decimal.TryParse(record.Value, out var value))
		    		    	throw new ArgumentException("Некорректное значение текущих показаний");

		    		    if (!DateTime.TryParse(record.Date, out var date))
		    		    	throw new ArgumentException("Некорректное значение текущей даты показаний");

		    		    counterFlow = new Flow()
                        {
                            Counter = counter,
                            PeriodId = period,
                            SubListId = sublistID,
                            PrevDate = periodDateEnd?.DateEnd.AddMonths(-1),
                            PrevValue = prevValue,
                            Date = date,
                            Value = value,
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
                            PrevDate = periodDateEnd?.DateEnd.AddMonths(-1),
                            PrevValue = prevValue,
                            Rate = rate,
                            FlowTypeId = 0
                        };


                    }

                    var counterFlowEnd = new Flow()
                    {
                        Counter = counter,
                        PeriodId = period - 1,
                        SubListId = sublistID,
                        PrevValue = prevValue,
                        Value = prevValue,
                        PrevDate = periodDateEnd?.DateEnd.AddMonths(-1),
                        Date = periodDateEnd?.DateEnd.AddMonths(-1),
                        Rate = rate,
                        FlowTypeId = 5
                    };

                    await context.Flows.AddAsync(counterFlow);
                    await context.Flows.AddAsync(counterFlowEnd);
                }
            }
                await context.SaveChangesAsync();
        }

        return $"Успешно загружено {counterCount} ИПУ!\n" +
                $"Из них закреплено за л/с {accountCount}.";
    }
}
