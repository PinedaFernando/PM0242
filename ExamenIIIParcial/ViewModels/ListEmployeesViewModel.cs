using Acr.UserDialogs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using ExamenIIIParcial.Config;
using ExamenIIIParcial.Models;
using ExamenIIIParcial.Views;
using Xamarin.Forms;

namespace ExamenIIIParcial.ViewModels
{
    class ListEmployeesViewModel:BaseViewModel
    {
        public Command DeleteCommand { get; }
        public Command EditCommand { get; }
        public Command AddCommand { get; }
        ObservableCollection<Pagos> listEmployees;

        bool isEmpty;
        bool isNotEmpty;
        string foto;
        public bool IsEmpty { get => isEmpty; set { SetProperty(ref isEmpty, value); } }
        public bool IsNotEmpty { get => isNotEmpty; set { SetProperty(ref isNotEmpty, value); } }

        public ObservableCollection<Pagos> ListEmployees { get=>listEmployees; set { SetProperty(ref listEmployees, value); } }

        public string Foto { get => foto; set { SetProperty(ref foto,value); } }

        Page Page;
        public ListEmployeesViewModel(Page page)
        {
            Page = page;
            Constants.WasChange = true;
            
            DeleteCommand = new Command( async (employeeSelected)=> {
                var employee = employeeSelected as Pagos;
                bool canDelete = await Page.DisplayAlert("Advertencia","¿Seguro desea eliminar el pago de con fecha "+employee.Fecha+"?","Aceptar","Cancelar");
                if(canDelete)
                {
                     int res = await DataBase.CurrentDB.DeleteEmployee((Pagos)employeeSelected);
                    if (res == Constants.SUCCESS)
                     ListEmployees.Remove((Pagos)employeeSelected);
                    Load();
                }
              
            });
            EditCommand = new Command( async(employeeSelected) => {
                var employee = employeeSelected as Pagos;
                UserDialogs.Instance.ShowLoading("Cargando");
                await Page.Navigation.PushAsync(new EmployesPage(employee));
                UserDialogs.Instance.HideLoading();
            });
            AddCommand = new Command(async() => {
                await Page.Navigation.PushAsync(new EmployesPage(new Pagos()));
            });
             
            
        }


       public async void Load()
        {
            Title = "Pagos";
            Foto = "lista_vacia.png";
           
                 int count= await DataBase.CurrentDB.GetEmpleyeeCount();
                if (count > 0)
                {
                    if (Constants.WasChange)
                    {
                    var list = await DataBase.CurrentDB.GetAllEmployees();
                        ListEmployees = new ObservableCollection<Pagos>(list);
                    Constants.WasChange = false;
                    }
                    IsEmpty = false;
                    IsNotEmpty = !IsEmpty;
                }
                else
                {
                    IsEmpty = true;
                    IsNotEmpty = !IsEmpty;

                }
        }

    }
}   






































//Examen Fernando Pineda_201820040042