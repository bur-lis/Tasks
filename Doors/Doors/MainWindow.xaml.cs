using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data;
using System.Data.SqlClient;


namespace Doors
{
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();

            DoorsDS DoorsDS = ((DoorsDS)(this.FindResource("DoorsDS")));
            DoorsDSTableAdapters.departmentTableAdapter DoorsDSdepartmentTableAdapter = new DoorsDSTableAdapters.departmentTableAdapter();

            // Загрузить данные в таблицу department.
            DoorsDSdepartmentTableAdapter = new DoorsDSTableAdapters.departmentTableAdapter();
            DoorsDSdepartmentTableAdapter.Fill(DoorsDS.department);

            // Загрузить данные в таблицу workers.
            DoorsDSTableAdapters.workersTableAdapter DoorsDSworkersTableAdapter = new DoorsDSTableAdapters.workersTableAdapter();
            DoorsDSworkersTableAdapter.Fill(DoorsDS.workers);

            // Загрузить данные в таблицу rooms.
            DoorsDSTableAdapters.roomsTableAdapter DoorsDSroomsTableAdapter = new DoorsDSTableAdapters.roomsTableAdapter();
            DoorsDSroomsTableAdapter.Fill(DoorsDS.rooms);

            // Загрузить данные в таблицу access_department.
            DoorsDSTableAdapters.access_departmentTableAdapter DoorsDSaccess_departmentTableAdapter = new DoorsDSTableAdapters.access_departmentTableAdapter();
            DoorsDSaccess_departmentTableAdapter.Fill(DoorsDS.access_department);

            // Загрузить данные в таблицу access_workers.
            DoorsDSTableAdapters.access_workersTableAdapter DoorsDSaccess_workersTableAdapter = new DoorsDSTableAdapters.access_workersTableAdapter();
            DoorsDSaccess_workersTableAdapter.Fill(DoorsDS.access_workers);


        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DoorsDS DoorsDS = ((DoorsDS)(this.FindResource("DoorsDS")));
            // Загрузить данные в таблицу access_department.
            Doors.DoorsDSTableAdapters.access_departmentTableAdapter DoorsDSaccess_departmentTableAdapter = new Doors.DoorsDSTableAdapters.access_departmentTableAdapter();
            DoorsDSaccess_departmentTableAdapter.Fill(DoorsDS.access_department);
            System.Windows.Data.CollectionViewSource access_departmentViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("access_departmentViewSource")));
            access_departmentViewSource.View.MoveCurrentToFirst();
        }

        private void addComboBox(StackPanel stn, string table)
        {
            DoorsDS DoorsDS = ((DoorsDS)(this.FindResource("DoorsDS")));

            ComboBox combobox = new ComboBox();
            combobox.IsDropDownOpen = true;
            combobox.ItemsSource = DoorsDS.Tables[table].DefaultView;
            combobox.DisplayMemberPath = "name";
            combobox.SelectedValuePath = "id";

            int stnl = stn.Children.Count - 1;
            Button buttonAdd = (Button)stn.Children[stnl];
            stn.Children.RemoveAt(stnl);
            stn.Children.Add(combobox);
            stn.Children.Add(buttonAdd);
        }

        private void Add_Room(object sender, RoutedEventArgs e)
        {
            addComboBox(st1, "rooms");
        }

        private void Add_Departament(object sender, RoutedEventArgs e)
        {
            addComboBox(st2, "department");
        }

        private void Add_Worker(object sender, RoutedEventArgs e)
        {
            addComboBox(st3, "workers");
        }

        private List<int> listID(StackPanel stn)
        {
            List<int> listIdComboBox = new List<int> { };
            for (int i = 0; i < stn.Children.Count - 1; i++)
            {
                int id = Convert.ToInt32(((ComboBox)stn.Children[i]).SelectedValue);
                listIdComboBox.Add(id);
            }
            return listIdComboBox;
        }

        private List<string> listItem(StackPanel stn)
        {
            List<string> listItemComboBox = new List<string> { };
            for (int i = 0; i < stn.Children.Count - 1; i++)
            {
                string id = ((ComboBox)stn.Children[i]).Text;
                listItemComboBox.Add(id);
            }
            return listItemComboBox;
        }

        private void Give_Access(object sender, RoutedEventArgs e)
        {
            DoorsDS DoorsDS = ((DoorsDS)(this.FindResource("DoorsDS")));
           
            List<int> roomsList = listID(st1);
            List<int> departamentList = listID(st2);
            List<int> workersList = listID(st3);

            GiveAccess x = new GiveAccess(); 
            string message = x.Give_Access(roomsList, departamentList, workersList, DoorsDS);
            MessageBox.Show(message);

        }

        private void Check_Access(object sender, RoutedEventArgs e)
        {
            DoorsDS DoorsDS = ((DoorsDS)(this.FindResource("DoorsDS")));
            
            List<int> roomsList = listID(st1);
            List<int> departamentList = listID(st2);
            List<int> workersList = listID(st3);
            List<string> roomsListItem = listItem(st1);
            List<string> departamentListItem = listItem(st2);
            List<string> workersListItem = listItem(st3);

            CheckAccess x = new CheckAccess();
            string message = x.Check_Access(roomsList, 
                                            departamentList, 
                                            workersList,
                                            roomsListItem, 
                                            departamentListItem, 
                                            workersListItem, 
                                            DoorsDS);
        
                MessageBox.Show(message);

        }

        private void Clear_Form(object sender, RoutedEventArgs e)
        {
            StackPanel[] stnm = new StackPanel[3] {st1, st2, st3 };
            foreach (StackPanel stn in stnm)
            {
                for (int i = stn.Children.Count - 2; i >= 0; i--)
                {
                    stn.Children.RemoveAt(i);
                }
            }

        }
    }
}
