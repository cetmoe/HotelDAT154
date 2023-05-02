using DesktopApplication.Models;
using System.Linq;
using System.Windows;

namespace DesktopApplication
{
    /// <summary>
    /// Interaction logic for Editor.xaml
    /// </summary>
    public partial class Editor : Window
    {

        public CetmoeContext Db { get; set; }
        public int? roomId = null;

        public Editor()
        {
            InitializeComponent();
        }

        public Editor(Reservation r)
        {
            InitializeComponent();

            this.roomId = r.Id;
            Id.Text = r.Id.ToString();

            roomNumber.Text = r.Room.RoomNumber.ToString();
            fromDate.SelectedDate = r.From;
            toDate.SelectedDate = r.To;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Reservation? temp = Db.Reservations.Find(this.roomId);
            if (temp is null) return;

            if (int.TryParse(roomNumber.Text, out int roomNr))
            {
                Room room = Db.Rooms.Where(r => r.RoomNumber == roomNr).FirstOrDefault();

                if (room != null)
                {
                    temp.Room = room;
                }
            }

            if (fromDate.SelectedDate.HasValue)
            {
                temp.From = fromDate.SelectedDate.Value;
            }

            if (toDate.SelectedDate.HasValue)
            {
                temp.To = toDate.SelectedDate.Value;
            }

            Db.SaveChanges();
            Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Reservation? temp = Db.Reservations.Find(this.roomId);
            if (temp is null) return;

            Db.Reservations.Remove(temp);
            Db.SaveChanges();
            Close();
        }
    }
}
