using System;
using System.Collections.Generic;
using System.Reflection;
using CsvFormsApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace CsvFormsApp
{
    public partial class DataContext : DbContext
    {
        public DataContext()
        {
        }

        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
        }

        public virtual DbSet<CounterAccount> Accounts { get; set; } = null!;
        public virtual DbSet<Owner> Owners { get; set; } = null!;
        public virtual DbSet<Authorization> Authorizations { get; set; } = null!;
        public virtual DbSet<ClosedVerificationDate> ClosedVerificationDates { get; set; } = null!;
        public virtual DbSet<CountersInGroup> CountersInGroups { get; set; } = null!;
        public virtual DbSet<Flat> Flats { get; set; } = null!;
        public virtual DbSet<Flow> Flows { get; set; } = null!;
        public virtual DbSet<FlowType> FlowTypes { get; set; } = null!;
        public virtual DbSet<Group> Groups { get; set; } = null!;
        public virtual DbSet<House> Houses { get; set; } = null!;
        public virtual DbSet<Counter> Lists { get; set; } = null!;
        public virtual DbSet<Mark> Marks { get; set; } = null!;
        public virtual DbSet<RemoteMeteringInfo> RemoteMeteringInfos { get; set; } = null!;
        public virtual DbSet<ValueOld> ValueOlds { get; set; } = null!;
        public virtual DbSet<VerificationHistory> VerificationHistories { get; set; } = null!;
        public virtual DbSet<ViewAccountSubList> ViewAccountSubLists { get; set; } = null!;
        public virtual DbSet<ViewApartmentSubList> ViewApartmentSubLists { get; set; } = null!;
        public virtual DbSet<ViewHouseSubList> ViewHouseSubLists { get; set; } = null!;
        public virtual DbSet<FileModel> Files { get; set; } = null!;


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            modelBuilder.Entity<CounterAccount>(entity =>
            {
                entity.HasKey(e => new { e.AccountId, e.CounterId })
                    .HasName("PK_Account_3");

                entity.ToTable("Account", "Counter");

                entity.HasIndex(e => new { e.AccountId, e.CounterId }, "IX_Account_AccountID");

                entity.HasIndex(e => new { e.CounterId, e.AccountId }, "IX_Account_CounterID");

                entity.Property(e => e.AccountId).HasColumnName("AccountID");

                entity.Property(e => e.CounterId).HasColumnName("CounterID");

                entity.HasOne(d => d.Counter)
                    .WithMany(p => p.Accounts)
                    .HasForeignKey(d => d.CounterId)
                    .HasConstraintName("FK_AccountCounter_List");
            });

            modelBuilder.Entity<Authorization>(entity =>
            {
                entity.ToTable("Authorization", "Counter");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.HouseId).HasColumnName("HouseID");

                entity.Property(e => e.Login)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Url)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ClosedVerificationDate>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("ClosedVerificationDate", "Counter");

                entity.Property(e => e.AccountId).HasColumnName("AccountID");

                entity.Property(e => e.CloseType).HasMaxLength(50);

                entity.Property(e => e.CounterId).HasColumnName("CounterID");

                entity.Property(e => e.Cvalue)
                    .HasColumnType("numeric(13, 5)")
                    .HasColumnName("CValue");

                entity.Property(e => e.FailureDate).HasColumnType("date");

                entity.Property(e => e.FlowDate).HasColumnType("date");

                entity.Property(e => e.FlowTypeId).HasColumnName("FlowTypeID");

                entity.Property(e => e.PeriodId).HasColumnName("PeriodID");

                entity.Property(e => e.SublistId).HasColumnName("SublistID");

                entity.Property(e => e.Value).HasColumnType("numeric(13, 5)");

                entity.Property(e => e.VerificationDate).HasColumnType("date");
            });

            modelBuilder.Entity<CountersInGroup>(entity =>
            {
                entity.ToTable("CountersInGroup", "Counter");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.ChildCounterId).HasColumnName("ChildCounterID");

                entity.Property(e => e.GroupId).HasColumnName("GroupID");

                entity.Property(e => e.ParentCounterId).HasColumnName("ParentCounterID");

                entity.Property(e => e.SubListId).HasColumnName("SubListID");

                entity.HasOne(d => d.Group)
                    .WithMany(p => p.CountersInGroups)
                    .HasForeignKey(d => d.GroupId)
                    .HasConstraintName("FK_CountersInGroup_Group");
            });

            modelBuilder.Entity<Flat>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Flat", "Counter");

                entity.Property(e => e.CounterId).HasColumnName("CounterID");

                entity.Property(e => e.FlatId).HasColumnName("FlatID");

                entity.HasOne(d => d.Counter)
                    .WithMany()
                    .HasForeignKey(d => d.CounterId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Flat_List");
            });

            modelBuilder.Entity<Flow>(entity =>
            {
                entity.HasKey(e => new { e.CounterId, e.PeriodId })
                    .HasName("PK_Flow_Counter");

                entity.ToTable("Flow", "Counter");

                entity.HasIndex(e => e.FlowTypeId, "IDX_FlowType");

                entity.HasIndex(e => new { e.PeriodId, e.FlowTypeId }, "IDX_PeriodID_FlowType");

                entity.HasIndex(e => new { e.PeriodId, e.SubListId }, "IDX_PeriodID_SublistID");

                entity.HasIndex(e => new { e.PeriodId, e.SubListId, e.CounterId }, "IDX_Period_Sublist_Counter_Values")
                    .IsUnique();

                entity.Property(e => e.CounterId).HasColumnName("CounterID");

                entity.Property(e => e.PeriodId).HasColumnName("PeriodID");

                entity.Property(e => e.Cvalue)
                    .HasColumnType("numeric(13, 5)")
                    .HasColumnName("CValue");

                entity.Property(e => e.Date).HasColumnType("date");

                entity.Property(e => e.Flow1)
                    .HasColumnType("numeric(25, 11)")
                    .HasColumnName("Flow")
                    .HasComputedColumnSql("(case when ((isnull([Value],(0))-isnull([PrevValue],(0)))-isnull([CValue],(0)))*[Rate]<(0) then (0) else ((isnull([Value],(0))-isnull([PrevValue],(0)))-isnull([CValue],(0)))*[Rate] end)", true);

                entity.Property(e => e.FlowTypeId)
                    .HasColumnName("FlowTypeID")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.PrevDate).HasColumnType("date");

                entity.Property(e => e.PrevValue).HasColumnType("numeric(13, 5)");

                entity.Property(e => e.Rate).HasColumnType("numeric(9, 6)");

                entity.Property(e => e.SecurityDate).HasColumnType("datetime");

                entity.Property(e => e.SessionId).HasColumnName("SessionID");

                entity.Property(e => e.SubListId).HasColumnName("SubListID");

                entity.Property(e => e.Value).HasColumnType("numeric(13, 5)");

                entity.HasOne(d => d.Counter)
                    .WithMany(p => p.Flows)
                    .HasForeignKey(d => d.CounterId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Flow_List");
            });

            modelBuilder.Entity<FlowType>(entity =>
            {
                entity.ToTable("FlowType", "Counter");

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ID");

                entity.Property(e => e.Name)
                    .HasMaxLength(150)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Group>(entity =>
            {
                entity.ToTable("Group", "Counter");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Name)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.SubListId).HasColumnName("SubListID");

                entity.Property(e => e.SublistIdby)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("SublistIDby");
            });

            modelBuilder.Entity<House>(entity =>
            {
                entity.HasKey(e => new { e.HouseId, e.CounterId })
                    .HasName("PK_House_Counter");

                entity.ToTable("House", "Counter");

                entity.Property(e => e.HouseId).HasColumnName("HouseID");

                entity.Property(e => e.CounterId).HasColumnName("CounterID");

                entity.HasOne(d => d.Counter)
                    .WithMany(p => p.Houses)
                    .HasForeignKey(d => d.CounterId)
                    .HasConstraintName("FK_CounterHouse_List");
            });

            modelBuilder.Entity<Counter>(entity =>
            {
                entity.ToTable("List", "Counter");

                entity.HasIndex(e => new { e.DateEnd, e.IsShowValue }, "IDX_DateEnd");

                entity.HasIndex(e => e.IsShowValue, "IDX_IsShowValue");

                entity.HasIndex(e => new { e.SubListId, e.Active, e.IsCalculation }, "IDX_Sublist_Active");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Active)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.AntiMagnetStampNumber)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CloseDescriptionId).HasColumnName("CloseDescriptionID");

                entity.Property(e => e.DateBegin).HasColumnType("date");

                entity.Property(e => e.DateEnd).HasColumnType("date");

                entity.Property(e => e.FactorySealDate).HasColumnType("date");

                entity.Property(e => e.FailureDate).HasColumnType("date");

                entity.Property(e => e.InstallationDate).HasColumnType("date");

                entity.Property(e => e.IsCalculation)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.IsExportToGis)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.IsShowValue).HasDefaultValueSql("((1))");

                entity.Property(e => e.MarkId).HasColumnName("MarkID");

                entity.Property(e => e.Model)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Number).HasDefaultValueSql("((1))");

                entity.Property(e => e.PressureSensingElementInfo)
                    .HasMaxLength(2000)
                    .IsUnicode(false);

                entity.Property(e => e.Rate)
                    .HasColumnType("numeric(9, 6)")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.RemoteMeteringInfoId).HasColumnName("RemoteMeteringInfoID");

                entity.Property(e => e.SerialNumber)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ServiceGuid).HasDefaultValueSql("(newid())");

                entity.Property(e => e.StampNumber)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.SubListId).HasColumnName("SubListID");

                entity.Property(e => e.TemperatureSensingElementInfo)
                    .HasMaxLength(2000)
                    .IsUnicode(false);

                entity.Property(e => e.UniqueNumber)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.UnitId).HasColumnName("UnitID");

                entity.Property(e => e.VerificationDate).HasColumnType("date");

                entity.HasOne(d => d.Mark)
                    .WithMany(p => p.Lists)
                    .HasForeignKey(d => d.MarkId)
                    .HasConstraintName("FK_List_Mark");

                entity.HasOne(d => d.RemoteMeteringInfo)
                    .WithMany(p => p.Lists)
                    .HasForeignKey(d => d.RemoteMeteringInfoId)
                    .HasConstraintName("FK_List_RemoteMeteringInfo");
            });

            modelBuilder.Entity<Mark>(entity =>
            {
                entity.ToTable("Mark", "Counter");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<RemoteMeteringInfo>(entity =>
            {
                entity.ToTable("RemoteMeteringInfo", "Counter");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .HasMaxLength(2000)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ValueOld>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("ValueOld", "Counter");

                entity.Property(e => e.CounterId).HasColumnName("CounterID");

                entity.Property(e => e.Cvalue)
                    .HasColumnType("numeric(13, 5)")
                    .HasColumnName("CValue");

                entity.Property(e => e.Date).HasColumnType("date");

                entity.Property(e => e.FlowTypeId).HasColumnName("FlowTypeID");

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ID");

                entity.Property(e => e.Value).HasColumnType("numeric(13, 5)");
            });

            modelBuilder.Entity<VerificationHistory>(entity =>
            {
                entity.ToTable("VerificationHistory", "Counter");

                entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Counter)
                    .WithMany(p => p.VerificationHistories)
                    .HasForeignKey(d => d.CounterId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_VerificationHistory_Counter");
            });

            modelBuilder.Entity<ViewAccountSubList>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("View_Account_SubList", "Counter");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Name)
                    .HasMaxLength(150)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ViewApartmentSubList>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("View_Apartment_SubList", "Counter");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Name)
                    .HasMaxLength(150)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ViewHouseSubList>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("View_House_SubList", "Counter");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Name)
                    .HasMaxLength(150)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Owner>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("Owners", "Adress");

                entity.Property(e => e.AccountId).HasColumnName("AccountID");

                entity.Property(e => e.AccountName)
                    .HasMaxLength(12)
                    .IsUnicode(false);

                entity.Property(e => e.Fio).HasColumnName("FIO");

                entity.Property(e => e.FlatBookId).HasColumnName("FlatBookID");

                entity.Property(e => e.FlatId).HasColumnName("FlatID");

                entity.Property(e => e.FlatName)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.HouseId).HasColumnName("HouseID");

                entity.Property(e => e.OldAccountName)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.ShortNameAdress)
                    .HasMaxLength(320)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
