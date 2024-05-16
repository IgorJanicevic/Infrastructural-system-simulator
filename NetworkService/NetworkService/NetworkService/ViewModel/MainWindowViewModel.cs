using MVVM3.Helpers;
using MVVMLight.Messaging;
using NetworkService.Helpers;
using NetworkService.Model;
using Notification.Wpf;
using Projekat.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace NetworkService.ViewModel
{
    public class MainWindowViewModel : BindableBase
    {
        public static ObservableCollection<Entity> Entites { get; set; }

        private HomeViewModel homeViewModel =  new HomeViewModel();
        private NetworkEntitesViewModel networkentitesViewModel= new NetworkEntitesViewModel();
        private NetworkDisplayViewModel networkdisplayViewModel;
        private MeasurementGraphViewModel measurementGraphViewModel;
        private BindableBase currentViewModel;
      
        public Stack<MyICommand> CommandsHistory { get; private set; }
        public BindableBase CurrentViewModel
        {
            get
            {
                return currentViewModel;
            }

            set
            {
                SetProperty(ref currentViewModel, value);
            }
        }
        public MyICommand<string> NavCommand { get; private set; }
        public MyICommand UndoCommand { get; private set; }

        private DataIO serializer= new DataIO();
        private NotificationManager notificationManager;



        private int count = 0; // Inicijalna vrednost broja objekata u sistemu
                                // ######### ZAMENITI stvarnim brojem elemenata
                                //           zavisno od broja entiteta u listi

        public MainWindowViewModel()
        {
            LoadData();
            measurementGraphViewModel = new MeasurementGraphViewModel(Entites);
            networkdisplayViewModel = new NetworkDisplayViewModel();
            networkentitesViewModel= new NetworkEntitesViewModel(Entites);
            notificationManager = new NotificationManager();
            NavCommand = new MyICommand<string>(OnNav);
            UndoCommand = new MyICommand(UndoFunc); //Implementirati
            CurrentViewModel = homeViewModel;
            Messenger.Default.Register<Tuple<Entity,string>>(this, AddOrDelete);
            Messenger.Default.Register<NotificationContent>(this, ShowToastNotification);
            createListener(); //Povezivanje sa serverskom aplikacijom

        }    
        private void ShowToastNotification(NotificationContent notificationContent)
        {
            notificationManager.Show(notificationContent, "WindowNotificationArea");
        }     
        private void AddOrDelete(Tuple<Entity,string> temp )
        {
            if (temp.Item2.Equals("ADD"))
            {
                Entites.Add(temp.Item1);
                SaveData();
                count++;
            }
            else
            {
                Entites.Remove(temp.Item1);
                SaveData();
                count--;
            }

            MessageBox.Show("Num: "+count);
        }      
        private void LoadData()
        {
            Entites = serializer.DeSerializeObject<ObservableCollection<Entity>>("Entites.xml");
            count = Entites.Count;
        }
        private void SaveData()
        {
            serializer.SerializeObject<ObservableCollection<Entity>>(Entites, "Entites.xml");
        }
        private void UndoFunc()
        {
            MyICommand last = CommandsHistory.Pop();

        }
        private void OnNav(string destination)
        {
            switch (destination)
            {
                case "homeView":
                    CurrentViewModel = homeViewModel;
                    break;
                case "addView":
                    CurrentViewModel = networkentitesViewModel;
                    break;
                case "displayView":
                    CurrentViewModel = networkdisplayViewModel;
                    break;
                case "graphView":
                    CurrentViewModel = measurementGraphViewModel;
                    break;
            }
        }
        private void createListener()
        {
            var tcp = new TcpListener(IPAddress.Any, 25675);
            tcp.Start();

            var listeningThread = new Thread(() =>
            {
                while (true)
                {
                    var tcpClient = tcp.AcceptTcpClient();
                    ThreadPool.QueueUserWorkItem(param =>
                    {
                        //Prijem poruke
                        NetworkStream stream = tcpClient.GetStream();
                        string incomming;
                        byte[] bytes = new byte[1024];
                        int i = stream.Read(bytes, 0, bytes.Length);
                        //Primljena poruka je sacuvana u incomming stringu
                        incomming = System.Text.Encoding.ASCII.GetString(bytes, 0, i);

                        //Ukoliko je primljena poruka pitanje koliko objekata ima u sistemu -> odgovor
                        if (incomming.Equals("Need object count"))
                        {
                            //Response
                            /* Umesto sto se ovde salje count.ToString(), potrebno je poslati 
                             * duzinu liste koja sadrzi sve objekte pod monitoringom, odnosno
                             * njihov ukupan broj (NE BROJATI OD NULE, VEC POSLATI UKUPAN BROJ)
                             * */
                            Byte[] data = System.Text.Encoding.ASCII.GetBytes(count.ToString());
                            stream.Write(data, 0, data.Length);
                        }
                        else
                        {
                            try
                            {
                                //U suprotnom, server je poslao promenu stanja nekog objekta u sistemu
                                Console.WriteLine(incomming); //Na primer: "Entitet_1:272"
                                string[] parts = incomming.Split('_');
                                string name = parts[0];
                                string[] parts2 = parts[1].Split(':');
                                int id = Convert.ToInt32(parts2[0]);
                                int measure = Convert.ToInt32(parts2[1]);

                                Entites[id].Update(measure);
                                LogData.Log($"{id}|{measure}");
                                Messenger.Default.Send<ObservableCollection<Entity>>(Entites);
                            }
                            catch(Exception ex) { }
                            {
                                Console.WriteLine("Error with create measure! " );
                            }






                            //################ IMPLEMENTACIJA ####################
                            // Obraditi poruku kako bi se dobile informacije o izmeni
                            // Azuriranje potrebnih stvari u aplikaciji

                        }
                    }, null);
                }
            });

            listeningThread.IsBackground = true;
            listeningThread.Start();
        }
    }
}
