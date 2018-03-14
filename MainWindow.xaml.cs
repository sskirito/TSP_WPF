using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
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
using TSP_WPF;

namespace TSP_WPF
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        static Canvas mainPanel = new Canvas();
        static Canvas sidePanel = new Canvas();
        static Canvas bestPanel = new Canvas();
        static WinData GlobalBest, ASBest, HCBest, GABest;
        public MainWindow()
        {
            InitializeComponent();
            GlobalBest = new WinData
            {
                //runtimes = "0"
            };
            ASBest = new WinData
            {
                //runtimes = "0"
            };
            HCBest = new WinData
            {
                //runtimes = "0"
            };
            GABest = new WinData
            {
                //runtimes = "0"
            };
            Best.DataContext = GlobalBest;
            AS_Best.DataContext = ASBest;
            HillClimbing_Best.DataContext = HCBest;
            GA_Best.DataContext = GABest;
            CCC_Copy1.Content = bestPanel;
            printOpt();
        }
        static async public void DrawingLine(Point startPt, Point endPt)
        {
            await mainPanel.Dispatcher.BeginInvoke((Action)(() =>
            {
                mainPanel.Children.Add(new Line
                {
                    X1 = startPt.X * 3,
                    X2 = endPt.X * 3,
                    Y1 = startPt.Y * 3,
                    Y2 = endPt.Y * 3,
                    Stroke = Brushes.Black
                });
            }));
        }
        static async public void DrawingLine2(Point startPt, Point endPt)
        {
            await sidePanel.Dispatcher.BeginInvoke((Action)(() =>
            {
                sidePanel.Children.Add(new Line
                {
                    X1 = startPt.X * 3,
                    X2 = endPt.X * 3,
                    Y1 = startPt.Y * 3,
                    Y2 = endPt.Y * 3,
                    Stroke = Brushes.Black
                });
            }));
        }

        static async public void DrawingLine3(Point startPt, Point endPt)
        {
            await bestPanel.Dispatcher.BeginInvoke((Action)(() =>
            {
                bestPanel.Children.Add(new Line
                {
                    X1 = startPt.X * 1.5,
                    X2 = endPt.X * 1.5,
                    Y1 = startPt.Y * 1.5,
                    Y2 = endPt.Y * 1.5,
                    Stroke = Brushes.Black
                });
            }));
        }
        static async public void Clear()
        {
            //mainPanel.Children.Clear();
            //System.Windows.Application.Current.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal,
            //                                            new Program.DeleFunc(mainPanel.Children.Clear));
            await mainPanel.Dispatcher.BeginInvoke((Action)(() =>
            {
                mainPanel.Children.Clear();
            }));
        }
        static async public void ClearSide()
        {
            //mainPanel.Children.Clear();
            //System.Windows.Application.Current.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal,
            //                                            new Program.DeleFunc(mainPanel.Children.Clear));
            await sidePanel.Dispatcher.BeginInvoke((Action)(() =>
            {
                sidePanel.Children.Clear();
            }));
        }

        private void Start(object sender, RoutedEventArgs e)
        {
            CCC.Content = mainPanel;
            CCC_Copy.Content = sidePanel;
            //test.Content = ouput;
            AS.INITIAL_TEMPERATURE = double.Parse(Init_Temperature.Text);
            AS.MIN_TEMPERATURE = double.Parse(Min_Temperature.Text);
            AS.SPEED = double.Parse(speed.Text);


            //Thread t2 = new Thread(new ThreadStart(Program.Run1));
            //t2.IsBackground = true;
            //t2.Start();
            ////Console.ReadKey();
            //System.Windows.Application.Current.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal,
            //                                            new Program.DeleFunc(Program.Run1));
            ////await Task.Factory.StartNew(() =>
            //    Program.Run1());
            //Program.Run1();
            //Thread NetServer = new Thread(new ThreadStart(Program.Run1));
            //NetServer.SetApartmentState(ApartmentState.STA);
            //NetServer.IsBackground = true;
            //NetServer.Start();
            //printOpt();
            Thread anotherThread = new Thread(() =>
            {
                //for (int i = 0; i < 10; i++)
                //{
                //    mainPanel.Dispatcher.BeginInvoke((Action)(() =>
                //    {
                //        mainPanel.Children.Add(new Ellipse
                //        {
                //            Height = 30,
                //            Width = 60,
                //            Fill = i % 2 == 0 ? Brushes.Red : Brushes.Yellow,
                //            Stroke = i % 2 == 0 ? Brushes.Black : Brushes.Green
                //        });
                //    }));

                //    Thread.Sleep(1000);
                //}
                Program.ParallelInvokeMethod();
                string TextOut = "";
                TextOut += "程序输出结果为：";
                TextOut += Convert.ToDouble(AS.BestRoute.GetTotalStringDistance()) > Convert.ToDouble(GABest.runtimes) ? GABest.runtimes : AS.BestRoute.GetTotalStringDistance();
                TextOut += "\n";
                TextOut += "实际最优结果为：629\n";
                TextOut += "结果比较：";
                double Compare = (AS.BestRoute.GetTotalDistance() - AS.RealBestRoute) / AS.BestRoute.GetTotalDistance();
                TextOut += Compare.ToString();
                SetText(TextOut);
            });

            anotherThread.SetApartmentState(ApartmentState.STA);
            anotherThread.Start();
        }

        public async void SetText(string TextOut)
        {
            
            await Result.Dispatcher.BeginInvoke(new Action(delegate
            {
                Result.Text = TextOut;
            }));
        }

        static public void SetT(string str, string me)
        {
            if (Convert.ToDouble(str) == 0)
                return;
            if (me == "AS")
                ASBest.runtimes = str;
            else if (me == "HC")
                HCBest.runtimes = str;
            GlobalBest.runtimes = str;
        }

        static public string GetASBest()
        {
            return ASBest.runtimes;
        }
        static public string GetHCBest()
        {
            return HCBest.runtimes;
        }
        static public string GetGlobalBest()
        {
            return GlobalBest.runtimes;
        }

        static public void SetGABest(string str)
        {
            GABest.runtimes = str;
        }

        public void printOpt()
        {
            string me = "printOpt";
            List<City> list_cities = new List<City> { };
            List<City> ans_cities = new List<City> { };
            string[] lines = System.IO.File.ReadAllLines(@"..\..\eil101.tsp");
            //System.Console.WriteLine("Contents of WriteLines2.txt = ");
            bool flag = false;

            foreach (string line in lines)
            {
                // Use a tab to indent each line of the file.
                if (flag && line != "EOF")
                {
                    List<string> list = new List<string>(line.Split(' '));

                    int count = list.Count;
                    for (int i = 0; i < count; i++)
                        if (list[i].Count() == 0)
                        {
                            // 记录当前位置
                            int newCount = i++;

                            // 对每个非空元素，复制至当前位置 O(n)
                            for (; i < count; i++)
                                if (list[i].Count() != 0)
                                    list[newCount++] = list[i];

                            // 移除多余的元素 O(n)
                            list.RemoveRange(newCount, count - newCount);
                            break;
                        }
                    for (int i = 0; i < list.Count; i += 3)
                    {
                        list_cities.Add(new City(Convert.ToDouble(list[i + 1]), Convert.ToDouble(list[i + 2]), list[i]));
                    }

                }

                if (line == "NODE_COORD_SECTION")
                    flag = true;
            }

            string[] lines2 = System.IO.File.ReadAllLines(@"..\..\eil101.opt.tour");
            flag = false;
            foreach (string line in lines2)
            {
                // Use a tab to indent each line of the file.
                if (flag && line != "-1" && line!= "EOF")
                {
                    int k = Convert.ToInt32(line);
                    ans_cities.Add(list_cities[k - 1]);
                }
                if (line == "TOUR_SECTION")
                {
                    flag = true;
                }
            }

            List<Point> l = new List<Point>();
            foreach (var c in ans_cities)
            {
                l.Add(new Point(c.GetX(), c.GetY()));
            }
            
            for (var i = 0; i < l.Count; i++)
            {
                MainWindow.DrawingLine3(l[i], l[(i + 1) % l.Count]);
            }
            
        }
    }



    public static class StringLength
    {
        static public string str;
        static public double cnt = 9999999;
    }

    class City
    {
        private double X;

        private double Y;

        private String name;

        public City(double X, double Y, String name)
        {
            this.X = X;
            this.Y = Y;
            this.name = name;
        }

        public double GetX()
        {
            return X;
        }
        public void SetX(double X)
        {
            this.X = X;
        }
        public double GetY()
        {
            return Y;
        }
        public void SetY(double Y)
        {
            this.Y = Y;
        }
        public String GetName()
        {
            return name;
        }
        public void SetName(String name)
        {
            this.name = name;
        }

        public String toString()
        {
            return name;
        }

        public double MeasureDistance(City city)
        {
            double x = Math.Abs(city.X - X);
            double y = Math.Abs(city.Y - Y);
            return Math.Sqrt(x * x + y * y);
        }
    }

    class WinData : INotifyPropertyChanged
    {
        //必须实现
        public event PropertyChangedEventHandler PropertyChanged;
        private string _runtimes;//私有  
        public string runtimes
        {
            //获取值时将私有字段传出；  
            get { return _runtimes; }
            set
            {
                //赋值时将值传给私有字段  
                _runtimes = value;
                //一旦执行了赋值操作说明其值被修改了，则立马通过INotifyPropertyChanged接口告诉UI(IntValue)被修改了  
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("runtimes"));
            }
        }
    }

    class Route
    {
        private List<City> cities = new List<City>();
        public double sum = 0;
        public Route(List<City> cities)
        {
            this.cities = cities;
            var count = cities.Count;
            var last = count - 1;
            var rnd = new Random();
            for (var i = 0; i < last; ++i)
            {
                var r = rnd.Next(i, count);
                var tmp = cities[i];
                cities[i] = cities[r];
                cities[r] = tmp;
            }

            //var result = this.cities.OrderBy(item => rnd.Next());
            //Collections.shuffle(this.cities);
        }

        public Route(Route route)
        {
            foreach (City city in route.GetCities())
                this.cities.Add(city);
        }

        public double GetTotalDistance()
        {
            int citiesSize = cities.Count;
            //double sum = 0D;
            sum = 0;
            int index = 0;
            while (index < citiesSize - 1)
            {
                sum += this.cities[index].MeasureDistance(this.cities[index + 1]);
                index++;
            }
            sum += this.cities[citiesSize - 1].MeasureDistance(this.cities[0]);
            return sum;
        }

        public String GetTotalStringDistance()
        {
            //String returnString = String.Format("{0}", this.GetTotalDistance());
            return sum.ToString();
        }

        public List<City> GetCities()
        {
            return cities;
        }

        public void SetCities(List<City> cities)
        {
            this.cities = cities;
        }

        public String toString()
        {
            string str = "";
            foreach (var c in cities)
            {
                str += c.toString() + " ";
            }
            return str;
        }

        public List<Point> ToList()
        {
            List<Point> list = new List<Point>();
            foreach (var c in cities)
            {
                list.Add(new Point(c.GetX(), c.GetY()));
            }
            return list;
        }

    }

    class AS
    {
        // 最大迭代次数
        public static int ITERATIONS_BEFORE_MAXIMUM = 550000;
        public static double INITIAL_TEMPERATURE = 100.0;
        public static double SPEED = 0.98;
        public static double MIN_TEMPERATURE = 0.001;
        public static Route BestRoute, ASBestRoute, HCBestRoute;
        public static double RealBestRoute = 629;
        public static Random random = new Random();

        public Route FindShortestRouteHillClimbing(Route currentRoute, string me, int Method)
        {
            Route adjacentRoute;
            int iterToMaximumCounter = 0;
            double Current_Temperature = INITIAL_TEMPERATURE;
            String compareRoutes = null;

            while (iterToMaximumCounter < ITERATIONS_BEFORE_MAXIMUM)
            {
                switch (Method)
                {
                    case 1:
                        adjacentRoute = ObtainAdjacentRouteInsert(new Route(currentRoute));
                        break;
                    case 2:
                        adjacentRoute = ObtainAdjacentRouteExchange(new Route(currentRoute));
                        break;
                    case 3:
                        adjacentRoute = ObtainAdjacentRouteReverse(new Route(currentRoute), 1);
                        break;
                    default:
                        adjacentRoute = ObtainAdjacentRouteExchange(new Route(currentRoute));
                        break;
                }
                double distance = adjacentRoute.GetTotalDistance();
                if (distance < currentRoute.GetTotalDistance())
                {
                    compareRoutes = "更新";
                    iterToMaximumCounter = 0;
                    currentRoute = new Route(adjacentRoute);
                }

                if (StringLength.cnt > distance)
                {
                    StringLength.cnt = distance;
                    //Console.WriteLine("  | " + compareRoutes);
                    //Console.Write(currentRoute.toString() + " |  " + currentRoute.GetTotalStringDistance());
                    var list = currentRoute.ToList();
                    MainWindow.ClearSide();
                    for (var i = 0; i < list.Count; i++)
                    {
                        MainWindow.DrawingLine2(list[i], list[(i + 1) % list.Count]);
                    }
                    BestRoute = currentRoute;
                }
                var strDistance = currentRoute.GetTotalStringDistance();
                var asb = MainWindow.GetHCBest();
                if (Convert.ToDouble(MainWindow.GetHCBest()) > Convert.ToDouble(strDistance) || Convert.ToDouble(MainWindow.GetHCBest()) == 0)
                    MainWindow.SetT(strDistance, me);


            }

            //Console.WriteLine("  | " + compareRoutes);
            //Console.Write(" |  " + currentRoute.GetTotalStringDistance() + "   | 可能的最优解");
            Console.WriteLine(me + "Over");

            return currentRoute;
        }

        public Route FindShortestRouteAS(Route currentRoute, string me, int Method)
        {
            Route adjacentRoute;
            int iterToMaximumCounter = 0;
            double Current_Temperature = INITIAL_TEMPERATURE;
            String compareRoutes = null;

            while (Current_Temperature > MIN_TEMPERATURE)
            {
                while (iterToMaximumCounter < ITERATIONS_BEFORE_MAXIMUM)
                {
                    switch (Method)
                    {
                        case 1:
                            adjacentRoute = ObtainAdjacentRouteInsert(new Route(currentRoute));
                            break;
                        case 2:
                            adjacentRoute = ObtainAdjacentRouteExchange(new Route(currentRoute));
                            break;
                        case 3:
                            adjacentRoute = ObtainAdjacentRouteReverse(new Route(currentRoute), 2);
                            break;
                        default:
                            adjacentRoute = ObtainAdjacentRouteReverse(new Route(currentRoute), 2);
                            break;
                    }
                    double distance = adjacentRoute.GetTotalDistance();
                    if (distance < currentRoute.GetTotalDistance())
                    {
                        compareRoutes = "更新";
                        iterToMaximumCounter = 0;
                        currentRoute = new Route(adjacentRoute);
                    }
                    else
                    {
                        //Random random = new Random();
                        if ((int)Math.Exp((currentRoute.GetTotalDistance() - distance) / Current_Temperature) > random.NextDouble())
                        {
                            compareRoutes = "更新";
                            currentRoute = new Route(adjacentRoute);
                        }
                        else
                        {
                            compareRoutes = " 当前迭代次数： " + iterToMaximumCounter;
                            iterToMaximumCounter++;
                        }
                    }

                    if (StringLength.cnt > distance)
                    {
                        StringLength.cnt = distance;
                        //Console.WriteLine("  | " + compareRoutes);
                        //Console.Write(currentRoute.toString() + " |  " + currentRoute.GetTotalStringDistance());
                        var list = currentRoute.ToList();
                        MainWindow.ClearSide();
                        for (var i = 0; i < list.Count; i++)
                        {
                            MainWindow.DrawingLine2(list[i], list[(i + 1) % list.Count]);
                        }
                        BestRoute = currentRoute;
                        
                    }
                    var strDistance = currentRoute.GetTotalStringDistance();
                    var asb = MainWindow.GetASBest();
                    if (Convert.ToDouble(MainWindow.GetASBest()) > Convert.ToDouble(strDistance)|| Convert.ToDouble(MainWindow.GetASBest()) == 0)
                        MainWindow.SetT(strDistance, me);
                }
                Current_Temperature *= SPEED;
                if (Method == 2) Console.WriteLine(Current_Temperature.ToString());
            }

            //Console.WriteLine("  | " + compareRoutes);
            //Console.Write(" |  " + currentRoute.GetTotalStringDistance() + "   | 可能的最优解");
            Console.WriteLine(me + "Over");

            return currentRoute;
        }

        public Route ObtainAdjacentRouteReverse(Route route, int HCorAS)
        {
            int x1 = 0, x2 = 0;
            //Random r = new Random();
            while (x1 == x2)
            {
                //if (HCorAS == 1) {
                //    x1 = (int)(route.GetCities().Count * randomForHCReverse.NextDouble());
                //    x2 = (int)(route.GetCities().Count * randomForHCReverse.NextDouble());
                //} else {
                //    x1 = (int)(route.GetCities().Count * randomForASReverse.NextDouble());
                //    x2 = (int)(route.GetCities().Count * randomForASReverse.NextDouble());
                //}
                x1 = (int)(route.GetCities().Count * random.NextDouble());
                x2 = (int)(route.GetCities().Count * random.NextDouble());
            }

            if (x1>x2)
            {
                var x = x1;
                x1 = x2;
                x2 = x;
            }
            route.GetCities().Reverse(x1, x2-x1);
            return route;
        }

        public Route ObtainAdjacentRouteExchange(Route route)
        {
            int x1 = 0, x2 = 0;
            //Random r = new Random();
            while (x1 == x2)
            {
                x1 = (int)(route.GetCities().Count * random.NextDouble());
                x2 = (int)(route.GetCities().Count * random.NextDouble());
            }
            City city1 = route.GetCities()[x1];
            City city2 = route.GetCities()[x2];
            route.GetCities()[x2] = city1;
            route.GetCities()[x1] = city2;
            return route;
        }

        public Route ObtainAdjacentRouteInsert(Route route)
        {
            int x1 = 0, x2 = 0;
            Random r = new Random();
            while (x1 == x2)
            {
                x1 = (int)(route.GetCities().Count * r.NextDouble());
                x2 = (int)(route.GetCities().Count * r.NextDouble());
            }
            if (x1 > x2)
            {
                var x = x1;
                x1 = x2;
                x2 = x;
            }
            City city1 = route.GetCities()[x2];
            route.GetCities().RemoveAt(x2);
            route.GetCities().Insert(x1, city1);
            return route;
        }
    }


    class Program
    {
        public delegate void DeleFunc();
        static public void ASReverse1()
        {
            string me = "AS";
            List<City> cities = new List<City> { };
            string[] lines = System.IO.File.ReadAllLines(@"..\..\eil101.tsp");
            //System.Console.WriteLine("Contents of WriteLines2.txt = ");
            bool flag = false;
            foreach (string line in lines)
            {

                // Use a tab to indent each line of the file.
                if (flag && line != "EOF")
                {
                    //Console.WriteLine("\t" + line);
                    List<string> list = new List<string>(line.Split(' '));

                    int count = list.Count;
                    for (int i = 0; i < count; i++)
                        if (list[i].Count() == 0)
                        {
                            // 记录当前位置
                            int newCount = i++;

                            // 对每个非空元素，复制至当前位置 O(n)
                            for (; i < count; i++)
                                if (list[i].Count() != 0)
                                    list[newCount++] = list[i];

                            // 移除多余的元素 O(n)
                            list.RemoveRange(newCount, count - newCount);
                            break;
                        }

                    for (int i = 0; i < list.Count; i += 3)
                    {
                        cities.Add(new City(Convert.ToDouble(list[i + 1]), Convert.ToDouble(list[i + 2]), list[i]));
                    }

                    //foreach (var i in list)
                    //{
                    //    Console.WriteLine(i);
                    //}

                }

                if (line == "NODE_COORD_SECTION")
                    flag = true;
            }
            Route route = new Route(cities);
            new AS().FindShortestRouteAS(route, me, 3);
        }
        static public void ASExchange1()
        {
            string me = "AS";
            List<City> cities = new List<City> { };
            string[] lines = System.IO.File.ReadAllLines(@"..\..\eil101.tsp");
            //System.Console.WriteLine("Contents of WriteLines2.txt = ");
            bool flag = false;
            foreach (string line in lines)
            {

                // Use a tab to indent each line of the file.
                if (flag && line != "EOF")
                {
                    //Console.WriteLine("\t" + line);
                    List<string> list = new List<string>(line.Split(' '));

                    int count = list.Count;
                    for (int i = 0; i < count; i++)
                        if (list[i].Count() == 0)
                        {
                            // 记录当前位置
                            int newCount = i++;

                            // 对每个非空元素，复制至当前位置 O(n)
                            for (; i < count; i++)
                                if (list[i].Count() != 0)
                                    list[newCount++] = list[i];

                            // 移除多余的元素 O(n)
                            list.RemoveRange(newCount, count - newCount);
                            break;
                        }

                    for (int i = 0; i < list.Count; i += 3)
                    {
                        cities.Add(new City(Convert.ToDouble(list[i + 1]), Convert.ToDouble(list[i + 2]), list[i]));
                    }

                    //foreach (var i in list)
                    //{
                    //    Console.WriteLine(i);
                    //}

                }

                if (line == "NODE_COORD_SECTION")
                    flag = true;
            }
            Route route = new Route(cities);
            new AS().FindShortestRouteAS(route, me, 2);
        }
        static public void ASInsert1()
        {
            string me = "AS";
            List<City> cities = new List<City> { };
            string[] lines = System.IO.File.ReadAllLines(@"..\..\eil101.tsp");
            //System.Console.WriteLine("Contents of WriteLines2.txt = ");
            bool flag = false;
            foreach (string line in lines)
            {

                // Use a tab to indent each line of the file.
                if (flag && line != "EOF")
                {
                    //Console.WriteLine("\t" + line);
                    List<string> list = new List<string>(line.Split(' '));

                    int count = list.Count;
                    for (int i = 0; i < count; i++)
                        if (list[i].Count() == 0)
                        {
                            // 记录当前位置
                            int newCount = i++;

                            // 对每个非空元素，复制至当前位置 O(n)
                            for (; i < count; i++)
                                if (list[i].Count() != 0)
                                    list[newCount++] = list[i];

                            // 移除多余的元素 O(n)
                            list.RemoveRange(newCount, count - newCount);
                            break;
                        }

                    for (int i = 0; i < list.Count; i += 3)
                    {
                        cities.Add(new City(Convert.ToDouble(list[i + 1]), Convert.ToDouble(list[i + 2]), list[i]));
                    }

                    //foreach (var i in list)
                    //{
                    //    Console.WriteLine(i);
                    //}

                }

                if (line == "NODE_COORD_SECTION")
                    flag = true;
            }
            Route route = new Route(cities);
            new AS().FindShortestRouteAS(route, me, 1);

        }
        //static public void ASReverse2()
        //{
        //    string me = "AS";
        //    List<City> cities = new List<City> { };
        //    string[] lines = System.IO.File.ReadAllLines(@"..\..\eil101.tsp");
        //    //System.Console.WriteLine("Contents of WriteLines2.txt = ");
        //    bool flag = false;
        //    foreach (string line in lines)
        //    {

        //        // Use a tab to indent each line of the file.
        //        if (flag && line != "EOF")
        //        {
        //            //Console.WriteLine("\t" + line);
        //            List<string> list = new List<string>(line.Split(' '));

        //            int count = list.Count;
        //            for (int i = 0; i < count; i++)
        //                if (list[i].Count() == 0)
        //                {
        //                    // 记录当前位置
        //                    int newCount = i++;

        //                    // 对每个非空元素，复制至当前位置 O(n)
        //                    for (; i < count; i++)
        //                        if (list[i].Count() != 0)
        //                            list[newCount++] = list[i];

        //                    // 移除多余的元素 O(n)
        //                    list.RemoveRange(newCount, count - newCount);
        //                    break;
        //                }

        //            for (int i = 0; i < list.Count; i += 3)
        //            {
        //                cities.Add(new City(Convert.ToDouble(list[i + 1]), Convert.ToDouble(list[i + 2]), list[i]));
        //            }

        //            //foreach (var i in list)
        //            //{
        //            //    Console.WriteLine(i);
        //            //}

        //        }

        //        if (line == "NODE_COORD_SECTION")
        //            flag = true;
        //    }
        //    Route route = new Route(cities);
        //    new AS().FindShortestRouteAS(route, me, 3);
        //}
        //static public void ASExchange2()
        //{
        //    string me = "AS";
        //    List<City> cities = new List<City> { };
        //    string[] lines = System.IO.File.ReadAllLines(@"..\..\eil101.tsp");
        //    //System.Console.WriteLine("Contents of WriteLines2.txt = ");
        //    bool flag = false;
        //    foreach (string line in lines)
        //    {

        //        // Use a tab to indent each line of the file.
        //        if (flag && line != "EOF")
        //        {
        //            //Console.WriteLine("\t" + line);
        //            List<string> list = new List<string>(line.Split(' '));

        //            int count = list.Count;
        //            for (int i = 0; i < count; i++)
        //                if (list[i].Count() == 0)
        //                {
        //                    // 记录当前位置
        //                    int newCount = i++;

        //                    // 对每个非空元素，复制至当前位置 O(n)
        //                    for (; i < count; i++)
        //                        if (list[i].Count() != 0)
        //                            list[newCount++] = list[i];

        //                    // 移除多余的元素 O(n)
        //                    list.RemoveRange(newCount, count - newCount);
        //                    break;
        //                }

        //            for (int i = 0; i < list.Count; i += 3)
        //            {
        //                cities.Add(new City(Convert.ToDouble(list[i + 1]), Convert.ToDouble(list[i + 2]), list[i]));
        //            }

        //            //foreach (var i in list)
        //            //{
        //            //    Console.WriteLine(i);
        //            //}

        //        }

        //        if (line == "NODE_COORD_SECTION")
        //            flag = true;
        //    }
        //    Route route = new Route(cities);
        //    new AS().FindShortestRouteAS(route, me, 2);

        //}
        //static public void ASInsert2()
        //{
        //    string me = "AS";
        //    List<City> cities = new List<City> { };
        //    string[] lines = System.IO.File.ReadAllLines(@"..\..\eil101.tsp");
        //    //System.Console.WriteLine("Contents of WriteLines2.txt = ");
        //    bool flag = false;
        //    foreach (string line in lines)
        //    {

        //        // Use a tab to indent each line of the file.
        //        if (flag && line != "EOF")
        //        {
        //            //Console.WriteLine("\t" + line);
        //            List<string> list = new List<string>(line.Split(' '));

        //            int count = list.Count;
        //            for (int i = 0; i < count; i++)
        //                if (list[i].Count() == 0)
        //                {
        //                    // 记录当前位置
        //                    int newCount = i++;

        //                    // 对每个非空元素，复制至当前位置 O(n)
        //                    for (; i < count; i++)
        //                        if (list[i].Count() != 0)
        //                            list[newCount++] = list[i];

        //                    // 移除多余的元素 O(n)
        //                    list.RemoveRange(newCount, count - newCount);
        //                    break;
        //                }

        //            for (int i = 0; i < list.Count; i += 3)
        //            {
        //                cities.Add(new City(Convert.ToDouble(list[i + 1]), Convert.ToDouble(list[i + 2]), list[i]));
        //            }

        //            //foreach (var i in list)
        //            //{
        //            //    Console.WriteLine(i);
        //            //}

        //        }

        //        if (line == "NODE_COORD_SECTION")
        //            flag = true;
        //    }
        //    Route route = new Route(cities);
        //    new AS().FindShortestRouteAS(route, me, 1);
        //}
        static public void HillClimbingReverse()
        {
            string me = "HC";
            List<City> cities = new List<City> { };
            string[] lines = System.IO.File.ReadAllLines(@"..\..\eil101.tsp");
            //System.Console.WriteLine("Contents of WriteLines2.txt = ");
            bool flag = false;
            foreach (string line in lines)
            {

                // Use a tab to indent each line of the file.
                if (flag && line != "EOF")
                {
                    //Console.WriteLine("\t" + line);
                    List<string> list = new List<string>(line.Split(' '));

                    int count = list.Count;
                    for (int i = 0; i < count; i++)
                        if (list[i].Count() == 0)
                        {
                            // 记录当前位置
                            int newCount = i++;

                            // 对每个非空元素，复制至当前位置 O(n)
                            for (; i < count; i++)
                                if (list[i].Count() != 0)
                                    list[newCount++] = list[i];

                            // 移除多余的元素 O(n)
                            list.RemoveRange(newCount, count - newCount);
                            break;
                        }

                    for (int i = 0; i < list.Count; i += 3)
                    {
                        cities.Add(new City(Convert.ToDouble(list[i + 1]), Convert.ToDouble(list[i + 2]), list[i]));
                    }

                    //foreach (var i in list)
                    //{
                    //    Console.WriteLine(i);
                    //}

                }

                if (line == "NODE_COORD_SECTION")
                    flag = true;
            }
            Route route = new Route(cities);
            new AS().FindShortestRouteHillClimbing(route, me, 1);

        }
        static public void HillClimbingInsert()
        {
            string me = "HC";
            List<City> cities = new List<City> { };
            string[] lines = System.IO.File.ReadAllLines(@"..\..\eil101.tsp");
            //System.Console.WriteLine("Contents of WriteLines2.txt = ");
            bool flag = false;
            foreach (string line in lines)
            {

                // Use a tab to indent each line of the file.
                if (flag && line != "EOF")
                {
                    //Console.WriteLine("\t" + line);
                    List<string> list = new List<string>(line.Split(' '));

                    int count = list.Count;
                    for (int i = 0; i < count; i++)
                        if (list[i].Count() == 0)
                        {
                            // 记录当前位置
                            int newCount = i++;

                            // 对每个非空元素，复制至当前位置 O(n)
                            for (; i < count; i++)
                                if (list[i].Count() != 0)
                                    list[newCount++] = list[i];

                            // 移除多余的元素 O(n)
                            list.RemoveRange(newCount, count - newCount);
                            break;
                        }

                    for (int i = 0; i < list.Count; i += 3)
                    {
                        cities.Add(new City(Convert.ToDouble(list[i + 1]), Convert.ToDouble(list[i + 2]), list[i]));
                    }

                    //foreach (var i in list)
                    //{
                    //    Console.WriteLine(i);
                    //}

                }

                if (line == "NODE_COORD_SECTION")
                    flag = true;
            }
            Route route = new Route(cities);
            new AS().FindShortestRouteHillClimbing(route, me, 2);
        }

        //static public void GA()
        //{
        //    int lastBest = 0;
        //    var bestSolutionIndex = GA_TSP_CS.Program.bestSolutionIndex;
        //    while (true)
        //    {
        //        bestSolutionIndex = GA_TSP_CS.Program.bestSolutionIndex;
        //        if (lastBest != bestSolutionIndex)
        //        {
                    
        //            lastBest = bestSolutionIndex;
        //            //Console.WriteLine("current best is " + GA_TSP_CS.Program.group[bestSolutionIndex].adapt.ToString());
        //            var list = GA_TSP_CS.Program.group[bestSolutionIndex].city;
        //            var cities = GA_TSP_CS.Program.cities;
        //            MainWindow.Clear();
        //            for (var i = 0; i < GA_TSP_CS.Program.cityCount; i++)
        //            {
        //                MainWindow.DrawingLine(new Point(cities[list[i]].x, cities[list[i]].y), new Point(cities[list[(i + 1) % GA_TSP_CS.Program.cityCount]].x, cities[list[(i + 1) % GA_TSP_CS.Program.cityCount]].y));
        //            }
        //        }
        //    }
                
        //}

        static public void ParallelInvokeMethod()
        {
            Parallel.Invoke(GA_TSP_CS.Program.GA, ASReverse1, HillClimbingReverse);
            //Console.WriteLine("\n  |  结束" + AS.BestRoute.GetTotalStringDistance() + "  |  ");
        }
    }


}

namespace GA_TSP_CS
{
    class Program
    {
        public static int cityCount;
        public const int MAXN = 100000;
        public const int GroupSize = 20;
        public const int ChildrenSize = GroupSize;
        public const int GreatOneSize = 1;
        public const double pc = 0.8;
        public const double pm = 0.05;

        public static int bestSolutionIndex, lastBest;
        public static double bestSolution = double.MaxValue;
        public static double[,] distance;
        public static List<City> cities = new List<City>();

        public static Group[] group = new Group[GroupSize];
        public static Group[] groupTemp = new Group[GroupSize];
        public static Random random = new Random();

        public static void GA()
        {
            Init();
            GroupProduce();
            Evaluate();
            int t = 0;
            while (t++ < MAXN)
            {
                Choose();
                Cross();
                Mutation();
                Reverse();
                Console.Write("Generation " + t.ToString());
                Evaluate();
                if (lastBest != bestSolutionIndex)
                {
                    lastBest = bestSolutionIndex;
                    MainWindow.SetGABest(group[bestSolutionIndex].adapt.ToString());
                    Console.WriteLine("current best is " + group[bestSolutionIndex].adapt.ToString());
                    var list = group[bestSolutionIndex].city;
                    MainWindow.Clear();
                    for (var i = 0; i < cityCount; i++)
                    {
                        MainWindow.DrawingLine(new Point(cities[list[i]].x, cities[list[i]].y), new Point(cities[list[(i + 1) % cityCount]].x, cities[list[(i + 1) % cityCount]].y));
                    }
                }
                //GetBestSolution();
            }
            //Console.WriteLine("\nFinal");
            //for (int i = 0; i < GroupSize; i++) {
            //    for (int j = 0; j < cityCount; j++) {
            //        Console.Write(group[i].city[j].ToString() + " ");
            //    }
            //    Console.Write("  adapt:" + group[i].adapt + " p: " + group[i].p.ToString() + "\n");
            //}
            Console.WriteLine("The best one is " + group[bestSolutionIndex].adapt.ToString());
            //Console.ReadKey();
        }

        static void Init()
        {

            string[] lines = System.IO.File.ReadAllLines(@"../../eil101.tsp");

            bool flag = false;
            foreach (string line in lines)
            {
                // Use a tab to indent each line of the file.
                if (flag && line != "EOF")
                {
                    List<string> list = new List<string>(line.Split(' '));

                    int count = list.Count;
                    for (int i = 0; i < count; i++)
                        if (list[i].Count() == 0)
                        {
                            // 记录当前位置
                            int newCount = i++;

                            // 对每个非空元素，复制至当前位置 O(n)
                            for (; i < count; i++)
                                if (list[i].Count() != 0)
                                    list[newCount++] = list[i];

                            // 移除多余的元素 O(n)
                            list.RemoveRange(newCount, count - newCount);
                            break;
                        }
                    for (int i = 0; i < list.Count; i += 3)
                    {
                        cities.Add(new City(Convert.ToInt32(list[i + 1]), Convert.ToInt32(list[i + 2]), Convert.ToInt32(list[i])));
                    }

                }

                if (line == "NODE_COORD_SECTION")
                    flag = true;
            }

            foreach (var city in cities)
            {
                Console.WriteLine(city.x.ToString() + " " + city.y.ToString());
            }

            cityCount = cities.Count();

            distance = new double[cityCount, cityCount];

            for (int i = 0; i < cityCount; i++)
            {
                for (int j = i + 1; j < cityCount; j++)
                {
                    distance[i, j] = CalculateDistance(cities[i], cities[j]);
                    distance[j, i] = distance[i, j];
                }
            }
            
        }

        static void GroupProduce()
        {
            for (int i = 0; i < GroupSize; i++)
            {
                group[i] = new Group();
                groupTemp[i] = new Group();
                for (int j = 0; j < cityCount; j++)
                {
                    group[i].city[j] = -1;
                }
            }

            int[] baseArray = new int[cityCount];
            for (int i = 0; i < cityCount; i++)
            {
                baseArray[i] = i;
            }
            foreach (Group x in group)
            {
                int[] tempArray = baseArray.OrderBy(y => System.Guid.NewGuid()).ToArray();
                Array.Copy(tempArray, x.city, cityCount);
            }

        }

        static void Evaluate()
        {
            double distanceSum, tempSum = 0;
            double sumP = 0;
            int n1, n2;
            for (int i = 0; i < GroupSize; i++)
            {
                distanceSum = 0;
                for (int j = 1; j < cityCount; j++)
                {
                    n1 = group[i].city[j - 1];
                    n2 = group[i].city[j];
                    distanceSum += distance[n1, n2];
                }
                distanceSum += distance[group[i].city[0], group[i].city[cityCount - 1]];
                group[i].adapt = distanceSum;
                tempSum += distanceSum;
            }

            for (int i = 0; i < GroupSize; i++)
            {
                group[i].p = 1.0 - group[i].adapt / tempSum;
                sumP += group[i].p;
            }
            for (int i = 0; i < GroupSize; i++)
            {
                group[i].p = group[i].p / sumP;
            }

            bestSolutionIndex = 0;
            for (int i = 0; i < GroupSize; i++)
            {
                if (group[i].p > group[bestSolutionIndex].p)
                    bestSolutionIndex = i;
            }

            //for (int i = 0; i < GroupSize; i++) {
            //    Console.WriteLine("Member " + i.ToString() + "Path Length " + group[i].adapt.ToString() + "P: " + group[i].p.ToString());
            //}
            Console.WriteLine("current best is " + group[bestSolutionIndex].adapt.ToString());
        }

        static void Choose()
        {
            double[] gradient = new double[GroupSize];
            //double[] choose = new double[GroupSize];
            int[] choosen = new int[GroupSize];
            gradient[0] = group[0].p;
            for (int i = 1; i < GroupSize; i++)
            {
                gradient[i] = gradient[i - 1] + group[i].p;
            }
            //for(int i = 0; i < GroupSize; i++)
            //{
            //    choose[i] = random.NextDouble();
            //}
            for (int i = 1; i < GroupSize; i++)
            {
                double p = random.NextDouble();
                for (int j = 0; j < GroupSize; j++)
                {
                    if (p <= gradient[j])
                    {
                        choosen[i] = j;
                        break;
                    }
                }
            }
            //Console.WriteLine("After Choose");
            //foreach(var x in choosen)
            //{
            //    Console.WriteLine(x.ToString());
            //}
            for (int i = 0; i < GroupSize; i++)
            {
                groupTemp[i].adapt = group[i].adapt;
                groupTemp[i].p = group[i].p;
                Array.Copy(group[i].city, groupTemp[i].city, cityCount);
            }

            int temp;

            int ii;
            for (ii = 0; ii < GreatOneSize; ii++)
            {
                group[ii].adapt = groupTemp[bestSolutionIndex].adapt;
                group[ii].p = groupTemp[bestSolutionIndex].p;
                Array.Copy(groupTemp[bestSolutionIndex].city, group[ii].city, cityCount);
            }

            for (; ii < GroupSize; ii++)
            {
                temp = choosen[ii];
                group[ii].adapt = groupTemp[temp].adapt;
                group[ii].p = groupTemp[temp].p;
                Array.Copy(groupTemp[temp].city, group[ii].city, cityCount);
                //for (int j = 0; j < cityCount; j++)
                //    group[i].city[j] = groupTemp[temp].city[j];
            }
        }

        static void Cross1()
        {
            double pick;
            int choice1, choice2;
            int pos1, pos2;
            int[] conflict1 = new int[cityCount];
            int[] conflict2 = new int[cityCount];
            int num1, num2;

            List<Group> children = new List<Group>();
            while (children.Count < ChildrenSize)
            {
                Group child1 = new Group();
                Group child2 = new Group();
                pick = random.NextDouble();
                if (pick > pc)
                {
                    continue;
                }
                choice1 = random.Next(GroupSize);
                choice2 = random.Next(GroupSize);
                pos1 = random.Next(1, cityCount - 2);
                pos2 = random.Next(1, cityCount - 2);
                if (pos1 > pos2)
                    Swap(ref pos1, ref pos2);

                Array.Copy(group[choice1].city, child1.city, cityCount);
                child1.adapt = group[choice1].adapt;
                Array.Copy(group[choice2].city, child2.city, cityCount);
                child2.adapt = group[choice2].adapt;

                for (int i = pos1; i <= pos2; i++)
                {
                    Swap(ref child1.city[i], ref child2.city[i]);
                }
                num1 = num2 = 0;

                for (int i = 0; i <= pos1 - 1; i++)
                {
                    for (int j = pos1; j <= pos2; j++)
                    {
                        if (child1.city[i] == child1.city[j])
                        {
                            conflict1[num1] = i;
                            num1++;
                        }
                        if (child2.city[i] == child2.city[j])
                        {
                            conflict2[num2] = i;
                            num2++;
                        }
                    }
                }
                for (int i = pos2 + 1; i < cityCount; i++)
                {
                    for (int j = pos1; j <= pos2; j++)
                    {
                        if (child1.city[i] == child1.city[j])
                        {
                            conflict1[num1] = i;
                            num1++;
                        }
                        if (child2.city[i] == child2.city[j])
                        {
                            conflict2[num2] = i;
                            num2++;
                        }
                    }
                }

                if (num1 == num2 && num1 > 0)
                {
                    for (int i = 0; i < num1; i++)
                    {
                        Swap(ref child1.city[conflict1[i]], ref child2.city[conflict2[i]]);
                    }
                }
                child1.adapt = CalculatePathLength(child1.city);
                child2.adapt = CalculatePathLength(child2.city);
                children.Add(child1);
                children.Add(child2);
            }
            children.AddRange(group);
            children.Sort();
            for (int i = 0; i < GroupSize; i++)
            {
                Array.Copy(children[i].city, group[i].city, cityCount);
            }
        }

        static void Cross()
        {
            double pick;
            //double pick1, pick2;
            int choice1, choice2;
            int pos1, pos2;
            //int temp;
            int[] conflict1 = new int[cityCount];
            int[] conflict2 = new int[cityCount];
            int num1, num2;
            //int index1, index2;
            int move = GreatOneSize;

            while (move < GroupSize - 1)
            {
                pick = random.NextDouble();
                if (pick > pc)
                {
                    move += 2;
                    continue;
                }

                choice1 = move;
                choice2 = move + 1;

                pos1 = random.Next(1, cityCount - 2);
                pos2 = random.Next(1, cityCount - 2);

                if (pos1 > pos2)
                    Swap(ref pos1, ref pos2);

                for (int i = pos1; i <= pos2; i++)
                {
                    Swap(ref group[choice1].city[i], ref group[choice2].city[i]);
                }
                num1 = num2 = 0;
                for (int i = 0; i <= pos1 - 1; i++)
                {
                    for (int j = pos1; j <= pos2; j++)
                    {
                        if (group[choice1].city[i] == group[choice1].city[j])
                        {
                            conflict1[num1] = i;
                            num1++;
                        }
                        if (group[choice2].city[i] == group[choice2].city[j])
                        {
                            conflict2[num2] = i;
                            num2++;
                        }
                    }
                }
                for (int i = pos2 + 1; i < cityCount; i++)
                {
                    for (int j = pos1; j <= pos2; j++)
                    {
                        if (group[choice1].city[i] == group[choice1].city[j])
                        {
                            conflict1[num1] = i;
                            num1++;
                        }
                        if (group[choice2].city[i] == group[choice2].city[j])
                        {
                            conflict2[num2] = i;
                            num2++;
                        }
                    }
                }
                if (num1 == num2 && num1 > 0)
                {
                    for (int i = 0; i < num1; i++)
                    {
                        Swap(ref group[choice1].city[conflict1[i]], ref group[choice2].city[conflict2[i]]);
                    }
                }
                move += 2;
                //Console.WriteLine(choice1.ToString()+" "+choice2 + "Cross in positions" + pos1.ToString() + " " +pos2.ToString());
                //for (int i = 0; i < GroupSize; i++)
                //{
                //    for (int j = 0; j < cityCount; j++)
                //    {
                //        Console.Write(group[i].city[j].ToString() + " ");
                //    }
                //    Console.Write("\n");
                //}

            }
        }

        static void Mutation()
        {
            for (int i = GreatOneSize; i < GroupSize; i++)
            {
                if (random.NextDouble() < pm)
                {
                    int index1 = random.Next(cityCount - 1);
                    int index2 = random.Next(cityCount - 1);
                    Swap(ref group[i].city[index1], ref group[i].city[index2]);
                    //Console.WriteLine("Mutation on " + i.ToString() + " index is " + index1.ToString() + " " + index2.ToString());
                    //for (int k = 0; k < GroupSize; k++)
                    //{
                    //    for (int j = 0; j < cityCount; j++)
                    //    {
                    //        Console.Write(group[k].city[j].ToString() + " ");
                    //    }
                    //    Console.Write("\n");
                    //}
                }
            }
        }

        static void Reverse()
        {
            int flag;
            int pos1, pos2;
            int[] reverseArray = new int[cityCount];
            double reverseDistance;

            for (int i = 0; i < GroupSize; i++)
            {
                flag = 0;
                while (flag == 0)
                {
                    pos1 = random.Next(cityCount - 1);
                    pos2 = random.Next(cityCount - 1);
                    if (pos1 > pos2)
                    {
                        Swap(ref pos1, ref pos2);
                    }

                    if (pos1 < pos2)
                    {
                        Array.Copy(group[i].city, reverseArray, cityCount);
                        Array.Reverse(reverseArray, pos1, pos2 - pos1 + 1);
                        reverseDistance = CalculatePathLength(reverseArray);
                        if (reverseDistance < CalculatePathLength(group[i].city))
                        {
                            Array.Copy(reverseArray, group[i].city, cityCount);
                        }
                    }
                    flag = 1;
                }
            }
        }

        static double CalculateDistance(City c1, City c2)
        {
            double temp = Math.Pow(c1.x - c2.x, 2) + Math.Pow(c1.y - c2.y, 2);
            return Math.Sqrt(temp);
        }

        static double CalculatePathLength(int[] theCities)
        {
            double sum = 0;
            int n1, n2;
            for (int j = 1; j < cityCount; j++)
            {
                n1 = theCities[j];
                n2 = theCities[j - 1];
                sum += distance[n1, n2];
            }
            sum += distance[theCities[0], theCities[cityCount - 1]];
            return sum;
        }

        static void Swap<T>(ref T a, ref T b)
        {
            T temp = a;
            a = b;
            b = temp;
        }
        //static void Swap(ref double a, ref double b) {
        //    double temp = a;
        //    a = b;
        //    b = temp;
        //}

        static void GetBestSolution()
        {
            double temp = double.MaxValue;
            int indexT = 0;
            for (int i = 0; i < GroupSize; i++)
            {
                if (group[i].adapt < temp)
                {
                    temp = group[i].adapt;
                    indexT = i;
                }
            }

            if (bestSolution > temp)
            {
                bestSolution = temp;
            }
        }
    }

    class Group : IComparable<Group>
    {
        public int[] city;
        public double adapt;
        public double p;
        public Group()
        {
            city = new int[Program.cityCount];
        }

        public int CompareTo(Group other)
        {
            return -other.adapt.CompareTo(adapt);
        }
    }

    class City
    {
        public int x, y, index;
        public City() { }
        public City(int xx, int yy, int i)
        {
            x = xx; y = yy; index = i;
        }
    }

}
