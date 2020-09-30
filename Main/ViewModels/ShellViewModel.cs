using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main.ViewModels
{
  public  class ShellViewModel : BindableBase
    {

        #region property Title

        private string title = "Drive Info";

        /// <summary>
        /// Represent Title property
        /// </summary>
        public string Title
        {
            get => title;
            set => SetProperty(ref title, value);
        }

        /// <summary>
        /// Backing field for property Title
        /// </summary>

        #endregion
    }
}
