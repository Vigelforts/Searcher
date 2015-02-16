using System;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace WindowsFormsApplication1
{
    public partial class SearcherForm : Form
    {
        private readonly Searcher _searcher;//Экземпляр поисковика
        private Thread _searchThread;//Поток поиска для асинхронности
        private int _numberOfFoundedFiles;//Кол-во найденный файлов
        public SearcherForm()
        {
            

            _searcher = new Searcher();
            _searcher.CurrentFileChanged += SearcherOnCurrentFileChanged;//Пришло имя обрабатываемого файла
            _searcher.NotifyAccessError += AccessErrorHandler;//Ошибка доступа к файлу
            _searcher.NotifyFileDeleted += SearcherOnNotifyFileDeleted;//Удалён файл
            _searcher.NotifyFileRenamed += SearcherOnNotifyFileRenamed;//Файл переименован
            _searcher.FoundedFilesNumberChanged += SearcherOnFoundedFilesNumberChangedUp;//Кол-во найденных файлов увеличилось на 1
            _searcher.FileFounded += AddFile;//Найден файл
            _searcher.NotifyArgumentException += ArgumentExceptionCatched;//Введены неправильные аргументы
            _searcher.UpdateTime += SearcherOnUpdateTime;//Тик таймера(раз в секунду)
            this.Closing += SaveCurrentState;//При закрытии формы сохраняем значения полей
            InitializeComponent();
            if (Directory.GetFiles(Environment.CurrentDirectory, "settings.xml").Any())
            {
                //Если есть файл с сохраеннными настройками, то десериализуем его и заполняем поля 
                var reader = new FileStream("settings.xml", FileMode.Open);
                XmlSerializer deSerializer = new XmlSerializer(typeof(CurrentProgramStateStructure));
                CurrentProgramStateStructure savedState = new CurrentProgramStateStructure();
                savedState = (CurrentProgramStateStructure)deSerializer.Deserialize(reader);
                this.DirectoryBox.Text = savedState.Path;
                this.TemplateBox.Text = savedState.NameTemplate;
                this.TextInFileBox.Text = savedState.TextTemplate;
                File.Delete(Environment.CurrentDirectory + "settings.xml");
                reader.Dispose();
            }
        }

        private void SaveCurrentState(object sender, CancelEventArgs cancelEventArgs)
        {
            //Сохрянаем текущие поля в структуру и сериализуем её
            XmlSerializer serializer = new XmlSerializer(typeof(CurrentProgramStateStructure));
            var currentStateStructure = new CurrentProgramStateStructure();
            currentStateStructure.Path = DirectoryBox.Text;
            currentStateStructure.NameTemplate = TemplateBox.Text;
            currentStateStructure.TextTemplate = TextInFileBox.Text;
            FileStream writer = new FileStream("settings.xml",FileMode.OpenOrCreate);
            serializer.Serialize(writer,currentStateStructure);
            writer.Dispose();

        }

        private void SearcherOnFoundedFilesNumberChangedUp(object sender, EventArgs eventArgs)
        {
            Action changeNumber = () =>
            {
                ++_numberOfFoundedFiles;
                numberOfParsedFiles.Text = String.Format("Найдено {0} файлов", _numberOfFoundedFiles);
            };
            CurrentFileName.Invoke(changeNumber);
        }
        private void SearcherOnFoundedFilesNumberChangedDown(object sender, EventArgs eventArgs)
        {
            Action changeNumber = () =>
            {
                --_numberOfFoundedFiles;
                numberOfParsedFiles.Text = String.Format("Найдено {0} файлов", _numberOfFoundedFiles);
            };
            CurrentFileName.BeginInvoke(changeNumber);
        }
        private void SearcherOnNotifyFileRenamed(object sender, string[] strings)
        {
            Action renameFileInTree = () =>
            {
                var subPathAgg = string.Empty;
                var oldPath = strings[0].Substring(0,strings[0].LastIndexOf('\\'));
                foreach (var subPath in oldPath.Split('\\'))
                {
                    subPathAgg += subPath + '\\';
                    var nodes = FilesTreeView.Nodes.Find(subPathAgg, true);
                    if(!nodes.Any()) continue;
                    var fullPath = nodes[0].FullPath;
                    if (fullPath == oldPath)
                    {
                        if (!FilesTreeView.Nodes.Find(strings[0]+'\\', true).Any()) 
                        {
                            SearcherOnFoundedFilesNumberChangedUp(this, new EventArgs());
                            AddFile(this, strings[1]);
                        }
                        else
                        {
                            AddFile(this,strings[1]);
                            var nodeToDelete = FilesTreeView.Nodes.Find(strings[0] + '\\', true);
                            FilesTreeView.Nodes.Remove(nodeToDelete[0]);
                        }
                    }
                }
            };
            FilesTreeView.BeginInvoke(renameFileInTree);
        }

        private void SearcherOnNotifyFileDeleted(object sender, string path)
        {

            Action deleteFileFromTree = () =>
            {
                var subPathAgg = string.Empty;
                foreach (var subPath in path.Split('\\'))
                {
                    subPathAgg += subPath + '\\';
                    var nodes = FilesTreeView.Nodes.Find(subPathAgg, true);

                    var fullPath = nodes[0].FullPath;
                    if (fullPath == path)
                    {
                        SearcherOnFoundedFilesNumberChangedDown(this,new EventArgs());
                        FilesTreeView.Nodes.Remove(nodes[0]);
                    }
                }
            };
            FilesTreeView.BeginInvoke(deleteFileFromTree);

        }

        private void AddFile(object sender, string path)
        {
            Action addFileToTreeAction = () =>
            {

                    TreeNode lastNode = null;
                    var subPathAgg = string.Empty;
                    foreach (var subPath in path.Split('\\'))
                    {
                        subPathAgg += subPath + '\\';
                        var nodes = FilesTreeView.Nodes.Find(subPathAgg, true);
                        if (nodes.Length == 0)
                            if (lastNode == null)
                                lastNode = FilesTreeView.Nodes.Add(subPathAgg, subPath);
                            else
                                lastNode = lastNode.Nodes.Add(subPathAgg, subPath);
                        else
                            lastNode = nodes[0];
                    }
            };
            FilesTreeView.BeginInvoke(addFileToTreeAction);
        }

        private void SearcherOnUpdateTime(object sender, int i)
        {
            Action updateTime = () =>
            {

                timerLabel.Text = String.Format("На поиск затрачено {0} секунд", i);
            };
            timerLabel.BeginInvoke(updateTime);
        }

        private void ArgumentExceptionCatched(object sender, ArgumentException e)
        {
            MessageBox.Show("Проверьте верность введенных параметров");
            Thread.CurrentThread.Abort();

        }


        private void AccessErrorHandler(object sender, Exception e)
        {
            Action changeText = () =>
            {
                CurrentFileName.Text = e.Message;
            };
            CurrentFileName.BeginInvoke(changeText);
        }

        private void StartButtonOn_Click(object sender, EventArgs e)
        {
            
               _searchThread = new Thread(StartSearch);
            _numberOfFoundedFiles = 0;
            if (FilesTreeView.Nodes.Count != 0)
            {
                FilesTreeView.Nodes.Clear();//Очищаем дерево
            }
            _searchThread.Start();//Поехали!
        }

        private void StartSearch()
        {
            _searcher.Initialiaze(DirectoryBox.Text, TemplateBox.Text, TextInFileBox.Text);
        }
        private void SearcherOnCurrentFileChanged(Object sender, string s)
        {
            Action changeText = () =>
            {
                CurrentFileName.Text = String.Format("Обрабатывается файл {0}", s);
            };
            CurrentFileName.BeginInvoke(changeText);
        }

        private void StopButtonOn_Click(object sender, EventArgs e)
        {
            _searchThread.Abort();//Убиваем поток поиска
            CurrentFileName.Text = "Поиск остановлен";
        }
    }
}
