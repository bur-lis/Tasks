using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace Doors
{
    class CheckAccess
    {
        public string Check_Access(List<int> roomsList, 
                                   List<int> departamentList,
                                   List<int> workersList,
                                   List<string> roomsListItem,
                                   List<string> departamentListItem,
                                   List<string> workersListItem,
                                   DoorsDS DoorsDS)
        {

            List<int> departamentAccessList = new List<int>();

            string messageDepartament = "";
            for (int i = 0; i < roomsList.Count; i++)
            {
                for (int j = 0; j < departamentList.Count; j++)
                {
                    object[] key = new object[2];
                    key[0] = departamentList[j];
                    key[1] = roomsList[i];

                    try
                    {
                        string dr = DoorsDS.access_department.Rows.Find(key).ToString();
                        messageDepartament += " + " + departamentListItem[j] + " " + roomsListItem[i] + "\n";
                        departamentAccessList.Add(departamentList[j]);
                    }
                    catch
                    {
                        messageDepartament += " – " + departamentListItem[j] + " " + roomsListItem[i] + "\n";
                    }
                }
            }

            string messageWorker = "";
            for (int i = 0; i < roomsList.Count; i++)
            {
                for (int j = 0; j < workersList.Count; j++)
                {
                    object[] key = new object[2];
                    key[0] = workersList[j];
                    key[1] = roomsList[i];



                    try
                    {
                        string dr = DoorsDS.access_department.Rows.Find(key).ToString();
                        messageWorker += " + " + workersListItem[j] + " " + roomsListItem[i] + "\n";
                    }
                    catch
                    {
                        messageWorker += " – " + workersListItem[j] + " " + roomsListItem[i] + "\n";
                    }
                }
            }

            string messageWorkerFromDepartament = "";
            for (int i = 0; i < roomsList.Count; i++)
            {
                for (int j = 0; j < workersList.Count; j++)
                {
                    try
                    {
                        DataRow dr = DoorsDS.workers.Rows.Find(workersList[j]);
                        int fd = Convert.ToInt32(dr["id_department"]);

                        for (int k = 0; k < departamentAccessList.Count; k++)
                        {
                            if (departamentAccessList[k] == fd)
                            {
                                messageWorkerFromDepartament += " + " + workersListItem[j] + " " + roomsListItem[i] + "\n";
                            }
                        }
                    }
                    catch { }
                }
            }

            string message = "";
            if (messageDepartament != "")
                message += "Доступ отделам:\n" + messageDepartament + "\n";
            if (messageWorker != "")
                message += "Доступ работникам:\n" + messageWorker + "\n";
            if (messageWorkerFromDepartament != "")
                message += "Доступ работникам по отделу:\n" + messageWorkerFromDepartament;
            if (message == "")
                message = "Заполните поля для проверки";

            return message;

        }
    }
}
