using System;
using System.Collections.Generic;
using System.ComponentModel; // пространство имен для использования BindingList
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
using ToDo_WPF.Models;
using ToDo_WPF.Services;

namespace ToDo_WPF
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly string PATH = $"{Environment.CurrentDirectory}\\todoDataList.json"; // Чтобы файл с данными положить рядом с екзешником, используем класс Enviroment с методом CurrentDirectory
        private BindingList<ToDoModel> _todoDataList;
        private IOService _ioservice;
        public MainWindow() 
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e) // Обработчик событий, реагирующий на загрузку приложения
        {

            _ioservice = new IOService(PATH);
            try
            {
                _todoDataList = _ioservice.LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message); // Информация об исключении
                Close(); // Вызов метода Close класса MainWindow для закрытия приложения
            }
            
             
            dgTodoList.ItemsSource = _todoDataList; // Обращаемся к полю с именем dgTodoList и вызываем у него метод ItemSource (отвечает за отражение данных)

            _todoDataList.ListChanged += _todoDataList_ListChanged; // Добавляем метод, вызывающийся при появлении изменений в _todoDataList
        }

        private void _todoDataList_ListChanged(object sender, ListChangedEventArgs e) // Если что-то обновится в списке, будет вызываться это событие

        {

            if( e.ListChangedType == ListChangedType.ItemAdded ||
                e.ListChangedType == ListChangedType.ItemChanged ||
                e.ListChangedType == ListChangedType.ItemDeleted)
            {
                try
                {
                    _ioservice.SaveData(sender);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message); // Информация об исключении
                    Close(); // Вызов метода Close класса MainWindow для закрытия приложения
                }
            }
                        
        }
    }
}
