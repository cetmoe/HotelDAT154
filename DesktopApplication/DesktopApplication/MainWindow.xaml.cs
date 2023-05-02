using DesktopApplication.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Linq;
using System.Windows;

namespace DesktopApplication
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly CetmoeContext Db = new();

        private readonly LocalView<Room> Rooms;
        private readonly LocalView<RoomType> RoomTypes;
        private readonly LocalView<User> Users;
        private readonly LocalView<Reservation> Reservations;
        private readonly LocalView<ServiceTask> Services;



        public MainWindow()
        {
            InitializeComponent();

            Rooms = Db.Rooms.Local;
            RoomTypes = Db.RoomTypes.Local;
            Users = Db.Users.Local;
            Reservations = Db.Reservations.Local;
            Services = Db.ServiceTasks.Local;

            Db.Reservations.Load();
            Db.Rooms.Load();
            Db.Users.Load();
            Db.RoomTypes.Load();
            Db.ServiceTasks.Load();

            resList.DataContext = Reservations.ToList();
            roomList.DataContext = Rooms.ToList();
            serviceList.DataContext = Services.ToList();
        }

        private void openEditor(object sender, EventArgs e)
        {
            Reservation selectedReservation = (Reservation)resList.SelectedItem;

            if (selectedReservation == null) return;

            Editor ed = new(selectedReservation)
            {
                Db = Db
            };

            ed.Show();
        }

        private void toggleCheckIn(object sender, EventArgs e)
        {
            Room selectedRoom = (Room)roomList.SelectedItem;

            if (selectedRoom == null) return;

            selectedRoom.CheckInStatus = !selectedRoom.CheckInStatus;
            Db.SaveChanges();

            roomList.DataContext = Rooms.ToList();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            resList.DataContext = Reservations.ToList();
            roomList.DataContext = Rooms.ToList();
            serviceList.DataContext = Services.ToList();
        }

        private void Button_Click_New(object sender, RoutedEventArgs e)
        {
            NewReservation newRes = new(Db);

            newRes.Show();
        }

        private void Button_Click_New_Service(object sender, RoutedEventArgs e)
        {
            NewService service = new(Db);

            service.Show();
        }
    }
}
