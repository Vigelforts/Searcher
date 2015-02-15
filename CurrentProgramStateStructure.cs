using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    [Serializable]
    public struct CurrentProgramStateStructure
    {
        //Структура хранит в себе сохраненные значения полей формы поиска
        public string Path;
        public string NameTemplate;
        public string TextTemplate;
    }
}
