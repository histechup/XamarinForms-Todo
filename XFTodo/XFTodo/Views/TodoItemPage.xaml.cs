using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XFTodo.ViewModels;
using XFTodo.ViewModels.Base;

namespace XFTodo.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class TodoItemPage : ContentPage
	{
		public TodoItemPage ()
		{
			InitializeComponent ();
		}

	   
	}
}