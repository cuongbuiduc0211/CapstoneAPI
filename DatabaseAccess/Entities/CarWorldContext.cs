using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace DatabaseAccess.Entities
{
    public partial class CarWorldContext : DbContext
    {
        public CarWorldContext()
        {
        }

        public CarWorldContext(DbContextOptions<CarWorldContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Accessory> Accessories { get; set; }
        public virtual DbSet<Attribution> Attributions { get; set; }
        public virtual DbSet<Brand> Brands { get; set; }
        public virtual DbSet<CarModel> CarModels { get; set; }
        public virtual DbSet<City> Cities { get; set; }
        public virtual DbSet<ContestEvent> ContestEvents { get; set; }
        public virtual DbSet<ContestEventRegister> ContestEventRegisters { get; set; }
        public virtual DbSet<ContestPrize> ContestPrizes { get; set; }
        public virtual DbSet<District> Districts { get; set; }
        public virtual DbSet<EngineType> EngineTypes { get; set; }
        public virtual DbSet<Exchange> Exchanges { get; set; }
        public virtual DbSet<ExchangeAccessorryDetail> ExchangeAccessorryDetails { get; set; }
        public virtual DbSet<ExchangeCarDetail> ExchangeCarDetails { get; set; }
        public virtual DbSet<ExchangeResponse> ExchangeResponses { get; set; }
        public virtual DbSet<Feedback> Feedbacks { get; set; }
        public virtual DbSet<Generation> Generations { get; set; }
        public virtual DbSet<GenerationAttribution> GenerationAttributions { get; set; }
        public virtual DbSet<InterestedBrand> InterestedBrands { get; set; }
        public virtual DbSet<Post> Posts { get; set; }
        public virtual DbSet<Prize> Prizes { get; set; }
        public virtual DbSet<Proposal> Proposals { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Ward> Wards { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=cosplane.asia;Database=CarWorld;User Id=buicuong;Password=BuiCuong!@#123");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Accessory>(entity =>
            {
                entity.ToTable("Accessory");

                entity.Property(e => e.Id)
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.Property(e => e.BrandId)
                    .IsRequired()
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.Property(e => e.Image)
                    .IsRequired()
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.HasOne(d => d.Brand)
                    .WithMany(p => p.Accessories)
                    .HasForeignKey(d => d.BrandId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Accessory_Brand");
            });

            modelBuilder.Entity<Attribution>(entity =>
            {
                entity.ToTable("Attribution");

                entity.Property(e => e.Id)
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.Property(e => e.EngineType)
                    .IsRequired()
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.Property(e => e.Measure)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.RangeOfValue)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.HasOne(d => d.EngineTypeNavigation)
                    .WithMany(p => p.Attributions)
                    .HasForeignKey(d => d.EngineType)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Attribution_EngineType");
            });

            modelBuilder.Entity<Brand>(entity =>
            {
                entity.ToTable("Brand");

                entity.HasIndex(e => e.Name, "UQ__Brand__737584F69CC4B6C1")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.Property(e => e.Description).HasMaxLength(200);

                entity.Property(e => e.Image)
                    .IsRequired()
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<CarModel>(entity =>
            {
                entity.ToTable("CarModel");

                entity.Property(e => e.Id)
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.Property(e => e.BrandId)
                    .IsRequired()
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.HasOne(d => d.Brand)
                    .WithMany(p => p.CarModels)
                    .HasForeignKey(d => d.BrandId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CarModel_Brand");
            });

            modelBuilder.Entity<City>(entity =>
            {
                entity.ToTable("City");

                entity.Property(e => e.Id)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<ContestEvent>(entity =>
            {
                entity.ToTable("ContestEvent");

                entity.Property(e => e.Id)
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.Property(e => e.BrandId)
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Description).IsRequired();

                entity.Property(e => e.EndDate).HasColumnType("datetime");

                entity.Property(e => e.EndRegister).HasColumnType("datetime");

                entity.Property(e => e.Image)
                    .IsRequired()
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.ProposalId)
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.Property(e => e.StartDate).HasColumnType("datetime");

                entity.Property(e => e.StartRegister).HasColumnType("datetime");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.Venue)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.HasOne(d => d.Brand)
                    .WithMany(p => p.ContestEvents)
                    .HasForeignKey(d => d.BrandId)
                    .HasConstraintName("FK_ContestEvent_Brand");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.ContestEventCreatedByNavigations)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ContestEvent_Creater");

                entity.HasOne(d => d.ModifiedByNavigation)
                    .WithMany(p => p.ContestEventModifiedByNavigations)
                    .HasForeignKey(d => d.ModifiedBy)
                    .HasConstraintName("FK_ContestEvent_Modifier");

                entity.HasOne(d => d.Proposal)
                    .WithMany(p => p.ContestEvents)
                    .HasForeignKey(d => d.ProposalId)
                    .HasConstraintName("FK_Contest_Proposal");
            });

            modelBuilder.Entity<ContestEventRegister>(entity =>
            {
                entity.ToTable("ContestEvent_Register");

                entity.Property(e => e.Id)
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.Property(e => e.ContestEventId)
                    .IsRequired()
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.Property(e => e.RegisterDate).HasColumnType("datetime");

                entity.HasOne(d => d.ContestEvent)
                    .WithMany(p => p.ContestEventRegisters)
                    .HasForeignKey(d => d.ContestEventId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ContestEvent_Register");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.ContestEventRegisters)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_User_Register");
            });

            modelBuilder.Entity<ContestPrize>(entity =>
            {
                entity.ToTable("Contest_Prize");

                entity.Property(e => e.Id)
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.Property(e => e.ContestId)
                    .IsRequired()
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.PrizeId)
                    .IsRequired()
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.Property(e => e.PrizeOrder)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.Contest)
                    .WithMany(p => p.ContestPrizes)
                    .HasForeignKey(d => d.ContestId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Contest_Prize_ContestEvent");

                entity.HasOne(d => d.Manager)
                    .WithMany(p => p.ContestPrizeManagers)
                    .HasForeignKey(d => d.ManagerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Contest_Prize_Manager");

                entity.HasOne(d => d.Prize)
                    .WithMany(p => p.ContestPrizes)
                    .HasForeignKey(d => d.PrizeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Prize_Contest");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.ContestPrizeUsers)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_Contest_Prize_Users");
            });

            modelBuilder.Entity<District>(entity =>
            {
                entity.ToTable("District");

                entity.Property(e => e.Id)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.CityId)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.HasOne(d => d.City)
                    .WithMany(p => p.Districts)
                    .HasForeignKey(d => d.CityId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_District_City");
            });

            modelBuilder.Entity<EngineType>(entity =>
            {
                entity.ToTable("EngineType");

                entity.Property(e => e.Id)
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.Property(e => e.Name).HasMaxLength(50);
            });

            modelBuilder.Entity<Exchange>(entity =>
            {
                entity.ToTable("Exchange");

                entity.Property(e => e.Id)
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.CityId)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Description).HasMaxLength(200);

                entity.Property(e => e.DistrictId)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.WardId)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.HasOne(d => d.City)
                    .WithMany(p => p.Exchanges)
                    .HasForeignKey(d => d.CityId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Exchange_City");

                entity.HasOne(d => d.District)
                    .WithMany(p => p.Exchanges)
                    .HasForeignKey(d => d.DistrictId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Exchange_District");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Exchanges)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Exchange_Users1");

                entity.HasOne(d => d.Ward)
                    .WithMany(p => p.Exchanges)
                    .HasForeignKey(d => d.WardId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Exchange_Ward");
            });

            modelBuilder.Entity<ExchangeAccessorryDetail>(entity =>
            {
                entity.ToTable("ExchangeAccessorryDetail");

                entity.Property(e => e.Id)
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.Property(e => e.AccessoryName)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.BrandId)
                    .IsRequired()
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.Property(e => e.ExchangeId)
                    .IsRequired()
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.Property(e => e.Image)
                    .IsRequired()
                    .IsUnicode(false);

                entity.HasOne(d => d.Brand)
                    .WithMany(p => p.ExchangeAccessorryDetails)
                    .HasForeignKey(d => d.BrandId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ExchangeAccessorryDetail_Brand");

                entity.HasOne(d => d.Exchange)
                    .WithMany(p => p.ExchangeAccessorryDetails)
                    .HasForeignKey(d => d.ExchangeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ExchangeAccessorryDetail_Exchange");
            });

            modelBuilder.Entity<ExchangeCarDetail>(entity =>
            {
                entity.ToTable("ExchangeCarDetail");

                entity.Property(e => e.Id)
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.Property(e => e.BrandId)
                    .IsRequired()
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.Property(e => e.CarName)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.ExchangeId)
                    .IsRequired()
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.Property(e => e.Image)
                    .IsRequired()
                    .IsUnicode(false);

                entity.Property(e => e.LicensePlate)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Origin)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.Brand)
                    .WithMany(p => p.ExchangeCarDetails)
                    .HasForeignKey(d => d.BrandId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ExchangeCarDetail_Brand");

                entity.HasOne(d => d.Exchange)
                    .WithMany(p => p.ExchangeCarDetails)
                    .HasForeignKey(d => d.ExchangeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ExchangeCarDetail_Exchange");
            });

            modelBuilder.Entity<ExchangeResponse>(entity =>
            {
                entity.ToTable("ExchangeResponse");

                entity.Property(e => e.Id)
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.ExchangeId)
                    .IsRequired()
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.Property(e => e.Message).HasMaxLength(200);

                entity.HasOne(d => d.Exchange)
                    .WithMany(p => p.ExchangeResponses)
                    .HasForeignKey(d => d.ExchangeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Response_Exchange");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.ExchangeResponses)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ExchangeResponse_Users");
            });

            modelBuilder.Entity<Feedback>(entity =>
            {
                entity.ToTable("Feedback");

                entity.Property(e => e.Id)
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.Property(e => e.ContestEventId)
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.Property(e => e.ExchangeId)
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.Property(e => e.ExchangeResponseId)
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.Property(e => e.FeedbackContent)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.FeedbackDate).HasColumnType("datetime");

                entity.Property(e => e.ReplyContent).HasMaxLength(200);

                entity.Property(e => e.ReplyDate).HasColumnType("datetime");

                entity.HasOne(d => d.ContestEvent)
                    .WithMany(p => p.Feedbacks)
                    .HasForeignKey(d => d.ContestEventId)
                    .HasConstraintName("FK_Feedback_ContestEvent");

                entity.HasOne(d => d.Exchange)
                    .WithMany(p => p.Feedbacks)
                    .HasForeignKey(d => d.ExchangeId)
                    .HasConstraintName("FK_Feedback_Exchange");

                entity.HasOne(d => d.ExchangeResponse)
                    .WithMany(p => p.Feedbacks)
                    .HasForeignKey(d => d.ExchangeResponseId)
                    .HasConstraintName("FK_Feedback_ExchangeResponse");

                entity.HasOne(d => d.FeedbackUser)
                    .WithMany(p => p.FeedbackFeedbackUsers)
                    .HasForeignKey(d => d.FeedbackUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Feedback_Users");

                entity.HasOne(d => d.ReplyUser)
                    .WithMany(p => p.FeedbackReplyUsers)
                    .HasForeignKey(d => d.ReplyUserId)
                    .HasConstraintName("FK_ReplyFeedback_User");
            });

            modelBuilder.Entity<Generation>(entity =>
            {
                entity.ToTable("Generation");

                entity.Property(e => e.Id)
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.Property(e => e.CarModelId)
                    .IsRequired()
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.Property(e => e.Image)
                    .IsRequired()
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.HasOne(d => d.CarModel)
                    .WithMany(p => p.Generations)
                    .HasForeignKey(d => d.CarModelId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Generation_CarModel");
            });

            modelBuilder.Entity<GenerationAttribution>(entity =>
            {
                entity.ToTable("Generation_Attribution");

                entity.Property(e => e.Id)
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.Property(e => e.AttributionId)
                    .IsRequired()
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.Property(e => e.GenerationId)
                    .IsRequired()
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.Property(e => e.Value)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.HasOne(d => d.Attribution)
                    .WithMany(p => p.GenerationAttributions)
                    .HasForeignKey(d => d.AttributionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Car_Attribution_Attribution");

                entity.HasOne(d => d.Generation)
                    .WithMany(p => p.GenerationAttributions)
                    .HasForeignKey(d => d.GenerationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Car_Attribution_Car");
            });

            modelBuilder.Entity<InterestedBrand>(entity =>
            {
                entity.ToTable("InterestedBrand");

                entity.Property(e => e.Id)
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.Property(e => e.BrandId)
                    .IsRequired()
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.HasOne(d => d.Brand)
                    .WithMany(p => p.InterestedBrands)
                    .HasForeignKey(d => d.BrandId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_InterestedBrand_Brand");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.InterestedBrands)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_InterestedBrand_Users");
            });

            modelBuilder.Entity<Post>(entity =>
            {
                entity.ToTable("Post");

                entity.Property(e => e.BrandId)
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.Property(e => e.Contents).IsRequired();

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.FeaturedImage).IsRequired();

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.Overview).IsRequired();

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.HasOne(d => d.Brand)
                    .WithMany(p => p.Posts)
                    .HasForeignKey(d => d.BrandId)
                    .HasConstraintName("FK_Post_Brand");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.PostCreatedByNavigations)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Post_Creater");

                entity.HasOne(d => d.ModifiedByNavigation)
                    .WithMany(p => p.PostModifiedByNavigations)
                    .HasForeignKey(d => d.ModifiedBy)
                    .HasConstraintName("FK_Post_Modifier");
            });

            modelBuilder.Entity<Prize>(entity =>
            {
                entity.ToTable("Prize");

                entity.Property(e => e.Id)
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.Property(e => e.Description).HasMaxLength(200);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Proposal>(entity =>
            {
                entity.ToTable("Proposal");

                entity.Property(e => e.Id)
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Description).IsRequired();

                entity.Property(e => e.EndDate).HasColumnType("datetime");

                entity.Property(e => e.Image)
                    .IsRequired()
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.StartDate).HasColumnType("datetime");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.Venue)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.HasOne(d => d.Manager)
                    .WithMany(p => p.ProposalManagers)
                    .HasForeignKey(d => d.ManagerId)
                    .HasConstraintName("FK_Proposal_Approver");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.ProposalUsers)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Proposal_Users");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("Role");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Email)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.FullName).HasMaxLength(100);

                entity.Property(e => e.Image).IsUnicode(false);

                entity.Property(e => e.Password)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Phone)
                    .HasMaxLength(12)
                    .IsUnicode(false);

                entity.Property(e => e.TokenMobile).IsUnicode(false);

                entity.Property(e => e.TokenWeb).IsUnicode(false);

                entity.Property(e => e.Username)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.YearOfBirth).HasColumnType("date");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Users_Role");
            });

            modelBuilder.Entity<Ward>(entity =>
            {
                entity.ToTable("Ward");

                entity.Property(e => e.Id)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.DistrictId)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.HasOne(d => d.District)
                    .WithMany(p => p.Wards)
                    .HasForeignKey(d => d.DistrictId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Ward_District");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
