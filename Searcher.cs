﻿using System;
using System.Linq;
using System.IO;
using System.Net;
using System.Threading;
using Timer = System.Timers.Timer;

/*Модель логики приложения.Поисковик осуществляет поиск файлов по нужному шаблоку имени и текста в корневом каталоге
  и подкаталогах.Так же присутствует обозреватель, который следит за изменением файлов внутри каталогов
  Если вдруг измненение произошло, список файлов будет немедленно обновлен
  
  На вход подается корневой каталог и шаблон поиска. Результат отдается через эвенты */

namespace WindowsFormsApplication1
{


    internal class Searcher
    {

        private readonly FileSystemWatcher _watcher = new FileSystemWatcher();

        private string _textTemplate;
        private string _nameTemplate;
        private int _passedTime;
        private Timer _timer;
        

        public event EventHandler<string> FileFounded;//Найден файл
        public event EventHandler<String> CurrentFileChanged; //Уведомляет об изменении текущего обрабатываемого файла
        public event EventHandler<UnauthorizedAccessException> NotifyAccessError;//Запрещен доступ
        public event EventHandler<ArgumentException> NotifyArgumentException;//Неверный аргумент
        public event EventHandler<string> NotifyFileDeleted;//Файл удален
        public event EventHandler<string[]> NotifyFileRenamed; //Файл переименован
        public event EventHandler FoundedFilesNumberChangedUp;//Число найденных файлов увеличено
        public event EventHandler FoundedFilesNumberChangedDown;
        public event EventHandler<int> UpdateTime;//Тик таймера
        public void Initialiaze(String root, String nameTemplate, String text)
        {
            //Обозреватель инициализируется начальными значениями и начинает работать
            InitializeWatcher(root,nameTemplate,text);
            //Таймер инициализируется
            InitialiazeTimer();
            //Инициализурется процесс поиска
            InitialiazeSearch(root,nameTemplate,text);
            
        }

        private void InitialiazeSearch(String root, String nameTemplate, String text)
        {
            _timer.Start();
            StartSearch(root, nameTemplate, text);
            _timer.Stop();
        }

        private void InitialiazeTimer()
        {
            _passedTime = 0;
            _timer = new Timer();
            _timer.Enabled = true;
            _timer.Interval = 1000;
            _timer.Elapsed += TimerOnTick;
            
           
        }

        private void TimerOnTick(object sender, EventArgs eventArgs)
        {
            ++_passedTime;
            if (UpdateTime != null) UpdateTime(this,_passedTime);
        }


        private void InitializeWatcher(String root, String nameTemplate,string text)
        {
            try
            {
                _textTemplate = text;
                _nameTemplate = nameTemplate;
                _watcher.Path = root;
                _watcher.Filter = nameTemplate;
                _watcher.IncludeSubdirectories = true;
                _watcher.EnableRaisingEvents = true;
                _watcher.Renamed += WatcherOnRenamed;
                _watcher.Deleted += WatcherOnDeleted;
                _watcher.Created += WatcherOnCreated;
            }
            catch (ArgumentException e)
            {
                if (NotifyArgumentException != null) NotifyArgumentException.Invoke(this, e);
            }
        }

        private void WatcherOnRenamed(object sender, RenamedEventArgs e)
        {
            var path = e.OldFullPath.Substring(0, e.FullPath.LastIndexOf('\\'));
            var newName = e.FullPath.Split('\\').Last();
            var files = Directory.GetFiles(path, _nameTemplate);
            var fileNames = files.Select(s => s.Split('\\').Last()).ToArray();
            var contains = fileNames.Contains(newName);
            if (contains)
            {
                using (var stringReader = new StreamReader(e.FullPath))
                {
                    var content = stringReader.ReadToEnd();
                    if (content.Contains(_textTemplate))
                    {
                        if (NotifyFileRenamed != null)
                            NotifyFileRenamed.Invoke(this, new string[] { e.OldFullPath, e.FullPath });
                    }
                    else
                    {
                        if (NotifyFileDeleted != null) NotifyFileDeleted.Invoke(this, e.OldFullPath);
                    }
                }
            }
            else
            {
                if (NotifyFileDeleted != null) NotifyFileDeleted.Invoke(this,e.OldFullPath);
            }

        }

        private void WatcherOnCreated(object sender, FileSystemEventArgs e)
        {
            if (FoundedFilesNumberChangedUp != null) FoundedFilesNumberChangedUp.Invoke(this, new EventArgs());
            if (FileFounded != null) FileFounded.Invoke(this, e.FullPath);
        }

        private void WatcherOnDeleted(object sender, FileSystemEventArgs e)
        {
            if (NotifyFileDeleted != null) NotifyFileDeleted.Invoke(this, e.FullPath);
        }



        private void StartSearch(String directory, String template, String text)
            //Функция рекурсивно спускается по всем директориям и ищет в них файлы по заданым критериям
        {

            try
            {
                var directories = Directory.GetDirectories(directory).ToList();

                if (directories.Capacity != 0)
                {
                    foreach (var elem in directories)
                    {
                        StartSearch(elem, template, text); //Рекурсивный спуск
                    }
                }
                {
                    var files = Directory.GetFiles(directory, template);
                    foreach (var file in files)
                    {
                        if (CurrentFileChanged != null)
                            CurrentFileChanged(this, file);
                        if (text == "")
                        {
                            if (FoundedFilesNumberChangedUp != null)
                                FoundedFilesNumberChangedUp.Invoke(this, new EventArgs());
                            if (FileFounded != null) FileFounded.Invoke(this, file);
                        }
                        else
                        {
                            try
                            {
                                using (var stringReader = new StreamReader(file))
                                {
                                    var content = stringReader.ReadToEnd();
                                    if (content.Contains(text))
                                    {
                                        FoundedFilesNumberChangedUp.Invoke(this, new EventArgs());
                                        FileFounded.Invoke(this, file);
                                    }
                                }
                            }
                            catch (System.IO.IOException)
                            {
                                continue;//На случай ошибки открытия файла
                            }
                        }
                    }
                }
            }
            catch (UnauthorizedAccessException e)
            {
                NotifyAccessError.Invoke(this, e);
            }
            catch (ThreadAbortException)
            {
                _timer.Stop();
            }
            

            
        }
    }
}
