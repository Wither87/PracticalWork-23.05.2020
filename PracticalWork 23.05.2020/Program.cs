using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PracticalWork_23._05._2020
{
    class Coder
    {
        public Coder(string command) {
            SplitCommand(command);
            PerformCommand();
        }

        string FileName { get; set; } // записыается имя файла: к примеру index.html

        string Sourse { get; set; } // Место куда сохранить файл: к прмеру ../../12345.html

        string Action { get; set; } // Какое совершить действие: Закодировать или Декодировать

        // Разделить команду на части
        void SplitCommand(string command) {
            // разделить команду на части
            string[] spl = command.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries); // разделить команду: к примеру FileEncoder encode file index.html
            // зачем вообще нам нужно FileEncoder ?
            Action = spl[1];
            Sourse = spl[2];
            FileName = spl[3];
        }

        // Выполнить команду
        void PerformCommand() {

            // ИСПОЛЬЗУЙ ЕНАМЫ Б##ТЬ

            if (Action == "encode") {
                EnCoding();
            }
            else if (Action == "decode") {
                DeCoding();
            }
        }

        // закодировать
        public void EnCoding()
        {
            try {
                // закодировать Файл, а не текст из файла
                byte[] str = File.ReadAllBytes(FileName);
                string encodedString = Convert.ToBase64String(str); // закодировал строку
                EnCoding_SaveFile(encodedString); // сохранил строку
                Console.WriteLine("Файл закодирован.");
            }
            catch {
                Console.WriteLine("Ошибка. Файл не закодирован");
            }
        }

        // декодировать
        public void DeCoding()
        {
            // надо реализовать декодинг из буфера в файл
            // если sourse == file => декодировать из файла и сохранить в index.html
            // если sourse == buffer => декодировать из буфера и сохранить в index.html
            try {

                // ИСПОЛЬЗУЙ ЕНАМЫ Б##ТЬ

                if (Sourse == "buffer") {
                    string str = Clipboard.GetText();
                    DeCoding_SaveFile(str);
                }
                else if (Sourse == "file") {
                    string str = File.ReadAllText(FileName);
                    DeCoding_SaveFile(str);
                }
                Console.WriteLine("Файл декодирован.");
            }
            catch {
                Console.WriteLine("Ошибка.");
            }
        }

        // Сохранение в буфер или txt файл для EnCoding
        void EnCoding_SaveFile(string text) {

            // ИСПОЛЬЗУЙ ЕНАМЫ Б##ТЬ

            if (Sourse == "buffer") {
                Clipboard.SetText(text);
            }
            else if(Sourse == "file") {
                string[] parsName = FileName.Split(new char[] { '/','.' }, StringSplitOptions.RemoveEmptyEntries);
                File.WriteAllText($"../../{parsName[0]}_{parsName[1]}.txt", text);
            }
        }

        // Сохранение в txt для DeCoding
        void DeCoding_SaveFile(string text) {
            byte[] buffer = Convert.FromBase64String(text);
            string str = Encoding.UTF8.GetString(buffer);
            string[] parsName = FileName.Split(new char[] { '/', '.', '_' }, StringSplitOptions.RemoveEmptyEntries);
            File.WriteAllText($"../../{parsName[0]}.html", str); 
            // .html :(
            // Надо добавить свойство с расширением файла
        }
    }

    class Program {
        [STAThread]
        static void Main(string[] args) {
            while (true) {
                // Ввожу команду ручками
                // Проверял как работает команда
                // Хотя надо было массивчик с командами сделать и в цыкле вызывать... я тупенький
                string command = Console.ReadLine();
                Coder cod = new Coder(command);
                // Программа работает
            }
        }
    }
}
