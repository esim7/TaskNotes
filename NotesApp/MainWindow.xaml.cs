using Context;
using Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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

namespace NotesApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public NotesContext Context;
        public List<Note> Notes { get; set; } = new List<Note>();
        public MainWindow()
        {
            InitializeComponent();
            Context = new NotesContext();
            Notes = Context.Notes.ToList();           
            dataGrid.ItemsSource = Notes;
        }

        private void DeleteNoteButton(object sender, RoutedEventArgs e)
        {
            try
            {
                var index = dataGrid.SelectedIndex;
                var id = Notes.ElementAt(index).Id;
                Task.Run(() => DeleteNote(id));
                dataGrid.ItemsSource = null;
                Notes.Remove(Notes.ElementAt(index));
                dataGrid.ItemsSource = Notes;
            }
            catch
            {
                MessageBox.Show("Выберите заметку!");
            } 
        }

        private void SaveNotesButton(object sender, RoutedEventArgs e)
        {
            Task.Run(() => AddOrUpdateNotes());
        }

        public Task AddOrUpdateNotes()
        {
            using (var context = new NotesContext())
            {
                foreach (var item in Notes)
                {
                    var note = context.Notes.AsNoTracking().Any(x => x.Id == item.Id);
                    if (note)
                    {
                        context.Update(item);
                        context.SaveChanges();
                        continue;
                    }
                    context.Add(item);
                    context.SaveChanges();
                }
            }
            return Task.CompletedTask;
        }

        public Task DeleteNote(Guid id)
        {                
            using (var context = new NotesContext())
            {
                var deleteNote = context.Notes.Where(x => x.Id == id).FirstOrDefault();
                context.Remove(deleteNote);
                context.SaveChanges();
            }
            return Task.CompletedTask;
        }

    }
}
