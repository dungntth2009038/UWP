using Exam.Entities;
using Microsoft.Data.Sqlite;
using System;
using System.IO;
using System.Threading.Tasks;
using Windows.Storage;

namespace Exam.Models
{
    // Update database theo code project.
    public class DatabaseMigration
    {
        public static string _databasePath;
        private static string _databaseName = "mynote.db";
        private static string _createNoteTable = "CREATE TABLE IF NOT EXISTS contacts " +
            "(Name NVARCHAR(255) NOT NULL," +
            "Phone NVARCHAR(255) NOT NULL UNIQUE,)";
        public async static void UpdateDabase()
        {
            await ApplicationData.Current.LocalFolder.CreateFileAsync(_databaseName, CreationCollisionOption.OpenIfExists);
            _databasePath = Path.Combine(ApplicationData.Current.LocalFolder.Path, _databaseName);
            using (SqliteConnection db = new SqliteConnection($"Filename={_databasePath}"))
            {
                db.Open();
                SqliteCommand createTableNote = new SqliteCommand(_createNoteTable, db);
                createTableNote.ExecuteNonQuery();
            }
            await GenerateData();
        }

        public async static Task GenerateData()
        {
            ContactModel noteModel = new ContactModel();
            noteModel.Save(new Contact()
            {
                Name = "Cuong",
                Phone = "0987654321",
            });
            noteModel.Save(new Contact()
            {
                Name = "Thang",
                Phone = "0123456789",
            });
            noteModel.Save(new Contact()
            {
                Name = "Nhat",
                Phone = "0999999999",
            });
        }
    }
}
