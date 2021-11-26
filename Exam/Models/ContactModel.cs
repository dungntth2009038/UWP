using Exam.Entities;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace Exam.Models
{
    public class ContactModel
    {
        private static string _insertStatementTemplate = "INSERT INTO contacts (Name, Phone)" +
            " values (@name, @phone)";
        private static string _selectStatementTemplate = "SELECT * FROM contacts";
        private static string _selectStatementWithConditionTemplate = "SELECT * FROM contacts WHERE Name like @keyword";

        public bool Save(Contact contact)
        {
            try
            {
                using (SqliteConnection db = new SqliteConnection($"Filename={DatabaseMigration._databasePath}"))
                {
                    db.Open();
                    SqliteCommand insertCommand = new SqliteCommand(_insertStatementTemplate, db);
                    insertCommand.Parameters.AddWithValue("@name", contact.Name);
                    insertCommand.Parameters.AddWithValue("@phone", contact.Phone);
                    insertCommand.ExecuteNonQuery();
                    return true;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return false;
        }

        public List<Contact> FindAll()
        {
            List<Contact> contacts = new List<Contact>();
            try
            {
                // mở kết nối.
                using (SqliteConnection cnn = new SqliteConnection($"FileName={DatabaseMigration._databasePath}"))
                {
                    cnn.Open();
                    // tạo câu lệnh.
                    SqliteCommand cmd = new SqliteCommand(_selectStatementTemplate, cnn);
                    // bắn lệnh vào và lấy dữ liệu.
                    var reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        var name = Convert.ToString(reader["Name"]);
                        var phone = Convert.ToString(reader["Phone"]);
                        var contact = new Contact()
                        {
                            Name = name,
                            Phone = phone,
                        };
                        contacts.Add(contact);
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return contacts;
        }

        public async Task<List<Contact>> SearchByKeyword(string keyword)
        {
            List<Contact> contacts = new List<Contact>();
            try
            {
                //
                using (SqliteConnection cnn = new SqliteConnection($"Filename={DatabaseMigration._databasePath}"))
                {
                    cnn.Open();
                    //Tạo câu lệnh
                    SqliteCommand cmd = new SqliteCommand(_selectStatementWithConditionTemplate, cnn);
                    cmd.Parameters.AddWithValue("@keyword", "%" + keyword + "%");
                    //Bắn lệnh vào và lấy dữ liệu.
                    var reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        var phone = Convert.ToString(reader["Phone"]);
                        var name = Convert.ToString(reader["Name"]);
                        var contact = new Contact()
                        {
                            Phone = phone,
                            Name = name
                        };
                        contacts.Add(contact);
                    }
                    if (reader == null)
                    {
                        ContentDialog contentDialog = new ContentDialog();
                        contentDialog.Title = "Khong tim thay";
                        contentDialog.PrimaryButtonText = "Khong tim thay";
                        await contentDialog.ShowAsync();
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return contacts;
        }
    }
}
