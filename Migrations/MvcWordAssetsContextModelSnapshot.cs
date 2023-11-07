﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ProjectBackend.Migrations
{
    [DbContext(typeof(MvcWordAssetsContext))]
    partial class MvcWordAssetsContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.12");

            modelBuilder.Entity("AnimationDataWordAssetData", b =>
                {
                    b.Property<long>("AnimationsID")
                        .HasColumnType("INTEGER");

                    b.Property<long>("WordAssetDatasID")
                        .HasColumnType("INTEGER");

                    b.HasKey("AnimationsID", "WordAssetDatasID");

                    b.HasIndex("WordAssetDatasID");

                    b.ToTable("AnimationDataWordAssetData");
                });

            modelBuilder.Entity("AudioDataWordAssetData", b =>
                {
                    b.Property<long>("AudiosId")
                        .HasColumnType("INTEGER");

                    b.Property<long>("WordAssetDatasID")
                        .HasColumnType("INTEGER");

                    b.HasKey("AudiosId", "WordAssetDatasID");

                    b.HasIndex("WordAssetDatasID");

                    b.ToTable("AudioDataWordAssetData");
                });

            modelBuilder.Entity("ImageDataWordAssetData", b =>
                {
                    b.Property<long>("ImagesId")
                        .HasColumnType("INTEGER");

                    b.Property<long>("WordAssetDatasID")
                        .HasColumnType("INTEGER");

                    b.HasKey("ImagesId", "WordAssetDatasID");

                    b.HasIndex("WordAssetDatasID");

                    b.ToTable("ImageDataWordAssetData");
                });

            modelBuilder.Entity("Model3DDataWordAssetData", b =>
                {
                    b.Property<long>("Model3DsId")
                        .HasColumnType("INTEGER");

                    b.Property<long>("WordAssetDatasID")
                        .HasColumnType("INTEGER");

                    b.HasKey("Model3DsId", "WordAssetDatasID");

                    b.HasIndex("WordAssetDatasID");

                    b.ToTable("Model3DDataWordAssetData");
                });

            modelBuilder.Entity("ProjectBackend.Models.AnimationData", b =>
                {
                    b.Property<long>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.HasKey("ID");

                    b.ToTable("AnimationData");
                });

            modelBuilder.Entity("ProjectBackend.Models.AudioData", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<long>("AudioType")
                        .HasColumnType("INTEGER");

                    b.Property<long>("Duration")
                        .HasColumnType("INTEGER");

                    b.Property<string>("FilePath")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("AudioData");
                });

            modelBuilder.Entity("ProjectBackend.Models.GameData", b =>
                {
                    b.Property<long>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<long>("Thumb")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Type")
                        .HasColumnType("TEXT");

                    b.HasKey("ID");

                    b.ToTable("GameData");
                });

            modelBuilder.Entity("ProjectBackend.Models.GameLessonData", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("GameConfigJson")
                        .HasColumnType("TEXT");

                    b.Property<long>("GameDataID")
                        .HasColumnType("INTEGER");

                    b.Property<long>("WordAssetDataID")
                        .HasColumnType("INTEGER");

                    b.Property<string>("WordDisturbing")
                        .HasColumnType("TEXT");

                    b.Property<string>("WordTeaching")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("GameDataID");

                    b.HasIndex("WordAssetDataID");

                    b.ToTable("GameLessonData");
                });

            modelBuilder.Entity("ProjectBackend.Models.ImageData", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("FilePath")
                        .HasColumnType("TEXT");

                    b.Property<long>("ImageType")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Link")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("ImageData");
                });

            modelBuilder.Entity("ProjectBackend.Models.Mode3DBehaviorData", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<long>("ActionType")
                        .HasColumnType("INTEGER");

                    b.Property<long>("Audio")
                        .HasColumnType("INTEGER");

                    b.Property<long>("Model3DDataID")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<long>("Thumb")
                        .HasColumnType("INTEGER");

                    b.Property<long>("WordAssetID")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("Model3DDataID");

                    b.ToTable("Mode3DBehaviorData");
                });

            modelBuilder.Entity("ProjectBackend.Models.Model3DData", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("FileType")
                        .HasColumnType("TEXT");

                    b.Property<string>("LinkDownload")
                        .HasColumnType("TEXT");

                    b.Property<string>("Location")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<string>("NameAsset")
                        .HasColumnType("TEXT");

                    b.Property<float>("ScaleFactor")
                        .HasColumnType("REAL");

                    b.HasKey("Id");

                    b.ToTable("Model3DData");
                });

            modelBuilder.Entity("ProjectBackend.Models.StoryData", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Category")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("StoryDataConfigContent")
                        .HasColumnType("TEXT");

                    b.Property<long>("Thumb")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Type")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("StoryData");
                });

            modelBuilder.Entity("ProjectBackend.Models.SyncAudioData", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<long>("AudioDataDataID")
                        .HasColumnType("INTEGER");

                    b.Property<long?>("AudioDataId")
                        .HasColumnType("INTEGER");

                    b.Property<long>("End")
                        .HasColumnType("INTEGER");

                    b.Property<long>("Start")
                        .HasColumnType("INTEGER");

                    b.Property<long>("Te")
                        .HasColumnType("INTEGER");

                    b.Property<long>("Ts")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Word")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("AudioDataId");

                    b.ToTable("SyncAudioData");
                });

            modelBuilder.Entity("ProjectBackend.Models.TopicData", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Thumb")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("TopicData");
                });

            modelBuilder.Entity("ProjectBackend.Models.VideoData", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("Duration")
                        .HasColumnType("INTEGER");

                    b.Property<string>("FilePath")
                        .HasColumnType("TEXT");

                    b.Property<string>("Link")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<int>("VideoType")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("VideoData");
                });

            modelBuilder.Entity("ProjectBackend.Models.WordAssetData", b =>
                {
                    b.Property<long>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("LevelAge")
                        .HasColumnType("INTEGER");

                    b.Property<string>("PathAsset")
                        .HasColumnType("TEXT");

                    b.Property<long>("SentenceType")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Text")
                        .HasColumnType("TEXT");

                    b.Property<long>("TopicDataID")
                        .HasColumnType("INTEGER");

                    b.Property<long?>("WordAssetDataID")
                        .HasColumnType("INTEGER");

                    b.HasKey("ID");

                    b.HasIndex("TopicDataID");

                    b.HasIndex("WordAssetDataID");

                    b.ToTable("WordAssetData");
                });

            modelBuilder.Entity("ProjectBackend.Models.WordAssets", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime?>("DateCreated")
                        .HasColumnType("TEXT");

                    b.Property<string>("LinkDownLoad")
                        .HasColumnType("TEXT");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("WordAssets");
                });

            modelBuilder.Entity("StoryDataWordAssetData", b =>
                {
                    b.Property<long>("StoriesId")
                        .HasColumnType("INTEGER");

                    b.Property<long>("WordAssetDatasID")
                        .HasColumnType("INTEGER");

                    b.HasKey("StoriesId", "WordAssetDatasID");

                    b.HasIndex("WordAssetDatasID");

                    b.ToTable("StoryDataWordAssetData");
                });

            modelBuilder.Entity("VideoDataWordAssetData", b =>
                {
                    b.Property<int>("VideosId")
                        .HasColumnType("INTEGER");

                    b.Property<long>("WordAssetDatasID")
                        .HasColumnType("INTEGER");

                    b.HasKey("VideosId", "WordAssetDatasID");

                    b.HasIndex("WordAssetDatasID");

                    b.ToTable("VideoDataWordAssetData");
                });

            modelBuilder.Entity("AnimationDataWordAssetData", b =>
                {
                    b.HasOne("ProjectBackend.Models.AnimationData", null)
                        .WithMany()
                        .HasForeignKey("AnimationsID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ProjectBackend.Models.WordAssetData", null)
                        .WithMany()
                        .HasForeignKey("WordAssetDatasID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("AudioDataWordAssetData", b =>
                {
                    b.HasOne("ProjectBackend.Models.AudioData", null)
                        .WithMany()
                        .HasForeignKey("AudiosId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ProjectBackend.Models.WordAssetData", null)
                        .WithMany()
                        .HasForeignKey("WordAssetDatasID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ImageDataWordAssetData", b =>
                {
                    b.HasOne("ProjectBackend.Models.ImageData", null)
                        .WithMany()
                        .HasForeignKey("ImagesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ProjectBackend.Models.WordAssetData", null)
                        .WithMany()
                        .HasForeignKey("WordAssetDatasID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Model3DDataWordAssetData", b =>
                {
                    b.HasOne("ProjectBackend.Models.Model3DData", null)
                        .WithMany()
                        .HasForeignKey("Model3DsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ProjectBackend.Models.WordAssetData", null)
                        .WithMany()
                        .HasForeignKey("WordAssetDatasID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ProjectBackend.Models.GameLessonData", b =>
                {
                    b.HasOne("ProjectBackend.Models.GameData", "GameData")
                        .WithMany()
                        .HasForeignKey("GameDataID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ProjectBackend.Models.WordAssetData", "WordAssetData")
                        .WithMany("Games")
                        .HasForeignKey("WordAssetDataID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("GameData");

                    b.Navigation("WordAssetData");
                });

            modelBuilder.Entity("ProjectBackend.Models.Mode3DBehaviorData", b =>
                {
                    b.HasOne("ProjectBackend.Models.Model3DData", "Model3DData")
                        .WithMany("Behavior")
                        .HasForeignKey("Model3DDataID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Model3DData");
                });

            modelBuilder.Entity("ProjectBackend.Models.SyncAudioData", b =>
                {
                    b.HasOne("ProjectBackend.Models.AudioData", "AudioData")
                        .WithMany("SyncData")
                        .HasForeignKey("AudioDataId");

                    b.Navigation("AudioData");
                });

            modelBuilder.Entity("ProjectBackend.Models.WordAssetData", b =>
                {
                    b.HasOne("ProjectBackend.Models.TopicData", "TopicData")
                        .WithMany("WordAssetDatas")
                        .HasForeignKey("TopicDataID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ProjectBackend.Models.WordAssetData", null)
                        .WithMany("FilterWords")
                        .HasForeignKey("WordAssetDataID");

                    b.Navigation("TopicData");
                });

            modelBuilder.Entity("StoryDataWordAssetData", b =>
                {
                    b.HasOne("ProjectBackend.Models.StoryData", null)
                        .WithMany()
                        .HasForeignKey("StoriesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ProjectBackend.Models.WordAssetData", null)
                        .WithMany()
                        .HasForeignKey("WordAssetDatasID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("VideoDataWordAssetData", b =>
                {
                    b.HasOne("ProjectBackend.Models.VideoData", null)
                        .WithMany()
                        .HasForeignKey("VideosId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ProjectBackend.Models.WordAssetData", null)
                        .WithMany()
                        .HasForeignKey("WordAssetDatasID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ProjectBackend.Models.AudioData", b =>
                {
                    b.Navigation("SyncData");
                });

            modelBuilder.Entity("ProjectBackend.Models.Model3DData", b =>
                {
                    b.Navigation("Behavior");
                });

            modelBuilder.Entity("ProjectBackend.Models.TopicData", b =>
                {
                    b.Navigation("WordAssetDatas");
                });

            modelBuilder.Entity("ProjectBackend.Models.WordAssetData", b =>
                {
                    b.Navigation("FilterWords");

                    b.Navigation("Games");
                });
#pragma warning restore 612, 618
        }
    }
}
