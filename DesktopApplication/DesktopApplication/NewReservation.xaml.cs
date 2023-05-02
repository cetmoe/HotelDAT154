using DesktopApplication.Models;
using System;
using System.Linq;
using System.Windows;

namespace DesktopApplication
{
    /// <summary>
    /// Interaction logic for NewReservation.xaml
    /// </summary>
    public partial class NewReservation : Window
    {
        public CetmoeContext Db { get; set; }
        public NewReservation(CetmoeContext Database)
        {
            InitializeComponent();

            Db = Database;

            UserSelect.DataContext = Db.Users.ToList();
            UserSelect.SelectedValue = Db.Users.FirstOrDefault();

            RoomSelect.DataContext = Db.Rooms.ToList();
            RoomSelect.SelectedValue = Db.Rooms.FirstOrDefault();
        }

        private void UserSelect_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            User selectedUser = (User)UserSelect.SelectedValue;

            if (selectedUser != null)
            {
                NewFirstName.Text = selectedUser.FirstName;
                NewLastName.Text = selectedUser.LastName;
                NewEmail.Text = selectedUser.Email;
                NewPhoneNumber.Text = selectedUser.PhoneNumber;
            }
        }

        private void RoomSelect_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            Room selectedRoom = (Room)RoomSelect.SelectedValue;

            if (selectedRoom != null)
            {
                NewRoomSize.Text = selectedRoom.RoomType.RoomSize;
                NewPrice.Text = selectedRoom.RoomType.Price.ToString();
                NewBeds.Text = selectedRoom.RoomType.Beds.ToString();
            }
        }

        private void CreateReservation_Click(object sender, RoutedEventArgs e)
        {
            DateTime selectedFromDate;
            if (!NewFromDate.SelectedDate.HasValue) return;

            DateTime selectedToDate;
            if (!NewToDate.SelectedDate.HasValue) return;

            Room selectedRoom = (Room)RoomSelect.SelectedValue;
            User selectedUser = (User)UserSelect.SelectedValue;
            selectedFromDate = NewFromDate.SelectedDate.Value;
            selectedToDate = NewToDate.SelectedDate.Value;

            Reservation reservation = new()
            {
                User = selectedUser,
                Room = selectedRoom,
                From = selectedFromDate,
                To = selectedToDate,
            };

            Db.Reservations.Add(reservation);
            Db.SaveChanges();
        }
    }
}
