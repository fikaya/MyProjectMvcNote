using MyMvcProject.Entities.Entity;
using MyMvcProject.Entities.Interface.NavigationInterface;
using MyMvcProject.Entities.IntermediateTable.Liked;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMvcProject.DataAccessLayer.EntityFramework.MySql
{
    //Bu class ne zaman çalışacak.Onu belirtmemiz gerek.Onun için CreateDatabaseIfNotExists sınıfını kullanarak eğer bu database oluşturulmadı ise mantığı ile çalışacak.
    public class MyInitializer : CreateDatabaseIfNotExists<MySqlDatabaseContext>
    {
        protected override void Seed(MySqlDatabaseContext context)
        {
            //Database oluşturuldaktan sonra örnek dataları database e basacağımız metot.

            //Adding admin user.
            MyProjectUser admin = new MyProjectUser()
            {
                Name = "Murat",
                Surname = "Kaya",
                EMail = "muratkayagmail.com",
                IsActive = true,
                IsAdmin = true,
                UserName = "muratkaya",
                Password = "12345",
                CreatedOn = DateTime.Now,
                ModifiedOn = DateTime.Now,
                ModifiedUsername = " "
            };

            List<int?> myProjectUserList = new List<int?>();
            int count = 5;

            for (int a = 0; a < count; a++)
            {
                //Adding standart user.
                MyProjectUser myProjectUser = new MyProjectUser()
                {
                    Name = FakeData.NameData.GetFirstName(),
                    Surname = FakeData.NameData.GetSurname(),
                    EMail = FakeData.NetworkData.GetEmail(),
                    IsActive = true,
                    IsAdmin = false,
                    UserName = $"kadirkaya{a}",
                    Password = "12345",
                    CreatedOn = FakeData.DateTimeData.GetDatetime(DateTime.Now.AddYears(-1), DateTime.Now),
                    ModifiedOn = DateTime.Now,
                    ModifiedUsername = "system"
                };

                context.MyProjectUsers.Add(myProjectUser);
                context.SaveChanges();
                //Burada ekleyip veri tabanına kayıt ettiğimiz zaman nesnemizin ID si otomatik olarak oluşmuş oluyor.@@Identity gibi düşünebiliriz.
                //int standartUser01ID = myProjectUser.ID;
                myProjectUserList.Add(myProjectUser.ID);
            }

            //Adding fake categories...
            for (int i = 0; i < count; i++)
            {
                Category category = new Category()
                {
                    Title = FakeData.PlaceData.GetStreetName(),
                    Description = FakeData.PlaceData.GetAddress(),
                    CreatedOn = FakeData.DateTimeData.GetDatetime(DateTime.Now.AddYears(-1), DateTime.Now),
                    ModifiedOn = DateTime.Now,
                    ModifiedUsername = "system"
                };
                context.Categories.Add(category);
                context.SaveChanges();

              
                //Adding fake notes...
                for (int k = 0; k < count; k++)
                {
                    Note note = new Note()
                    {
                        Title = FakeData.TextData.GetAlphabetical(FakeData.NumberData.GetNumber(5, 25)),
                        Text = FakeData.TextData.GetSentences(FakeData.NumberData.GetNumber(1, 3)),
                        MyProjectUserID = myProjectUserList[k],
                        CategoryID = category.ID,
                        IsDraft = false,
                        LikeCount = myProjectUserList.Count,
                        CreatedOn = FakeData.DateTimeData.GetDatetime(DateTime.Now.AddYears(-1), DateTime.Now),
                        ModifiedOn = DateTime.Now,
                        ModifiedUsername = "system"
                    };
                    context.Notes.Add(note);
                    context.SaveChanges();

                    //Adding fake comments...
                    for (int l = 0; l < count; l++)
                    {
                        Comment comment = new Comment()
                        {
                            Text = FakeData.TextData.GetSentences(FakeData.NumberData.GetNumber(5, 8)),
                            MyProjectUserID = myProjectUserList[l],
                            NoteID = note.ID,
                            CreatedOn = FakeData.DateTimeData.GetDatetime(DateTime.Now.AddYears(-1), DateTime.Now),
                            ModifiedOn = DateTime.Now,
                            ModifiedUsername = "system"
                        };
                        context.Comments.Add(comment);
                        context.SaveChanges();
                    }

                    //Adding fake likes...
                    for (int l = 0; l < note.LikeCount; l++)
                    {
                        Liked liked = new Liked()
                        {
                            MyProjectUserID = myProjectUserList[l],
                            NoteID = note.ID,
                        };
                        context.Likes.Add(liked);
                        context.SaveChanges();
                    }
                }
            }
        }
    }
}
