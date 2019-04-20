namespace PrSchool.DAL.EF
{
    using PrSchool.DAL.Entities;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;

    public class PrSchoolContext : DbContext
    {
        public PrSchoolContext()
            : base( "PrSchoolConnection")
        {
        }

        public DbSet<Class> Classes { get; set; }
        public DbSet<Pupil> Pupils { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Teacher> Teachers { get; set; }

        static PrSchoolContext()
        {
            Database.SetInitializer(new SchoolDBInitializer());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Class>().HasMany(i => i.Teachers)
                .WithMany(p => p.Classes)
                .Map(t => t.MapLeftKey("ClassId")
                .MapRightKey("TeacherId")
                .ToTable("ClassTeacher"));

            modelBuilder.Entity<Class>().HasMany(i => i.Pupils)
           .WithOptional(i => i.Class).HasForeignKey(i => i.ClassId)
           .WillCascadeOnDelete(false);


            base.OnModelCreating(modelBuilder);
        }
    }

    public class SchoolDBInitializer : DropCreateDatabaseAlways<PrSchoolContext>
    {
        protected override void Seed(PrSchoolContext context)
        {

            Subject s1 = new Subject { Name = "Астраномия" };
            Subject s2 = new Subject { Name = "Физика" };
            Subject s3 = new Subject { Name = "Английский язык" };
            Subject s4 = new Subject { Name = "Химия" };
            Subject s5 = new Subject { Name = "Биология" };
            Subject s6 = new Subject { Name = "Математика" };
            Subject s7 = new Subject { Name = "История" };
            context.Subjects.Add(s1);
            context.Subjects.Add(s2);
            context.Subjects.Add(s3);
            context.Subjects.Add(s4);
            context.Subjects.Add(s5);
            context.Subjects.Add(s6);
            context.Subjects.Add(s7);

            Teacher t1 = new Teacher { FirstMidlName = "Владислав", LastName = "Петров", Post = "Учитель" };
            Teacher t2 = new Teacher { FirstMidlName = "Сергей", LastName = "Сидоров", Post = "Учитель" };
            Teacher t3 = new Teacher { FirstMidlName = "Оксана", LastName = "Иванова", Post = "Зауч" };
            Teacher t4 = new Teacher { FirstMidlName = "Дарья", LastName = "Павлова", Post = "Директор" };
            context.Teachers.Add(t1);
            context.Teachers.Add(t2);
            context.Teachers.Add(t3);
            context.Teachers.Add(t4);

            Class c1 = new Class { Name = "11 А", Subject = "Астраномия", Teachers = new List<Teacher> { t1, t2 } };
            Class c2 = new Class { Name = "10 Б", Subject = "Химия", Teachers = new List<Teacher> { t2, t3 } };
            Class c3 = new Class { Name = "8 А", Subject = "Математика", Teachers = new List<Teacher> { t3, t4 } };
            Class c4 = new Class { Name = "9 Г", Subject = "История", Teachers = new List<Teacher> { t4, t1 } };
            context.Classes.Add(c1);
            context.Classes.Add(c2);
            context.Classes.Add(c3);
            context.Classes.Add(c4);

            Pupil p1 = new Pupil { Age = 8, FirstMidlName = "Евгений", LastName = "Петров", Sex = "Male", Birthday = DateTime.Parse("1996-01-01"), Class = c2 };
            Pupil p2 = new Pupil { Age = 9, FirstMidlName = "Петр", LastName = "Сидоров", Sex = "Male", Birthday = DateTime.Parse("1997-02-11"), Class = c1 };
            Pupil p3 = new Pupil { Age = 10, FirstMidlName = "Алексей", LastName = "Иванов", Sex = "Male", Birthday = DateTime.Parse("1995-03-22"), Class = c2 };
            Pupil p4 = new Pupil { Age = 11, FirstMidlName = "Виктория", LastName = "Павлова", Sex = "Female", Birthday = DateTime.Parse("1994-04-04"), Class = c2 };
            Pupil p5 = new Pupil { Age = 12, FirstMidlName = "Павел", LastName = "Чехов", Sex = "Male", Birthday = DateTime.Parse("1994-04-01"), Class = c3 };
            Pupil p6 = new Pupil { Age = 13, FirstMidlName = "Мария", LastName = "Алексеева", Sex = "Female", Birthday = DateTime.Parse("1994-06-16"), Class = c3 };
            Pupil p7 = new Pupil { Age = 15, FirstMidlName = "Анна", LastName = "Петрова", Sex = "Female", Birthday = DateTime.Parse("1999-07-09"), Class = c4 };
            Pupil p8 = new Pupil { Age = 16, FirstMidlName = "Сергей", LastName = "Сергеев", Sex = "Male", Birthday = DateTime.Parse("1990-08-01"), Class = c4 };
            Pupil p9 = new Pupil { Age = 9, FirstMidlName = "Александр", LastName = "Александров", Sex = "Male", Birthday = DateTime.Parse("2002-11-22"), Class = c2 };
            Pupil p10 = new Pupil { Age = 18, FirstMidlName = "Николай", LastName = "Петров", Sex = "Male", Birthday = DateTime.Parse("1994-12-23"), Class = c3 };
            context.Pupils.Add(p1);
            context.Pupils.Add(p2);
            context.Pupils.Add(p3);
            context.Pupils.Add(p4);
            context.Pupils.Add(p5);
            context.Pupils.Add(p6);
            context.Pupils.Add(p7);
            context.Pupils.Add(p8);
            context.Pupils.Add(p9);
            context.Pupils.Add(p10);

            base.Seed(context);

            base.Seed(context);
        }
    }
}