using System;
using System.Collections.Generic;

namespace CsvFormsApp
{
    public partial class List
    {
        public List()
        {
            Accounts = new HashSet<Account>();
            Flows = new HashSet<Flow>();
            Houses = new HashSet<House>();
            VerificationHistories = new HashSet<VerificationHistory>();
        }

        public int Id { get; set; }
        public DateTime DateBegin { get; set; }
        public DateTime? DateEnd { get; set; }
        public int SubListId { get; set; }
        public bool? Active { get; set; }
        public short Number { get; set; }
        public decimal Rate { get; set; }
        public int? UnitId { get; set; }
        public string? SerialNumber { get; set; }
        public DateTime? InstallationDate { get; set; }
        public DateTime? VerificationDate { get; set; }
        public string? StampNumber { get; set; }
        public string? AntiMagnetStampNumber { get; set; }
        public int? VerificationInterval { get; set; }
        public int? CloseDescriptionId { get; set; }
        public int? MarkId { get; set; }
        public string? Model { get; set; }
        public byte IsShowValue { get; set; }
        public byte CounterType { get; set; }
        public DateTime? FactorySealDate { get; set; }
        public bool? TemperatureSensor { get; set; }
        public bool? PressureSensor { get; set; }
        public bool? RemoteMeteringMode { get; set; }
        public string? TemperatureSensingElementInfo { get; set; }
        public string? PressureSensingElementInfo { get; set; }
        public int? RemoteMeteringInfoId { get; set; }
        public Guid? VersionGuid { get; set; }
        public Guid? RootGuid { get; set; }
        public string? UniqueNumber { get; set; }
        public Guid ServiceGuid { get; set; }
        public bool? IsExportToGis { get; set; }
        public bool? IsCalculation { get; set; }
        public DateTime? FailureDate { get; set; }

        public virtual Mark? Mark { get; set; }
        public virtual RemoteMeteringInfo? RemoteMeteringInfo { get; set; }
        public virtual ICollection<Account> Accounts { get; set; }
        public virtual ICollection<Flow> Flows { get; set; }
        public virtual ICollection<House> Houses { get; set; }
        public virtual ICollection<VerificationHistory> VerificationHistories { get; set; }
    }
}
