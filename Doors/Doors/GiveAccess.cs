using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace Doors
{
    class GiveAccess
    {
        public string Give_Access(List<int> roomsList, List<int> departamentList, List<int> workersList, DoorsDS DoorsDS)
        {
            DoorsDSTableAdapters.access_departmentTableAdapter DoorsDSaccess_departmentTableAdapter = new DoorsDSTableAdapters.access_departmentTableAdapter();
            DoorsDSaccess_departmentTableAdapter.Fill(DoorsDS.access_department);

            DoorsDSTableAdapters.access_workersTableAdapter DoorsDSaccess_workersTableAdapter = new DoorsDSTableAdapters.access_workersTableAdapter();
            DoorsDSaccess_workersTableAdapter.Fill(DoorsDS.access_workers);
            

            DoorsDS.access_departmentRow newAccess_departmentRow;
            DoorsDS.access_workersRow newAccess_workersRow;


            string message = "";

            try
            {
                for (int i = 0; i < roomsList.Count; i++)
                {
                    for (int j = 0; j < workersList.Count; j++)
                    {
                        newAccess_workersRow = DoorsDS.access_workers.Newaccess_workersRow();
                        newAccess_workersRow.id_workers = workersList[j];
                        newAccess_workersRow.id_rooms = roomsList[i];
                        DoorsDS.access_workers.Rows.Add(newAccess_workersRow);
                    }
                }
                DoorsDSaccess_workersTableAdapter.Update(DoorsDS.access_workers);
                message += "Доступ работникам пердоствлен\n";
            }
            catch
            {
                message += "Доступ работникам не пердоствлен\n";
            }

            //Save the new row to the database
            try
            {
                for (int i = 0; i < roomsList.Count; i++)
                {
                    for (int j = 0; j < departamentList.Count; j++)
                    {
                        newAccess_departmentRow = DoorsDS.access_department.Newaccess_departmentRow();
                        newAccess_departmentRow.id_department = departamentList[j];
                        newAccess_departmentRow.id_rooms = roomsList[i];
                        DoorsDS.access_department.Rows.Add(newAccess_departmentRow);
                    }
                }
                DoorsDSaccess_departmentTableAdapter.Update(DoorsDS.access_department);
                message += "Доступ отделам пердоствлен\n";
            }
            catch
            {
                message += "Доступ отделам не пердоствлен\n";
            }

            return message;

        }
    }
}
