using Newtonsoft.Json; 
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDo_WPF.Models;

namespace ToDo_WPF.Services
{
    internal class IOService // Описываем класс с двумя методами для сохранения и считывания данных с жесткого диска
    {

        private readonly string PATH; // Переменная для пути к файлу сохранения/считывания данных

        public IOService(string path) // Конструктор класса, принимающий в качестве аргумента путь к данным
        {
            PATH = path;
        }
        public BindingList<ToDoModel> LoadData() // Метод для чтения данных из файла
        {
            var fileExists = File.Exists(PATH); // Проверяем, существует ли файл с помощью метода Exists у класса File, передав ему путь к файлу
            if(!fileExists) 
            {
                File.CreateText(PATH).Dispose(); // При отсутствии файла создаем его по этому пути с помощью метода CreateText и освобождаем ресурсы с помощью метода Dispose
                return new BindingList<ToDoModel>();// Создаем и возвращаем пустой объект BindingList
            }
            using(var reader = File.OpenText(PATH)) // Если файл существует, мы прочтем его в reader с помощью метода OpenText
            {
                var fileText = reader.ReadToEnd();
                return JsonConvert.DeserializeObject<BindingList<ToDoModel>>(fileText); // Дессириализируем из JSON в BindingList и возвращаем
            }
        }

        public void SaveData(object todoDataList) // Метод для записи данных в файл
        {
            using (StreamWriter writer = File.CreateText(PATH)) // Создаем объект writer путем вызова метода CreateText у класса File и передаем этому методу путь к файлу 
                // using используем для вызова мнетода Dispose у обьекта writter - метод освобождает ресурсы, которые мы используем для записи данных в файл
            {
                string output = JsonConvert.SerializeObject(todoDataList); // конвертируем наш список в json и сохранеяем в output
                writer.Write(output);
            }
        }
    }
}
