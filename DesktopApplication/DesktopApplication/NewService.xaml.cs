using DesktopApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace DesktopApplication
{
    /// <summary>
    /// Interaction logic for NewService.xaml
    /// </summary>
    public partial class NewService : Window
    {
        public CetmoeContext Db { get; set; }
        public List<int> types = new List<int>() { 0, 1, 2 };

        public NewService(CetmoeContext database)
        {
            InitializeComponent();
            Db = database;

            ServiceRoomSelect.DataContext = Db.Rooms.ToList();
            ServiceRoomSelect.SelectedIndex = 0;
            TypeSelect.DataContext = types;
            TypeSelect.SelectedIndex = 0;


        }

        private void CreateService_Click(object sender, RoutedEventArgs e)
        {

            DateTime scheduledDate;
            if (!ScheduledDate.SelectedDate.HasValue) return;

            int selectedType = (int)TypeSelect.SelectedValue;
            Room selectedRoom = (Room)ServiceRoomSelect.SelectedItem;
            scheduledDate = ScheduledDate.SelectedDate.Value;

            ServiceTask service = new()
            {
                Type = selectedType,
                Room = selectedRoom,
                ScheduledDate = scheduledDate,
            };

            Db.ServiceTasks.Add(service);
            Db.SaveChanges();
            Close();
        }
    }
}
