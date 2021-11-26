using Exam.Entities;
using Exam.Models;
using System;
using Windows.Foundation;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Exam.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class CreateContact : Page
    {
        private ContactModel contactModel = new ContactModel();
        public CreateContact()
        {
            this.InitializeComponent();
            ApplicationView.GetForCurrentView().SetPreferredMinSize(new Size(460, 400));
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            var contact = new Contact()
            {
                Name = txtName.Text,
                Phone = txtPhone.Text,
            };
            var result = contactModel.Save(contact);
            ContentDialog contentDialog = new ContentDialog();
            if (result)
            {
                contentDialog.Title = "Action success!";
                contentDialog.Content = "Contact saved";
            }
            else
            {
                contentDialog.Title = "Action fails!";
                contentDialog.Content = "Please try again later!";
            }
            contentDialog.PrimaryButtonText = "Ok";
            await contentDialog.ShowAsync();
            this.Frame.Navigate(typeof(MainPage));
        }
    }
}
