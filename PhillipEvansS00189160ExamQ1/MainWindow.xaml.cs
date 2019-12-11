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

namespace PhillipEvansS00189160ExamQ1
{
    //Positions
    public enum Position { GOALKEEPER, DEFENDER, MIDFIELDER, FORWARD }
    
    //Player Class
    public class Player : IComparable
    {
        //Properties
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public Position PreferredPosition { get; set; }
        public DateTime DateofBirth { get; set; }

        public DateTime today = new DateTime();
        private int Age
        {
            get
            {
                return _age;
            }
            set
            {
                //_age = today.Year - DateofBirth.Year;
                _age = DateofBirth.Year - today.Year;
                //if (DateofBirth.Date > today.AddYears(-Age))
                //{
                //    _age--;
                //}

            }
        }

        public int _age { get; set; }

        public Player(string first, string last, int age, Position pos)
        {
            FirstName = first;
            Surname = last;
            _age = age;
        }

        public Player(string first, string last, Position pos, DateTime date, int age)
        {
            FirstName = first;
            Surname = last;
            PreferredPosition = pos;
            DateofBirth = date;
            Age = age;
        }

        public int CompareTo(object obj)
        {
            Player that = (Player)obj;
            return (PreferredPosition.CompareTo(that.PreferredPosition) + FirstName.CompareTo(that.FirstName));
        }

        public override string ToString()
        {
            //Format the date to display as shown in string
            return string.Format($"{FirstName} {Surname} ({_age}) {PreferredPosition}");
        }
    }

    
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Player> players = new List<Player>();
        List<Player> chosen = new List<Player>();
        Random rng = new Random();
        public int spaces = 11;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void WndMain_Loaded(object sender, RoutedEventArgs e)
        {
            LblSpace.Content = $"Spaces {spaces}";
            CreatePlayers();
            players.Sort();
            LstAll.ItemsSource = players;
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (LstAll.SelectedItem != null)
            {
                if (spaces != 0)
                {
                    spaces -= 1;
                    LblSpace.Content = $"Spaces {spaces}";
                    players.Remove((Player)LstAll.SelectedItem);
                    chosen.Add((Player)LstAll.SelectedItem);
                    Update();
                }
                else
                {
                    MessageBox.Show("No Spaces Remaining");
                }
            }
            else
            {
                MessageBox.Show("No Player Selected");
            }
            
        }

        private void BtnRemove_Click(object sender, RoutedEventArgs e)
        {
            if (LstSelected.SelectedItem != null)
            {
                if (spaces != 12)
                {
                    spaces += 1;
                    LblSpace.Content = $"Spaces {spaces}";
                    chosen.Remove((Player)LstSelected.SelectedItem);
                    players.Add((Player)LstSelected.SelectedItem);
                    Update();
                }
                else
                {
                    MessageBox.Show("Exceeding Space Limit");
                }
            }
            else
            {
                MessageBox.Show("No Player Selected");
            }
        }

        //Create a new player
        private Player RandomPlayer()
        {
            string[] firstNames = {
                "Adam", "Amelia", "Ava", "Chloe", "Conor", "Daniel", "Emily",
                "Emma", "Grace", "Hannah", "Harry", "Jack", "James",
                "Lucy", "Luke", "Mia", "Michael", "Noah", "Sean", "Sophie" };

            string[] lastNames = {
                "Brennan", "Byrne", "Daly", "Doyle", "Dunne", "Fitzgerald", "Kavanagh",
                "Kelly", "Lynch", "McCarthy", "McDonagh", "Murphy", "Nolan", "O'Brien",
                "O'Connor", "O'Neill", "O'Reilly", "O'Sullivan", "Ryan", "Walsh"
            };

            Position[] positions = { Position.GOALKEEPER, Position.DEFENDER, Position.MIDFIELDER, Position.MIDFIELDER };

            //get random first and last name
            int rngFirst = rng.Next(0, 20); //0, 1 or 2
            int rngLast = rng.Next(0, 20);
            int rngPos = rng.Next(0, 4);
    
            string first = firstNames[rngFirst];
            string last = lastNames[rngLast];
            Position pos = positions[rngPos];
            //get random date
            int rngDate = rng.Next(19, 31);
            DateTime randomDate = DateTime.Now.AddYears(-rngDate);
            DateTime today = new DateTime();

            int age =  randomDate.Year - today.Year;

            //create Player
            Player p1 = new Player(first, last, age, pos);

            return p1;
        }

        //Create 18 players
        private void CreatePlayers()
        {
            for (int i = 0; i < 18; i++)
            {
                players.Add(RandomPlayer());
            }

        }

        private void Update()
        {
            //sets listbox sources to null to clear the listbox
            LstAll.ItemsSource = null;
            players.Sort();
            LstAll.ItemsSource = players;
            LstSelected.ItemsSource = null;
            chosen.Sort();
            LstSelected.ItemsSource = chosen;
        }
    }
}
