using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDo_WPF.Models
{
    // Описываем модель одной задачи
    internal class ToDoModel : INotifyPropertyChanged// Создаем класс с тремя полями 
    {

        private bool _isDone;    // Поле для работы с колонкой Done
        private string _text;    // Поле для работы с колонкой Task
        // Добавляем несколько полей, которые будут представлены в нашем view, и эти поля будут отвечать за колонки
        public DateTime CreationDate { get; set; } = DateTime.Now; // Поле для работы с колонкой CreationDate, устанавливает текущее время добавления записи
                
        public bool IsDone // Имплеминтируем логику для поля IsDone
        {
            get { return _isDone; }
            set 
            {
                if (_isDone == value) // Если изменения аналогичны тому, что уже было, то ничего не меняем, вызывая return
                    return;
                _isDone = value;
                OnOropertyChanged("IsDone"); // Если изменения присутствуют, то вызываем OnPropertyChanged
            }
        }
                
        public string Text // Здесь та же логика, что и в поле IsDone
        {
            get { return _text; }
            set 
            {
                if (_text == value) 
                    return;
                _text = value;
                OnOropertyChanged("Text"); 
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnOropertyChanged(string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
